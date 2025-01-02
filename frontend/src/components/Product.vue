<script setup>
import { ref, computed, onMounted } from "vue";
import { IconTrash, IconPencil } from "@tabler/icons-vue";
import { useProductStore } from '../stores/product';
import { storeToRefs } from "pinia";
const store = useProductStore();
const { productArray,singleProduct} = storeToRefs(store);
const {saveProduct,retrieveProducts}=store;
const dialog = ref(false);
const dialogDelete = ref(false);
const editedIndex = ref(-1);
const formTitle = computed(() => {
  return editedIndex.value === -1 ? "New Item" : "Edit Item";
});

const closeProduct = () => {
  dialog.value = false;
  editedIndex.value = -1;
};
const closeDeleteProductDialog = () => {
  dialogDelete.value = false;
  editedIndex.value = -1;
};

const showProductDialogForDelete = (item) => {
  dialogDelete.value = true;
  //editedIndex.value = defaultItem.value.composition.indexOf(item);
};
const save = () => {

  // if (editedIndex.value === -1) {
  //  productArray.value.push(singleProduct.value);
  // } else {
  //   productArray.value[editedIndex.value] = singleProduct.value;
  // }
  saveProduct();
  dialog.value = false;
  singleProduct.value = {
        id:0,
        name:"",
        category:"",
        description:"",
        price:0,
        quantityInStock:0
  };
  editedIndex.value = -1;

};

const deleteProductItem = () => {
  //deleteCompositionConfirm(editedIndex.value);
  editedIndex.value = -1;
  dialogDelete.value = false;
  // composition.value = {
  //   material: "",
  //   count: 0,
  //   weight: 0,
  //   price: 0,
  //   purity: "",
  // };
};
const editProduct = (item) => {
  // composition.value = { ...item };
  //editedIndex.value = defaultItem.value.composition.indexOf(item);
  dialog.value = true;
};


const headers = [
  { title: "Id,", key: "id" },
  { title: "Name", key: "name" },
  { title: "Category", key: "category" },
  { title: "Description", key: "description" },
  { title: "Price", key: "price" },
  { title: "QuantityInStock", key: "quantityInStock" },
];
onMounted(()=>{
retrieveProducts();
});
</script>

<template>
  <v-data-table :headers="headers" :items="productArray" class="ma-5">
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title class="ma-3">PRODUCTS</v-toolbar-title>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ props }">
            <v-btn class="mb-2" color="primary" dark v-bind="props">
              New Item
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ formTitle }}</span>
            </v-card-title>

            <v-card-text>
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
                    <v-text-field v-model="singleProduct.quantityInStock" type="Number"  label="Quantity In Stock"></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue-darken-1" variant="text" @click="closeProduct">
                Cancel
              </v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="save">
                Save
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>

        <!-- Dialog box for delete -->
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue-darken-1" variant="text" @click="closeDeleteProductDialog">Cancel</v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="deleteProductItem">OK</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>

    <template v-slot:item.actions="{ item }">
      <v-icon class="ma-2" size="small" @click="editProduct(item)" style="color: blue">
        <IconPencil />
      </v-icon>
      <v-icon size="small" @click="showProductDialogForDelete(item)" style="color: red">
        <IconTrash />
      </v-icon>
    </template>
  </v-data-table>
</template>
