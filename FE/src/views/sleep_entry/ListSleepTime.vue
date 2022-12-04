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
                    </div>
                    <div class="card-content">
                        <div class="card-body">
                            <!-- Table -->
                            <div class="table-responsive mb-3 scroll-x">
                                <table class="table table-lg table-hover text-sm">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th class="text-nowrap">User</th>
                                            <th class="text-nowrap">Time1</th>
                                            <th class="text-nowrap">Time2</th>
                                            <th class="text-nowrap">Time3</th>
                                            <th class="text-nowrap">Time4</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <td><div class="text-nowrap">No data</div></td>
                                        <td><div class="text-nowrap">No data</div></td>
                                        <td><div class="text-nowrap">No data</div></td>
                                        <td><div class="text-nowrap">No data</div></td>
                                        <td><div class="text-nowrap">No data</div></td>
                                        <td><div class="text-nowrap">No data</div></td>
                                    </tbody>
                                </table>
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
// import { ref, onMounted } from "vue";
// import { listUser } from "@/api/user";
// import { useToast } from "vue-toastification"

// const toast = useToast()

// const listSleepTime = ref([] as any[])
// const loading = ref(false)

// async function getListSleepTime() {
//     loading.value = true
//     const data = await listUser()
//     loading.value = false
//     if(data.errorCode === 0) {
//         listSleepTime.value = data
//     } else {
//         toast.error(data.errorsList[0])
//     }
// }

// onMounted(async() => {
//     await getListSleepTime()
// })

</script>
