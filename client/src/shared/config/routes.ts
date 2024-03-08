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
    path: '/account/password/recovering',
    getLink: () => '/account/password/recovering',
};

const recoveryPasswordChanging = {
    path: '/account/password/recovering/changing',
    getLink: () => '/account/password/recovering/changing',
};

const emailConfiramtion = {
    path: '/account/email/confirmation',
    getLink: () => '/account/email/confirmation',
};

export const routes = {
    root,
    login,
    registration,
    passwordRecovery,
    recoveryPasswordChanging,
    emailConfiramtion,
};
