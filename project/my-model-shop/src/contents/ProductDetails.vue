<template>
    <div class="container">
        <div class="container_content">
            <div class="descriptionPhoto">
                <div class="imageProduct">
                    <img :src="product.selectedImage.value" alt="">
                </div>
                <div class="photoList" >
                    <img v-for="(photo, index) in productsData" :key="index" :src="photo.img_name" @click="selectImage(photo.img_name,photo.product_name, photo.product_price, photo.product_id)" alt="">
                </div>
                <div class="persenLike"><i class="fa-regular fa-heart m-6px"></i><p class="m-6px">Đã thích</p><span class="m-6px">(700)</span></div>
                <div class="sellerChannel">
                    <div class="avartaStore">
                        <img src="http://localhost:8080/img/Logo.80e45c60.png" alt="">
                    </div>
                    <div class="informationAndStatus">
                        <div class="interactWithInteract">
                            <div class="chatWithStore">Chat ngay</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container_product">
                <div class="titleproduct">
                    {{ product.titleProduct.value }}
                </div>
                <div class="assessmentInformation">
                    <div class="numberShoper">
                        còn lại: {{ product.remaining.value }} 
                    </div>
                    <div class="evaluate">
                        5 sao
                    </div>
                </div>
                <div class="productPrice"><span class="price">Giá bán:</span><span class="price">{{ formattedPrice }}</span></div>
                <div class="container_samples">
                    <div class="sampleTitles">{{ sampleTitles }}</div>
                    <div class="productSamples">
                        <div class="product_size">(Chức năng đang phát triển)</div>
                    </div>
                </div>
                <div class="ChooseQuantity">
                    <div class="quantity">Số lượng</div>
                    <div class="quantityBox">
                        <div class="minus" @click="decrement"><i class="fas fa-minus"></i></div>
                        <input type="number" @blur="onBlurIp" v-model="product.quantity.value" @input="quantityLimit"  min="1" max="999">
                        <div class="plus" @click="increment"><i class="fas fa-plus"></i></div>
                    </div>
                </div>
                <div class="decision">
                    <div  v-if="product.remaining.value != 0" @click="addToCart" class="addToCart"> 
                        <i class="fa-solid fa-cart-plus"></i>
                        <span>Thêm vào giỏ hàng</span>
                    </div> 
                    <div v-if="product.remaining.value != 0" @click="letPageBuy()" class="buyProduct">
                        Mua Ngay
                    </div>
                </div>
            </div>      
        </div>
        <div class="container_information">
            <div class="informationDescribe">
                <h2>Mô tả sản phẩm</h2>
                <p></p>
            </div>
        </div>
        <div class="otherProducts">
            <div class="otherProductsTitle">Các sản phẩm khác</div>
            <ProductsVue></ProductsVue>
        </div>
    </div>
</template>

