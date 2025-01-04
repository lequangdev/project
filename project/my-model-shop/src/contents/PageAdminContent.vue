<template>
    <div class="container_admin">
        <div class="title_add">Thêm sản phẩm:</div>
        <div class="add_product">
            <div class="nameProduct fl-column p-b-12px">
                <span class="namebox">Tên sản phẩm</span>
                <InputVue v-model="product_name"></InputVue>
            </div>
            <div class="priceProduct fl-column p-b-12px">
                <span class="namebox">Giá Bán</span>
                <InputVue v-model="product_price"></InputVue>
            </div>
            <div class="quantityProduct fl-column p-b-12px">
                <span class="namebox">Số lượng</span>
                <InputVue type="number"  min="1" v-model="quantity"></InputVue>
            </div>
            <div class="imgProduct fl-column p-b-12px">
                <span class="namebox">Chọn ảnh</span>
                <input
                id="employee-avatar"
                type="file"
                accept="image/*"
                @change="handleImageUpload"
              />
            </div>
        </div>
        <div class="box_btn">
            <div class="btnAdd" @click="add_product">Thêm mới</div>
        </div>
    </div>
</template>
<script>
import InputVue from "@/components/InputVue.vue"
import axios from 'axios';
import { ref, computed } from "vue";
import { useRouter } from "vue-router";
import { uploadImage } from "@/cloudinary";
import { useStore } from 'vuex';
export default {
    components: {
        InputVue
    },
    setup(){
        let product_name = ref("")
        let product_price = ref(0)
        const store = useStore()
        let quantity = ref(0)
        let img_name = ref("")
        const router = useRouter()
        async function add_product(){
            let url
            if(img_name.value instanceof File){
                const res_Url = await uploadImage([img_name.value])
                url = res_Url[0]
            }else{
                return alert('Vui lòng chọn ảnh cho sản phẩm')
            }
            let product = {
                    product_name: product_name.value,
                    product_price: product_price.value || 1,
                    product_quantity: quantity.value || 1,
                    img_name:url
                } 

            axios({
                method: "post",
                url: 'http://localhost:1234/api/Products', 
                data: product
            })
            .then(response => {
                console.log("thành công");
                store.dispatch("getAllProduct")
            })
            .catch(error => {
                console.log(error);
            });

        }
        function handleImageUpload(event) {
            const file = event.target.files[0];
            if (file) {
                img_name.value = file;
            }
        }
        return {add_product,
                product_name,
                product_price,
                quantity,
                img_name,
                handleImageUpload
                
                
            }
    }

}
</script>
<style scoped>
.container_admin{
    padding: 12px;
    user-select: none;
}
.title_add{
    color:rgb(255, 117, 140);
    margin-bottom: 12px;
    font-size: 22px;
}
.box_btn {
    display: flex;
    justify-content: space-between;
}
.home{
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
.namebox{
    padding-left: 2px;
}
.titleProduct{
    display: flex;
    max-width: 500px;
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