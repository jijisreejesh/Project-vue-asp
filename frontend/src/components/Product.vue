<script setup>
import { onMounted, ref, computed } from "vue";
import axios from "../services/axios.js";
import Shop from "./Shop.vue";

const singleProduct = ref({
  id: 0,
  name:null,
  category: null,
  description: null,
  price: null,
  quantity_In_Stock: null,
});
const tableItems = ref([]);
const tableHeaders = [
  // { title: "Id,", key: "id" },
  { title: "Name", key: "name" },
  { title: "Category", key: "category" },
  { title: "Description", key: "description" },
  { title: "Price", key: "price" },
  { title: "QuantityInStock", key: "quantity_In_Stock" },
  { title: "Actions", key: "actions" },
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
  } catch (err) {
    console.log("Error : " + err);
  }
};

const editItemDetails = (item, index) => {
  singleProduct.value = { ...item };
  editedIndex.value = index;
};

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
  reset();
  await retrieveProducts();
};

const deleteProduct = async (item) => {
  try {
    const res = await axios.delete(`/api/Products/delete/${item.id}`);
    console.log(res.data);
  } catch (err) {
    console.log("Error : " + err);
  }
  reset();
  await retrieveProducts();
};
const reset = () => {
  editedIndex.value = -1;
  singleProduct.value = {
    id: 0,
    name: null,
    category: null,
    description: null,
    price: null,
    quantity_In_Stock: null,
  };
};
onMounted(async () => {
  await retrieveProducts();
});
</script>

<template>
  <Shop
    :headers="tableHeaders"
    :items="tableItems"
    formTitle="Product Form"
    @save="saveProduct"
    @edit="editItemDetails"
    @delete="deleteProduct"
    @cancel="reset"
  >
    <template #itemDetails>
      <v-container>
        <v-row>
          <v-col cols="12"  sm="6">
            <v-text-field
              v-model="singleProduct.name"
              :rules="[(v) => !!v || 'Field required']"
              label="Product Name"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-select
              label="Select Category"
              :rules="[(v) => !!v || 'Field required']"
              v-model="singleProduct.category"
              :items="['Soap', 'Pen', 'Pencil', 'Book']"
            ></v-select>
          </v-col>
          <v-col cols="12">
            <v-textarea
              v-model="singleProduct.description"
              :rules="[(v) => !!v || 'Field required']"
              label="Description"
            ></v-textarea>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="singleProduct.price"
              type="text"
              :rules="[
                (v) => !!v || 'Field required',
                (v) => !isNaN(v) || 'Please enter a valid number',
              ]"
              label="Price"
            ></v-text-field>
          </v-col>
          <v-col cols="12"  sm="6">
            <v-text-field
              v-model="singleProduct.quantity_In_Stock"
              type="text"
              :rules="[
                (v) => !!v || 'Field required',
                (v) => !isNaN(v) || 'Please enter a valid number',
              ]"
              label="Quantity In Stock"
            ></v-text-field>
          </v-col>
        </v-row>
      </v-container>
    </template>
  </Shop>
</template>
