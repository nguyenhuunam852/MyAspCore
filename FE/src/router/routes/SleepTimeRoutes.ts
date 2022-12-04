import type { RouteRecordRaw } from "vue-router";

const SleepTimeRoutes: Array<RouteRecordRaw> = [
    {
        path: '/sleep-time',
        name: 'sleepTime.list',
        component: () => import('@/views/sleep_entry/ListSleepTime.vue'),
        meta: {
            requiresAuth: true
        }
    },
    {
        path: '/create-sleep-time',
        name: 'sleepTime.create',
        component: () => import('@/views/sleep_entry/CreateSleepTime.vue'),
        meta: {
            requiresAuth: true
        }
    },
];

export default SleepTimeRoutes;
