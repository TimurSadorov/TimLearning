import { useGate, useUnit } from 'effector-react';
import { $user, $userLoading, UserGate } from './model';

export const useUser = () => {
    useGate(UserGate);
    const user = useUnit($user);
    const loading = useUnit($userLoading);

    return { user, loading };
};
