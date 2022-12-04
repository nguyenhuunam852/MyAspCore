import createClient from '@/lib/axios/axios'
import type { UserGroup } from '@type'
const client = createClient()

// List User
export async function listUser(filter?: UserGroup){
    const url = '/user'
    try {
        const { data } = await client.get(url, { params: filter })
        return data
    } catch (error: any) {
        return error.response.data
    }
}

// Edit User
export async function editUser(payload: { userId: number, fullName: string, email: string } ){
    const url = '/user'
    try {
        const { data } = await client.put(url, payload)
        return data
    } catch (error: any) {
        return error.response.data
    }
}

// Delete User
export async function deleteUser(userId: number){
    const url = '/user'
    try {
        const { data } = await client.delete(url, { data: { userId: userId } })
        console.log('data', data);
        return data
    } catch (error: any) {
        return error.response.data
    }
}