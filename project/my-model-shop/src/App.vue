<template>
    <router-view></router-view>
</template>

<script>
import decodeJWT from './commonFunc/jwtDecode';
import router from './router';
export default {
  setup(){

    return {
    }
  }
}
</script>

<style>
*{
  padding: 0;
  margin: 0;
  box-sizing: border-box;
  font-family: Arial, Helvetica, sans-serif;
}
</style>
