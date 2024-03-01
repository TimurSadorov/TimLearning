// Либо использовать @loadable/component, в рамках туториала - некритично
import React from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import { routes } from '@shared/config';
import { AuthFeature } from '@features';
import { TestPage } from './test';
import { LoginPage } from './login';

export const Routing = () => {
    return (
        <Routes>
            <Route
                element={<AuthFeature.UI.RequiredAuth needAuth={false} navigateLinkIfUnavailable={routes.root.path} />}
            >
                <Route path={routes.login.path} element={<LoginPage />} />
            </Route>
            <Route element={<AuthFeature.UI.RequiredAuth needAuth navigateLinkIfUnavailable={routes.login.path} />}>
                <Route path="*" element={<Navigate to={routes.root.path} />} />
                <Route path={routes.root.path} element={<TestPage />} />
            </Route>
        </Routes>
    );
};
