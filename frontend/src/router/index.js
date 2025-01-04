import { createRouter, createWebHistory } from "vue-router";

import Sales from '../components/Sales.vue';
import Product from '../components/Product.vue';
import Customer from '../components/Customer.vue';
const routes = [
  {
    path: "/product",
    name: "product",
    component: Product,
  },
  {
    path: "/customer",
    name: "customer",
    component: Customer,
  },
  {
    path: "/sales",
    name: "sales",
    component: Sales,
  },
  
];

const router = createRouter({ history: createWebHistory(), routes });

export default router;
