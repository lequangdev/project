<template>
    <div class="container">
        <div class="titlePage">ĐỊA CHỈ  NHẬN HÀNG</div>
        <div class="title_add">Thêm địa chỉ:</div>
        <div class="add_product ">
            <div class="user_name  fl-column p-b-12px">
                <span class="namebox">Tên người nhận</span>
                <InputVue v-model="dataAddress.fullName.value" placeholder="Nhập tên người nhận" ></InputVue>
            </div>
            <div class="phone_number fl-column p-b-12px">
                <span class="namebox">Số điện thoại</span>
                <InputVue v-model="dataAddress.phoneNumber.value" placeholder="Nhập số điện thoại"></InputVue>
            </div>
            <div class=" fl-column p-b-12px">
                <span class="namebox">Tỉnh (thành phố)</span>
                <InputVue v-model="dataAddress.province.value" placeholder="Nhập Tỉnh (thành phố)"></InputVue>
            </div>
            <div class="priceProduct  fl-column p-b-12px">
                <span class="namebox">Huyện (Quận)</span>
                <InputVue v-model="dataAddress.district.value" placeholder="Nhập Huyện (Quận)"></InputVue>
            </div>
            <div class=" fl-column p-b-12px">
                <span class="namebox">Xã (Phường)</span>
                <InputVue v-model="dataAddress.commune.value" placeholder="Nhập Xã (Phường)"></InputVue>
            </div>
            <div class=" fl-column p-b-12px">
                <span class="namebox">Thôn (Làng xóm...)</span>
                <InputVue v-model="dataAddress.village.value" placeholder="Nhập Thôn (Làng xóm...)"></InputVue>
            </div>
            <div class=" fl-column p-b-12px">
                <span class="namebox">Địa chỉ cụ thể hoặc số nhà</span>
                <InputVue v-model="dataAddress.apartment.value" placeholder="Nhập Địa chỉ cụ thể hoặc số nhà"></InputVue>
            </div>
        </div>
        <div class="address_defalt"><input @click="addressDefault" type="checkbox" class="checked"><p>Chọn làm địa chỉ mặc định !</p></div>
        <div class="btn-box">
            <div class="btnAdd" @click="add_address">Thêm mới </div>
            <div class="btnback" @click="backPage">Quay lại </div>
        </div>
        <div class="edit_product"></div>
        <div class="delete_product"></div>
      
    </div>
</template>
<script>
import InputVue from "@/components/InputVue.vue"
import axios from 'axios'
import { useStore } from 'vuex'
import { ref } from "vue"
import { unmounted }from 'vue'
import { useRouter } from 'vue-router'

export default {
    components: {
        InputVue
    },

    setup(){
        const store = useStore()
        const router = useRouter()
        let checked = 1
       let dataAddress = {
            fullName: ref(""),
            phoneNumber: ref(""),
            province: ref(""),
            district: ref(""),
            commune: ref(""),
            village: ref(""),
            apartment: ref("")   
        }

        function addressDefault() {
            if(checked > 1){
                checked = 1
            }
            else{
                checked = 2
            }
        }
        
        function add_address(){
            
            let AddressUser = {
                address_fullName: dataAddress.fullName.value,
                address_phoneNumber: dataAddress.phoneNumber.value,
                address_province: dataAddress.province.value,
                address_district: dataAddress.district.value,
                address_commune: dataAddress.commune.value,
                address_village: dataAddress.village.value,
                address_apartment: dataAddress.apartment.value,
                user_id: localStorage.getItem("user_id"),
                address_default: checked
            } 
            console.log(AddressUser);
            
            axios({
                method: "Post",
                url: 'http://localhost:1234/api/Address', 
                data: AddressUser
            })
            
            .then(response => {
                console.log("thành công");
                alert('Thêm địa chỉ mới thành công')
            })
            .catch(error => {
                console.log(error);
            });
            
        }
        function backPage(){
            window.history.back();
        }
        return {
           dataAddress,
            add_address,
            addressDefault,
            backPage
        }

    }

}
</script>
<style scoped>
.btn-box{
    display:flex;
    justify-content:space-between;
}
.btn-box .btnback{
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
.address_defalt {
    display: flex;
    margin-bottom: 5px;
}
.address_defalt input{
    cursor: pointer;
}
.address_defalt p{
    margin-left: 10px;
    font-size: 14px;
}
.container{
    padding: 12px ;
    user-select: none;
    max-width: 1200px;
    margin: 0 auto;
    background-color: rgb(247, 247, 247)
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
.add_product{
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
    color:rgb(255, 117, 140);
}
.add_product input{
    padding: 12.5px;
    background-color: white;
    border: 1px solid rgb(221, 221, 221);
    border-radius: 2px;
    width: 400px;
}
.fl-column{
    display: flex;
    flex-direction: column;
}
.border-b{
    border-bottom: 1px solid gainsboro
}
.namebox{
    padding-left: 2px;
}
.titleProduct{
    display: flex;
    max-width: 500px;
}
.container{
    flex: 1;
}
.p-b-12px{
    padding-bottom: 12px;
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