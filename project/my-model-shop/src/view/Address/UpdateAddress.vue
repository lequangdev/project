<template>
    <div class="container">
        <div class="titlePage">ĐỊA CHỈ  NHẬN HÀNG</div>
        <div class="title_add">Thêm địa chỉ:</div>
        <div class="add_product ">
            <div class="user_name  fl-column p-b-12px">
                <span class="namebox">Tên người nhận</span>
                <input v-model="dataAddress.fullName.value">
            </div>
            <div class="phone_number fl-column p-b-12px">
                <span class="namebox">Số điện thoại</span>
                <input v-model="dataAddress.phoneNumber.value">
            </div>
            <div class=" fl-column p-b-12px">
                <span class="namebox">Tỉnh (thành phố)</span>
                <input v-model="dataAddress.province.value">
            </div>
            <div class="priceProduct  fl-column p-b-12px">
                <span class="namebox">Huyện (Quận)</span>
                <input v-model="dataAddress.district.value">
            </div>
            <div class=" fl-column p-b-12px">
                <span class="namebox">Xã</span>
                <input v-model="dataAddress.commune.value">
            </div>
            <div class=" fl-column p-b-12px">
                <span class="namebox">Thôn (Làng xóm...)</span>
                <input v-model="dataAddress.village.value">
            </div>
            <div class=" fl-column p-b-12px">
                <span class="namebox">Địa chỉ cụ thể hoặc số nhà</span>
                <input v-model="dataAddress.apartment.value">
            </div>  
            
            
        </div>
        <div class="address_defalt"><input @click="addressDefault" type="checkbox" class="checked"><p>Chọn làm địa chỉ mặc định !</p></div>
        <div class="btnAdd" @click="add_address">Hoàn thành</div>
        <div class="edit_product"></div>
        <div class="delete_product"></div>
      
    </div>
</template>
<script>
//import InputVue from "@/components/InputVue.vue"
import axios from 'axios'
import { ref } from "vue"
import { unmounted, onMounted} from 'vue'
import { useRouter } from 'vue-router'
export default {
      components: {
        //InputVue
    },
    setup(){
        const route = useRouter()
        let UpdateAddress = JSON.parse(localStorage.getItem('address')) || {}
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
         onMounted(() => {
            if (UpdateAddress) {
                dataAddress.fullName.value = UpdateAddress.address_fullName || "";
                dataAddress.phoneNumber.value = UpdateAddress.address_phoneNumber || "";
                dataAddress.province.value = UpdateAddress.address_province || "";
                dataAddress.district.value = UpdateAddress.address_district || "";
                dataAddress.commune.value = UpdateAddress.address_commune || "";
                dataAddress.village.value = UpdateAddress.address_village || "";
                dataAddress.apartment.value = UpdateAddress.address_apartment || "";
            }
        })

        function addressDefault() {
            if(checked > 1){
                checked = 1
            }
            else{
                checked = 2
            }
        }
        
        function add_address(){
            let data = {
                address_id: UpdateAddress.address_id,
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
               
            axios({
                method: "Put",
                url: 'http://localhost:1234/api/Address', 
                data: data
            })
            
            .then(response => {
                console.log("thành công");
                route.push('/ListAddress')
            })
            .catch(error => {
                console.log(error);
            });
            
        }
        return {
           dataAddress,
            add_address,
            addressDefault,
            UpdateAddress
        }
    }
   

}
</script>
<style scoped>
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
    padding: 12px;
    user-select: none;
    max-width: 1200px;
    margin: 0 auto;
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