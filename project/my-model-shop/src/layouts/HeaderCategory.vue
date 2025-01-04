<template>
    <div class="container">
        <HeaderVue class="header">
            <div class="headMasthead">
                <div class="logoOnBar">
                    <img src="../img_logo/Logo.png" alt="">
                    <h3>Wang</h3>
                </div>        
            </div>
            <div class="searchArea">
                <div class="searchBox">
                    <input @keydown.enter="searchQuery"  v-model="search" type="text" placeholder="Tìm kiếm">
                </div>
                <div @click="searchQuery" class="searchBtn" >
                    <i class="fa-solid fa-magnifying-glass"></i>
                </div>
            </div>      
            <div class="lastMasthead">
                <div title="Giỏ hàng" class="cart-shopping">
                    <cartIcon @Click="toCart()"  class="fa-solid fa-cart-shopping"></cartIcon>
                </div>
                <div title="Đơn đặt hàng" class="delivery">
                    <i @click="OrderPlaced" class="fa-solid fa-box"></i>
                    </div>
                <div title="Lịch sử mua hàng" class="history">
                    <i @click="purchaseHistory" class="fa-solid fa-clock-rotate-left"></i>
                </div>
                <div title="Thông báo"  class="notification"><i class="fa-solid fa-bell"></i></div>
                <div @click="onProfileUser" class="avatar"><img :src="avatar" alt="avatar"></div>
            </div>
        </HeaderVue>

        <div class="main">
            <CategoryVue class="category">
                <div class="homePage form"><router-link to="/" ><h3>Trang chủ</h3></router-link></div>
                <div class="news form"><router-link to="/News" ><h3>Tin tức</h3></router-link></div>
                <div class="introduce form"><router-link to="/Introduce"><h3>Giới thiệu</h3></router-link></div>
                <div class="contact form"><router-link to="Contact" ><h3>Liên hệ</h3></router-link></div>
                <div class="promotion form"><router-link to="Promotion" ><h3>Khuyến mãi</h3></router-link></div>
                <div v-if="false" class="dropdown-container">
                    <div class="pageAdmin form dropdown">
                        <div>
                            <h3>Trang quản lý Admin</h3>
                        </div>
                    </div>
                    <div class="dropdown-content">
                        <router-link  to="AdminVue"><h4>Quản lý sản phẩm</h4></router-link>
                        <router-link to="OrderManagement"><h4>Đơn đặt hàng</h4></router-link>
                        <router-link to="/PageRevenueStatistics"><h4>Thống kê doanh thu</h4></router-link>
                    </div>
                    <div v-if="false" class="pageStaff form">
                            <router-link to="StaffDelivery">
                                <h3>Nhân viên giao hàng</h3>
                            </router-link>
                    </div>
                </div>

            </CategoryVue>
            <slot></slot>
        </div>  
    </div>
 </template>
 
<script>
import cartIcon from '@/components/cartIcon.vue'
import HeaderVue from './HeaderVue.vue'
import CategoryVue from './CategoryVue.vue'
import { stringifyQuery, useRouter } from 'vue-router'
import axios from 'axios'
import productsInCart from '@/products_in_cart/cart'
import { useStore } from 'vuex'
import { ref, watch } from "vue"
import { jwtDecode } from 'jwt-decode'
export default{
    setup() {
        let search = ref("")
        let debounceTimer = null
        let store = useStore()
        let checkStaff = false
        let jwtBol = false
        const router = useRouter()
        const avatar = localStorage.getItem("avatar_url")
        const token = localStorage.getItem('jwtToken')
        let account;
        if(token){
            account = jwtDecode(token).account
        }
        function onProfileUser() {
            if(token){
                router.push({name: "ProfileUser", params: {}})
            }
            else{
                router.push("Login")
            }
        }
        function searchQuery(){
            if(search.value == "" || search.value == null) {
                store.dispatch("getAllProduct")
            }
            else{
                store.dispatch("getProductBySearch", search.value)
            }
        }
        // Theo dõi sự thay đổi của input `search`
        watch(search, (newSearch) => {
            clearTimeout(debounceTimer); // Xóa bộ đếm trước đó

            // Đặt bộ đếm mới 
            debounceTimer = setTimeout(() => {
                searchQuery(); // Gọi searchQuery sau khi dừng nhập
            }, 1000);
        });
        function toCart() {
            if(localStorage.getItem("jwtToken")){
                if(store.getters.updateChangeCart > 0){
                    store.dispatch('updateDataProduct');
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
            onProfileUser, jwtBol,toCart, checkStaff, OrderPlaced, purchaseHistory, search, searchQuery, avatar
        }

    },
    components: {
        HeaderVue, CategoryVue, cartIcon
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
.fa-bell:before {
    content: "\f0f3";
    color: rgb(255, 117, 140);
}
.introduce{
    width: 100%;
}
.main{
    display: flex;
    max-width: 1200px;
    justify-content: space-between;
    margin-left: auto;
    margin-right: auto;
}
.category{
   min-width: 216px;
   display: flex;
   flex-direction: column;
   padding:12px 12px 0 0 ;
   font-family: Arial, Helvetica, sans-serif;
   height: calc(100vh - 60px);
   color: rgb(83, 83, 83);
   user-select: none;
   overflow-y: auto;
}
.category::-webkit-scrollbar{
   width: 6px;
}
.category::-webkit-scrollbar-thumb {
  background-color: #888; /* Màu của thanh kéo */
  border-radius: 2px; /* Độ cong viền của thanh kéo */

}

.category::-webkit-scrollbar-track {
  background-color: #f1f1f1; /* Màu của phần track */
  
}
.homePage:hover {
   border-top: 1px solid gray ; 
}
.form {
   line-height: 40px; 
   border-bottom:1px solid gray ; 
   height: 40px;
    background-color: white;
    z-index: 99;
}

.form h3{
   padding: 0 4px;
   font-size: 18px;
   border-left: 1px solid rgb(255, 117, 140);
    color: rgb(255, 117, 140);
}
a{
   text-decoration: none;
}
.form h3:hover{
   background-color: #f7f7f7;
   cursor: pointer;
}

.dropdown-container {
    display: flex;
    flex-direction: column;
}

.dropdown {
    position: relative;
}

.dropdown-content {
    z-index: 1;
    max-height: 0;
    position: relative;
    background-color: white;
    min-width: 200px;
    overflow: hidden;
    transform: translateY(-20px); /* Bắt đầu từ trên */
    transition: max-height 0.3s ease, transform 0.3s ease;
    border-left: 1px solid rgb(255, 117, 140);
}


.dropdown-content h4 {
    padding: 10px;
    font-size: 16px;
    color: rgb(255, 117, 140);
    border-bottom: 1px solid #f1f1f1;
}

.dropdown-content h4:hover {
    background-color: #f7f7f7;
    cursor: pointer;
}

.dropdown:hover ~ .dropdown-content {
    max-height: 200px; /* Đặt chiều cao tối đa đủ lớn để chứa toàn bộ nội dung */
    transform: translateY(0); /* Hiển thị vị trí ban đầu */
    transform: translateX(+4px);
}

.dropdown-content:hover {
    max-height: 200px; /* Đặt chiều cao tối đa đủ lớn để chứa toàn bộ nội dung */
    transform: translateY(0); /* Hiển thị vị trí ban đầu */
    transform: translateX(+4px);
}

</style>