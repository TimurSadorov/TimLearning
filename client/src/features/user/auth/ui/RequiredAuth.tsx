import React from 'react';
import { Navigate } from 'react-router-dom';
import { useUnit } from 'effector-react';
import { UserEntity } from '@entities';

interface Props {
    needAuth: boolean;
    navigateLinkIfUnavailable: string;
    element: JSX.Element;
}

export const RequiredAuth = (props: Props) => {
    const user = useUnit(UserEntity.Model.$user);

    if (!!user !== props.needAuth) {
        return <Navigate to={props.navigateLinkIfUnavailable} />;
    }

    return props.element;
};
