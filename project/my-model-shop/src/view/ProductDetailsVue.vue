<template >
    <div v-if="!role || role === 'client'" class="container">
        <HeaderVue class="header">
            <div class="headMasthead">
                <div class="logoOnBar">
                    <img src="../img_logo/Logo.png" alt="">
                    <h3>Wang</h3>
                </div>        
            </div>
               
            <div class="lastMasthead">
                <div @click="toCart" title="Giỏ hàng"  class="cart-shopping">
                    <i class="fa-solid fa-cart-shopping"></i>
                </div>
                <div title="Đơn đặt hàng" class="delivery">
                    <i @click="OrderPlaced" class="fa-solid fa-box"></i>
                    </div>
                <div title="Lịch sử mua hàng" class="history">
                    <i @click="purchaseHistory" class="fa-solid fa-clock-rotate-left"></i>
                </div>
                <div title="Thông báo"  class="notification"><i class="fa-solid fa-bell"></i></div>
                <div @click="onProfileUser" class="avatar"><img src="https://tse3.mm.bing.net/th?id=OIP.wUU79RnsVB3zcJjsCPNlAgHaHa&pid=Api&P=0&h=220" alt="avatar"></div>
            </div>
        </HeaderVue>
        <NavigationBarVue>
            <div class="navBar">
                <router-link class="home p-top-bot-5" to="/">Home</router-link>
                <i class="fa-solid fa-chevron-right p-top-bot-5"></i>
                <span class="page-name p-top-bot-5">{{ pageName }}</span>
            </div>
        </NavigationBarVue>
        <div class="content">
            <ProductDetails></ProductDetails>
        </div>
    </div>
</template>

<script>
import HeaderVue from '@/layouts/HeaderVue.vue';
import NavigationBarVue from '@/layouts/NavigationBarVue.vue';
import ProductDetails from '@/contents/ProductDetails.vue';
import { useRouter } from 'vue-router';
import productsInCart from '@/products_in_cart/cart';
import axios from 'axios';
import { useStore } from 'vuex';
import { mapActions } from 'vuex';
import { jwtDecode } from 'jwt-decode';
export default {
    components: {
        ProductDetails,NavigationBarVue,HeaderVue
    },
    setup() {
        
        const token = localStorage.getItem('jwtToken')
        let role
        if(token){
            role = jwtDecode(token).user_role
        }
        const store = useStore()
        const pageName = 'Chi tiết sản phẩm'     
        const router = useRouter()
        function onProfileUser() {
            if(localStorage.getItem('jwtToken') != null){
                router.push({name: "ProfileUser", params: {}})
            }
            else{
                router.push({name: "Login", params: {}})
            }
        } 
        function toCart() {
            if(localStorage.getItem("jwtToken")){
                if(store.getters.updateChangeCart > 0){
                    store.dispatch('updateDataProduct')
                }
                else{
                    router.push("/Cart")
                }
            }
            else{
                router.push("/Login")
            }
        }
        function OrderPlaced(){
            if(localStorage.getItem('jwtToken') != null){
                router.push("/OrderPlaced")
            }
            else{
                router.push("/Login")
            } 
        }
        function purchaseHistory(){
            if(localStorage.getItem('jwtToken') != null){
                router.push("/CompleteDelivery")
            }
            
            else{
                router.push("/Login")
            } 
        }

        return {
            onProfileUser,
            pageName,
            toCart,
            OrderPlaced,
            purchaseHistory,
            role
        }

    },
    methods: {
        ...mapActions(['updateDataProduct']) 
    }
}
</script>

<style scoped> 
:root {
  --primary-color: rgb(247, 247, 247); /* Định nghĩa màu chính */
}
*{
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}
.header{
    font-family: Arial, Helvetica, sans-serif;
    display: flex;
    height: 60px; 
    line-height: 60px;
    justify-content: space-between;
    padding: 0 16px;
    background-color: rgb(247, 247, 247);
    user-select: none;
    max-width: 1200px;
    margin-left: auto;
    margin-right: auto;
}
.content{
    
}
.headMasthead{
    display: flex;
}

.logoOnBar{
    display: flex;
}
.logoOnBar img{
    width: 50px;
    height: 50px;
    margin: 5px 18px;
    border-radius: 10px;
}
.history .fa-clock-rotate-left{
    font-size: 24px;
    width: 40px;
    height: 40px;
    padding: 8px;
    margin: 10px;
    text-align: center;
    cursor: pointer;
    color: rgb(255, 117, 140);
}
.delivery .fa-box {
    font-size: 24px;
    width: 40px;
    height: 40px;
    padding: 8px;
    margin: 10px;
    text-align: center;
    cursor: pointer;
    color: rgb(255, 117, 140);
}
.logoOnBar h3 {
    font-size: 24px;
    color: rgb(255, 117, 140);
}
.searchArea{
    display: flex;
    height: 40px;
    margin: 10px 40px;
    line-height: 40px;
}
.searchBox {

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
.lastMasthead{
    display: flex;

}
.avatar img{
    width: 48px;
    height: 48px;
    border-radius: 24px;
    margin: 6px ;
    cursor: pointer;
}

.notification > .fa-bell{
    font-size: 24px;
    width: 40px;
    height: 40px;
    padding: 8px;
    margin: 10px;
    text-align: center;
    cursor: pointer;
    color: rgb(255, 117, 140);
}
.cart-shopping > .fa-cart-shopping{
    font-size: 24px;
    width: 40px;
    height: 40px;
    padding: 8px;
    margin: 10px;
    text-align: center;
    cursor: pointer;
    color: rgb(255, 117, 140);
}


.navBar{
    width: 1200px;
    height: 28px;
    display: flex;
   margin: 20px auto;
    color: gray;
    line-height: 28px;
    font-family: Arial, Helvetica, sans-serif;
}

.home{
    color: gray;
    text-decoration: none;
}
.fa-chevron-right{
    font-size: 12px;
}
.fa-chevron-right::before{
    line-height: 28px;
}
.p-top-bot-5{
    padding: 0 5px;
}

</style>