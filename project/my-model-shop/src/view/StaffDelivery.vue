<template>
    <div class="container">
        <HeaderVue>
            <div class="introduce">
                <div class="logoOnBar">
                    <img src="@/img_logo/Logo.png" alt="">
                    <h3>{{ introduce }}</h3>
                    <div class="title">{{ title }}</div>  
                </div> 
                <div class="profile">
                    <div @click="onProfileUser" class="avatar"><img :src="avatar" alt="avatar"></div>
                </div>
            </div>
        </HeaderVue>
        
        <div class="order_content">
            <div class="titlePage">Đơn cần giao</div>
            <div v-for="(data, index) in productsOrder"  :key="index" class="order_product">
                <ul v-if="data.status === 'Đang giao hàng'" class="order_product-list">
                    <li>
                        <div class="product-box">
                            <div class="product-avatar">
                                <img :src="data.img_name" alt="">
                            </div>
                            <div class="tag-infor">
                                <span class="product_name">{{ data.product_name }}</span>
                                <span class="address_user">
                                    <span class="user-name">Tên người nhận: {{ data.address_fullName }},</span>
                                    <div class="address-detail">
                                        Địa chỉ giao hàng: 
                                        <span class="province">{{ data.address_province }},</span>
                                        <span class="district">{{ data.address_district }},</span>
                                        <span class="commune">{{ data.address_commune }},</span>
                                        <span class="village">{{ data.address_village }},</span>
                                        <span class="detailed-location">{{ data.address_apartment }}</span>
                                    </div>
                                </span>
                                <span class="phone-number">Số điện thoại: {{ data.address_phoneNumber}}</span>
                            </div>
                        </div>
                        <div class="product_quantity">
                            {{ data.quantity }}
                        </div>
                        <div class="product_price">
                            <span class="tags-price">Giá: {{ data.price }}đ</span>
                        </div>
                        <div class="box-status">
                            <div @click="complete(data, index)" class="complete">Hoàn thành</div>
                            <div @click="deleteOrder(data, index)" class="refund">Thất bại</div>
                        </div>
                    </li>
                </ul>

            </div>
            <div v-if="false" class="home" @click="pageHome"> Trang chủ </div>
        </div>
    </div>
</template>

<script>
import { useRouter } from 'vue-router'
import HeaderVue from '@/layouts/HeaderVue.vue'
import { getCurrentInstance, ref, reactive, onMounted, computed  } from 'vue'
import decodeJWT from '@/commonFunc/jwtDecode'
import { useStore } from 'vuex'
import axios from 'axios'
export default {
    data(){
        return {
           
        }
    },
    setup() {
        onMounted(() => {
            store.dispatch("getAllProductOrder")
        })
        
        let store = useStore()
        const user_name = decodeJWT(localStorage.getItem('jwtToken')).fullname
        const avatar = localStorage.getItem('avatar_url') 
        let selectAddress = store.getters.selectAddress
        let cart = store.getters.buyProduct
        let productsOrder = computed(() => 
            store.getters.productAllOrder
        )
        function backPage(){
            window.history.back();
        }
        let checked = false;
        let checkedItems = []; 
        let sumPrice = ref(0);
        let quantityProductBuy = ref(0)
        const router = useRouter()
        const introduce = 'WANG'
        const title = 'Giao Hàng' 
        cart.forEach(e => {
            sumPrice.value += e.product_price * e.quantity;
            quantityProductBuy.value += e.quantity
        });
        function pageHome(){
            router.push("/")
        }
        function deleteOrder(data, index){
            let payload = {
                idOrder: data.order_id,
                index: index
            }
            let insertData = {
                product_id: data.product_id,
                address_id: data.address_id,
                user_id: data.user_id,
                status: data.status,
                price: data.price,
                quantity: data.quantity,
                delivery_date: new Date().toISOString(),
                staff_id: localStorage.getItem("user_id")
            }
            insertData.status = "2"
            axios({
                method: "post",
                url: 'http://localhost:1234/api/Historydelivery',
                data: insertData
            })
            .then(response => {
                if(response.data){
                    store.dispatch("deleteProductsOrders", payload)
                }
            })
            .catch(error => {
                console.log("thất bại");
            });
        }
        function onProfileUser(){
                router.push('profileUser')
            }
        function complete(data, index){
            let payload = {
                idOrder: data.order_id,
                index: index
            }
            let insertData = {
                product_id: data.product_id,
                address_id: data.address_id,
                user_id: data.user_id,
                status: data.status,
                price: data.price,
                quantity: data.quantity,
                delivery_date: new Date().toISOString(),
                staff_id: localStorage.getItem("user_id")
            }
            insertData.status = "1"
            axios({
                method: "post",
                url: 'http://localhost:1234/api/Historydelivery',
                data: insertData
            })
            .then(response => {
                console.log("thành công");
                if(response.data){
                    store.dispatch("deleteProductsOrders", payload)
                }
            })
            .catch(error => {
                console.log("thất bại");
            });
        }
        return {
            introduce,title,quantityProductBuy,sumPrice,checked,checkedItems,
            user_name,cart, selectAddress,productsOrder, deleteOrder,pageHome, backPage,
            complete, onProfileUser, avatar
        }
    },
    components: {
        HeaderVue
    }
}

</script>

