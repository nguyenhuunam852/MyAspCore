<template>
    <div id="dashboard">
        <SidebarPart></SidebarPart>
        <div id="main">
            <header class="mb-3">
                <a type="button" class="burger-btn d-inline-block d-xl-none fs-3">
                    <span class="ionicon ionicon-reorder-four-outline"></span>
                </a>
            </header>
            <slot></slot>

            <footer>
                <div class="footer clearfix mb-0 text-muted">
                    <div class="float-start"><p>2022 © User Management</p></div>
                    <div class="float-end">
                        <p>Build by <a href="https://eplus.vn/">Evan Nguyen</a></p>
                    </div>
                </div>
            </footer>
        </div>
    </div>
</template>

<script setup lang="ts">
import SidebarPart from "@/layouts/parts/SidebarPart.vue"
import { onBeforeUnmount } from 'vue'
// @ts-ignore
import { removeSidebar } from "@/lib/mazer/js/components/sidebar.js"

onBeforeUnmount(() => {
    removeSidebar()
})
</script>
