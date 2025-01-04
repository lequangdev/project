<template>
    <div class="container_At-content">
        <div class="searchArea">
            <div class="searchBox">
                <input @keydown.enter="searchQuery"  v-model="search" type="text" placeholder="Tìm kiếm">
            </div>
            <div @click="searchQuery" class="searchBtn" >
                <i class="fa-solid fa-magnifying-glass"></i>
            </div>
        </div> 
       <div class="container_At-content--main-products">
            <div  v-for="(product, index) in products" :key="index"  class="main-product col-2">
                <div class="contais_thumbnail"><img class="thumbnail" :src="product.img_name" alt=""></div>
                <div class="contais_information">
                    <div  class="details">
                        <div class="title_product"><h3>{{ product.product_name }}</h3></div> 
                        <div class="number_shoppers">Giá: {{ product.product_price }}đ</div>
                        <div class="online_shop">Số lượng: <span class="span">{{ product.product_quantity }} cái</span></div>
                        <div v-if="product.product_status === 1" @click="statusProduct(product.product_id, $event, product)" class="delete_product">Gỡ sản phẩm</div>
                        <div v-else-if="product.product_status === 2" @click="statusProduct(product.product_id, $event, product)" class="restore_product">Đăng lại</div>
                        <div @click="updateProduct($event, product)" class="edit_product"> Sửa </div>
                    </div>
                </div>
            </div>
       </div>
       <div v-if="isEditModalVisible" class="modal-overlay" @click="closeEditModal">
            <div class="modal-content" @click.stop>
                <div class="modal-buttons">
                    <div class="add_product">
                        <div class="nameProduct fl-column p-b-12px">
                            <span class="namebox">Tên sản phẩm</span>
                            <input v-model="UpdatedProduct.product_name.value" >
                        </div>
                        <div class="priceProduct fl-column p-b-12px">
                            <span class="namebox">Giá Bán</span>
                            <input v-model="UpdatedProduct.product_price.value"  >
                        </div>
                        <div class="quantityProduct fl-column p-b-12px">
                            <span class="namebox">Số lượng</span>
                            <input type="number"  min="1" v-model="UpdatedProduct.product_quantity.value" >
                        </div>
                        <div class="imgProduct fl-column p-b-12px">
                            <span class="namebox">Chọn ảnh</span>
                            <input id="employee-avatar" type="file" accept="image/*" @change="handleImageUpload"/>
                        </div>
                    </div>
                    <div class="box_btn">
                        <button class="submit" @click="saveChanges" >Lưu</button>
                        <button class="close" @click="closeEditModal">Hủy</button>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="isDeleteModalVisible" class="modal-overlay" @click="closeDeleteModal">
            <div class="modal-content" @click.stop>
                <img class="imgConfirm" :src="imgConfirm" alt="">
                <h3 class="title_confirm">Bạn có chắc chắn muốn gỡ sản phẩm này?</h3>
                <div class="modal-buttons">
                    <div class="box_btn">
                        <button class="submit" @click="confirmDelete">Có</button>
                        <button class="close" @click="closeDeleteModal">Hủy</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
