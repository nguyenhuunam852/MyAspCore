// import { createWebHistory } from 'vue-router';
import { createRouter, createWebHashHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router';
import { useAuthStore } from "@/stores/auth";

import AuthRoutes from './routes/AuthRoutes';
import UsersRoutes from './routes/UsersRoutes';
import ErrorRoutes from './routes/ErrorRoutes';
import SleepTimeRoutes from './routes/SleepTimeRoutes';

const routes: Array<RouteRecordRaw> = [
    ...AuthRoutes,
    ...UsersRoutes,
    ...SleepTimeRoutes,
    ...ErrorRoutes
];

const router = createRouter({
    history: createWebHashHistory(import.meta.env.BASE_URL),
    routes: routes
})

// Check authentication status before enter routes
router.beforeEach(async (to, from, next) => {
    const store = useAuthStore()
    // loginStatus: false -> can't go to any screen back all to Login
    if (to.meta.requiresAuth && !store.loginStatus ){
        next({ name: "auth.login" });
    }
    // loginStatus: true -> can't go to Login
    else if(store.loginStatus && to.name === 'auth.login') {
        next({ name: "users.index" })
    } else {
        next()
    }
})

export default router
