<template>
    <div class="container">
        <div @click="getAddressUser()" class="titlePage">ĐỊA CHỈ  NHẬN HÀNG</div>
        <div class="title_add">Danh sách địa chỉ <div @click="pageInsertAddress" class="page-addressUser">Thêm địa chỉ mới <i class="fa-solid fa-circle-plus"></i></div></div>
        <div class="add_product">
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">Tên người nhận</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">Số điện thoại</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">Tỉnh (thành phố)</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">Huyện (Quận)</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">Xã (Khu Vực)</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">Thôn (Làng xóm...)</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">Địa chỉ cụ thể hoặc số nhà</span>
            </div>
            <div class=" p-t-b-12px">
                <span class="namebox ">Thao Tác</span>
            </div>
        </div>  
        <div v-for="(inforAddress, index) in listAddress"  :key="inforAddress.address_id" @click="selectAddress(inforAddress.address_id)" class="address_content" >
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">{{inforAddress.address_fullName}}</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">{{inforAddress.address_phoneNumber}}</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">{{inforAddress.address_province}}</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">{{inforAddress.address_district}}</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">{{inforAddress.address_commune}}</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">{{inforAddress.address_village}}</span>
            </div>
            <div class=" fl-1 p-t-b-12px">
                <span class="namebox">{{ inforAddress.address_apartment}}</span>
            </div>
             <div class="p-t-b-12px ">
                <span @click="selectAddressUpdate(inforAddress,$event)" class="margin-l-r cursor namebox hover ">Sửa</span>
                <span @click="eDeleteAddresUser(inforAddress.address_id, index,$event)" class="margin-l-r cursor namebox hover">Xóa</span>
            </div>
        </div>
        <div class="btn-box">
            <div class="btnback" @click="backPage">Quay lại </div>
        </div>
    </div>
    
</template>
<script>
import InputVue from "@/components/InputVue.vue"
import axios from 'axios';
import { ref, computed } from "vue";
import { useStore } from 'vuex'
import { onMounted } from 'vue';
import { useRouter } from 'vue-router'
export default {
    components: {

    },
    setup(){
        const route = useRouter()
        let store = useStore()
        let listAddress = computed(() => store.getters.listAddress)  
        function eDeleteAddresUser(value, index,event){
            event.stopPropagation();
            var payload = {
                index,
                value
            }
            store.dispatch("deleteAddressUser", payload)
        }
        function backPage(){
            if(store.getters.backPage == ""){
                route.push("/Buy")
            }
        }
        function selectAddress(value) {
             axios({
                method: "Put",
                url: 'http://localhost:1234/api/Address/UpdateAddressdefault', 
                data: {
                    address_id:value,
                    user_id:localStorage.getItem("user_id")
                }
                
            })
            
            .then(response => {
                console.log("thành công");
                route.push('/Buy')
            })
            .catch(error => {
                console.log(error);
            });
        }
        function selectAddressUpdate(value,event){
            event.stopPropagation();
            localStorage.setItem('address', JSON.stringify(value));
            route.push('/UpdateAddress')
        }
        function pageInsertAddress(){
            localStorage.setItem("pageBefore", "ListAddress")
            route.push("/AddressUser")
        }
        onMounted(() => {
            store.dispatch("getAddressUser")
        })
        return {
            listAddress,store, eDeleteAddresUser, selectAddress, selectAddressUpdate,backPage,pageInsertAddress
        }
    },

    
}
</script>

<style scoped>
*{
  padding: 0;
  margin:0;
  box-sizing: border-box;
}
.btnback{
    margin-left: 5px;
    width: 200px;
    height: 40px;
    line-height: 40px;
    text-align: center;
    font-size: 18px;
    border-radius: 4px;
    background-color: rgb(255, 117, 140);
    color: white;
    cursor: pointer;
}
.btn-box{
    display: flex;
    justify-content: flex-end;
}
.cursor{
    cursor: pointer;
}
.hover{
    color: rgb(158, 15, 39);
    font-weight: bold;
}
.hover:hover{
    opacity: 0.8;
    color: rgb(255, 117, 140);
   
}

.title_add{
    color:rgb(255, 117, 140);
    margin-bottom: 12px;
    font-size: 22px;
}
.titlePage{
    background-color: rgb(255, 117, 140);
    width: 100%;
    height: 60px;
    line-height: 60px;
    font-size: 24px;
    color: white;
    margin-bottom: 24px;
    text-align: center;
}
.address_content{
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
    color:rgb(255, 117, 140);
    cursor: pointer;
}
.page-addressUser{
    font-size: 16px;
    color:rgb(255, 117, 140);
    cursor: pointer;
}
.address_content:hover{
    background-color: white;
}
.add_product{
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
    color:rgb(255, 117, 140);
}
.margin-l-r{
   margin: 0 2px;
}
.add_product input{
    padding: 12.5px;
    background-color: white;
    border: 1px solid rgb(221, 221, 221);
    border-radius: 2px;
    width: 400px;
}
.fl-1{
    flex:1;
    text-align: center;
}
.fl-col{
    display: flex;
    flex-direction: column;
}
.namebox{
    padding-left: 2px;
    font-size:14px;

}
.titleProduct{
    display: flex;
    max-width: 500px;
}
.container{
    flex: 1;
    padding: 12px ;
    user-select: none;
    max-width: 1200px;
    margin: 0 auto;
    background-color: rgb(247, 247, 247);
}
.p-t-b-12px{
    padding: 12px 0;
}
.btnAdd{
    width: 200px;
    height: 40px;
    line-height: 40px;
    text-align: center;
    font-size: 18px;
    border-radius: 4px;
    background-color: rgb(255, 117, 140);
    color: white;
}
.btnAdd:hover{
    opacity: 0.8;
    cursor: pointer;
}
</style>