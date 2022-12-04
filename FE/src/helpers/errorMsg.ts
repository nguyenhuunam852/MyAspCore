const errorMsg: {[key: string]: string} = {
    NOT_FOUND_USER: "User does not exist",
	NOT_FOUND_THIRD_PARTY: "Third party not found",
	NOT_DATA_UPDATE_THIRD_PARTY: "Third party not data update",
	INVALID_PASSWORD: "Invalid Password",
	FORBIDDEN: "Forbidden",
	NOT_FOUND_TOKEN: "Token not found",
	NOT_FOUND_ADDRESS: "Address not found",
	NOT_FOUND_TRANSACTION: "Transaction not found",
	INVALID_TOKEN: "Invalid Token",
	INVALID_AGENT_CODE: "Invalid Agent code",
	INVALID_SIGNATURE: "Invalid Signature",
	INVALID_TO_ADDRESS: "Invalid To Address",

	EXIST_TOKEN_CODE: "Token Code already exist",
	EXIST_EMAIL: "Email already exist",
	OTHER: "OTHER",
	SUCCESS: "SUCCESS",

	WEBHOOK_NOT_FOUND_ADDRESS: "Webhook not found Address",
	WEBHOOK_EXIST_HASH: " Hash already exist in Webhook",
	WEBHOOK_INVALID_ACCESS_KEY: "Invalid Access Key in Webhook",

	AMOUNT_NOT_ENOUGH: "Amount not enough",
}

function getErrorMsg(key: string | number) {
	if (errorMsg[key]) {
		return errorMsg[key]
	} else {
        return "Something went wrong"
    }
}
export default getErrorMsg
