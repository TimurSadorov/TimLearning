import { jwtDecode } from 'jwt-decode';

export const decodeSystemInfoJwt = (token: string) => {
    const jwtObj = jwtDecode<{ exp: number; email: string }>(token);
    return {
        expire: new Date(jwtObj.exp * 1000),
        email: jwtObj.email,
    };
};
