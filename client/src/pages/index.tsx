// Либо использовать @loadable/component, в рамках туториала - некритично
import React from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import { routes } from 'shared/config';
import Login from './login';
import { TestPage } from './test';

export const Routing = () => {
    return (
        <Routes>
            <Route path={routes.login.path}>
                <Route index element={<Login />} />
            </Route>

            <Route path={routes.root.path} element={<TestPage />} />
            <Route path="*" element={<Navigate to={routes.root.path} />} />
        </Routes>
    );
};
