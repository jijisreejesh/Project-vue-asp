<script setup>
import { ref } from 'vue';
import {  IconTrash, IconPencil, IconSearch,IconCaretUp, IconHeart, IconPlus } from "@tabler/icons-vue";
 const tab=ref(null);
 const items=ref([
  {value:1,icon:  IconHeart},
  {value:2,icon:  IconSearch},
  {value:3,icon:  IconTrash},
  {value:4,icon:  IconPencil}
])
 const text=ref("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat")
 const dialog=ref(false);
</script>


  
<template>
  <v-card>
    <v-tabs v-model="tab" align-tabs="title">
      <v-tab v-for="item in items" :key="item.value" :value="item.value">
       <v-icon :icon="item.icon"></v-icon>
      </v-tab>
    </v-tabs>
    <v-tabs-window v-model="tab">
      <v-tabs-window-item v-for="item in items" :key="item.value" :value="item.value">
     
        <v-card flat>
          <v-card-text>{{ text }}</v-card-text>
        </v-card>
    </v-tabs-window-item>
    </v-tabs-window>
  </v-card>

   <v-carousel show-arrows="hover">
  <v-carousel-item
    src="https://cdn.vuetifyjs.com/images/cards/docks.jpg"
    cover
  ></v-carousel-item>

  <v-carousel-item
    src="https://cdn.vuetifyjs.com/images/cards/hotel.jpg"
    cover
  ></v-carousel-item>

  <v-carousel-item
    src="https://cdn.vuetifyjs.com/images/cards/sunshine.jpg"
    cover
  ></v-carousel-item>
</v-carousel>

<div class="text-subtitle-2 mt-4 mb-2">Faqs</div>

<v-expansion-panels variant="accordion">
  <v-expansion-panel
  :collapse-icon=IconCaretUp :expand-icon=IconPlus
    v-for="i in 3"
    :key="i"
    text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
    :title="`Question${i}`"
  >
  <template #header>
    <span>{{ `Question ${i}` }}</span>
  </template>
  </v-expansion-panel>
</v-expansion-panels>

<!-- Privacy policy -->
<v-btn @click="dialog = true">
     Privacy policy
    </v-btn>


    <v-dialog
      v-model="dialog"
      width="auto"
    >
<v-sheet
    border="md"
    class="pa-6 text-white mx-auto"
    color="#141518"
    max-width="400"
  >
    <h4 class="text-h5 font-weight-bold mb-4">Your Privacy</h4>

    <p class="mb-8">
      This business determines the use of personal data collectied on our media properties and across the internet. We may collect data that you submit to us directly or data that we collect automatically including from cookies (such as device information or IP address).

      <br>
      <br>

      Please read our <a class="text-red-accent-2" href="#">Privacy Policy</a> to learn about our privacy practices or click "Your Preferences" to exercise control over your data.
    </p>

    <v-btn
      class="text-none text-black mb-4"
      color="red-accent-2"
      size="x-large"
      variant="flat"
      block
    >
      Accept
    </v-btn>

    <v-btn
      class="text-none text-black"
      color="red-accent-2"
      size="x-large"
      variant="outlined"
      block
    >
      Your Preferences
    </v-btn>
  </v-sheet>
</v-dialog>

  </template>



