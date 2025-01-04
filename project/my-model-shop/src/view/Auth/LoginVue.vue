<template>
    <div class="container">
        <PageDisplayBar :pageName="pageName"></PageDisplayBar>
        <div class="content">
            <div class="introduce">
                <div class="imgLogo">
                    <img @click="test" src="../../img_logo/Logo.png" alt="">
                </div>
                <span>{{ introduce }}</span>
            </div>
            <div class="mainContent">
                <div class="title">{{ pageName }}</div>
                <div class="information">
                    <div class="insertInformation">
                        <InputVue v-model="userCheck.account" name="account" placeholder="Tài khoản, email hoặc số điện thoại" ></InputVue>
                    </div>
                    <div class="insertInformation">
                        <InputVue v-model="userCheck.passWord" placeholder="Mật khẩu"></InputVue>
                    </div>
                    <div v-show="messageLogin" class="message">Tài khoản hoặc mật khẩu không chính xác !</div>
                    <LoginCompon @click="onSubmit(userCheck)" v-if="preventSubmit"  class="btnfinish">{{ pageName }}</LoginCompon>
                </div>
                <div class="nextPage">
                    <router-link class="cssRouter" to="/"><PasswordRetrieval class="passwordRetrieval"></PasswordRetrieval></router-link>
                    <router-link class="cssRouter" to="/Register"><RegisterCompon class="register"></RegisterCompon></router-link>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios'
import LoginCompon from '@/components/LoginCompon.vue';
import RegisterCompon from '@/components/RegisterCompon.vue';
import PasswordRetrieval from '@/components/PasswordRetrieval.vue';
import PageDisplayBar from '@/layouts/PageDisplayBar.vue';
import InputVue from '@/components/InputVue.vue';
import { useRouter } from 'vue-router';
import { user } from './loginData';
import decodeJWT from '@/commonFunc/jwtDecode';
import { jwtDecode } from 'jwt-decode';
import { ref } from 'vue';

export default {
    setup() {
        const router = useRouter(); 
        let preventSubmit = true;
        let pageName = 'Đăng nhập';     
        const  introduce = "Nền tảng thương mại chuyên các mặt hàng về mô hình hot hiện nay"
        let userCheck = user
        let inforUser;
        let messageLogin = ref(false);

        function onSubmit(user){
            messageLogin.value = false
            pageName = 'Loading...'; 
            axios({
                method: "post",
                url: 'http://localhost:1234/api/Users/login',
                data: user
            })
            
            .then(response => {
                // Xử lý phản hồi khi request thành công
                console.log('Thành công:'+ response.data);
                inforUser = response.data
                userCheck = null 
                // xóa jwt cũ
                localStorage.removeItem('jwtToken');
                // lưu jwt mới
                localStorage.setItem('jwtToken', inforUser.token);   
                // lưu avatar mới
                localStorage.setItem('avatar_url', inforUser.res.avatar_url); 
                // xóa user_id cũ
                localStorage.removeItem('user_id');
                // lưu user_id mới
                localStorage.setItem('user_id', inforUser.res.user_id);
                // xóa sản phẩm cũ
                localStorage.removeItem('productInCart');
                if(jwtDecode(localStorage.getItem('jwtToken')).user_role === "manager"){
                    router.push('/AdminVue');
                }
                else if(jwtDecode(localStorage.getItem('jwtToken', inforUser.token)).user_role === "employee"){
                    router.push("StaffDelivery")
                }
                else{
                    router.push("ProfileUser")
                }
            })
            .catch(error => {
                // Xử lý lỗi khi request thất bại
                messageLogin.value = true
                console.error('Đã có lỗi:', error);
                pageName = 'Đăng nhập';
            });
            
        }
        return {
            pageName, introduce, userCheck, onSubmit,inforUser,preventSubmit,messageLogin
        }
    },
    components: {
        PageDisplayBar, InputVue, PasswordRetrieval,RegisterCompon,LoginCompon
    }

}


</script>
<style scoped>
*{
    box-sizing: border-box;
    padding: 0;
    margin: 0;
}

.container{
    font-family: Arial, Helvetica, sans-serif;
    user-select: none;
}
.content{
    display: flex;
    background-color: #f7f7f7;
}
.message{
    color: red;
    margin-top: 4px;
    margin-left: 2px;
    font-size: 12px;
    font-weight: bold;
}
.introduce{
    min-width: 800px;
    display: flex;
    flex-direction: column;
    text-align: center;
    padding: 15px;
    margin: 20px 0;
    margin-left: auto;
}
.imgLogo > img{
    width: 200px;
    height: 200px;
}
.introduce span{
    font-size: 26px;
    width: 400px;
    margin: 50px auto;
    color: #ff758c;
}

.mainContent{
    min-width: 400px;
    padding: 15px;
    margin: 20px 0;
    background-color: white;
    border-radius: 5px;
    margin-right: auto;
}
.title {
    font-size: 22px;
    padding: 20px 20px 20px 0;
    color: #ff758c;
}
.insertInformation{
    min-width: 100%;
    margin-top: 20px;
}
.insertInformation input{
    min-width: 100%;
    padding: 12.5px;
    background-color: white;
    border: 1px solid rgb(221, 221, 221);
    border-radius: 2px;
}
.btnfinish{
    min-width: 100%;
    margin-top: 20px;
    height: 40px;
    line-height: 40px;
    text-align: center;
    font-size: 16px;
    background-color: #ff758c;
    color: white;
    border-radius: 2px;
    cursor: pointer;
}
.nextPage {
    display: flex;
    padding: 10px 5px;
    font-size: 12px;
    justify-content: space-between;
    
}

.cssRouter{
    text-decoration: none;
    color: royalblue;
}

</style>