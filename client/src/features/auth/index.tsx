import React from 'react';
import { Outlet } from 'react-router-dom';

export const RequiredAuth = () => {
    // const { isAuthenticated } = userModel.useUser();

    // if (isAuthenticated) {
    //     return <Navigate to={routes.root.path} />;
    // }

    return <Outlet />;
};