<style scoped>
*{
    box-sizing: border-box;
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
.order_content{
    width: 1200px;
    margin: 15px auto;
    background-color: #f7f7f7;
    padding: 15px 15px 15px 15px;

}
.home{
    margin: 10px 0;
    float: right;
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
.order_product-list{
    list-style-type: none;
    background-color: white;
    height: 100px;
}
.address_user{
    color: #808080;
    font-size: 14.4px;
}
.phone-number{
    color: #808080;
    font-size: 14.4px;
}
.complete{
    height: 30px;
    width: 120px;
    line-height: 30px;
    background-color: #28a745;
    color: white;
    cursor: pointer;
    text-align: center;
    margin-bottom: 10px
}
.refund{
    height: 30px;
    width: 120px;
    line-height: 30px;
    background-color: #dc3545;
    color: white;
    cursor: pointer;
    text-align: center;
}
.order_product-list li{
    display: flex;
    justify-content: space-between;
    height: 100%;
    border: 0.5px solid orange;
}
.tag-infor{
    display: flex;
    flex-direction: column;
    font-size: 16px;
    font-weight: 500;
    margin-left: 30px;
    padding: 15px 0;
    max-width: 500px;
    word-wrap: break-word;
}
.product-box{
    display: flex;
    width: 600px;
}
.product_price{
    padding: 15px;
    line-height: 70px;
    color: #ff758c;
    font-size: 20px;
    font-weight: 500;
    margin-right: 50px;
}
.product_quantity{
    width: 50px;
    text-align: center;
    line-height: 25px;
    height: 25px;
    font-size: 15px;
    font-weight: 500;
    margin-right: 50px;
    margin: auto 15px;
    background-color: rgb(247, 247, 247);
}
.product-avatar img{
    height: 100px;
    width: 100px;
}
.fa-location-dot:before{
    line-height: 28px;
    padding: 5px;
    color: rgb(255, 117, 140);
}
.content-address{
    display: flex;
    font-size: 16px;
}
.content-address h2{
    font-size: 16px;
}
.container{
    display: flex;
    flex-direction: column;
    font-family: Arial, Helvetica, sans-serif;
    user-select: none;
    min-height: 100vh;
    position: relative;
}
.introduce{
    width: 1200px;
    height: 80px;
    background-color: #f7f7f7;
    margin: 0 auto;
    display: flex;
    justify-content: space-between;
}
.logoOnBar{
    display: flex;
   
}
.logoOnBar img{
    width: 50px;
    height: 50px;
    margin: auto 18px;
    border-radius: 10px;
}
.logoOnBar h3 {
    font-size: 24px;
    color: rgb(255, 117, 140);
    line-height: 80px;
    padding-right: 10px ;
}
.title {
    padding-left: 10px;
    font-size: 22px;
    color: rgb(255, 117, 140);
    line-height: 80px;
    border-left: 1px solid rgb(255, 117, 140); 
}
.profile{
    margin: auto 12px ;
}
.avatar img{
    width: 48px;
    height: 48px;
    border-radius: 24px;    cursor: pointer;
}
.mainContent{
    width: 1200px;
    margin: 0 auto;
}
.columnName{
    display: flex;
    font-size: 14px;
    height: 55px;
    line-height: 55px;
    background-color: #f7f7f7;
    padding:0 12px;
    border: 1px solid orange;

}
.columnName_products{
    width: 500px;
}
.colum1{
    flex: 1;
}
.center{
    text-align: center;
    margin: auto 0;
}
.selectAll{
    margin:0 20px;
    width: 18px;
}
.color808080{
    color: #808080;
}

.selectProduct{
    width: 18px;
    margin: 0 20px;
    width: 18px;
}


.quantityBox{
    display: flex;
    max-height: 190px;
    justify-content: center;
}
.quantityBox input{
    width: 50px;
    height: 32px;
    outline: none;
    border: none;
    border-top: 1px solid rgb(221, 221, 221);
    border-bottom: 1px solid rgb(221, 221, 221);
    text-align: center;
}

input[type=number]::-webkit-inner-spin-button,
input[type=number]::-webkit-outer-spin-button {
    -webkit-appearance: none;
}
.minus{
    width: 32px;
    height: 32px;
    text-align: center;
    border:  1px solid rgb(221, 221, 221);
    cursor: pointer;
}
.minus i{
    line-height: 32px;
}
.plus{
    width: 32px;
    height: 32px;
    text-align: center;
    border: 1px solid rgb(221, 221, 221);
    cursor: pointer;
}
.plus i{
    line-height: 32px;
}
.thumb{
    width: 80PX;
    height: 80px;
}
.product{
    display: flex;
    padding: 12px
}
.name_product{
    width: 420px;
    padding-left: 10px;
}
.operation_product{
    display: flex;
    flex: 1;
}
.product_list{
    margin-top: 12px;
    background-color: #f7f7f7;
}
.price_product{
    font-size: 14px;
}
.act_product:hover{
    color: rgb(255, 117, 140);
    cursor: pointer;
}
.boxBuyProduct{
    display: flex;
    width: 100%;
    height: 60px;
    background-color: white;
    position: sticky;
    bottom: 0;
    line-height: 60px;
    border-bottom:1px solid rgb(255, 117, 140) ;
    color: rgb(255, 117, 140);
}
.buyProduct{
    width: 100px;
    height: 50px;
    color: white;
    background-color: rgb(255, 117, 140);
    cursor: pointer;
    padding: 5px 14px 5px 14px;
    margin: 5px 0;
    position: absolute;
    right: 0;
    line-height: 40px;
 text-align: center;

}
.sumPrice{
    margin-left: 480px;
}
.buyProduct:hover{
    opacity: 0.8;
}
</style>