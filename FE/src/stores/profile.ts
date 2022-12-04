import { ref } from 'vue';
import { defineStore } from 'pinia';

type ProfileStore = {
    userName: string | null
}

// Store
export const useProfileStore = defineStore('profile', () => {
        // state -> ref() or reactive()
        const profileStore = ref(null as ProfileStore | null);

        // actions -> function()
        function setProfile(data: ProfileStore) {
            profileStore.value = {
                userName: data.userName
            }
        }
        function delProfile() {
            profileStore.value = null
        }

        return {
            profileStore,
            setProfile,
            delProfile
        }
    }
)