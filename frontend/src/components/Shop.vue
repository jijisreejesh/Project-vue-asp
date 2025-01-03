<script setup>
import { ref, computed, onMounted } from "vue";
import { IconTrash, IconPencil } from "@tabler/icons-vue";
const props = defineProps({
    headers:Array,
    items:Array,
    formTitle:String,
    dialogDelete:Object
});

// const formTitle = computed(() => {
//   return editedIndex.value === -1 ? "New Item" : "Edit Item";
// });
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
const save=()=>{
    emit("save");
    closeDialog();
}

</script>

<template>
  <v-data-table :headers="headers" :items="items" class="ma-5">
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title class="ma-3">Items</v-toolbar-title>
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
        <v-btn color="blue-darken-1" variant="text" @click="save">Save</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>


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
