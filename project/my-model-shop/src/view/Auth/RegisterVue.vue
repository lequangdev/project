<template>
    <div class="container">
        <PageDisplayBar :pageName="pageName">
        </PageDisplayBar>
        <div class="content">
            <div class="introduce">
                <div class="imgLogo">
                    <img src="../../img_logo/Logo.png" alt="">
                </div>
                <span>{{ introduce }}</span>
            </div>
            <div class="mainContent">
                <div class="title">{{ pageName }}</div>
                <div class="information">
                    <div class="insertInformation">
                        <InputVue v-model="userSetup.fullName" placeholder="Full name" ></InputVue>
                    </div>
                    <div class="insertInformation">
                        <InputVue v-model="userSetup.email" placeholder="Email" ></InputVue>
                    </div>
                    <div class="insertInformation">
                        <InputVue type="Number" min="0" v-model="userSetup.phone_number" placeholder="Số điện thoại" ></InputVue>
                    </div>
                    <div class="insertInformation">
                        <InputVue v-model="userSetup.account" placeholder="Tài khoản đăng nhập" ></InputVue>
                    </div>
                    <div class="insertInformation">
                        <InputVue v-model="userSetup.passWord" placeholder="Mật khẩu"></InputVue>
                    </div>
                    <div class="insertInformation">
                        <InputVue v-model="userSetup.password_retype" placeholder="Nhập lại mật khẩu" ></InputVue>
                    </div>
                    <RegisterCompon @click="onSubmit(userSetup)" class="btnfinish">{{ pageName }}</RegisterCompon>
                    <span class="message"> {{ message }}</span>
                </div>
                <div class="nextPage">
                    <router-link class="cssRouter" to="/"><PasswordRetrieval class="passwordRetrieval"></PasswordRetrieval></router-link>
                    <router-link class="cssRouter" to="/Login"><LoginCompon class="register"></LoginCompon></router-link>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios'
import LoginCompon from '@/components/LoginCompon.vue'
import RegisterCompon from '@/components/RegisterCompon.vue'
import PasswordRetrieval from '@/components/PasswordRetrieval.vue'
import PageDisplayBar from '@/layouts/PageDisplayBar.vue'
import InputVue from '@/components/InputVue.vue'
import { user } from './registerData'
import { ref } from 'vue'
export default {
    setup() {
        const userSetup = user
        const pageName = 'Đăng ký'   
        const introduce = "Nền tảng thương mại chuyên các mặt hàng về mô hình hot hiện nay"
        let message = ref()
        function onSubmit(user){
            if(isObjectEmpty(user)){
                message.value = 'Vui lòng nhập đủ các trường !'
                return message.value
            }if (user.passWord != user.password_retype){
                message.value = 'Mật khẩu nhập lại không chính xác !'
                return message.value
            }
            axios({
                method: "post",
                url: 'http://localhost:1234/api/Users/Register',
                data: user
            })
            .then(response => {
                // Xử lý phản hồi khi request thành công
                console.log('Response:', response.data);
                message.value = response.data
            })
            .catch(error => {
                // Xử lý lỗi khi request thất bại
                console.error('Đã có lỗi:', error);
            });
        }
        function isObjectEmpty(obj) {
            for (const key in obj) {
                if (obj[key] === null || obj[key] === undefined || obj[key] === '') {
                return true; 
                }
            }
            return false;
        }

        return {
            pageName, introduce, onSubmit, userSetup, message
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
.message {
    color: red;
    margin-top: 4px;
    margin-left: 2px;
    font-size: 12px;
    font-weight: bold;
}
</style>