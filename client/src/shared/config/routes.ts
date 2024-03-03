const root = {
    path: '/',
    getLink: () => '/',
};

const login = {
    path: '/login',
    getLink: () => '/login',
};

const registration = {
    path: '/registration',
    getLink: () => '/registration',
};

const passwordRecovery = {
    path: '/account/password/recovery',
    getLink: () => '/account/password/recovery',
};

export const routes = {
    root,
    login,
    registration,
    passwordRecovery,
};