import axios from 'axios';
import { useRouter } from 'vue-router';
import { computed, onMounted, ref, watch } from 'vue';
import { useStore } from 'vuex';
import { uploadImage } from "@/cloudinary";
export default{
    components: {
    },
    setup() {
        const store = useStore()
        const router = useRouter()
        const isEditModalVisible = ref(false)
        const isDeleteModalVisible = ref(false);
        const productToDelete = ref(null);
        const productIndexToDelete = ref(null);
        let imgConfirm = ref('')
        const search = ref('')
        let imgSelect = ref('')
        let products = computed(() => store.getters.productsAll)
        const searchTimeout = ref(null)
        watch(search, (newSearch) => {
            if (searchTimeout.value) {
                clearTimeout(searchTimeout.value)
            }
            searchTimeout.value = setTimeout(() => {
                searchQuery()
            }, 500)
        })
        function searchQuery(){
            if(search.value == "" || search.value == null) {
                store.dispatch("getAllProduct")
            }
            else{
                store.dispatch("getProductBySearch", search.value)
            }
        }
        function handleImageUpload(event) {
            const file = event.target.files[0];
            if (file) {
                imgSelect.value = file;
            }
        }
        let UpdatedProduct = {
            product_id: ref(""),
            product_name: ref(""),
            product_price: ref(0),
            product_quantity: ref(1),
            img_name: ref(""),
            product_status: ref(1)
        }
        function goProductDetails(product) {
            localStorage.setItem("productDetail", JSON.stringify(product))
            router.push({name: "ProductDetails", params: {}})  
        }
        function statusProduct(param,event, product) {
            event.stopPropagation();
            productToDelete.value = param;
            isDeleteModalVisible.value = true;
            UpdatedProduct.product_id.value = product.product_id
            UpdatedProduct.product_name.value  = product.product_name
            UpdatedProduct.product_price.value = product.product_price || 1
            UpdatedProduct.product_quantity.value = product.product_quantity || 1
            UpdatedProduct.img_name.value = product.img_name
            imgConfirm.value = product.img_name
            if(product.product_status == 1){
                UpdatedProduct.product_status.value = 2
            }else{
                UpdatedProduct.product_status.value = 1
            }
        }

        function closeDeleteModal() {
            isDeleteModalVisible.value = false;
            productToDelete.value = null;
            productIndexToDelete.value = null;
        }

        function confirmDelete() {
            let imageUrl = UpdatedProduct.img_name.value;
            let data = {
                product_id:UpdatedProduct.product_id.value,
                product_name:UpdatedProduct.product_name.value,
                product_price:UpdatedProduct.product_price.value,
                product_quantity:UpdatedProduct.product_quantity.value,
                img_name:imageUrl,
                product_status:  UpdatedProduct.product_status.value
            } 
            
            axios({
                method: "Put",
                url: 'http://localhost:1234/api/Products', 
                data: data
            })
            
            .then(response => {
                store.dispatch("getAllProduct")
                document.removeEventListener('keydown', handleEnterKey)
                closeDeleteModal()
            })
            .catch(error => {
                console.log(error);
            });
        }
        function updateProduct(event, product) {
            event.stopPropagation()
            isEditModalVisible.value = true;
            UpdatedProduct.product_id.value = product.product_id
            UpdatedProduct.product_name.value  = product.product_name
            UpdatedProduct.product_price.value = product.product_price || 1
            UpdatedProduct.product_quantity.value = product.product_quantity || 1
            UpdatedProduct.img_name.value = product.img_name
            UpdatedProduct.product_status.value = product.product_status
            document.addEventListener('keydown', handleEnterKey);
        }
        function closeEditModal() {
            isEditModalVisible.value = false;
            document.removeEventListener('keydown', handleEnterKey)
            imgSelect.value = null
        }
        async function saveChanges(){
            let imageUrl = UpdatedProduct.img_name.value;
            if (imgSelect.value instanceof File) {
                const uploadedImage = await uploadImage([imgSelect.value]);
                imageUrl = uploadedImage[0];
            }
            let data = {
                product_id:UpdatedProduct.product_id.value,
                product_name:UpdatedProduct.product_name.value,
                product_price:UpdatedProduct.product_price.value,
                product_quantity:UpdatedProduct.product_quantity.value,
                img_name:imageUrl,
                product_status: UpdatedProduct.product_status.value
            } 
            axios({
                method: "Put",
                url: 'http://localhost:1234/api/Products', 
                data: data
            })
            
            .then(response => {
                store.dispatch("getAllProduct")
                document.removeEventListener('keydown', handleEnterKey)
                isEditModalVisible.value = false
            })
            .catch(error => {
                console.log(error);
            });
        }
        onMounted(() => {
            store.dispatch("getAllProduct")
        })
        function handleEnterKey(event) {
            if (event.key === 'Enter' && isEditModalVisible.value ) {
                saveChanges();
            }
        }
        return {
            goProductDetails, products, statusProduct, updateProduct, search,
            closeEditModal, isEditModalVisible, UpdatedProduct, saveChanges,searchQuery,handleImageUpload,
            productToDelete, isDeleteModalVisible, productIndexToDelete, confirmDelete, closeDeleteModal,imgConfirm
        }
    }
}
</script>

