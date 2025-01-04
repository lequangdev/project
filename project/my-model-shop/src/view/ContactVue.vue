<template>
    <div class="container">
       <HeaderCategory><ContactContent></ContactContent></HeaderCategory> 
    </div>
</template>

<script>
import HeaderCategory from '@/layouts/HeaderCategory.vue';
import ContactContent from '@/contents/ContactContent.vue';

export default {

   components: {
    HeaderCategory,ContactContent
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