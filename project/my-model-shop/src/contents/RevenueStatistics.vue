<template>
    <div class="Revenue_content">
      <div class="operation">
        <div class="filter">
            <div class="filter_fromDay">
                Từ ngày:
                <input type="text" v-model="startingDate" placeholder="ngày/tháng/năm">
            </div>
            <div class="filter_fromDay">
                Đến ngày:
                <input type="text" v-model="endingDate" placeholder="ngày/tháng/năm">
            </div>
            <div @click="getAllHistory()" class="filter_submit">
                <i class="fa-solid fa-filter"></i>
            </div>
        </div> 
        <div  class="sumOrder formTotal">
          Tổng đơn: {{ total.sumOrder }}
        </div>
        <div class="sumProduct formTotal">
          Sản phẩm: {{total.sumProduct}}
        </div>
        <div class="sumMoney formTotal">
          Doanh thu: {{formatMoney(total.sumMoney)}}
        </div>
        <div @click="isShow" v-if="showTab"  class="chart">
          <i class="fa-solid fa-chart-line"></i>
        </div>
        <div @click="isShow" v-if="!showTab"  class="table">
          <i class="fa-solid fa-table"></i>
        </div>
      </div>
      <div v-show="showTab" class="revenue_table">
          <table>
              <tr class="header-row">
                  <th>Thời gian</th>
                  <th>Khách hàng</th>
                  <th>Sản phẩm</th>
                  <th>Số lượng</th>
                  <th>Giá tiền</th>
              </tr>
              <tr v-for="(data, index) in result" :key="index">
                  <td>{{ getFormattedDate(data.delivery_date) }}</td> 
                  <td>{{ data.fullname }}</td>
                  <td>{{ data.product_name }}</td>
                  <td>{{ data.quantity }}</td>
                  <td>{{ formatMoney(data.price) }}</td>
              </tr>
          </table>
      </div>
      <div v-show="!showTab" class="revenue_chart">
          <div class="revenue_chart-day">
              <canvas id="chartBar"></canvas>
          </div>
      </div>
    </div>
</template>

<script>
    import { useRouter } from 'vue-router'
    import { Chart, registerables } from 'chart.js';
    import HeaderVue from '@/layouts/HeaderVue.vue'
    import { getCurrentInstance, ref, reactive, onMounted, computed, watch  } from 'vue'
    import { useStore } from 'vuex'
    import axios from "axios"
    Chart.register(...registerables);
    export default{
  setup() {
    onMounted(() => {
      getAllHistory();
    });
    const router = useRouter()
    let store = useStore()
    let dateTime = ref(new Date().toISOString().split("T")[0])
    let result = ref([])
    let arrTime = ref([])
    let arrSumday = ref([])
    let showTab = ref(true)
    let total = ref({
      sumMoney: 0,
      sumProduct: 0,
      sumOrder: 0
    })
    let today = new Date().toISOString().split("T")[0];
    function postFormattedDate(date) {
      if (date != "") {
        let year = date.slice(6, 10)
        let month = date.slice(3, 5)
        let day = date.slice(0, 2)
        let dateTime = `${year}-${month}-${day}T00:00:00.000z`
        return new Date(dateTime)
      }
    }
    function totalTypes(param){
      total.value.sumOrder = 0
      total.value.sumMoney = 0
      total.value.sumProduct = 0
      param.forEach((e) => {
        total.value.sumProduct += e.quantity
        total.value.sumMoney += e.price
        total.value.sumOrder ++
      })
    }
    function getFormattedDate(date) {
      let year = date.slice(0, 4)
      let month = date.slice(5, 7)
      let day = date.slice(8, 10)
      let time = date.slice(12, 20)
      let dateTime = day + "/" + month + "/" + year
      return dateTime
    }
    function formatMoney(value) {
      if (value >= 1000000000) {
          return (value / 1000000000).toFixed(1).replace('.0', '') + " Tỉ";
      } else if (value >= 1000000) {
          return (value / 1000000).toFixed(1).replace('.0', '') + " Tr";
      } else if (value >= 1000) {
          return (value / 1000).toFixed(1).replace('.0', '') + " Nghìn";
      } else {
          return value.toString() + " Đồng";
      }
    }

    let startingDate = ref("01/01/0001")
    let endingDate = ref(getFormattedDate(today))
    let timer;
    let isUpdating = ref(false) // Thêm flag để đánh dấu API call đang thực hiện
    watch(startingDate, (newValue, oldValue) => {
      clearTimeout(timer);
      timer = setTimeout(() => {
        if (newValue !== oldValue && !isUpdating.value) {
            getAllHistory();
        }
      }, 2000);
    });
    watch(endingDate, (newValue, oldValue) => {
      clearTimeout(timer);
      timer = setTimeout(() => {
          if (newValue !== oldValue && !isUpdating.value) {
            getAllHistory()
          }
      }, 2000);
    });

    function getAllHistory() {
      isUpdating.value = true; // Set flag khi API call bắt đầu
      let date = {
        startingDate: postFormattedDate(startingDate.value),
        endingDate: postFormattedDate(endingDate.value),
      };
      axios({
        method: "get",
        url: "http://localhost:1234/api/Historydelivery/GetAllByTime",
        params: date,
      })
        .then((response) => {
          result.value = response.data
          valueDay(response.data)
          valueMonth(response.data)
          updateChart()
          totalTypes(response.data)
        })
        .catch((error) => {
          console.log(error);
          result.value = error;
        })
        .finally(() => {
          isUpdating.value = false; // Reset flag sau khi API call kết thúc
        });
    }
    function backPage() {
      window.history.back();
    }
    function pageHome() {
      router.push("/")
    }
    function valueDay(param) {
      let uniqueDates = new Set();
      param.forEach((item) => {
        const formattedDate = getFormattedDate(item.delivery_date);
        uniqueDates.add(formattedDate);
      });
      arrTime.value = Array.from(uniqueDates);

      arrSumday.value = []; // Reset arrSumday trước khi tính toán lại
      arrTime.value.forEach((item) => {
        let sum = 0;
        param.forEach((check) => {
          if (getFormattedDate(check.delivery_date) == item) {
            sum += Number(check.price);
          }
        });
        arrSumday.value.push(sum);
      });
    }
    function valueMonth(param) {
      let uniqueDates = new Set();
      param.forEach((item) => {
        const formattedDate = getFormattedDate(item.delivery_date);
        uniqueDates.add(formattedDate);
      });
    }

    // chart
    function chartRevenue(param, type, id, value, nameChart) {
      const ctx = document.getElementById(id);
      if (ctx) {
        if (ctx.chart) {
          ctx.chart.destroy();
        }
        const chart = new Chart(ctx.getContext("2d"), {
          type: type,
          data: {
            labels: Array.from(param),
            datasets: [
              {
                label: "Doanh thu " + nameChart,
                data: value,
                backgroundColor: "#70c5fd",
                borderColor: "rgb(52, 152, 219)",
                borderWidth: 1,
              },
            ],
          },
          options: {
            responsive: true,
            scales: {
              y: {
                beginAtZero: true,
              },
            },
          },
        });
        ctx.chart = chart;
      } else {
        console.error("Canvas element not found");
      }
    }
     function updateChart(){
      chartRevenue(arrTime.value, 'bar', 'chartBar', arrSumday.value, 'ngày');
    }
    function isShow() {
      showTab.value = !showTab.value;
       updateChart();
    }
    return {
      backPage, pageHome, result, dateTime,
      getAllHistory, startingDate, endingDate,
      getFormattedDate, showTab, isShow, total, formatMoney
    };
  },
    };
