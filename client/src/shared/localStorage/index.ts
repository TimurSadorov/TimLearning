const storageKeys = {
    ACCESS_TOKEN: 'access',
    REFRESH_TOKEN: 'refresh',
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
