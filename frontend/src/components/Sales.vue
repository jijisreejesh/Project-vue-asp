<script setup>
import { onMounted, ref ,computed} from "vue";
import axios from "../services/axios.js";
import Shop from './Shop.vue';
const sales = ref({
    id: 0,
    customer_id: "",
    product_id: "",
    quantity: "",
    total_price:"",
    sales_date:"",
    payment_method:"",
    status:""
  });
const tableItems = ref([]);
const tableHeaders = [
  { title: "Id,", key: "id" },
  { title: "Customer_Id", key: "Customer_id" },
  { title: "Product_Id", key: "product_id" },
  { title: "Quantity", key: "quantity" },
  { title: "Total_Price", key: "total_price" },
  { title: "Sales_Date", key: "sales_date" },
  { title: "Payment_Method", key: "payment_method"},
  { title: "status", key:"status"},
  { title: "Actions", key: "actions" }
];
const editedIndex = ref(-1);

const retrievedDetails = async () => {
    try {
      const res = await axios.get("/api/Sales/GetSales");
      if (res.status === 200) {
        tableItems.value = res.data;
      } else {
        console.log("Non-200 response:", res.status);
    }
    editedIndex.value=-1;
    sales.value = {
        id: 0,
    customer_id: "",
    product_id: "",
    quantity: "",
    total_price:"",
    sales_date:"",
    payment_method:"",
    status:""
  };
  }
    catch (err) {
      console.log("Error : " + err);
    }
  }

  const editItemDetails=(item,index)=>{
   sales.value={...item};
    editedIndex.value=index;
  }

  const saveSales = async () => {
    try {
     
  if (editedIndex.value === -1) {
    await axios.post("/api/Sales/AddSales", sales.value);
  } else {
    await axios.put("/api/Sales/Edit", sales.value);
   
  }
      
    } catch (err) {
      console.log("Error : " + err);
    }
    await retrievedDetails();
  };

  const deleteSales=async(item)=>{
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
    @save="saveSales"
    @edit="editItemDetails"
     @delete="deleteSales"
     >
     <template #itemDetails>
     <v-container>
                <!-- <v-row>
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
                </v-row> -->
              </v-container>
            </template>
</Shop>
</template>
