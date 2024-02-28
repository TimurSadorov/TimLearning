import React from 'react';
import { Spin } from 'antd';
import { UserEntity } from '@entities';
import { Navigate, Outlet } from 'react-router-dom';
import { routes } from '@shared/config';

export const RequiredAuth = () => {
    const { user, loading } = UserEntity.Model.useUser();
    if (loading) {
        return <Spin size="large" />;
    }

    if (!user) {
        return <Navigate to={routes.login.path} />;
    }

    return <Outlet />;
};
