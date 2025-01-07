<script setup>
import { ref, computed, onMounted } from "vue";
import { IconTrash, IconPencil, IconSearch, IconPlus } from "@tabler/icons-vue";
const props = defineProps({
  headers: Array,
  items: Array,
  formTitle: String,
  alertMessage1:String
});
const search = ref("");

const snackbar = ref(false);
const alertMessage=ref('');

const itemsPerPage = ref(10);
// Form state
const formRef = ref(null);
const isFormValid = ref(false);

const emit = defineEmits(["save", "edit", "delete", "cancel"]);
const editedIndex = ref(-1);
const showDialog = ref(false);
const dialogDelete = ref(false);
const selectedItem = ref(null);
const closeDialog = async () => {
  showDialog.value = false;
  editedIndex.value = -1;
  emit("cancel");
};

const showEditItemDialog = (item, index) => {
  editedIndex.value = index;
  showDialog.value = true;
  emit("edit", item, index);
};

const closeDeleteDialog = () => {
  dialogDelete.value = false;
};
const showDeleteDialog = (item) => {
  dialogDelete.value = true;
  selectedItem.value = item;
};
const deleteItemConfirm = () => {
  emit("delete", selectedItem.value);
  dialogDelete.value = false;
  snackbar.value=true;
  alertMessage.value=props.alertMessage1;
  console.log(alertMessage.value);
};
const saveData = () => {
  validateForm();
  if (isFormValid.value) {
    emit("save");
    closeDialog();
    alertMessage.value="Successful"
  } else {
    console.log("Something went wrong")
    alertMessage.value="Something went wrong"
  }
  snackbar.value=true;
};
// Form methods
const validateForm = () => {
  if (formRef.value) {
    formRef.value.validate();
  }
};
</script>

<template>
  <v-container class="w-100 mt-3">
    <v-data-table
      :headers="headers"
      fixed-header
      :items="items"
      :search="search"
      :items-per-page="itemsPerPage"
      class="w-100 pa-3 text-h6 elevation-24"
      style="overflow: auto; height: 750px"
    >
      <template v-slot:top>
        <v-toolbar flat class="bg-blue-lighten-4 mb-5">
          <v-toolbar-title
            class="text-uppercase text-pink-accent-3"
            :class="$vuetify.display.mdAndDown ? 'text-sm-body-1' : 'text-h8'"
            >{{ formTitle }}</v-toolbar-title
          >
          <v-spacer></v-spacer>
          <v-text-field
            v-model="search"
            density="compact"
            label="Search"
            :prepend-inner-icon="IconSearch"
            variant="outlined"
            clearable
            flat
            hide-details
            single-line
          ></v-text-field>
          <v-spacer></v-spacer>
          <v-btn
            color="primary"
            variant="outlined"
            class="mr-3"
            @click="showDialog = true"
          >
            <IconPlus />New Item
          </v-btn>
          <v-form ref="formRef" v-model="isFormValid">
            <v-dialog v-model="showDialog" max-width="500px">
              <v-card>
                <v-card-title>
                  <span class="text-h5">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <slot name="itemDetails"></slot>
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue-darken-1" variant="text" @click="closeDialog"
                    >Cancel</v-btn
                  > 
                    <v-btn color="blue-darken-1" @click="saveData">Save</v-btn>
                    
                 
              

                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-form>
        </v-toolbar>
      </template>

      <template v-slot:item.actions="{ item, index }">
        <v-icon
          class="ma-2"
          size="x-large"
          @click="showEditItemDialog(item, index)"
          style="color: blue"
        >
          <IconPencil />
        </v-icon>
        <v-icon size="x-large" @click="showDeleteDialog(item)" style="color: red">
          <IconTrash />
        </v-icon>
      </template>

      <template v-slot:item.status="{ item }">
        <v-chip
          :color="item.status == 'completed' ? 'green' : 'red'"
          :text="item.status == 'completed' ? 'completed' : 'Pending'"
          class="text-uppercase"
          label
        ></v-chip>
      </template>
    </v-data-table>
  </v-container>

  <!-- delete dialogbox -->
  <v-dialog v-model="dialogDelete" max-width="500px">
    <v-card>
      <v-card-title class="text-h5"
        >Are you sure you want to delete this item?</v-card-title
      >
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue-darken-1" variant="text" @click="closeDeleteDialog"
          >Cancel</v-btn
        >
        <v-btn color="blue-darken-1" variant="text" @click="deleteItemConfirm">OK</v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>

  <v-snackbar v-model="snackbar" timeout="5000">
                   {{ alertMessage }}
                    <template  v-slot:actions>
                      <v-btn color="pink" variant="text" @click="snackbar = false">
                        Close
                      </v-btn>
                    </template> 
                                  
                  </v-snackbar>
</template>

<style>
.v-table__wrapper thead tr:first-child {
  background-color: rgb(156, 202, 202);
  font-size: x-large;
  color: black;
}

@media screen and (max-width: 900px) {
  .v-table__wrapper thead tr:first-child {
    font-size: 15px;
  }
}
</style>
