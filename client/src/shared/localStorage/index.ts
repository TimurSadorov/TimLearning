const storageKeys = {
    ACCESS_TOKEN: 'access',
    REFRESH_TOKEN: 'refresh',
    SELECTED_LESSONS: 'selected-lessons',
};

export const setAccessToken = (token: string) => {
    localStorage.setItem(storageKeys.ACCESS_TOKEN, token);
};

export const getAccessToken = () => {
    return localStorage.getItem(storageKeys.ACCESS_TOKEN);
};

export const setRefresfToken = (token: string) => {
    return localStorage.setItem(storageKeys.REFRESH_TOKEN, token);
};

export const getRefreshToken = () => {
    return localStorage.getItem(storageKeys.REFRESH_TOKEN);
};

export const clearTokens = () => {
    localStorage.removeItem(storageKeys.ACCESS_TOKEN);
    localStorage.removeItem(storageKeys.REFRESH_TOKEN);
};

type SelectedLessons = Record<string, string>;

export const setSelectedLessonInCourse = (courseId: string, lessonId: string) => {
    const selectedLessons = getSelectedLessonsMap();
    selectedLessons[courseId] = lessonId;
    localStorage.setItem(storageKeys.SELECTED_LESSONS, JSON.stringify(selectedLessons));
};

export const getSelectedLessonIdInCourse = (courseId: string): string | undefined => {
    const selectedLessons = getSelectedLessonsMap();
    return selectedLessons[courseId];
};

const getSelectedLessonsMap = (): SelectedLessons => {
    const selectedLessonsJson = localStorage.getItem(storageKeys.SELECTED_LESSONS);

    if (!!selectedLessonsJson) {
        try {
            return JSON.parse(selectedLessonsJson);
        } catch {}
    }

    return {};
};
