<template>
    <div class="container">
        <HeaderVue>
            <div class="introduce">
                <div class="logoOnBar">
                    <img src="@/img_logo/Logo.png" alt="">
                    <h3>{{ introduce }}</h3>
                    <div class="title">{{ title }}</div>  
                </div> 
                <div class="header_right">
                    <div @click="pageHome" class="home">
                    <i class="fa-solid fa-house"></i>
                    </div>
                    <div class="profile">
                        <div @click="onProfileUser" class="avatar"><img :src="avatar" alt="avatar"></div>
                    </div>
                </div>
            </div>
        </HeaderVue>
        <div class="mainContent">
            <div class="columnName">
                <input class="selectAll" v-model="checkedAll"  @click="selectAll()" type="checkbox">
                <div class="columnName_products ">Sản phẩm</div>
                <div class="columnName_price colum1 center color808080">Đơn giá</div>
                <div class="columnName_quantity colum1 center color808080">Số lượng</div>
                <div class="columnName_act colum1 center color808080">Thao tác</div>
            </div>
            <div class="product_list">
                <div class="product" v-for="(product, index) in cart" :key="index" >
                    <input class="selectProduct" type="checkbox" v-model="checkedItems[index]" @click="statusCheckbox(index)">
                    <img class="thumb" :src="product.img_name"  alt="product">
                    <div class="name_product">{{ product.product_name}}</div>
                    <div class="operation_product">
                        <div class="price_product colum1 center">{{ product.product_price}}</div>
                        <div class="quantityBox colum1 center">
                            <div class="minus" @click="decrement(index)"><i class="fas fa-minus"></i></div>
                            <input type="number" @blur="onBlurIp(index)" v-model="product.quantity" @input="limitCart(product)">
                            <div class="plus" @click="increment(index)"><i class="fas fa-plus"></i></div>
                        </div>
                        <div @click="deleteProduct(product.cart_id,index)"  class="act_product colum1 center">Xóa</div>
                    </div>
                </div>
            </div>
            <div class="boxBuyProduct">
                <div class="quantyProductBuy">
                    Số lượng sản phẩm:{{ quantityProductBuy }}
                </div>
                <div class="sumPrice">
                    Tổng tiền:{{ sumPrice }}Đ
                </div>
                <div @click="letPageBuy()" class="buyProduct">
                    Mua ngay
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { useRouter } from 'vue-router'
import HeaderVue from '@/layouts/HeaderVue.vue'
import NavigationBarVue from "@//layouts/NavigationBarVue.vue"
import { getCurrentInstance, ref, reactive  } from 'vue'
import { useStore } from 'vuex'
import { onBeforeUnmount, computed, onUnmounted ,} from 'vue'
export default {
    data(){
        return {
            // cart: productsInCart
        }
    },
    setup() {   
        const store = useStore()
        let proxy = getCurrentInstance()   
        let checked = false
        let checkedItems = []
        let sumPrice = ref(0)
        let quantityProductBuy = ref(0)
        let cart = computed(() => store.getters.products)
        let indexProductBuy = []
        const avatar = localStorage.getItem('avatar_url') 
        const router = useRouter()
        const introduce = 'WANG'
        const title = ' Giỏ hàng' 
        let checkedAll = ref(false);
        // số lượng còn lại
        const remaining = ref(999)

        function limitCart(product){
            if(product.quantity < 0) {
                product.quantity = 1;
            } else if (product.quantity > 999) {
                product.quantity = 999
            }
        }
        function localUpdateData(){
            if(store.getters.products == "" || store.getters.products == null || store.getters.products == undefined){
                store.dispatch('updateDataProduct');
            }
        }
        function pageHome(){
            router.push("/")
        }
        localUpdateData()
        // số lượng mặc định
        function onProfileUser() {
            if(localStorage.getItem('jwtToken') != null){
                router.push({name: "ProfileUser", params: {}})
            }
            else{
            router.push({name: "Login", params: {}})
            }
        }
        
        //tăng giảm số lượng
        function increment(index) { 
            if(cart.value[index].quantity < 999){
               cart.value[index].quantity++
            }else if(cart.value[index].quantity > 999){
                cart.value[index].quantity = 999
            }
            checkedItems[index] = true
            changeBox(index)
        }
        function decrement(index) {
            if(cart.value[index].quantity >=2){
                cart.value[index].quantity--
            }
            checkedItems[index] = true
            changeBox(index)
        }
        function onBlurIp(index){
            if (cart.value[index].quantity <= 0 || isNaN(cart.value[index].quantity)) {
                cart.value[index].quantity = 1;
            }
            changeBox(index)
            
        }
        // xóa khỏi giỏ hàng
        function deleteProduct(value,index){
            let payload = {
                value,
                index
            }
            store.dispatch("deleteProductInCart", payload)
        }
        // Tổng giá
        function changeBox(index){
            // Calculate sumPrice and quantityProductBuy based on checked items
            quantityProductBuy.value = 0;
            sumPrice.value = 0;
            for (let i = 0; i < cart.value.length; i++) {
                if (checkedItems[i]) {
                    sumPrice.value += cart.value[i].product_price * cart.value[i].quantity;
                    quantityProductBuy.value += cart.value[i].quantity;
                }
            }
        }

        function statusCheckbox(index) {
            checkedItems[index] = !checkedItems[index];
            changeBox(index)
        }
        // chọn tất cả 
        function selectAll(){
            checkedAll.value = !checkedAll.value
            if (checkedAll.value) {
                cart.value.forEach(function (currentValue, index) {
                    checkedItems[index] = true;
                });
            } 
            else {
                cart.value.forEach(function (currentValue, index) {
                    checkedItems[index] = false;
                });
            }
            changeBox();
        }
        // vào mua hàng
        function letPageBuy() {
            store.getters.buyProduct.length = 0
            checkedItems.forEach(function(e, index){
                if(e && cart.value[index].product_quantity != 0){
                    store.dispatch("eventBuyProduct", cart.value[index]) 
                }
            })
            router.push("/Buy")
        }
        return {
            introduce,title,onProfileUser,increment,decrement,onBlurIp,cart,deleteProduct,quantityProductBuy,
            sumPrice,checked,statusCheckbox,selectAll,checkedItems,
            limitCart,letPageBuy,checkedAll,pageHome, avatar
        }
    },
    components: {
        HeaderVue,
    }
        
   
}

