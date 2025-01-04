import { createRouter, createWebHistory } from "vue-router";

const routes = [
    {
        path:"/",
        name:"Home",  
        meta: {
            layout: "home"
        },
        component:  () => import(/* webpackChunkName: "Home" */ "../view/HomeVue.vue" ),
    },
    {
        
        path:"/News",
        name:"News",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "News" */ "../view/NewsVue.vue" ),
    },
    {
        
        path:"/Contact",
        name:"Contact",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "Contact" */ "../view/ContactVue.vue" ),
    },
    {
        
        path:"/Introduce",
        name:"Introduce",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "Introduce" */ "../view/IntroduceVue.vue" ),
    },
    {
        
        path:"/Promotion",
        name:"Promotion",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "Promotion" */ "../view/PromotionVue.vue" ),
    },
    {
        
        path:"/ProductDetails",
        name:"ProductDetails",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "ProductDetails" */ "../view/ProductDetailsVue.vue" ),
    },
    {
        
        path:"/ProfileUser",
        name:"ProfileUser",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "ProfileUser" */ "../view/ProfileUser.vue" ),
    },   
    {
        
        path:"/Login",
        name:"Login",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "Login" */ "../view/Auth/LoginVue.vue" ),
    },
    {
        path:"/Register",
        name:"Register",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "Register" */ "../view/Auth/RegisterVue.vue" ),
    },
    {
        path:"/Cart",
        name:"Cart",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "Cart" */ "../view/cart/CartVue.vue"),
    },
    {
        path:"/AdminVue",
        name:"AdminVue",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "AdminVue" */ "../view/AdminVue.vue"),
    },
    {
        path:"/Buy",
        name:"Buy",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "Buy" */ "../view/buyProduct/PurchaseVue.vue"),
    },
    {
        path:"/AddressUser",
        name:"AddressUser",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "AddressUser" */ "../view/Address/AddressUser.vue"),
    },
    {
        path:"/OrderPlaced",
        name:"OrderPlaced",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "OrderPlaced" */ "../view/Orders_user/OrderPlaced.vue"),
    },
    {
        path:"/ListAddress",
        name:"ListAddress",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "ListAddress" */ "../view/Address/ListAddress.vue"),
    }, 
    {
        path:"/UpdateAddress",
        name:"UpdateAddress",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "UpdateAddress" */ "../view/Address/UpdateAddress.vue"),
    }, 
    {
        path:"/CompleteDelivery",
        name:"CompleteDelivery",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "CompleteDelivery" */ "../view/CompleteDelivery/CompleteDelivery.vue"),
    },
    {
        path:"/StaffDelivery",
        name:"StaffDelivery",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "StaffDelivery" */ "../view/StaffDelivery.vue"),
    },
    {
        path:"/OrderManagement",
        name:"OrderManagement",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "OrderManagement" */ "../view/OrderManagement.vue"),
    },  
    {
        path:"/PageRevenueStatistics",
        name:"PageRevenueStatistics",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "PageRevenueStatistics" */ "../view/PageRevenueStatistics.vue"),
    },
    {
        path:"/StaffHistory",
        name:"StaffHistory",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "StaffHistory" */ "../view/StaffHistory.vue"),
    },
    {
        path:"/EmployeeManagement",
        name:"EmployeeManagement",  
        meta: {
        },
        component:  () => import(/* webpackChunkName: "EmployeeManagement" */ "../view/EmployeeManagement.vue"),
    }, 
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
  });
  
  export default router;