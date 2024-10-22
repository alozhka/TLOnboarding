import axios, { CreateAxiosDefaults } from "axios";

const options: CreateAxiosDefaults = {
  baseURL: 'http://localhost:5219/api/v1',
  headers: {
    'Content-Type': 'application/json'
  }
}

const axiosClassic = axios.create(options)


export { axiosClassic }