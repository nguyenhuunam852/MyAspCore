<template>
    <DashboardLayout>
        <main class="users">
            <div class="page-heading">
                <h3>Users</h3>
                <p class="text-subtitle text-muted">Show all list users.</p>
            </div>

            <div class="page-content">
                <div class="card">
                    <div class="card-header pb-0">
                        <div class="d-flex align-items-end flex-wrap gap-3">
                            <div class="form-group text-sm mb-0 filter-item">
                                <label>Search</label>
                                <input type="text" v-model="filter.search" class="form-control text-sm" placeholder="Search ..." :disabled="loading">
                            </div>
                            <!-- <div class="form-group text-sm mb-0 filter-item">
                                <label class="mb-1">Order By</label>
                                <select class="form-select text-sm" v-model="filter.orderBy" :disabled="loading">
                                    <option v-for="item in listOrderBy" :key="item.name" :value="item.value">{{ item.name }}</option>
                                </select>
                            </div>
                            <div class="form-group text-sm mb-0 filter-item">
                                <label class="mb-1">Sort By</label>
                                <select class="form-select text-sm" v-model="filter.sortBy" :disabled="loading">
                                    <option v-for="item in listSortBy" :key="item.name" :value="item.value">{{ item.name }}</option>
                                </select>
                            </div> -->
                            
                            <!-- Clear filter -->
                            <div class="flex-shrink-0 justify-self-end">
                                <button @click="resetFilter" class="btn btn-outline-primary rounded-pill text-sm" :disabled="loading">
                                    <span class="ionicon ionicon-refresh"></span> Reset filter
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="card-content">
                        <div class="card-body">
                            <!-- Table -->
                            <div class="table-responsive mb-3 scroll-x">
                                <table class="table table-lg table-hover text-sm">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th class="text-nowrap">Full Name</th>
                                            <th class="text-nowrap">Username</th>
                                            <th class="text-nowrap">Email</th>
                                            <th class="text-nowrap">Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="user in users" :key="user.userId">
                                            <td><div class="text-nowrap">{{user.userId}}</div></td>
                                            <td><div class="text-nowrap">{{user.fullName ? user.fullName : '---'}}</div></td>
                                            <td><div class="text-nowrap">{{user.userName ? user.userName : '---'}}</div></td>
                                            <td><div class="text-nowrap">{{user.email ? user.email : '---'}}</div></td>
                                            <td>
                                                <div class="d-flex gap-3">
                                                    <a type="button" class="fs-5" data-bs-toggle="modal" data-bs-target="#modal-edit-user" @click="getCurrentUser(user)">
                                                        <span class="ionicon ionicon-create-outline" data-bs-toggle="tooltip" data-bs-title="Edit"></span>
                                                    </a>
                                                    <a type="button" class="fs-5" data-bs-toggle="modal" data-bs-target="#modal-delete-user" @click="getCurrentUser(user)">
                                                        <span class="ionicon ionicon-close-circle-outline" data-bs-toggle="tooltip" data-bs-title="Delete"></span>
                                                    </a>
                                                </div>
                                            </td>
                                        </tr>
                                        <!-- loading -->
                                        <tr v-show="loading">
                                            <td colspan="100%" class="table-no-hover w-100 text-center shadow-none" style="height: 300px">
                                                <div class="spinner-border mx-auto" style="width: 3rem; height: 3rem" role="status"></div>
                                                <p class="mb-0 mt-2">Wait a few seconds</p>
                                            </td>
                                        </tr>
                                        <!-- No user -->
                                        <tr v-if="users.length == 0 && !loading">
                                            <td colspan="100%" class="table-no-hover w-100 text-center shadow-none" style="height: 300px">
                                                <span class="ionicon ionicon-file-tray-outline fs-1"></span>
                                                <p class="mb-0 mt-2">No User</p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                            <!-- Pagination -->
                            <!-- <PaginationView
                                v-if="users.length > 0"
                                :totalPages="pagination.totalPage"
                                :currentPage="pagination.page"
                                @pageChanged="onPageChanged"
                            /> -->
                        </div>
                    </div>
                </div>
            </div>
        </main>

        <!-- Modal Edit User -->
        <ModalEditUser :data="currentUser" @reset="reset" @reload="getListUser"/>

        <!-- Modal Delete User -->
        <ModalDeleteUser :data="currentUser" @reload="getListUser"/>
    </DashboardLayout>
</template>

<script setup lang="ts">
import DashboardLayout from "@/layouts/DashboardLayout.vue";
import ModalEditUser from "./ModalEditUser.vue";
import ModalDeleteUser from "./ModalDeleteUser.vue";
import { ref, reactive, onMounted, watch } from "vue";
import { listUser } from "@/api/user";
import { useToast } from "vue-toastification"
import type { User } from "@type"
import { useRouter, useRoute } from 'vue-router'
import { useProfileStore } from "@/stores/profile";
import bootstrap from '@/lib/bootstrap/bootstrap';

const toast = useToast()
const router = useRouter()
const route = useRoute()
const storeProfile = useProfileStore()
const keyComponent = ref(0)

const users = ref([] as User[])
const loading = ref(false)
// const listOrderBy = ref([
//     { name: 'ASC', value: null},
//     { name: 'DESC', value: true},
// ])
// const listSortBy = ref([
//     { name: 'Username', value: 'username'},
//     { name: 'Full name', value: 'fullname'},
//     { name: 'Email', value: 'email'}
// ])
const debounce = ref(0 as ReturnType<typeof setTimeout> | number )
const filter = reactive({
    orderBy: null,
    search: null,
    sortBy: 'username'
})
const pagination = reactive({
    page: 1,
    totalPage: 0,
})
const userName = ref(null as null | string)
const currentUser = ref(null as any)

watch(() => filter.orderBy, () => handleDebounceFilterChanged())
watch(() => filter.search, () => handleDebounceFilterChanged())
watch(() => filter.sortBy, () => handleDebounceFilterChanged())

function handleDebounceFilterChanged() {
    clearTimeout(debounce.value)
    debounce.value = setTimeout(() => onPageChanged(1), 800)
}

async function getListUser() {
    const params = {
        DESC: filter.orderBy,
        Page: pagination.page,
        Filter: filter.search,
        SortBy: filter.sortBy
    }
    users.value = []
    loading.value = true
    const data = await listUser(params)
    loading.value = false
    if(data.errorCode === 0) {
        users.value = data.content.userInfoDtos
        userName.value = data.content.userName
        pagination.totalPage = data.content.pages
    } else {
        toast.error(data.errorsList[0])
    }
}

function resetFilter () {
    filter.orderBy = null,
    filter.search = null,
    filter.sortBy = 'username'
}

async function onPageChanged(page: number) {
    pagination.page = page
    router.push({ 
        path: route.path,
        query: {
            page: page
        }
    })
    await getListUser()
}

async function getCurrentUser(data: any) {
    currentUser.value = data
    keyComponent.value += 1
}

function reset(){
    currentUser.value = null
}

onMounted(async() => {
    if(route.query.page) pagination.page = +route.query.page
    await getListUser()

    if(!storeProfile.profileStore) {
        storeProfile.setProfile({
            userName: userName.value
        })
    }

    new bootstrap.Tooltip(document.body, {
        selector: "[data-bs-toggle='tooltip']",
        trigger: "hover",
        customClass: 'custom-tooltip'
    })
})

</script>
