import { createEffect } from 'effector';
import { jwtDecode } from 'jwt-decode';
import { getAccessToken } from '@shared/local-storage';
import { User } from '../types';

export const loadUserFx = createEffect(() => {
    const token = getAccessToken();
    console.log('d');
    const user = token ? jwtDecode<User>(token) : null;

    return user;
});

// const authFx = createEffect(async (data: { username: string }) => {
//     const resp = await auth(data);
//     const { jwt } = resp.data;

//     setToken(jwt);
// });
