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
    path: '/modules/:moduleId/lessons/editing',
    getLink: (moduleId: string) => `/modules/${moduleId}/lessons/editing`,
};

const editableLesson = {
    path: '/modules/:moduleId/lessons/:lessonId/editing',
    getLink: (moduleId: string, lessonId: string) => `/modules/${moduleId}/lessons/${lessonId}/editing`,
};

const userCourse = {
    path: '/courses/:courseId',
    getLink: (courseId: string) => `/courses/${courseId}`,
};

const userLesson = {
    path: '/lessons/:lessonId',
    getLink: (lessonId: string) => `/lessons/${lessonId}`,
};

const studyGroups = {
    path: '/study-groups',
    getLink: () => `/study-groups`,
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
    editableLesson,
    userCourse,
    userLesson,
    studyGroups,
};
