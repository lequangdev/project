<template>
    <div class="content">
        <NewsText></NewsText>
        <div class="img_example">
            <img src="https://tse2.mm.bing.net/th?id=OIP.RxqWW6sK5071XbsYeiN8KAHaEK&pid=Api&P=0&h=220" alt="">
            <img src="https://tse2.mm.bing.net/th?id=OIP.uBCG-QZWz6W5TMuDt94ThgHaEK&pid=Api&P=0&h=220" alt="">
            <img src="https://tse3.mm.bing.net/th?id=OIP.7sFcYNmC-Eh1_cQIvJjH3AHaEK&pid=Api&P=0&h=220" alt="">
        </div>
    </div>
</template>


<script>
import NewsText from '@/containsText/NewsText.vue';
export default{
    components: {
        NewsText
    }
}
</script>

<style scoped>
.img_example {
    display: flex;
    width: 100%;
}
.img_example img{
    max-width: 33.3333333%;
}
</style>