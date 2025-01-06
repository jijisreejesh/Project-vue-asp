<script setup>
import { RouterLink, RouterView } from "vue-router";
import { IconMenu2,IconChevronLeft,IconBrandProducthunt,IconUserCog,IconLetterSSmall,IconHome
} from "@tabler/icons-vue";
import {ref}from 'vue';
const drawer=ref(true);
const rail=ref(true);
</script>

<template>
  <v-card>
    <v-layout>
      <v-navigation-drawer v-model="drawer"
      width="200" 
        :rail="rail"
        permanent
        @click="rail = false">
        <v-list-item
          :prepend-icon="IconMenu2"
          title="Menu"
          nav
        ><template v-slot:append>
            <v-btn
              :icon="IconChevronLeft"
              variant="text"
              @click.stop="rail = !rail"
            ></v-btn>
          </template>
        </v-list-item>
        <v-divider></v-divider>
        <v-list density="compact" nav>
          <v-list-item :prepend-icon="IconBrandProducthunt"><RouterLink to="/product" class="nav-link">Products</RouterLink></v-list-item>
          <v-list-item :prepend-icon="IconUserCog
"><RouterLink to="/customer" class="nav-link">Customer</RouterLink></v-list-item>
          <v-list-item :prepend-icon="IconLetterSSmall"><RouterLink to="/sales" class="nav-link">Sales</RouterLink></v-list-item>
          <v-list-item :prepend-icon="IconHome"><RouterLink to="/" class="nav-link">Home</RouterLink></v-list-item>

        </v-list>
     
      </v-navigation-drawer>
      <v-main style="height: 100vh">
        <RouterView />
      </v-main>
    </v-layout>
  </v-card>
</template>
<style scoped>

.nav-link {
  text-decoration: none;
  color: #000; 
}


.nav-link.router-link-active {
  color: #007bff; 
  font-weight: bold;
}
.v-card{
  overflow-wrap: normal;
}

</style>