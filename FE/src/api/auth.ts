import createClient from '@/lib/axios/axios'
import type { LoginForm, RegisterForm } from "@type";
import { useAuthStore } from "@/stores/auth";

const client = createClient()

// Login
export async function login(theUser: LoginForm){
    const store = useAuthStore()
    const url = '/auth/login'
    try {
        const { data } = await client.post(url, theUser);
        
        if(data.errorCode === 0){
            // Keep token in store, vue-persist put store to localStorage if supported
            store.login(data.content.jwt)
        }
        return data
    } catch (error: any) {
        return error.response.data
    }
}

// Register
export async function register(payload: RegisterForm){
    const url = '/auth/register'
    try {
        const { data } = await client.post(url, payload)
        return data
    } catch (error: any) {
        return error.response.data
    }
}