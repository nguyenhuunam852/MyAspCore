import { ref, computed } from 'vue';
import { defineStore } from 'pinia';
import type { AuthStore } from "@type";
import { useProfileStore } from '@/stores/profile';

// Store
export const useAuthStore = defineStore('auth', () => {
        const storeProfile = useProfileStore()

        // state -> ref() or reactive()
        const authStore = ref({} as AuthStore);

        // actions -> function()
        function login(jwt: string) {
            authStore.value = {
                jwt: jwt,
                loggedIn: true
            }
        }
        function logout() {
            authStore.value = {
                jwt: '',
                loggedIn: false
            }
            storeProfile.delProfile()
        }

        // getters -> computed()
        const jwt = computed(() => authStore.value.jwt)
        const loginStatus = computed(() => authStore.value.loggedIn)

        return {
            authStore,
            jwt,
            loginStatus,
            login,
            logout
        }
    },
    {
        persist: {
            key: 'auth',
            storage: localStorage
        }
    }
)