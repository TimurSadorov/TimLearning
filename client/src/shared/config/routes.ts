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

const editableCourses = {
    path: '/courses/editing',
    getLink: () => '/courses/editing',
};

const editableModules = {
    path: '/courses/:courseId/modules/editing',
    getLink: (courseId: string) => `/courses/${courseId}/modules/editing`,
};

const editableLessons = {
    path: '/courses/:courseId/modules/:moduleId/lessons/editing',
    getLink: (courseId: string, moduleId: string) => `/courses/${courseId}/modules/${moduleId}/lessons/editing`,
};

export const routes = {
    root,
    login,
    registration,
    passwordRecovery,
    recoveryPasswordChanging,
    emailConfiramtion,
    editableCourses,
    editableModules,
    editableLessons,
};
