<template>
    <div class="container">
        <HeaderCategory><PromotionContent></PromotionContent></HeaderCategory>
    </div>
</template>

<script>
import PromotionContent from '@/contents/PromotionContent.vue';
import HeaderCategory from '@/layouts/HeaderCategory.vue';
export default {
   components: {
    HeaderCategory,PromotionContent
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