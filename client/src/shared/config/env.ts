export interface AppEnv {
    apiUrl: string;
}

export const appEnv: AppEnv = {
    apiUrl: process.env.REACT_APP_API_URL!,
};
