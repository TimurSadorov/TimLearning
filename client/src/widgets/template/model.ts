import { UserEntity } from '@entities';
import { Config } from '@shared';
import { useCallback } from 'react';
import { useNavigate } from 'react-router-dom';

export const useHeader = () => {
    const navigate = useNavigate();

    const toLogin = useCallback(() => navigate(Config.routes.login.path), [navigate]);
    const toAccount = useCallback(() => navigate(Config.routes.login.path), [navigate]);
    const logout = useCallback(() => UserEntity.Model.logoutFx(), []);

    const { isAuthorized, isInRole } = UserEntity.Model.useUser();

    return {
        userIsAuthorized: isAuthorized,
        toLogin,
        toAccount,
        logout,
        isInRole,
    };
};
