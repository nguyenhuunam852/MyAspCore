import axios from 'axios';
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, AxiosError } from 'axios';
import { useAuthStore } from "@/stores/auth";

function requestInterceptor(configs: AxiosRequestConfig): AxiosRequestConfig {
    // Configure axios before sending request
    
    // BaseURL
    if (import.meta.env.VITE_API_HOST) {
        configs.baseURL = import.meta.env.VITE_API_HOST
    }
    if (import.meta.env.VITE_API_PREFIX != '') {
        configs.baseURL += '/' + import.meta.env.VITE_API_PREFIX
    }

    // Headers
    configs.headers = configs.headers ?? {}
    configs.headers["Access-Control-Allow-Origin"] = "*"
    
    const store = useAuthStore()
    const token = store.jwt
    if (token) {
        configs.headers["Authorization"] = `Bearer ${token}`;
    }

    return configs
}

function responseOk(response: AxiosResponse): AxiosResponse {
    // Do something before return data to caller
    return response
}

function responseErr(error: AxiosError): Promise<AxiosError> {
    // Do something before return data to caller
    return Promise.reject(error);
}

function createClient(config: AxiosRequestConfig | Object = {}): AxiosInstance {
    const client = axios.create(config);

    client.interceptors.request.use(requestInterceptor);
    client.interceptors.response.use(responseOk, responseErr)

    return client
}

export default createClient;