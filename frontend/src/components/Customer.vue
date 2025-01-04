<script setup>
import { onMounted, ref ,computed} from "vue";
import axios from "../services/axios.js";
import Shop from './Shop.vue';
const customer = ref({
    id: 0,
    name: "",
    phone: "",
    email: "",
   city:""
  });
const tableItems = ref([]);
const tableHeaders = [
  // { title: "Id,", key: "id" },
  { title: "Name", key: "name" },
  { title: "Phone", key: "phone" },
  { title: "Email", key: "email" },
  { title: "City", key: "city" },
  { title: "Actions", key: "actions" }
];
const editedIndex = ref(-1);

const retrievedDetails = async () => {
    try {
      const res = await axios.get("/api/Customer/getData");
      console.log("Customers:", res.data);
      if (res.status === 200) {
        tableItems.value = res.data;
      } else {
        console.log("Non-200 response:", res.status);
    }
    editedIndex.value=-1;
    customer.value = {
        id: 0,
    name: "",
    phone: "",
    email: "",
   city:""
  };
  }
    catch (err) {
      console.log("Error : " + err);
    }
  }

  const editItemDetails=(item,index)=>{
    customer.value={...item};
    editedIndex.value=index;
  }

  const saveCustomer = async () => {
    try {
     
  if (editedIndex.value === -1) {
    await axios.post("/api/Customer/AddCustomer", customer.value);
  } else {
    await axios.put("/api/Customer/Edit", customer.value);
   
  }
      
    } catch (err) {
      console.log("Error : " + err);
    }
    await retrievedDetails();
  };

  const deleteCustomer=async(item)=>{
    try {
    const res = await axios.delete(`/api/Customer/Delete/${item.id}`);
    console.log(res.data);
  } catch (err) {
    console.log("Error : " + err);
  }
  await retrievedDetails();
  }

  onMounted(async()=>{
    await retrievedDetails();
  })
</script>

<template>
    <Shop 
    :headers="tableHeaders" 
    :items="tableItems"
    formTitle="Customer Form"
    @save="saveCustomer"
    @edit="editItemDetails"
     @delete="deleteCustomer"
     >
     <template #itemDetails>
     <v-container>
                <v-row>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="customer.name" 
                      label="Customer Name"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="customer.phone" 
                      label="Phone Number"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="customer.email"  label="Email"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="customer.city" label="City"></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </template>
</Shop>
</template>
