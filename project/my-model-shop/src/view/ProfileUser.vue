<template>
    <div class="container_profile">
        <HeaderVue class="header">
            <div class="headMasthead">
                <div class="logoOnBar">
                    <img src="../img_logo/Logo.png" alt="">
                    <h3>Wang</h3>
                </div>        
            </div>
            <div class="searchArea">
                <div class="searchBox">
                    <input type="text" placeholder="Tìm kiếm">
                </div>
                <div class="searchBtn">
                    <i class="fa-solid fa-magnifying-glass"></i>
                </div>
            </div>      
            <div class="lastMasthead">
                <div v-if="role === 'client'" @click="toCart" title="Giỏ hàng"  class="cart-shopping">
                    <i class="fa-solid fa-cart-shopping"></i>
                </div>
                <div title="Đơn đặt hàng" class="delivery">
                    <i v-if="role === 'client'" @click="OrderPlaced" class="fa-solid fa-box"></i>
                    <i v-if="role === 'employee'" @click="nextPage('StaffDelivery')" class="fa-solid fa-box"></i>
                    </div>
                <div title="Lịch sử" class="history">
                    <i v-if="role === 'client'" @click="purchaseHistory" class="fa-solid fa-clock-rotate-left"></i>
                    <i v-if="role === 'employee'" @click="nextPage('StaffHistory')" class="fa-solid fa-clock-rotate-left"></i>
                </div>
                <div title="Thông báo"  class="notification"><i class="fa-solid fa-bell"></i></div>
                <div class="avatar"><img :src="avatar" alt="avatar"></div>
            </div>
        </HeaderVue>
        <div class="content">
            <div class="avarteUser">
                <img :src="avatar" alt="avarta">
            </div>
            <div class="nameUser">{{ name }}</div>
            <div v-if="role === 'client'" class="userInformation">
                <div @click="goHome" class="goHome userInformation-list">Trang chủ</div>
                <div class="userAccount userInformation-list">Thông tin tài khoản</div>
                <div @click="OrderPlaced" class="goHome userInformation-list">Đơn hàng</div>
                <div @click="purchaseHistory" class="purchaseHistory userInformation-list">Lịch sử mua hàng</div>
                <div class="changePassword userInformation-list">Đổi mật khẩu</div>
                <div class="errorReport userInformation-list">Báo lỗi</div>
                <div class="customerSupport userInformation-list">Hỗ trợ khách hàng</div>
                <div @click="logout" class="loginOut userInformation-list">Đăng xuất</div>
            </div>
            <div v-if="role === 'employee'" class="userInformation">
                <div class="userAccount userInformation-list">Thông tin tài khoản</div>
                <div @click="nextPage('StaffDelivery')" class="goHome userInformation-list">Đơn hàng</div>
                <div @click="nextPage('StaffHistory')" class="purchaseHistory userInformation-list">Lịch sử giao hàng</div>
                <div class="changePassword userInformation-list">Đổi mật khẩu</div>
                <div class="errorReport userInformation-list">Báo lỗi</div>
                <div class="customerSupport userInformation-list">Liên hệ quản lý</div>
                <div @click="logout" class="loginOut userInformation-list">Đăng xuất</div>
            </div>
            <div v-if="role === 'manager'" class="userInformation">
                <div class="userAccount userInformation-list">Thông tin tài khoản</div>
                <div @click="nextPage('AdminVue')" class="goHome userInformation-list">Trang quản lý</div>
                <div @click="logout" class="loginOut userInformation-list">Đăng xuất</div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import { useRouter } from 'vue-router'
import HeaderVue from '@/layouts/HeaderVue.vue'
import { mapGetters } from 'vuex'
import { onMounted } from 'vue';
import { useStore } from 'vuex';
import decodeJWT from '@/commonFunc/jwtDecode';
import { jwtDecode } from 'jwt-decode';
export default {
    setup() {
        const role = jwtDecode(localStorage.getItem('jwtToken')).user_role
        const avatar = localStorage.getItem('avatar_url')   
        const store = useStore()
        const router = useRouter()
        function logout(){
            // xóa jwt cũ
            localStorage.removeItem('jwtToken');
            // xóa sản phẩm của tk cũ
            localStorage.removeItem('productInCart');
            // xóa user_id cũ
            localStorage.removeItem('user_id');
            router.push('/Login')
        }
        
        function goHome(){
            router.push('/')
        }
        function OrderPlaced(){
            router.push("/OrderPlaced")
        }
        function purchaseHistory(){
            router.push("/CompleteDelivery")
        }
        function nextPage(param){
            router.push(param)
        }

        const jwtToken = localStorage.getItem('jwtToken') // Lấy JWT từ localStorage
        function verifyToken() {
            axios.post('http://localhost:1234/api/Users/ProfileUser', null, {
                headers: {
                Profile: `Bearer ${jwtToken}` // Gửi JWT qua tiêu đề Profile
                }
            })
            .then(() => {
                router.push('/ProfileUser');
                store.commit("setName",decodeJWT(localStorage.getItem('jwtToken')).fullname)
                store.commit("setAccount",decodeJWT(localStorage.getItem('jwtToken')).account)
            })
            .catch(error => {
                // JWT không hợp lệ, người dùng chưa đăng nhập
                console.error('JWT không hợp lệ:', error); 
            });
        }
        const pageName = 'Chi tiết sản phẩm';     
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

        onMounted(() => {
            if (jwtToken) {
                // Nếu có JWT đã lưu trữ, gửi JWT lên backend để kiểm tra
                verifyToken();
            }
        });
        return {
            logout,
            goHome,
            OrderPlaced,
            purchaseHistory,
            toCart,nextPage,role,avatar
        }
    },
    computed: {
        ...mapGetters(['name'])
    },
    components: {
        HeaderVue
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
}
.container_profile{
    width: 1200px;
    margin-left: auto;
    margin-right: auto;
    font-family: Arial, Helvetica, sans-serif;
    user-select: none;
}
.avarteUser{
    margin-top: 24px;
    text-align: center;
}
.avarteUser img{
    width: 220px;
    height: 220px;
    border-radius: 110px;
}
.nameUser{
    margin-top: 12px;
    text-align: center;
    font-size: 24px;
    color: gray;
}
.userInformation{
    margin-top: 12px;
    border-left: 1px solid gray;
}
.userInformation-list{
    height: 50px;
    color: gray;
    color: 22px;
    line-height: 50px;
    padding:0 15px;
    cursor: pointer;
}
.userInformation-list:hover{
    background-color: #f7f7f7;
}
.loginOut{
    color: rgb(255, 83, 83);
}
</style>