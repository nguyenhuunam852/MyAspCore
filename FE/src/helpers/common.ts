export const dateConvert = (time: any) => {
    const d = new Date(time)
    const hour = d.getHours() < 10 ? `0${d.getHours()}` : d.getHours()
    const minutes = d.getMinutes() < 10 ? `0${d.getMinutes()}` : d.getMinutes()
    const date = d.getDate() < 10 ? `0${d.getDate()}` : d.getDate()
    const month = (d.getMonth() + 1) < 10 ? `0${d.getMonth() + 1}` : d.getMonth() + 1;
    const year = d.getFullYear();
    return `${hour}:${minutes} - ${date}/${month}/${year}`
}

export const getColorStatus = (status: any) => {
    if (status === 'completed') return 'border-success text-success bg-success bg-opacity-10'
    if (status === 'pending') return 'border-info text-info bg-info bg-opacity-10'
    if (status === 'cancel') return 'border-secondary text-secondary bg-secondary bg-opacity-10'
    if (status === 'fail') return 'border-warning text-warning bg-warning bg-opacity-10'
    if (status === 'reject') return 'border-danger text-danger bg-danger bg-opacity-10'
    return ''
}

export const getColorMethod = (method: any) => {
    if (method === 'deposit') return 'text-success'
    if (method === 'fee') return 'text-warning'
    if (method === 'couple') return 'text-info'
    if (method === 'withdraw') return 'text-danger'
    return ''
}