import { ref } from "vue";
import { defineStore } from "pinia";
import axios from "../services/axios";
export const useProductStore = defineStore("product", () => {
  const ProductArray = ref([]);
  const singleProduct = ref({
    id: 0,
    name: "",
    category: "",
    description: "",
    price: 0,
    quantityInStock: 0,
  });
  // const retrieveProducts=async()=>{
  //     try{
  //   const res=await axios.get('/api/productList');
  // }
  // catch(err){
  //   console.log("Error : " + err);
  // }
  // }

  const saveProduct = async () => {
    try {
      const res = await axios.post("/api/AddProduct", singleProduct.value);
      console.log("Successfull insertion : " + res.data);
    } catch (err) {
      console.log("Error : " + err);
    }
  };
  return { ProductArray, singleProduct, saveProduct };
});
