<template>
    <div class="modal fade text-left modal-borderless" id="modal-delete-user" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content" v-if="props.data">
                <div class="modal-body">
                    <div class="text-center my-3">
                        <span class="d-inline-block fs-1 p-3 bg-danger bg-opacity-25 rounded-pill ionicon ionicon-alert-outline text-danger"></span>
                    </div>
                    <h5 class="text-center text-danger">Delete User</h5>
                    <p class="text-center mb-0 mx-3">Are you sure that you want to delete user {{props.data.userName}} ?</p>
                </div>
                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn btn-light-primary" data-bs-dismiss="modal">
                        Close
                    </button>
                    <button type="button" class="btn btn-danger ml-1" :class="{'pe-none': loading}" :aria-disabled="loading" @click="handleDelete">
                        <span v-if="loading" class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Delete
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import { deleteUser } from '@/api/user';
import bootstrap from '@/lib/bootstrap/bootstrap';
import { useToast } from 'vue-toastification';

const toast = useToast()
type Props = {
    data: any
}
const props = defineProps<Props>()
const loading = ref(false)
const emit = defineEmits(['reload'])
const user_id = computed(() => props.data?.userId)

async function handleDelete () {
    loading.value = true
    const data = await deleteUser(user_id.value)
    loading.value = false
    
    if(data.errorCode === 0) {
        toast.success('User Deleted')
        const ModalEl = document.querySelector('#modal-delete-user')
        if(ModalEl) bootstrap.Modal.getInstance(ModalEl)?.hide()
        emit('reload')
    } else {
        toast.error(data.errorsList[0])
    }
}

</script>