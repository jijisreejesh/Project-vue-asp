<script setup>
import { onMounted, ref } from "vue";
import axios from "../services/axios.js";
import Shop from "./Shop.vue";
import dayjs from "dayjs";
const sales = ref({
  id: 0,
  customer_Id: null,
  product_Id: null,
  quantity: null,
  total_Price: null,
  sales_Date: null,
  payment_Method: null,
  status: null,
  customerName: null,
  productName: null,
});
const customerIdAndName = ref([]);
const productIdAndName = ref([]);
const tableItems = ref([]);
const tableHeaders = [
  { title: "Customer Name", key: "customerName" ,cellProps:{style:"color:green"} },
  { title: "Product Name", key: "productName" },
  { title: "Quantity", key: "quantity",  cellProps: { style: "color:orange; text-align:center" }},
  { title: "Total Price", key: "total_Price" ,cellProps: { style: "color:orange; text-align:center;" }},
  {
    title: "Sales Date",
    key: "sales_Date",
    value: ({ sales_Date }) => {
      return dayjs(sales_Date).format("DD-MM-YYYY");
    },
  },
  { title: "Payment Method", key: "payment_Method" },
  { title: "status", key: "status" },
  { title: "Actions", key: "actions" },
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
    const customerDetails = await axios.get("api/Customer/getCustomerList");
    if (customerDetails.status === 200) {
      customerIdAndName.value = customerDetails.data;
    } else {
      console.log("Non-200 response:", customerDetails.status);
    }
    const productDetails = await axios.get("api/Products/getProductList");
    if (productDetails.status === 200) {
      productIdAndName.value = productDetails.data;
    } else {
      console.log("Non-200 response:", productDetails.status);
    }
    reset();
  } catch (err) {
    console.log("Error : " + err);
  }
};
const reset = () => {
  editedIndex.value = -1;
  sales.value = {
    id: 0,
  customer_Id: null,
  product_Id: null,
  quantity: null,
  total_Price: null,
  sales_Date: null,
  payment_Method: null,
  status: null,
  customerName: null,
  productName: null,
  };
};
const editItemDetails = (item, index) => {
  sales.value = { ...item };
  sales.value.sales_Date = dayjs(sales.value.sales_Date).format("YYYY-MM-DD");
  editedIndex.value = index;
};

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

const deleteSales = async (item) => {
  try {
    const res = await axios.delete(`/api/Sales/Delete/${item.id}`);
    console.log(res.data);
  } catch (err) {
    console.log("Error : " + err);
  }
  await retrievedDetails();
};

onMounted(async () => {
  await retrievedDetails();
});
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
              :rules="[(v) => !!v || 'Field required']"
            ></v-autocomplete>
          </v-col>
          <v-col cols="12" md="4" sm="6">
            <v-autocomplete
              label="Select Product"
              v-model="sales.product_Id"
              :items="productIdAndName"
              item-title="label"
              item-value="id"
              :rules="[(v) => !!v || 'Field required']"
            ></v-autocomplete>
          </v-col>
          <v-col cols="12" md="4" sm="6">
            <v-text-field
              v-model="sales.quantity"
              type="text"
              :rules="[
                (v) => !!v || 'Field required',
                (v) => !isNaN(v) || 'Please enter a valid number',
              ]"
              label="Quantity"
            >
            </v-text-field>
          </v-col>
          <v-col cols="12" md="4" sm="6">
            <v-text-field
              v-model="sales.total_Price"
              type="text"
              :rules="[
                (v) => !!v || 'Field required',
                (v) => !isNaN(v) || 'Please enter a valid number',
              ]"
              label="TotalPrice"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="sales.sales_Date"
              type="date"
              :rules="[(v) => !!v || 'Field required']"
              label="Sales_Date"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="4" sm="6">
            <v-select
              label=" PaymentMethod"
              v-model="sales.payment_Method"
              :items="['Card', 'Cash']"
              :rules="[(v) => !!v || 'Field required']"
            ></v-select>
          </v-col>
          <v-col cols="12" md="4" sm="6">
            <v-select
              v-model="sales.status"
              class="text-center"
                :items="['completed', 'pending']"
              :rules="[(v) => !!v || 'Field required']"
              label="Status"
            ></v-select>
          </v-col>
        </v-row>
      </v-container>
    </template>
  </Shop>
</template>
<style scoped>

</style>