import axios from 'axios';

export const API = axios.create({
  baseURL : 'https://localhost:44302',
  timeout: 20000,
})