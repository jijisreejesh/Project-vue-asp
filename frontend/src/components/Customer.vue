<script setup>
import { onMounted, ref} from "vue";
import axios from "../services/axios.js";
import Shop from "./Shop.vue";
const customer = ref({
  id: 0,
  name: "",
  phone: "",
  email: "",
  city: "",
});
const alertMsg=ref('');
const tableItems = ref([]);
const tableHeaders = [
  { title: "Name", key: "name" },
  { title: "Phone", key: "phone" },
  { title: "Email", key: "email" },
  { title: "City", key: "city" },
  { title: "Actions", key: "actions" },
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
  } catch (err) {
    console.log("Error : " + err);
  }
};

const editItemDetails = (item, index) => {
  customer.value = { ...item };
  editedIndex.value = index;
};

const saveCustomer = async () => {
  try {
    if (editedIndex.value === -1) {
      if (customer.value) await axios.post("/api/Customer/AddCustomer", customer.value);
    } else {
      await axios.put("/api/Customer/Edit", customer.value);
    }
  } catch (err) {
    console.log("Error : " + err);
  }
  await retrievedDetails();
  reset();
};

const deleteCustomer = async (item) => {
  try {
        const res = await axios.delete(`/api/Customer/Delete/${item.id}`);
        alertMsg.value="Successfully deleted"
     
  } catch (err) {
    console.log("Error : " + err);
    alertMsg.value = "Not possible to delete this customer";

  }
  await retrievedDetails();
  reset();
};
const reset = () => {
  alertMsg.value=null;
  editedIndex.value = -1;
  customer.value = {
    id: 0,
    name: "",
    phone: "",
    email: "",
    city: "",
  };
};
onMounted(async () => {
  await retrievedDetails();
});
const nameRules = [
  (v) => !!v || "Name is required",
  (v) => (v && v.length >= 3) || "Name must be at least 3 characters long",
];

const emailRules = [
  (v) => !!v || "Email is required",
  (v) => /.+@.+\..+/.test(v) || "E-mail must be valid",
];
const phoneRules= [
        v => !!v || 'Phone number is required', // Ensures the input is not empty
        v => /^(?:\(\d{3}\)\s?|\d{3}[-\s]?)\d{3}[-\s]?\d{4}$/.test(v) || 'Invalid phone number format (e.g., (123) 456-7890)',
      ]
      const cityRules= [
  (v) => !!v || "City is required",
];
</script>

<template>
  <Shop
    :headers="tableHeaders"
    :items="tableItems"
    formTitle="Customer Form"
    :alertMessage1="alertMsg"
    @save="saveCustomer"
    @edit="editItemDetails"
    @delete="deleteCustomer"
    @cancel="reset"
  >
    <template #itemDetails>
      <v-container>
        <v-row>
          <v-col cols="12" md="4" sm="6">
            <v-text-field
              v-model="customer.name"
              label="Customer Name"
              :rules="nameRules"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="4" sm="6">
            <v-text-field
              v-model="customer.phone"
              :rules="phoneRules"
              label="Phone Number"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="4" sm="6">
            <v-text-field
              v-model="customer.email"
              :rules="emailRules"
              label="Email"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="4" sm="6">
            <v-text-field
              v-model="customer.city"
              :rules="cityRules"
              label="City"
            ></v-text-field>
          </v-col>
        </v-row>
      </v-container>
    </template>
  </Shop>
</template>
