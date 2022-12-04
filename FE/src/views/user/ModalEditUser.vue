<template>
    <div class="modal fade text-left modal-borderless" id="modal-edit-user" tabindex="-1" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content" v-if="data">
                <div class="modal-header pb-0">
                    <h5 class="modal-title">Edit user: {{ data.userName }}</h5>
                    <button type="button" class="close rounded-pill" data-bs-dismiss="modal" aria-label="Close" @click="resetForm()">
                        <span class="ionicon ionicon-close-outline"></span>
                    </button>
                </div>
                <div class="modal-body">
                    <form @submit="onConfig" class="p-3">
                        <!-- User ID -->
                        <div class="form-group">
                            <label class="text-sm mb-1">UserId</label>
                            <div class="form-control">{{ data.userId }}</div>
                        </div>
                        <!-- Username -->
                        <div class="form-group">
                            <label class="text-sm mb-1" for="input_full_name">Full Name</label>
                            <input type="text" class="form-control" id="input_full_name" v-model="fullName.value.value" :class="{ 'is-invalid': fullName.errorMessage.value }" placeholder="Enter Full name">
                            <div class="invalid-feedback">{{ fullName.errorMessage.value }}</div>
                        </div>
                        <!-- Email -->
                        <div class="form-group">
                            <label class="text-sm mb-1" for="input_email">Email</label>
                            <input type="text" class="form-control" id="input_email" v-model="email.value.value" :class="{ 'is-invalid': email.errorMessage.value }" placeholder="Enter email">
                            <div class="invalid-feedback">{{ email.errorMessage.value }}</div>
                        </div>
                        <div class="d-flex justify-content-center gap-2 mt-4">
                            <button type="button" class="btn btn-light-primary" data-bs-dismiss="modal" @click="resetForm()">
                                Close
                            </button>
                            <button type="submit" class="btn btn-primary" :disabled="loading" :class="{'user-select-none': loading}">
                                <span v-if="loading" class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>Save
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
import { defineRule, useField, useForm } from 'vee-validate';
import { required as requiredRule, email as emailRule } from '@vee-validate/rules'
import { useToast } from 'vue-toastification';
import bootstrap from '@/lib/bootstrap/bootstrap';
import type { User } from '@type'
import { editUser } from '@/api/user'

const toast = useToast()
type Props = {
    data: User
}
const props = defineProps<Props>()
const emit = defineEmits(['reset', 'reload'])
const loading = ref(false)

defineRule('required', requiredRule)
defineRule('email', emailRule)

const { handleSubmit } = useForm()

const fullName = useField<string>('Full name', 'required')
const email = useField<string>('Email', 'required|email')

watch(
    ()=>props.data,
    (newValue) => {
        fullName.value.value = newValue?.fullName
        email.value.value = newValue?.email
    }
)

const onConfig = handleSubmit(async() => {
    const payload = {
        userId: props.data.userId,
        email: email.value.value,
        fullName: fullName.value.value
    }
    loading.value = true
    const data = await editUser(payload)
    loading.value = false
    if(data.errorCode === 0) {
        toast.success("Edit user success");
        emit('reload')
        // reset Form
        resetForm()
        // Close modal
        const ModalEl = document.querySelector('#modal-edit-user')
        if(ModalEl) bootstrap.Modal.getInstance(ModalEl)?.hide()
    } else {
        toast.error(data.errorsList[0])
    }
})

function resetForm() {
    emit('reset')
}

</script>