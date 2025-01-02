import axios from "axios";
const instance = axios.create({
    baseURL: 'http://localhost:5114',
  });
  export default instance;