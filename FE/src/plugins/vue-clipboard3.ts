import useClipboard from "vue-clipboard3"
const { toClipboard } = useClipboard()
import { useToast } from "vue-toastification"

const toast = useToast()

const copy = async (value: any) => {
	try {
		await toClipboard(value)
        toast.success('Copied')
	} catch (e) {
		toast.success('Copy failed')
	}
}

export default copy