import App from './App.vue'

const app = createApp(App)


// Script js
import "./lib/bootstrap/bootstrap";

// Import styles
import "./lib/mazer/scss/pages/auth.scss";
import "./assets/styles/vendors.scss";
import "./assets/styles/app.scss";

// Import lib
import "./plugins/vee-validate"
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
import router from './router'
import Toast, { options } from './plugins/vue-toastification'

const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)
app.use(pinia)
app.use(router)
app.use(Toast, options)

app.mount('#app')