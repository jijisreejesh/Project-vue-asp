<script setup>
import { onMounted, ref} from "vue";
import axios from "../services/axios.js";
import Shop from './Shop.vue';
import dayjs from "dayjs";
const sales = ref({
    id: 0,
    customer_Id: 0,
    product_Id: 0,
    quantity: 0,
    total_Price:0,
    sales_Date:"",
    payment_Method:"",
    status:"",
    customerName:"",
    productName:""
  });
  const customerIdAndName=ref([]);
  const productIdAndName=ref([]);
const tableItems = ref([]);
const tableHeaders = [
  { title: "Customer_Name", key: "customerName" },
  { title: "Product_Name", key: "productName" },
  { title: "Quantity", key: "quantity" },
  { title: "Total_Price", key: "total_Price" },
  { title: "Sales_Date", key: "sales_Date" ,value:({sales_Date})=>{ return dayjs(sales_Date).format('DD-MM-YYYY')}},
  { title: "Payment_Method", key: "payment_Method"},
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
      const customerDetails=await axios.get("api/Customer/getCustomerList");  
      if (customerDetails.status === 200) {
        customerIdAndName.value = customerDetails.data;
      } else {
        console.log("Non-200 response:", customerDetails.status);
    }
    const productDetails=await axios.get("api/Products/getProductList");  
      if (productDetails.status === 200) {
        productIdAndName.value = productDetails.data;
      } else {
        console.log("Non-200 response:", productDetails.status);
    }
   reset();
  }
    catch (err) {
      console.log("Error : " + err);
    }
  }
const reset=()=>{
  editedIndex.value=-1;
    sales.value = {
      id: 0,
    customer_Id: 0,
    product_Id: 0,
    quantity: 0,
    total_Price:0,
    sales_Date:"",
    payment_Method:"",
    status:"",
    customerName:"",
    productName:""
  };
}
  const editItemDetails=(item,index)=>{
   sales.value={...item};
   sales.value.sales_Date = dayjs(sales.value.sales_Date).format("YYYY-MM-DD")
    editedIndex.value=index;
  }

  const saveSales = async () => {
    try {
    //  sales.value.sales_Date = new Date(sales.value.sales_Date); 
   
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
    const res = await axios.delete(`/api/Sales/Delete/${item.id}`);
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
    formTitle="Sales Form"
    @save="saveSales"
    @edit="editItemDetails"
     @delete="deleteSales"
     @cancel="reset"
     >
     <template #itemDetails>
     <v-container>
      <v-row>
        <v-col cols="12" md="4" sm="6">
          <v-autocomplete
            label="Select Customer"
           v-model="sales.customer_Id"
           :items="customerIdAndName" 
           item-title="label"
           item-value="id"
            :rules="[v => !!v || 'Item is required']"
          ></v-autocomplete>
      </v-col>
      <v-col cols="12" md="4" sm="6">
          <v-autocomplete
            label="Select Product"
           v-model="sales.product_Id"
           :items="productIdAndName" 
           item-title="label"
           item-value="id"
            :rules="[v => !!v || 'Item is required']"
          ></v-autocomplete>
      </v-col>
      <v-col cols="12" md="4" sm="6">
      <v-text-field v-model="sales.quantity" type="Number"
       :rules="[v => !!v || 'Item is required']"
          label="Quantity"></v-text-field>
         </v-col>
         <v-col cols="12" md="4" sm="6">
      <v-text-field v-model="sales.total_Price" type="Number"
       :rules="[v => !!v || 'Item is required']"
          label="TotalPrice"></v-text-field>
         </v-col>
      <v-col cols="12"  sm="6">
      <v-text-field v-model="sales.sales_Date" type="date" 
       :rules="[v => !!v || 'Item is required']"
          label="Sales_Date" ></v-text-field>
         </v-col> 
         <v-col cols="12" md="4" sm="6">
                    <v-select
                      label="Select PaymentMethod"
                      v-model="sales.payment_Method"
                      :items="[
                        'Card',
                        'Cash',
                      ]"
                       :rules="[v => !!v || 'Item is required']"
                    ></v-select>
                    </v-col>
                    <v-col cols="12" md="4" sm="6">
                      <v-text-field v-model="sales.status"
                       :rules="[v => !!v || 'Item is required']"
                      label="Status"></v-text-field>
                      </v-col>
      </v-row>
              </v-container>
            </template>
</Shop>
</template>
