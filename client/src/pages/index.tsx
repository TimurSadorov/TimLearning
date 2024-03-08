import React from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import { Config } from '@shared';
import { UserFeature } from '@features';
import { TestPage } from './test';
import { LoginPage } from './login';
import { RegistrationPage } from './registration';
import { PasswordChngingPage, PasswordRecoveryPage } from './passwordRecovery';
import { EmailConfirmationPage } from './emailConfirmation';

const routes = Config.routes;

export const Routing = () => {
    return (
        <Routes>
            <Route
                element={
                    <UserFeature.Auth.UI.RequiredAuth needAuth={false} navigateLinkIfUnavailable={routes.root.path} />
                }
            >
                <Route path={routes.login.path} element={<LoginPage />} />
                <Route path={routes.registration.path} element={<RegistrationPage />} />
                <Route path={routes.passwordRecovery.path} element={<PasswordRecoveryPage />} />
                <Route path={routes.recoveryPasswordChanging.path} element={<PasswordChngingPage />} />
            </Route>
            <Route
                element={<UserFeature.Auth.UI.RequiredAuth needAuth navigateLinkIfUnavailable={routes.login.path} />}
            >
                <Route path="*" element={<Navigate to={routes.root.path} />} />
                <Route path={routes.root.path} element={<TestPage />} />
            </Route>
            <Route path={routes.emailConfiramtion.path} element={<EmailConfirmationPage />} />
        </Routes>
    );
};
