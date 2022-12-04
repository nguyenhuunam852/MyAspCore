<template>
    <ul class="pagination pagination-primary justify-content-center">
        <li class="page-item" :class="{ 'disabled': isInFirstPage }">
            <a class="page-link" type="button" @click="onClickFirstPage">
                <span class="ionicon ionicon-arrow-back-outline" style="vertical-align:middle"></span>
            </a>
        </li>
        <li class="page-item" :class="{ 'disabled': isInFirstPage }">
            <a class="page-link" type="button" @click="onClickPreviousPage">
                <span class="ionicon ionicon-chevron-back-outline" style="vertical-align:middle"></span>
            </a>
        </li>
        <li class="page-item" v-for="page in pages" :key="page.name" :class="{ 'active': isPageActive(page.name) }
        ">
            <a class="page-link" type="button" @click="onClickPage(page.name)" :style="{ pointerEvents: page.isDisabled ? 'none' : 'auto' }">
                {{ page.name }}
            </a>
        </li>
        <li class="page-item" :class="{ 'disabled': isInLastPage }">
            <a class="page-link" type="button" @click="onClickNextPage">
                <span class="ionicon ionicon-chevron-forward-outline" style="vertical-align:middle"></span>
            </a>
        </li>
        <li class="page-item" :class="{ 'disabled': isInLastPage }">
            <a class="page-link" type="button" @click="onClickLastPage">
                <span class="ionicon ionicon-arrow-forward-outline" style="vertical-align:middle"></span>
            </a>
        </li>
    </ul>
</template>

<script setup lang="ts">
import { computed } from 'vue'

type Pagination = {
    maxVisibleButtons?: number
    totalPages: number,
    currentPage: number
}

const emit = defineEmits(['pageChanged'])

const props = withDefaults(defineProps<Pagination>(), {
    maxVisibleButtons: 3,
})

const startPage = computed<number>(() => {
    // When on the first page 
    if (props.currentPage === 1) {
        return 1;
    }
    // When on the last page 
    if (props.currentPage === props.totalPages) {
        return props.totalPages - Math.min(props.maxVisibleButtons, props.totalPages) + 1;
    }
    // When in between 
    return props.currentPage - 1;
})

const pages = computed(() => {
    const range = []
    for (
        let i = startPage.value;
        i <= Math.min(startPage.value + props.maxVisibleButtons - 1, props.totalPages);
        i += 1
    ) {
        range.push({
            name: i,
            isDisabled: i === props.currentPage,
        })
    }
    return range
})

const isInFirstPage = computed(() => props.currentPage === 1)

const isInLastPage = computed(() => props.currentPage === props.totalPages)


function onClickFirstPage() {
    emit("pageChanged", 1)
}

function onClickPreviousPage() {
    emit("pageChanged", props.currentPage - 1)
}

function onClickPage(page: number) {
   emit("pageChanged", page)
}

function onClickNextPage() {
    emit("pageChanged", props.currentPage + 1)
}

function onClickLastPage() {
    emit("pageChanged", props.totalPages)
}

function isPageActive(page: number) {
    return props.currentPage === page;
}

</script>