export type AuthStore = {
    jwt: string,
    loggedIn: boolean
}

export type LoginForm = {
    userName: string,
    userPassword: string
}

export type RegisterForm = {
    userName: string,
    userPassword: string,
    fullName: string,
    email: string,
    confirmPassword: string
}