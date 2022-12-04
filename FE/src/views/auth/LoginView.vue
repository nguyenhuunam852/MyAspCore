<template>
    <main>
        <div id="auth">
            <div class="row h-100">
                <div class="col-lg-5 col-12">
                    <div id="auth-left">
                        <div class="auth-logo">
                            <RouterLink :to="{ name: 'auth.login' }" class="fs-3 fw-bold">
                                <span class="ionicon ionicon-terminal-sharp"></span>
                                User Management
                            </RouterLink>
                        </div>
                        <h1 class="auth-title">Log in.</h1>
                        <p class="auth-subtitle mb-5">Log in with your data that you entered during registration.</p>

                        <form @submit="onSubmit">
                            <div class="form-group position-relative has-icon-left mb-3">
                                <input type="text" maxlength="100" class="form-control form-control-xl" placeholder="Username" v-model="username.value.value" :class="{'is-invalid' : username.errorMessage.value}">
                                <div class="form-control-icon">
                                    <span class="ionicon ionicon-person-circle-outline fs-3"></span>
                                </div>
                                <p class="invalid-feedback">{{ username.errorMessage.value }}</p>
                            </div>
                            <div class="form-group position-relative has-icon-left mb-3">
                                <input type="password" maxlength="128" class="form-control form-control-xl" placeholder="Password" v-model="password.value.value" :class="{'is-invalid' : password.errorMessage.value}">
                                <div class="form-control-icon">
                                    <span class="ionicon ionicon-shield-checkmark-outline fs-3"></span>
                                </div>
                                <p class="invalid-feedback">{{ password.errorMessage.value }}</p>
                            </div>
                            
                            <button type="submit" class="btn btn-primary btn-block btn-lg shadow-lg mt-4 py-sm-3 d-inline-flex align-items-center justify-content-center gap-2" :disabled="loading" :class="{'user-select-none': loading}">
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" v-show="loading"></span>
                                Log in
                            </button>
                        </form>

                        <div class="text-center mt-5 text-lg">
                            <p class="text-gray-600">Don't have an account? <RouterLink :to="{name: 'auth.register'}" class="font-bold">Register now</RouterLink>.</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-7 d-none d-lg-block">
                    <div id="auth-right" class="d-flex align-items-center justify-content-center">
                        <img src="@/assets/images/auth-img.png" alt="auth image" class="w-75">
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>

<script setup lang='ts'>
import { ref } from 'vue'
import { RouterLink, useRouter } from 'vue-router'
import { defineRule, useForm, useField } from "vee-validate";
import { alpha_num as alphaNumRule, required as requiredRule, min as minRule } from '@vee-validate/rules';
import { login } from '@/api/auth'
import md5Hash from '@/helpers/md5Hash'
import { useToast } from "vue-toastification";

const router = useRouter()
const toast = useToast()

defineRule("alpha_num", alphaNumRule);
defineRule("required", requiredRule);
defineRule("min", minRule);

const loading = ref(false)

const { handleSubmit } = useForm()
const username = useField<string>("Username", "required|alpha_num|min:5")
const password = useField<string>("Password", "required")

const onSubmit = handleSubmit(async() => {
    loading.value = true
    const data = await login({
        userName: username.value.value,
        userPassword: md5Hash(password.value.value)
    })
    
    loading.value = false
    if(data.errorCode === 0) {
        router.push({ name: "users.index" })
        toast.success("Login Success");
    } else {
        toast.error(data.errorsList[0])
    }
})

</script>