<script>
import { ref, computed, watch, onMounted } from "vue"
import ProductsVue from "./ProductsVue.vue"
import axios from "axios"
import { useRouter, useRoute } from "vue-router"
import { useStore } from 'vuex'
import { mapGetters } from 'vuex'
export default {
    setup() {
        onMounted(() => {
            getProductByID(product_id)
        })
        const store = useStore()
        const router = useRouter()
        const route = useRoute()
        
        let product_id = route.params.id
        const productsData = computed(() => store.getters.productsAll.filter(product => product.product_status === 1).slice(0, 5))
        const startIndex = ref(0)
        const endIndex = ref(5)
        // sự khác biệt giữa các mẫu
        const sampleTitles = ref("Tỉ lệ:")
        // thông tin sản phẩm
        let product = {
            selectedImage:ref(''),
            //giá
            prices:ref('1'),
            // tiêu đề sản phẩm
            titleProduct:ref(''),
            product_id:ref(''),
            // số lượng còn lại
            remaining: ref(''),
            // số lượng mặc định
            quantity: ref(1)
        }
        watch(() => route.params.id,(newID) => {
            getProductByID(newID)
        })
        function getProductByID(newID){
            axios({
                method: "Get",
                url: 'http://localhost:1234/api/products/GetAllByID', 
                params: {
                    id: newID
                }
            })
            
            .then(response => {  
                product.selectedImage.value = response.data[0].img_name
                product.prices.value = response.data[0].product_price
                product.remaining.value = response.data[0].product_quantity
                product.product_id.value = response.data[0].product_id
                product.titleProduct.value = response.data[0].product_name

            })
            .catch(error => {
                console.log(error);
            });
        }

        // tên cửa hàng
        const nameStore = "wang";
        // ảnh đại diện cửa hàng
        const imgStore = "https://down-vn.img.susercontent.com/file/vn-11134216-7qukw-lkdr1y5phuho8a_tn"
        // số lượng còn lại
        const remaining = ref(999)
        //tăng giảm số lượng
        function increment() {      
            product.quantity.value = Math.min(remaining.value, product.quantity.value + 1)
        }
        function decrement() {
            product.quantity.value = Math.max(1, product.quantity.value - 1)
            }
        //giới hạn số lượng
        function quantityLimit() {
            if (parseInt(product.quantity.value) > remaining.value) {
                product.quantity.value = remaining.value
            }
        }
        function onBlurIp(){
            if (parseInt(product.quantity.value) <= 0 || isNaN(parseInt(this.quantity))) {
                product.quantity.value = 1;
            }
        }
        // Thay đổi tiêu đề
         const changeTitle = (title) => {
            product.titleProduct.value = title
        }
        // Gía tiền
        const addPrice = (price) => {
            product.prices.value = price
        }
        // Hàm selectImage sử dụng để chọn ảnh
        const selectImage = (url, title, price, productId) => {
            product.selectedImage.value = url
            product.product_id.value = productId
            changeTitle(title)
            addPrice(price)
            router.push({name: "ProductDetails", params: {id:productId}})
        };
       
        // let page buy
        // vào mua hàng
         function letPageBuy(){
            if(localStorage.getItem('jwtToken') != null){
                store.getters.buyProduct.length = 0
                let data = 
                {
                    img_name: product.selectedImage.value,
                    product_name: product.titleProduct.value,
                    product_price: product.prices.value,
                    product_id: product.product_id.value,
                    quantity: product.quantity.value,
                    user_id: localStorage.getItem("user_id")
                }
                store.dispatch("eventBuyProduct", data)   
                addToCart()
                router.push("/Buy")
            }
            else{
                router.push("/Login")
            } 
        }

        //hàm thêm chấm ngăn cách 3 số cho giá thành
        const formattedPrice = computed(() => {

            return product.prices.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".") + " Đ";
        })
        
        // add to cart (thêm sản phẩm vào giỏ hàng)
        const addToCart = () => {
            if(localStorage.getItem('jwtToken') != null){
                let data = {
                    price:product.prices.value,
                    product_id: product.product_id.value,
                    quantity: product.quantity.value,
                    user_id: localStorage.getItem("user_id")
                }
                axios({
                    method: "post",
                    url: 'http://localhost:1234/api/Carts',
                    data: data
                })
                .then(response => {
                    store.dispatch("getUpdateChangeCart", 1)
                })
                .catch(error => {
                    console.log("thất bại");
                });
            }
            else{
                router.push("/Login")
            } 

        }



        return {
            product,
            productsData,
            selectImage,
            sampleTitles,
            increment,
            decrement,
            quantityLimit,
            onBlurIp,
            nameStore,
            imgStore,

            addToCart,
            formattedPrice,
            letPageBuy,

        };    
    },
    components:{
        ProductsVue
    },
    computed: {
        ...mapGetters(['updateChangeCart'])
    }

}
</script>


<style scoped>
ul{
   list-style-type: none;
}

.container {
    width:1200px;
    height: 100%;
    margin: 0 auto;
    user-select: none;
    font-family: Arial, Helvetica, sans-serif;
}
.container_content {
    display: flex;
    background-color:#f7f7f7;
}
.descriptionPhoto{
    width: 480px;
    padding: 15px;
}
.imageProduct img{
    width: 450px;
    height: 450px;
}

.photoList{
    height: 100px;
    overflow: hidden;
    white-space: nowrap;
    position: relative;
}
.photoList img{
    width: 82px;
    height: 82px;
    margin: 9px 4px; 
    cursor: pointer;
}

button {
    width: 20px;
    height: 40px;
    position: absolute;
    top: 50%;
    transform: translateY(-50%);
    background-color: gray;
    border: none;
    font-size: 24px;
    cursor: pointer;
    opacity: 0.7;
    margin: 0 4px;
    color: white;
}
button:first-child {
    left: 0;
}

button:last-child {
    right: 0;
}
.persenLike{
    display: flex;
    cursor: pointer;
    color: rgb(255, 117, 140);
    margin-top: 16px;
    height: 40px;
    max-width: 170px;
}
.m-6px{
    margin: 6px;
}
.persenLike i {
    font-size: 22px;
    line-height: 28px;
}
.persenLike p {
    font-size: 18px;
    line-height: 28px;
}

.persenLike span{
    font-size: 18px;
    line-height: 28px;
    flex: 1;
}

.container_product{
    width: 720px;
    padding: 15px;
}
.titleproduct{
    height: 48px;
    font-size: 20px;
    text-overflow:ellipsis;
    display: -webkit-box; 
    -webkit-box-orient: vertical; 
    -webkit-line-clamp: 2; 
    overflow: hidden; 
}
.assessmentInformation{
    display: flex;
    height: 26px;
    margin-top: 10px;
}
.numberShoper{
    border-right: 1px solid rgb(224, 224, 224);
    color: gray;
    padding-right: 10px;
    line-height: 26px;
}
.evaluate{
    color: gray;
    padding-left: 10px;
    line-height: 26px;
}

