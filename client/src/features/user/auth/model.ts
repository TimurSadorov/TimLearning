import { UserEntity } from '@entities';
import { sample } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';

export const UserGate = createGate();

export const useUser = () => {
    useGate(UserGate);
    const user = useUnit(UserEntity.Model.$user);
    const isLoging = useUnit(UserEntity.Model.$isLoging);

    return { user, isLoging };
};

sample({
    clock: UserGate.open,
    target: UserEntity.Model.loadUserFx,
});

sample({
    clock: UserGate.close,
    target: UserEntity.Model.resetUser,
});
