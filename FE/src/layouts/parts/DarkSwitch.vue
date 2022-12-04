<template>
    <div id="dark-switch">
        <div class="theme-toggle d-flex gap-2 align-items-center justify-content-center mb-4">
            <span class="ionicon ionicon-sunny-outline"></span>
            <div class="form-check form-switch fs-6 mb-0">
                <input class="form-check-input me-0" type="checkbox" id="toggle-dark" />
                <label class="form-check-label"></label>
            </div>
            <span class="ionicon ionicon-moon-outline"></span>
        </div>
    </div>
</template>

<script setup lang="ts">
// @ts-ignore
import DarkSwitch from '@/lib/mazer/js/components/dark.js'
import { onMounted, nextTick } from 'vue'
onMounted(() => {
    nextTick(() => {
        DarkSwitch();
    });
})
</script>
