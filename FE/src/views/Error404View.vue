<template>
    <div id="error">
        <div class="error-page container">
            <div class="col-md-8 col-12 offset-md-2">
                <div class="text-center">
                    <img class="img-error" src="@/assets/images/404.svg" alt="Not Found">
                    <h1 class="error-title">NOT FOUND</h1>
                    <p class='text-gray-600'>The page you are looking not found.</p>
                    <RouterLink :to="{ name: 'users.index' }" class="btn btn-outline-primary mt-3 my-5">Go Home</RouterLink>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router';
</script>