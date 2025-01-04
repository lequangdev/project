<template>
    <div class="container">
        <div class="page">
            <div class="logoWeb"><img src="../img_logo/Logo.png" alt=""></div>
            <div class="currentPage">{{ pageName }}</div>
        </div>
    </div>
</template>
<script>
export default {
    props: ['pageName']
}
</script>

<style scoped>
.container{
    height: 84px;
    width: 100%;
    font-family: Arial, Helvetica, sans-serif;
    background-color: #ff758c;
}
.page{
    display: flex;
}
.logoWeb img{
    width: 50px;
    height: 50px;
    border-radius: 4px;
    margin: 17px 14px;
}

.currentPage{
    font-size: 24px;
    line-height: 84px;
    color: white;
}

</style>