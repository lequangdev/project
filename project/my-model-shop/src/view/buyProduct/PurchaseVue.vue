<template>
    <div class="container">
        <HeaderVue>
            <div class="introduce">
                <div class="logoOnBar">
                    <img src="@/img_logo/Logo.png" alt="">
                    <h3>{{ introduce }}</h3>
                    <div class="title">{{ title }}</div>  
                </div> 
                <div class="header_right">
                    <div @click="pageHome" class="home">
                    <i class="fa-solid fa-house"></i>
                    </div>
                    <div class="profile">
                        <div @click="onProfileUser" class="avatar"><img src="https://tse3.mm.bing.net/th?id=OIP.wUU79RnsVB3zcJjsCPNlAgHaHa&pid=Api&P=0&h=220" alt="avatar"></div>
                    </div>
                </div>
            </div>
        </HeaderVue>
        <div class="user-address">
            <div class="title-address">
                <i class="fa-solid fa-location-dot"></i>
                <h2>Địa Chỉ Nhận Hàng</h2>
            </div>
            <div  class="container-address">
                <div  v-for="(inforAddress) in addressDefault"  :key="inforAddress.address_id" class="content-address">
                    <h2 class="user-name">{{ inforAddress.address_fullName }},</h2>
                    <h2 class="phone-number">SĐT:{{ inforAddress.address_phoneNumber }},</h2>
                    <div class="address-detail">
                        <span class="province">{{ inforAddress.address_province }},</span>
                        <span class="district">{{ inforAddress.address_district }},</span>
                        <span class="commune">{{ inforAddress.address_commune }},</span>
                        <span class="village">{{ inforAddress.address_village }},</span>
                        <span class="detailed-location">{{ inforAddress.address_apartment }}</span>
                    </div>
                </div> 
                <div @click="toPageListAddress()" class="btn-modify-address">Sửa đổi</div>
                <div @click="toPageInsertAddress" class="btn-modify-address">Thêm địa chỉ <i class="fa-solid fa-circle-plus"></i></div>
            </div>
        </div>
        <div class="mainContent">
            <div class="columnName">
                <div class="columnName_products ">Sản Phẩm</div>
                <div class="columnName_price colum1 center color808080">Đơn giá</div>
                <div class="columnName_quantity colum1 center color808080">Số Lượng</div>
            </div>
            <div class="product_list">
                <div class="product" v-for="(product, index) in cart" :key="index" >
                    <img class="thumb" :src="product.img_name"  alt="product">
                    <div class="name_product">{{ product.product_name}}</div>
                    <div class="operation_product">
                        <div class="price_product colum1 center">{{ product.product_price}}</div>
                        <div class="quantityBox colum1 center">
                            <div class="">{{ product.quantity }}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="boxBuyProduct">
                <div class="quantyProductBuy">
                    Số lượng sản phẩm:{{ quantityProductBuy }}
                </div>
                <div class="sumPrice">
                    Tổng tiền:{{ sumPrice }}đ
                </div>
                <div @click="backPage" class="backPage">
                    quay lại
                </div>
                <div @click="orders" class="buyProduct">
                    Mua
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { useRouter } from 'vue-router'
import HeaderVue from '@/layouts/HeaderVue.vue'
import { getCurrentInstance, ref, reactive , onMounted, computed } from 'vue'
import { useStore } from 'vuex'
import axios from 'axios';
export default {
    data(){
        
        return {
            // cart: productsInCart
        }
    },
    setup() {
        const route = useRouter()
        let store = useStore()
        onMounted(() => {
            store.dispatch("getAddressDefault")
        })
        let cart = store.getters.buyProduct
        let checkedItems = []
        let sumPrice = ref(0)
        let quantityProductBuy = ref(0)
        const introduce = 'WANG'
        const title = 'Mua Hàng' 
        let addressDefault = computed(() => store.getters.addressDefault)
        cart.forEach(e => {
            sumPrice.value += e.product_price * e.quantity;
            quantityProductBuy.value += e.quantity
         })
        function onProfileUser(){
            route.push("/ProfileUser")
        }
        function pageHome(){
            route.push("/")
        }
        function backPage(){
            route.push("/Cart")
        }
        function orders(){
            if(!addressDefault.value[0]){
                return alert('Vui lòng thêm địa chỉ')
            }
            let price;
            let data = [];
            cart.forEach(e => {
                price = e.product_price 
                data.push({
                    price:e.product_price * e.quantity,
                    user_id:e.user_id,
                    address_id:addressDefault.value[0].address_id,
                    product_id:e.product_id,
                    quantity:e.quantity,
                    status: "Chờ xác nhận"
                })

            })
            axios({
                method: "Post",
                url: 'http://localhost:1234/api/Order/insertList', 
                data: data
            })
            
            .then(response => {     
                console.log(response.status);
                if(response.data){
                    route.push("/OrderPlaced")
                }
            })
            .catch(error => {
                console.log(error);
            }); 
        }
        function toPageListAddress() {
            route.push('/ListAddress')
        }
        function toPageInsertAddress(){
            route.push('/AddressUser')
        }
       
        return {
            introduce, title, quantityProductBuy, sumPrice, 
            checkedItems, cart, orders, 
            toPageListAddress, addressDefault,toPageInsertAddress,onProfileUser,pageHome,backPage
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
.header_right{
    display: flex;
}
.home{
    margin: auto 8px;
    padding: 10px;
    height: 47px;
    font-size: 24px;
    color: rgb(255, 117, 140);
    cursor: pointer;
}
.container-address{
    display:flex;
}
.user-address{
    width: 1200px;
    height: 100px;
    margin: 15px auto;
    padding: 15px 15px 15px 30px;
    background-color: #f7f7f7;
}
.title-address{
    display: flex;
    margin-bottom: 20px;
    line-height: 28px;
}
.title-address h2{
    font-size: 18px;
    
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
.btn-modify-address{
    cursor: pointer;
    margin-left: 10px;
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
.backPage{
    width: 100px;
    height: 50px;
    color: white;
    background-color: rgb(255, 117, 140);
    cursor: pointer;
    padding: 5px 14px 5px 14px;
    margin: 5px 0;
    position: absolute;
    right: 106px;
    line-height: 40px;
    text-align: center;

}
.backPage:hover{
    opacity: 0.8;
}
.sumPrice{
    margin-left: 480px;
}
.buyProduct:hover{
    opacity: 0.8;
}
</style>