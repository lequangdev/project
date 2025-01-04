<template>
    <div class="container">
        <HeaderCategory><NewsContent></NewsContent></HeaderCategory>
    </div>
</template>

<script>
import HeaderCategory from '@/layouts/HeaderCategory.vue';
import NewsContent from '@/contents/NewsContent.vue';
export default {
   components: {
    HeaderCategory,NewsContent
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