import { createApp } from 'vue';
import App from './App.vue';
import router from "./router";
import Vuex from 'vuex';
import axios from 'axios';
import '@fortawesome/fontawesome-free/css/all.css'
let app  = createApp(App);

const store = new Vuex.Store({
    state:{
        name: "user",
        Account: "",
        products:[],
        updateChangeCart:1,
        buyProduct: [],
        listAddressVuex: [],
        addressDefaultX: [{
            address_fullName: "user",
            address_phoneNumber: "user",
            address_province: "user",
            address_district: "user",
            address_commune: "user",
            address_village: "user",
            address_apartment: "user",
        }],
        productsOrder:[],
        productsAll: [],
        backPage: "",
        productAllOrder: [],
        historyUser: []
    },
    getters: {
        name: function(state) {
            return state.name
        },
        updateChangeCart: function(state) {
            return state.updateChangeCart
        },
        products: function(state){
            return state.products
        },
        buyProduct: function(state){
            return state.buyProduct
        },
        listAddress: function(state){
            return state.listAddressVuex
        },
        addressDefault: function(state){
            return state.addressDefaultX
        },
        productsOrder: function(state){
            return state.productsOrder
        },
        productsAll: function(state){
            return state.productsAll
        },
        backPage: function(state){
            return state.backPage
        },
        productAllOrder: function(state){
            return state.productAllOrder
        },
        historyUser: function(state) {
            return state.historyUser
        }
    },
    mutations: {
        setBackPage(state, page){
            state.backPage = page
        },
        setBuyProduct(state, buyProduct){
            state.buyProduct.push(buyProduct)
        },
        setName(state, name) {
            state.name = name
        },
        setAccount(state, Account) {
            state.Account = Account
        },
        getProductsInCart(state) {
            axios({
                method: "Get",
                url: "http://localhost:1234/api/Carts/GetAllByUserID", 
                params: {
                    user_id: localStorage.getItem("user_id")
                }
            })
            .then(response => {
                localStorage.setItem('products', JSON.stringify(response.data))
                state.products = response.data
                state.updateChangeCart = 0
                router.push("/Cart")
            })
            .catch(error => {
                console.log("thất bại");
            });
        },
        setUpdateChangeCart(state, updateChangeCart){
            state.updateChangeCart = updateChangeCart
        },
        eDeleteAddressUser(state, index){
            state.listAddressVuex.splice(index, 1)
        },
        getlistAddress(state, arrAddress){
            state.listAddressVuex = arrAddress
        },
        eDeleteProductIncart(state, index){
            state.products.splice(index, 1)
        },
        setAddresDefault(state, addressdefault){
            state.addressDefaultX = addressdefault
        },
        pushListProductOrders(state, products){
            state.productsOrder = products
        },
        eDeleteProductsOrders(state, index){
            state.productsOrder.splice(index, 1)
            state.productAllOrder.splice(index, 1)
        },
        setAllProduct(state, value){
            state.productsAll = value
        },
        setAllProductOrder(state, value){
            state.productAllOrder = value
        },
        setHistoryUser(state, value){
            state.historyUser = value
        }
    },
    actions:{
        updateBackPage(context, page){
            context.commit("setBackPage", page)
        },
        updateDataProduct(context){
            context.commit("getProductsInCart")
        },
        getUpdateChangeCart(context, updateChangeCart){
            context.commit("setUpdateChangeCart", updateChangeCart)
        },
        eventBuyProduct(context, buyProduct){
            context.commit("setBuyProduct", buyProduct)
        },
        deleteAddressUser(context, payload) {
            axios({
                method: "delete",
                url: 'http://localhost:1234/api/Address', 
                params: {
                    id: payload.value
                }
            })
            
            .then(response => {
                context.commit('eDeleteAddressUser', payload.index);
            })
            .catch(error => {
                console.log(error)
            })    
        },
         getAddressUser(context) {
            axios({
                method: "Get",
                url: 'http://localhost:1234/api/Address/GetAddressUser', 
                params: {
                    user_id: localStorage.getItem("user_id")
                }
            })
            
            .then(response => {  
                context.commit('getlistAddress', response.data)  
            })
            .catch(error => {
                console.log(error);
            });
        },
        
        getAddressDefault(context){
            axios({
                method: "Get",
                url: 'http://localhost:1234/api/Address/GetAddressDefault', 
                params: {
                    user_id: localStorage.getItem("user_id")
                }
            })
            .then(response => {  
                context.commit('setAddresDefault', response.data)    
            })
            .catch(error => {
                console.log(error);
            });
        },
        deleteProductInCart(context, payload) {
            axios({
                method: "delete",
                url: 'http://localhost:1234/api/Carts', 
                params: {
                    id: payload.value
                }
             
            })
            
            .then(response => {
                context.commit('eDeleteProductIncart', payload.index);
            })
            .catch(error => {
                console.log(error)
            })    
        },
        getProductsOrders(context){
            axios({
                method: "Get",
                url: 'http://localhost:1234/api/Order/GetAllByUserID', 
                params: {
                    id: localStorage.getItem("user_id")
                }
            })
            .then(response => {  
                context.commit('pushListProductOrders', response.data)    
            })
            .catch(error => {
                console.log(error);
            });
        },
        deleteProductsOrders(context, payload) {
            axios({
                method: "delete",
                url: 'http://localhost:1234/api/Order', 
                params: {
                    id: payload.idOrder
                }
            })
            
            .then(response => {
                context.commit('eDeleteProductsOrders', payload.index);
            })
            .catch(error => {
                console.log(error)
            })    
        },
        getAllProduct(context){
            axios({
                method: "get",
                url: 'http://localhost:1234/api/products/GetAll', 
            })
            .then(response => {
                context.commit('setAllProduct', response.data)
            })
            .catch(error => {
                console.log("thất bại")
            });
        },
        getAllProductOrder(context){
            axios({
                method: "get",
                url: 'http://localhost:1234/api/order/GetAllDTO', 
            })
            .then(response => {
                context.commit('setAllProductOrder', response.data)
            })
            .catch(error => {
                console.log("thất bại")
            });
        },
        getHistoryByID(context){
            axios({
                method: "Get",
                url: 'http://localhost:1234/api/Historydelivery/GetAllByUserID', 
                params: {
                    id: localStorage.getItem("user_id")
                }
            })
            .then(response => {  
                context.commit('setHistoryUser', response.data)    
            })
            .catch(error => {
                console.log(error);
            });
        },
        getProductBySearch(context, payload){
            axios({
                method: "get",
                url: 'http://localhost:1234/api/products/GetBySearch', 
                params: {
                    payload
                }
            })
            .then(response => {
                context.commit('setAllProduct', response.data)
            })
            .catch(error => {
                console.log("thất bại")
            });
        },
    }
})
app.use(store);
app.use(router);
app.mount("#app"); 