import md5 from 'md5'
export default function md5Hash(string: string) {
    return md5(string)
}