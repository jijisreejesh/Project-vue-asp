<script setup>
import { ref, computed, onMounted } from "vue";
import { IconTrash, IconPencil } from "@tabler/icons-vue";
const props = defineProps({
    headers:Array,
    items:Array,
    formTitle:String,
 
});
const search = ref("");
const page=ref(1);
const itemsPerPage=ref(10)
// Form state
const formRef = ref(null);
const isFormValid = ref(false);

const emit=defineEmits(["save","edit","delete","cancel"]);
const editedIndex = ref(-1);
const showDialog=ref(false);
const dialogDelete=ref(false);
const selectedItem=ref(null);
const closeDialog = async() => {
showDialog.value=false;
editedIndex.value=-1;
emit("cancel");
};

const showEditItemDialog=(item,index)=>{
    editedIndex.value=index;
    showDialog.value=true;
    emit("edit",item,index);
}

const closeDeleteDialog=()=>{
dialogDelete.value=false;
}
const showDeleteDialog=(item)=>{
dialogDelete.value=true;
selectedItem.value=item;
}
const deleteItemConfirm=()=>{
    emit("delete",selectedItem.value)
    dialogDelete.value=false;
}
const saveData=()=>{
  validateForm();
  if (isFormValid.value) {
    emit("save");
    closeDialog();
  } else {
    console.log("Form is invalid, cannot save.");
  }
}
// Form methods
const validateForm = () => {
  if (formRef.value) {
    formRef.value.validate();
  }}

</script>

<template>
  <v-text-field
      v-model="search"
      label="Search"
      prepend-inner-icon="mdi-magnify"
      variant="outlined"
      hide-details
      single-line
      class="ma-7"
    ></v-text-field>

  <v-data-table :headers="headers" :items="items" 
  v-model:page="page"
  :search="search"
  :items-per-page="itemsPerPage"
   class="ma-5">
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title class="ma-3">{{formTitle}}</v-toolbar-title>

        <v-form
        ref="formRef"
        v-model="isFormValid"
      >
        <v-dialog v-model="showDialog" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="text-h5">{{formTitle}}</span>
      </v-card-title>
     
      <v-card-text>
             <slot name="itemDetails"></slot>
            </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue-darken-1" variant="text" @click="closeDialog">Cancel</v-btn>
        <v-btn color="blue-darken-1"
     @click="saveData">Save</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  </v-form>

        <v-btn color="primary" dark @click="showDialog=true">
            New Item
          </v-btn>
      </v-toolbar>
    </template>

    <template v-slot:item.actions="{ item,index }">
      <v-icon class="ma-2" size="small" @click="showEditItemDialog(item,index)" style="color: blue">
        <IconPencil />
      </v-icon>
      <v-icon size="small" @click="showDeleteDialog(item)" style="color: red">
        <IconTrash />
      </v-icon>
    </template>
  </v-data-table>

  <!-- delete dialogbox -->
  <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue-darken-1" variant="text" @click="closeDeleteDialog">Cancel</v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="deleteItemConfirm">OK</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog> 
</template>
