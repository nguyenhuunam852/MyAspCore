/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly VITE_APP_TITLE: string,
    readonly VITE_APP_TITLE: string,
    readonly VITE_APP_VER: string,
    readonly VITE_APP_HOST: string,
    readonly VITE_API_HOST: string,
    readonly VITE_API_PREFIX: string,
}

interface ImportMeta {
    readonly env: ImportMetaEnv
}
