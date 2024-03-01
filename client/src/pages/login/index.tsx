import { UserEntity } from '@entities';
import { routes } from '@shared/config';
import React from 'react';
import { Navigate } from 'react-router-dom';

const Login = () => {
    // const { user } = UserEntity.Model.useUser();

    // if (!!user) {
    //     return <Navigate to={routes.root.path} />;
    // }
    // // if (isAuthenticated) {
    // //     return <Navigate to={routes.root.path} />;
    // // }

    return <div>Login</div>;
};

export default Login;
