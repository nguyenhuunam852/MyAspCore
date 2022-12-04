import type { RouteRecordRaw } from "vue-router";

const AuthRoutes: Array<RouteRecordRaw> = [
    {
        path: '/login',
        name: 'auth.login',
        component: () => import('@/views/auth/LoginView.vue')
    },
    {
        path: '/register',
        name: 'auth.register',
        component: () => import('@/views/auth/RegisterView.vue')
    }
];

export default AuthRoutes;