<style scoped>
*{
    font-family: Arial, Helvetica, sans-serif;
}
.container_At-content{
    width: 100%;
    padding: 12px 0 0 12px;
    height: 100%;
 
}

.container_At-content--main-products{
    width: 100%;
    display: flex;
    flex-wrap: wrap;
    height: 100%;
    gap: 16px;
}

.main-product {
    height: auto; /* Thay vì cố định height, để nó tự điều chỉnh theo nội dung bên trong */
    user-select: none;
    flex: 1 1 calc(16.67% - 16px); 
    max-width: calc(16.67% - 16px); 
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}
.number_shoppers{
    color: rgb(255, 117, 140);
}
.col-2{
    width: 20%;
}
.modal-content input:focus {
    outline: none;
}
.delete_product {
    margin-top: 5px;
    height: 30px;
    width: 120px;
    line-height: 30px;
    background-color: #dc3545;
    color: white;
    cursor: pointer;
    text-align: center;
}
.edit_product {
    margin-top: 5px;
    height: 30px;
    width: 120px;
    line-height: 30px;
    background-color: #F5A623;
    color: white;
    cursor: pointer;
    text-align: center;
}
.contais_thumbnail{
    width: 100%;
    position: relative;
    padding-top: 100%; /* Tạo tỷ lệ 1:1 cho chiều rộng và chiều cao */
    background-color: rgb(241, 241, 241);
    border-radius: 8px;
    overflow: hidden; /* Ẩn phần ảnh thừa nếu có */
    text-align: center;
}
.thumbnail{
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    object-fit: cover; /* Cắt ảnh để lấp đầy khung chứa */
    border-radius: 8px;
}
.contais_information{
    max-width: 100%;
    display: flex;
    justify-content: flex-start;
    overflow: hidden; 
}
.avarta_shop{
    width: 38px;
    height: 38px;
    cursor: pointer;
    margin: 12px 0;
    border-radius: 19px;
}
.details{
    color: rgb(156, 156, 156);
    font-size: 12px;
    margin: 12px;
}
.title_product{
    font-size: 12px;
    color: black;
    max-height: 32px;
    max-width: 110px;
}
.title_product h3{
    max-width: 100%; 
    max-height: 38px;
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: 2; /* Số dòng tối đa */
    overflow: hidden;
    text-overflow: ellipsis;
    max-height: 44px; /* Số dòng * chiều cao của mỗi dòng */

}   
.imgConfirm{
    width: 100px;
    height: 100px;
}
.box_btn{
    display: flex;
    justify-content: space-between;
}
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 9999;
}

.modal-content {
    background: white;
    padding: 20px;
    border-radius: 8px;
    width: 440px;
    max-width: 90%;
    text-align: center;
}

.modal-content h2 {
    margin-bottom: 15px;
}

.modal-content input {
    width: 100%;
    padding: 8px;
    margin: 10px 0;
    border: 1px solid #ccc;
    border-radius: 4px;
}

.modal-buttons {
    display: flex;
    flex-direction: column;
    justify-content: center;
    gap:10px;
}

.modal-buttons button {
    padding: 10px 20px;
    border: none;
    border-radius: 4px;
    cursor: pointer;

}

.modal-buttons .submit {
    background-color: #28a745;
    color: white;
}

.modal-buttons .close {
    background-color: #dc3545;
    color: white;
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
.p-b-12px{
    padding-bottom: 12px;
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
.searchArea{
    display: flex;
    height: 40px;
    margin: 10px 40px;
    line-height: 40px;
    justify-content: center;
}
.title_confirm{
    margin: 10px 0;
}
.restore_product{
    margin-top: 5px;
    height: 30px;
    width: 120px;
    line-height: 30px;
    background-color: #28a745;
    color: white;
    cursor: pointer;
    text-align: center;
}

</style>