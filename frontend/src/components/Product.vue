<script setup>
import { onMounted, ref ,computed} from "vue";
import axios from "../services/axios.js";
import Shop from './Shop.vue';


const singleProduct = ref({
    id: 0,
    name: "",
    category: "",
    description: "",
    price: 0,
    quantity_In_Stock: 0,
  });
const tableItems = ref([]);
const tableHeaders = [
  // { title: "Id,", key: "id" },
  { title: "Name", key: "name" },
  { title: "Category", key: "category" },
  { title: "Description", key: "description" },
  { title: "Price", key: "price" },
  { title: "QuantityInStock", key: "quantity_In_Stock" },
  { title: "Actions", key: "actions" }
];
const editedIndex = ref(-1);

const retrieveProducts = async () => {
    try {
      const res = await axios.get("/api/Products/productList");
      console.log("Products:", res);
      if (res.status === 200) {
        tableItems.value = res.data;
      } else {
        console.log("Non-200 response:", res.status);
    }
    editedIndex.value=-1;
    singleProduct.value = {
        id:0,
        name:"",
        category:"",
        description:"",
        price:0,
        quantity_In_Stock:0
  };
  }
    catch (err) {
      console.log("Error : " + err);
    }
  }

  const editItemDetails=(item,index)=>{
    singleProduct.value={...item};
    editedIndex.value=index;
  }

  const saveProduct = async () => {
    try {
     
  if (editedIndex.value === -1) {
    await axios.post("/api/Products/AddProduct", singleProduct.value);
  } else {
    await axios.put("/api/Products/edit", singleProduct.value);
   
  }
      
    } catch (err) {
      console.log("Error : " + err);
    }
    await retrieveProducts();
  };

  const deleteProduct=async(item)=>{
    try {
    const res = await axios.delete(`/api/Products/delete/${item.id}`);
    console.log(res.data);
  } catch (err) {
    console.log("Error : " + err);
  }
  await retrieveProducts();
  }

  onMounted(async()=>{
    await retrieveProducts();
  })
</script>

<template>
    <Shop 
    :headers="tableHeaders" 
    :items="tableItems"
    formTitle="Product Form"
    @save="saveProduct"
    @edit="editItemDetails"
     @delete="deleteProduct"
     >
     <template #itemDetails>
     <v-container>
                <v-row>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="singleProduct.name" 
                      label="Product Name"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-select
                      label="Select Category"
                      v-model="singleProduct.category"
                      :items="[
                        'Soap',
                        'Pen',
                        'Pencil',
                        'Book',
                      ]"
                    ></v-select>
                  </v-col>
                  <v-col cols="12">
                    <v-textarea
                      v-model="singleProduct.description"
                      label="Description"
                    ></v-textarea>
                    </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="singleProduct.price" type="Number" label="Price"></v-text-field>
                  </v-col>
                  <v-col cols="12" md="4" sm="6">
                    <v-text-field v-model="singleProduct.quantity_In_Stock" type="Number"  label="Quantity In Stock"></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </template>
</Shop>
</template>