.productPrice{
    display: flex;
    height: 66px;
    padding: 14px 20px;
    background-color: white;
    margin-top: 10px;
}

.price{
    display: block;
    line-height: 38px;
    font-size: 30px;
    color: rgb(255, 117, 140);
    margin: 0 15px;
}

.container_samples{
    margin-top: 10px;
    max-height: 190px;
    background-color: antiquewhite;
    display: flex;
}
.sampleTitles{
    margin-top: 8px;
    font-size: 14px;
    color: rgb(180, 180, 180);
    width: 110px;
    margin-left: 10px;
}
.productSamples {
    margin: 8px 8px 0 0;
    display: flex;
    flex-wrap: wrap;
    flex: 1;
}
.productSamples ul{
    height: 40px;
    margin-right: 14px;
    margin-bottom: 8px;
    padding: 0 5px;
    cursor: pointer;
    display: flex; /* Sử dụng flexbox */
    align-items: center; /* Căn giữa theo chiều dọc */
    border: 1px solid rgb(214, 214, 214);
}
.productSamples ul li{
    display: flex; /* Sử dụng flexbox */
    align-items: center; /* Căn giữa theo chiều dọc */
}
.productSamples img{
    width: 24px;
    height: 24px;
    margin-right: 6px;
}

.ChooseQuantity{
    margin-top: 10px;
    max-height: 190px;
    display: flex;
}
.quantity{
    margin-top: 8px;
    font-size: 14px;
    color: rgb(180, 180, 180);
    width: 110px;
    margin-left: 10px;
}
.quantityBox{
    display: flex;
    margin-top: 10px;
}
.quantityBox input{
    width: 50px;
    height: 32px;
    outline: none;
    border: none;
    border-top: 1px solid rgb(221, 221, 221);
    border-bottom: 1px solid rgb(221, 221, 221);
    text-align: center;
}

input[type=number]::-webkit-inner-spin-button,
input[type=number]::-webkit-outer-spin-button {
    -webkit-appearance: none;
}

.minus{
    width: 32px;
    height: 32px;
    text-align: center;
    border:  1px solid rgb(221, 221, 221);
    cursor: pointer;
}
.minus i{
    line-height: 32px;
}
.plus{
    width: 32px;
    height: 32px;
    text-align: center;
    border: 1px solid rgb(221, 221, 221);
    cursor: pointer;
}
.plus i{
    line-height: 32px;
}

.decision{
    height: 48px;
    margin-top: 15px;
    display: flex;
    line-height: 36px;
}
.addToCart{
    display: flex;
    color: rgb(255, 117, 140);
    margin-right: 20px;
    border: 1px solid rgb(255, 117, 140);
    cursor: pointer;
    padding: 5px 14px 5px 14px;
}
.addToCart:hover{
   background-color: rgb(230, 230, 230);
}
.addToCart i{
    font-size: 20px;
    padding-right: 10px;
    margin: 6px 0 6px 6px;
}
.addToCart i::before{
    font-size: 20px;
    line-height: 24px;

}

.buyProduct{
    color: white;
    background-color: rgb(255, 117, 140);
    cursor: pointer;
    padding: 5px 14px 5px 14px;
}
.buyProduct:hover{
    opacity: 0.8;
}

.sellerChannel{
    display: flex;
    margin-top: 14px;
}
.avartaStore img{
    width: 50px;
    height: 50px;
    border-radius: 25px;
    margin-right: 12px;
    cursor: pointer;
}
.nameStore{
    font-size: 18px;
}
.operationalStatuS{
    font-size: 14px;
    color: #808080;
}
.interactWithInteract{
    display: flex;
    margin-top: 8px;
}
.chatWithStore{
    height: 34px;
    padding: 0 15px;
    margin-right: 10px;
    background-color: rgb(255, 117, 140);
    color: white;
    line-height: 34px;
    cursor: pointer;
}
.viewStore{
    height: 34px;
    padding: 0 15px;
    margin-right: 10px;
    background-color: rgb(255, 117, 140);
    color: white;
    line-height: 34px;
    cursor: pointer;

}
.container_information{
    margin-top: 24px;
    background-color:#f7f7f7;
}
.container_information h2 {
    color: rgba(0,0,0,.87);
    font-size: 1.125rem;
    font-weight: 400;
    padding: 14px;
    text-transform: capitalize;
}
.otherProductsTitle{
    color: #b4b4b4;
    font-size: 18px;
    font-weight: 400;
    padding-left: 14px;
    margin-top: 24px;
    text-transform: capitalize;
}
</style>

