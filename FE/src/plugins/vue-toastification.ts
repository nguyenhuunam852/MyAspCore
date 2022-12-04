import Toast, { POSITION } from "vue-toastification"
import type { PluginOptions } from "vue-toastification"
import "vue-toastification/dist/index.css"

export const options: PluginOptions = {
	// You can set your default options here
	position: POSITION.TOP_RIGHT,
	timeout: 2000,
	closeOnClick: true,
	pauseOnFocusLoss: false,
	pauseOnHover: true,
	draggable: true,
	showCloseButtonOnHover: true,
	hideProgressBar: false,
	closeButton: "button",
	icon: true,
	rtl: false,
    newestOnTop: true,
	transition: "Vue-Toastification__fade",
    maxToasts: 3,
	// filterBeforeCreate: (toast, toasts) => {
	// 	if (toasts.filter((t) => t.type === toast.type).length !== 0) {
	// 		// Returning false discards the toast
	// 		return false
	// 	}
	// 	// You can modify the toast if you want
	// 	return toast
	// },
}

export default Toast
