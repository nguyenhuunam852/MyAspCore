<template>
    <DashboardLayout>
        <main class="users">
            <div class="page-heading">
                <h3>Create Sleep Time</h3>
                <p class="text-subtitle text-muted">Create sleep time of user</p>
            </div>

            <div class="page-content">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <div class="card">
                            <div class="card-content">
                                <div class="card-body">
                                    <form @submit="onSubmit" class="form">
                                        <div class="form-body">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="form-group">
                                                        <label for="time-1">Time 1</label>
                                                        <input type="date" id="time-1" class="form-control" v-model="time_1.value.value" :class="{'is-invalid' : time_1.errorMessage.value}">
                                                        <p class="invalid-feedback">{{ time_1.errorMessage.value }}</p>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="form-group">
                                                        <label for="time-2">Time 2</label>
                                                        <input type="time" id="time-2" class="form-control" v-model="time_2.value.value" :class="{'is-invalid' : time_2.errorMessage.value}">
                                                        <p class="invalid-feedback">{{ time_2.errorMessage.value }}</p>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="form-group">
                                                        <label for="time-3">Time 3</label>
                                                        <input type="time" id="time-3" class="form-control" v-model="time_3.value.value" :class="{'is-invalid' : time_3.errorMessage.value}">
                                                        <p class="invalid-feedback">{{ time_3.errorMessage.value }}</p>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="form-group">
                                                        <label for="time-4">Time 4</label>
                                                        <input type="number" id="time-4" class="form-control" v-model="time_4.value.value" :class="{'is-invalid' : time_4.errorMessage.value}">
                                                        <p class="invalid-feedback">{{ time_4.errorMessage.value }}</p>
                                                    </div>
                                                </div>
                                                <div class="col-12 d-flex gap-3 mt-3">
                                                    <button type="submit" class="btn btn-primary me-1 mb-1" :disabled="loading" :class="{'user-select-none': loading}">
                                                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" v-show="loading"></span> Submit
                                                    </button>
                                                    <button type="reset" class="btn btn-light-secondary me-1 mb-1" @click="handleReset">
                                                        Reset
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </DashboardLayout>
</template>

<script setup lang="ts">
import DashboardLayout from "@/layouts/DashboardLayout.vue";
import { ref } from "vue";
// import { useToast } from "vue-toastification"
import { defineRule, useForm, useField } from "vee-validate";
import { required as requiredRule, integer as integerRule } from '@vee-validate/rules';

// const toast = useToast()
const loading = ref(false)

defineRule("required", requiredRule)
defineRule("integer", integerRule)
const { handleSubmit, handleReset } = useForm()

const time_1 = useField('Time 1', 'required')
const time_2 = useField('Time 2', 'required')
const time_3 = useField('Time 3', 'required')
const time_4 = useField('Time 4', 'required|integer')

const onSubmit = handleSubmit(() => {
    console.log('submit');
})

</script>
