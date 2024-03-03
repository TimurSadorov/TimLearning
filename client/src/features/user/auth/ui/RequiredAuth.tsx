import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useUser } from '../model';
import { SharedUI } from '@shared';

interface Props {
    needAuth: boolean;
    navigateLinkIfUnavailable: string;
}

export const RequiredAuth = (props: Props) => {
    const { user, isLoging } = useUser();
    if (!isLoging) {
        return <SharedUI.PageLoader />;
    }

    if (!!user !== props.needAuth) {
        return <Navigate to={props.navigateLinkIfUnavailable} />;
    }

    return <Outlet />;
};
