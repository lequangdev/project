<template>
    <i @click="handleClick" class="fa-solid fa-cart-shopping"></i>
</template>

<script>
export default {
 props: {
   username: String,
   handleClick: function(){},
 },
}
</script>
<style scoped>
</style>