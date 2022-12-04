<script setup lang="ts">

import { RouterView } from "vue-router";
import { onMounted } from "vue";

onMounted(() => {
    document.title = import.meta.env.VITE_APP_TITLE
})

</script>

<template>
    <RouterView />
</template>