</script>

<style scoped>
*{
    box-sizing: border-box;
}
.header_right{
    display: flex;
}
.home{
    margin: auto 8px;
    padding: 10px;
    height: 47px;
    font-size: 24px;
    color: rgb(255, 117, 140);
    cursor: pointer;
}
.container{
    display: flex;
    flex-direction: column;
    font-family: Arial, Helvetica, sans-serif;
    user-select: none;
    min-height: 100vh;
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
.mainContent{
    width: 1200px;
    margin: 0 auto;
    margin-top: 50px;

}
.columnName{
    display: flex;
    font-size: 14px;
    height: 55px;
    line-height: 55px;
    background-color: #f7f7f7;
    padding:0 12px;
    border: 1px solid orange;

}
.columnName_products{
    width: 500px;
}
.colum1{
    flex: 1;
}
.center{
    text-align: center;
    margin: auto 0;
}
.selectAll{
    margin:0 20px;
    width: 18px;
}
.color808080{
    color: #808080;
}

.selectProduct{
    width: 18px;
    margin: 0 20px;
    width: 18px;
}


.quantityBox{
    display: flex;
    max-height: 190px;
    justify-content: center;
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
.thumb{
    width: 80PX;
    height: 80px;
}
.product{
    display: flex;
    padding: 12px
}
.name_product{
    width: 420px;
    padding-left: 10px;
}
.operation_product{
    display: flex;
    flex: 1;
}
.product_list{
    margin-top: 12px;
    background-color: #f7f7f7;
}
.price_product{
    font-size: 14px;
}
.act_product:hover{
    color: rgb(255, 117, 140);
    cursor: pointer;
}
.boxBuyProduct{
    display: flex;
    width: 100%;
    height: 60px;
    background-color: white;
    position: sticky;
    bottom: 0;
    line-height: 60px;
   padding-left: 70px;
    border-bottom:1px solid rgb(255, 117, 140) ;
    color: rgb(255, 117, 140);
}
.buyProduct{
    width: 100px;
    height: 50px;
    color: white;
    background-color: rgb(255, 117, 140);
    cursor: pointer;
    padding: 5px 14px 5px 14px;
    margin: 5px 0;
    position: absolute;
    right: 0;
    line-height: 40px;
}
.sumPrice{
    margin-left: 400px;
}
.buyProduct:hover{
    opacity: 0.8;
}
</style>