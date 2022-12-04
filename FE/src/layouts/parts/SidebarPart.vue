<template>
    <div id="sidebar" class="active">
        <div class="sidebar-wrapper active">
            <div class="sidebar-header position-relative">
                <div class="d-flex justify-content-between align-items-center">
                    <div class="logo">
                        <router-link :to="{ name: 'users.index' }" class="fs-5">
                            <span class="ionicon ionicon-terminal-sharp"></span>
                            USER MANAGEMENT
                        </router-link>
                    </div>
                    <div class="sidebar-toggler x">
                        <a href="#" class="sidebar-hide d-xl-none d-block fs-4">
                            <span class="ionicon ionicon-chevron-back-outline"></span>
                        </a>
                    </div>
                </div>
            </div>

            <div class="sidebar-profile">
                <img src="@/assets/images/avatar.png" alt="avatar default" class="profile-avt">
                <div class="profile-info">
                    <h5 class="name">Administrator</h5>
                    <p class="email">@{{ storeProfile.profileStore ? storeProfile.profileStore.userName : 'Username' }}</p>
                </div>
            </div>

            <div class="sidebar-menu">
                <ul class="menu">
                    <li v-for="(item, i) in menu" :key="i" class="sidebar-item"
                        :class="{ 'has-sub': item.hasSub, 'active': activeAt(item.activeAt) }">
                        <a v-if="item.hasSub" type="button" class="sidebar-link">
                            <span :class="item.icon"></span>
                            <span>{{ item.label }}</span>
                        </a>
                        <RouterLink v-else :to="item.route" class="sidebar-link">
                            <span :class="item.icon"></span>
                            <span>{{ item.label }}</span>
                        </RouterLink>
                        <ul v-if="item.children && item.children.length > 0" class="submenu" :class="{ 'active': activeAt(item.activeAt) }">
                            <li v-for="(child, i) in item.children" :key="i" class="submenu-item" :class="{ 'active': activeAt(child.activeAt) }">
                                <RouterLink :to="child.route">{{ child.label }}</RouterLink>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>

            <div class="sidebar-footer">
                <div class="mt-5 mb-3">
                    <DarkSwitch></DarkSwitch>
                    <a type="button" data-bs-toggle="modal" data-bs-target="#modal-confirm" class="btn btn-secondary d-flex align-items-center justify-content-center py-2"
                        style="border-radius: 0.5rem">
                        <span class="ionicon ionicon-log-out-outline"></span>
                        <span class="ms-2">Logout</span>
                    </a>
                </div>
            </div>
        </div>
        <ModalConfirm
            title="Logout"
            desc="Are you sure that you want to sign out?"
            class-color="text-danger"
            ionicon="alert-outline"
            @submit="handleLogout"
        />
    </div>
</template>

<script setup lang="ts">
// @ts-ignore
import { createSidebar } from "@/lib/mazer/js/components/sidebar.js"
import DarkSwitch from "./DarkSwitch.vue"
import { onMounted, nextTick } from "vue"
import { useRoute, useRouter, RouterLink } from "vue-router"
import { menu } from "./Menu"
import type { RouteName } from "@type"
import ModalConfirm from "@/components/ModalConfirm.vue"
import { useToast } from "vue-toastification"
import bootstrap from "@/lib/bootstrap/bootstrap"
import { useAuthStore } from "@/stores/auth";
import { useProfileStore } from "@/stores/profile";

const storeAuth = useAuthStore()
const storeProfile = useProfileStore()
const route = useRoute()
const router = useRouter()
const toast = useToast()


function activeAt(list_namepath: Array<RouteName>): boolean {
    return list_namepath.includes(route.name)
}

function handleLogout(elModal: HTMLElement) {
    storeAuth.logout()
    bootstrap.Modal.getInstance(elModal)?.hide()
    router.push({ name: 'auth.login' })
    toast.success('Logout Success')    
}

onMounted(async() => {
    nextTick(() => {
        createSidebar()
    })
})

</script>