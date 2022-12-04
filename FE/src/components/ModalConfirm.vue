<template>
    <div ref="refModal" class="modal fade text-left modal-borderless" id="modal-confirm" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header" v-if="heading">
                    <h5 class="modal-title">{{heading}}</h5>
                    <button type="button" class="close rounded-pill" data-bs-dismiss="modal" aria-label="Close">
                        <span class="ionicon ionicon-close-outline"></span>
                    </button>
                </div>
                <div class="modal-body">
                    <div v-if="ionicon" class="text-center my-3">
                        <span class="d-inline-block fs-1 p-3 bg-danger bg-opacity-25 rounded-pill" :class="['ionicon', `ionicon-${ionicon}`, classColor]"></span>
                    </div>
                    <h5 class="text-center" :class="classColor">{{title}}</h5>
                    <p class="text-center" :class="{'mb-0': !heading}">{{desc}}</p>
                </div>
                <div class="modal-footer" :class="`justify-content-${footerAlign}`">
                    <button type="button" class="btn btn-light-primary" data-bs-dismiss="modal">
                        Close
                    </button>
                    <button v-if="!disableSubmit" type="button" class="btn btn-primary ml-1" :class="{'pe-none': loadingSubmit}" :aria-disabled="loadingSubmit" @click="handleSubmit">
                        <span v-if="loadingSubmit" class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Accept
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const refModal = ref<HTMLElement | null>(null)
const emit = defineEmits(['submit'])

type Props = {
    heading?: string,
    title: string,
    desc: string,
    ionicon?: string,
    classColor?: string,
    footerAlign?: 'center' | 'start' | 'end'
    disableSubmit?: boolean,
    loadingSubmit?: boolean
}

withDefaults(defineProps<Props>(),{
        footerAlign: 'center',
        loadingSubmit: false
    }
)

function handleSubmit() {
    emit('submit', refModal.value)
}
</script>