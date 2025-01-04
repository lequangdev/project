<template>
    <div class="container">
        <HeaderCategory><ProductsVue></ProductsVue></HeaderCategory>
    </div>
</template>

<script>
import ProductsVue from '@/contents/ProductsVue.vue';
import HeaderCategory from '@/layouts/HeaderCategory.vue';
import { jwtDecode } from 'jwt-decode';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
export default {
    setup(){
        let role = ref('')
        const token = localStorage.getItem('jwtToken');
        if(token && jwtDecode(token)?.user_role){
            role.value = jwtDecode(token).user_role
        }
        const router = useRouter()
        onMounted(() => {
            if(role.value === 'manager'){
                router.push("AdminVue")
            }
            else if(role.value === 'employee'){
                router.push("staffDelivery")
            }
        })
        return {role}
    },
   components: {
    ProductsVue,HeaderCategory
   }
}
</script>

<style scoped> 

*{
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

</style>