</script>

<style scoped>
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}
.btnback{
margin: 10px 5px;
width: 200px;
height: 40px;
line-height: 40px;
text-align: center;
font-size: 18px;
border-radius: 4px;
background-color: rgb(255, 117, 140);
color: white;
cursor: pointer;
float: right;

}
.Revenue_content{
    width: 1200px;
    margin: 15px auto;
    padding: 15px 15px 15px 15px;
    background-color: #f7f7f7;
}
.disabled {
    pointer-events: none;
    opacity: 0.5;
}
.moneyColor{
    color: rgb(255, 117, 140);
}
.formTotal{
  line-height: 52px;
  width: 150px;
  border-radius: 6px;
}
.searchArea{
    display: flex;
    height: 40px;
    justify-content: center;
    line-height: 40px;
}
.filter_fromDay input{
    width: 100px;
    height: 30px;
    outline: none;
    margin-right: 10px;
}
.filter_fromDay {
   margin: 10px 0;
}
.filter {
    display: flex;
}
.operation{
    display: flex;
    justify-content: space-between;
    margin: 10px 0;
    background-color: white;
}
.searchBox input{
    height: 40px;
    width: 536px;
    border: none;
    outline: none;
    padding:  5px 20px 5px 20px;
    font-size: 16px;
    color: rgb(73, 73, 73);
    border-top-left-radius: 10px;
    border-bottom-left-radius: 10px;
    background-color: white;
    border: 1px solid rgb(221, 221, 221);
}
.searchBox input::placeholder{
    font-size: 16px;
}
.searchBtn{
    text-align: center;
    width: 50px;
    border-top: 1px solid rgb(221, 221, 221);
    border-right: 1px solid rgb(221, 221, 221);
    border-bottom: 1px solid rgb(221, 221, 221);
    border-top-right-radius: 10px;
    border-bottom-right-radius: 10px;
    cursor: pointer;
}

table {
        width: 100%;
        border-collapse: collapse;
        text-align: center;
    }
    th, td {
        border: 1px solid black;
        padding: 10px;
    }
    .header-cell {
        font-weight: bold;
    }
    .header-merge {
        text-align: center;
        border: 1px solid black;
    }
    .header-row th {
        border-bottom: 2px solid black;
    }
    .title-row th {
        border: none;
    }
    .filter_submit {
        font-size: 24px;
        cursor: pointer;
        padding: 12px 12px;
        border-radius: 50%;
        color: #3498db;
    }
    .revenue_table{
      background-color: white;
    }
    .revenue_chart{
      background-color: white;
    }
    .chart{
        font-size: 24px;
        cursor: pointer;
        padding: 12px 12px;
        border-radius: 50%;
        color: #3498db;
        float: right;
    }
    .table{
        font-size: 24px;
        cursor: pointer;
        padding: 12px 12px;
        border-radius: 50%;
        color: #3498db;
        float: right;

    }
</style>