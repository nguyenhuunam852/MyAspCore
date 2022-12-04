export type User = {
    userId: number,
    userName: string,
    email: string,
    fullName: string
}

export type UserGroup = {
    DESC: boolean | null,
    Page: number,
    Filter: string | null,
    SortBy: strong | null
}