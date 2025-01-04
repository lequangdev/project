<template>
    <div class="container_At-content">
       <div class="container_At-content--main-products">
            <div  v-for="(product, index) in filteredProducts" :key="index" @click="goProductDetails(product)" class="main-product col-2">
                <div class="contais_thumbnail"><img class="thumbnail" :src="product.img_name" alt=""></div>
                <div class="contais_information">
                    <div  class="details">
                        <div class="title_product"><h3>{{ product.product_name }}</h3></div> 
                        <div class="number_shoppers">Giá: {{ product.product_price }}đ</div>
                        <div class="online_shop">Còn lại: <span class="span">{{ product.product_quantity }}</span></div>
                    </div>
                </div>
            </div>
       </div>
    </div>
</template>

<script>
import axios from 'axios';
import { useRouter } from 'vue-router';
import { computed, onMounted, onUnmounted, ref } from 'vue';
import { useStore } from 'vuex';
import { watch } from 'vue';
export default{
    setup() {
        const store = useStore()
        const router = useRouter()
        const productLimit = ref(54);
        function goProductDetails(product) {
            localStorage.setItem("productDetail", JSON.stringify(product))
            router.push({name: "ProductDetails", params: {}})
        }
        let products = computed(() => store.getters.productsAll)
        let filteredProducts = computed(() => {
            return products.value.filter(product => product.product_status === 1).slice(0, productLimit.value);
        });
        function loadMoreProducts() {
            // Kiểm tra nếu người dùng đã cuộn gần cuối trang, tăng giới hạn thêm 6 sản phẩm
            if (window.innerHeight + window.scrollY >= document.body.offsetHeight - 100 && productLimit.value < 54) {
                productLimit.value += 6;
            }
        }
        onMounted(() => {
            store.dispatch("getAllProduct")
        })
        onUnmounted(() => {
        })
        return {
            goProductDetails, products, productLimit, filteredProducts
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
    cursor: pointer;
    user-select: none;
    flex: 1 1 calc(16.67% - 16px); 
    max-width: calc(16.67% - 16px); 
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s, box-shadow 0.3s;
}
.main-product:hover {
    transform: translateY(-4px); 
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2); 
}
.number_shoppers{
    color: rgb(255, 117, 140);
    padding: 5px 0;
}
.col-2{
    width: 20%;
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

</style>