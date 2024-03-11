import React from 'react';
import { Navigate } from 'react-router-dom';
import { UserEntity } from '@entities';

interface Props {
    needAuth: boolean;
    navigateLinkIfUnavailable: string;
    element: JSX.Element;
}

export const RequiredAuth = (props: Props) => {
    const { isAuthorized } = UserEntity.Model.useUser();

    if (isAuthorized !== props.needAuth) {
        return <Navigate to={props.navigateLinkIfUnavailable} />;
    }

    return props.element;
};
