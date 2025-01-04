<template>
    <div class="container_admin">
        <HeaderVue>
            <div class="introduce">
                <div class="logoOnBar">
                    <img src="@/img_logo/Logo.png" alt="">
                    <h3>{{ introduce }}</h3>
                    <div class="title">{{ title }}</div>  
                </div> 
                <div class="box-head-right">
                    <div @click="navigate('AdminVue')" class="btn-product-management btn-form-management">Sản phẩm</div>
                    <div @click="navigate('OrderManagement')" class="btn-order-management btn-form-management">Đơn hàng</div>
                    <div @click="navigate('PageRevenueStatistics')" class="btn-revenue-management btn-form-management">Doanh thu</div>
                    <div @click="navigate('EmployeeManagement')" class="btn-staff-management btn-form-management">Nhân viên</div>
                    <div class="logout"><i  @click="logout()" class="fa-solid fa-right-from-bracket"></i></div>
                    <div class="profile">
                        <div @click="onProfileUser" class="avatar"><img :src="avatar" alt="avatar"></div>
                    </div>
                </div>
            </div>
        </HeaderVue>
        <div class="content">
            <div class="titlePage">Quản lý đặt hàng</div>
            <OrderManagement></OrderManagement>
        </div>
    </div>
</template>
<script>
import HeaderVue from "@/layouts/HeaderVue.vue"
import { useRouter } from "vue-router";
import OrderManagement from "@/contents/OrderManagement.vue"
export default {
    components: {
        HeaderVue,OrderManagement
    },
    setup(){
        const introduce = 'WANG'
        const router = useRouter()
        const title = 'Trang quản lý (Admin)' 
        const avatar = localStorage.getItem("avatar_url")
        function navigate(param){
            router.push(param)
        }
        function logout(){
            // xóa jwt cũ
            localStorage.removeItem('jwtToken');
            // xóa sản phẩm của tk cũ
            localStorage.removeItem('productInCart');
            // xóa user_id cũ
            localStorage.removeItem('user_id');
            router.push('/Login')
        }
        function onProfileUser(){
            router.push('ProfileUser')
        }
        return {
            introduce, title, navigate, logout, avatar, onProfileUser
        }
    }
}
</script>
<style scoped>
*{
    box-sizing: border-box;
}
.logout i{
    font-size: 30px;
    margin: 25px 10px;
    color: rgb(255, 117, 140);
    cursor: pointer;
}
.btn-order-management {
    opacity: 0.7;
}
.box-head-right{
    display: flex;
}
.btn-form-management{
    width: 80px;
    height: 40px;
    line-height: 40px;
    background-color: rgb(255, 117, 140);
    color: white;
    line-height: 40px;
    text-align: center;
    font-size: 14px;
    border-radius: 4px;
    cursor: pointer;
    margin: auto 10px;
}
.container_admin{
    display: flex;
    flex-direction: column;
    font-family: Arial, Helvetica, sans-serif;
    user-select: none;
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
.content{
    width: 1200px;
    margin: 15px auto;
    background-color: #f7f7f7;
}
.titlePage{
    margin: 10px 15px;
    background-color: rgb(255, 117, 140);
    max-width: 100%;
    height: 60px;
    line-height: 60px;
    font-size: 24px;
    color: white;
    text-align: center;
}
</style>
