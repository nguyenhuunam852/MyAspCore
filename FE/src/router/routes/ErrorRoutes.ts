import type { RouteRecordRaw } from "vue-router";
import Error404View from "@/views/Error404View.vue";

const ErrorRoutes: Array<RouteRecordRaw> = [
    {
        path: '/:pathMatch(.*)*',
        name: 'error',
        component: Error404View,
    },
];

export default ErrorRoutes;