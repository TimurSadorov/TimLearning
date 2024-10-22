import React from 'react';
import { Navigate } from 'react-router-dom';
import { UserEntity } from '@entities';
import { Config, SharedTypes } from '@shared';

interface Props {
    needAuth: boolean;
    navigateLinkIfUnavailable: string;
    element: JSX.Element;
    requiredRole?: SharedTypes.UserRole;
}

export const RequiredAuth = (props: Props) => {
    const { isAuthorized, isInRole } = UserEntity.Model.useUser();

    if (isAuthorized !== props.needAuth) {
        return <Navigate to={props.navigateLinkIfUnavailable} />;
    }

    if (!!props.requiredRole) {
        if (!isInRole(props.requiredRole)) {
            return <Navigate to={Config.routes.root.path} />;
        }
    }

    return props.element;
};
