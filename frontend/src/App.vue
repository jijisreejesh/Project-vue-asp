<script setup>
import { RouterLink,RouterView } from 'vue-router';

</script>

<template>
   <RouterLink class="links" to="/product">Products</RouterLink>
  <RouterLink class="links" to="/customer">Customer</RouterLink> 
  <RouterLink class="links" to="/sales">Sales</RouterLink>
  <RouterView></RouterView>
  
</template>

<style scoped>
.links{
  margin: 30px;
}
</style>
