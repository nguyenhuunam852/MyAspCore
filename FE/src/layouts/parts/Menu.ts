type MenuItem = {
    icon: string;
    label: string;
    hasSub: boolean;
    activeAt: string[];
    route: { name: string }
    children?: {
        label: string;
        route: { name: string };
        activeAt: string[];
    }[],
}

export const menu = <MenuItem[]>([
    {
        label: 'Users',
        icon: 'ionicon ionicon-people',
        hasSub: false,
        route: {name: 'users.index'},
        activeAt: ['users.index'],
    },
    {
        label: 'SleepTime',
        icon: 'ionicon ionicon-alarm',
        hasSub: true,
        activeAt: ['sleepTime.create', 'sleepTime.list'],
        children: [
            { label: 'Create Sleep Time', route: {name: 'sleepTime.create'}, activeAt: ['sleepTime.create']},
            { label: 'List Sleep Time', route: {name: 'sleepTime.list'}, activeAt: ['sleepTime.list']},
        ]
    },
])