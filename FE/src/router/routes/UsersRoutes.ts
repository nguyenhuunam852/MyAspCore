import type { RouteRecordRaw } from "vue-router";

const UsersRoutes: Array<RouteRecordRaw> = [
    {
        path: '/users',
        alias: '/',
        name: 'users.index',
        component: () => import('@/views/user/ListUserView.vue'),
        meta: {
            requiresAuth: true
        }
    },
];

export default UsersRoutes;
