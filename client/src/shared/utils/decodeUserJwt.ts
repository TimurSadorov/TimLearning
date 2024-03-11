import { jwtDecode } from 'jwt-decode';
import { SharedTypes } from '@shared';

export const decodeUserJwt = (token: string): SharedTypes.User => {
    const jwtObj = jwtDecode<JwtToken>(token);
    return {
        id: jwtObj.id,
        email: jwtObj.email,
        roles: Array.isArray(jwtObj.role) ? jwtObj.role : [jwtObj.role],
    };
};

interface JwtToken {
    id: string;
    email: string;
    role: SharedTypes.UserRole | SharedTypes.UserRole[];
}
