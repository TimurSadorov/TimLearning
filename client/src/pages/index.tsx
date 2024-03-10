import React from 'react';
import { Route, Routes, Navigate, Outlet } from 'react-router-dom';
import { Config, SharedUI } from '@shared';
import { UserFeature } from '@features';
import { LoginPage } from './login';
import { RegistrationPage } from './registration';
import { PasswordChngingPage, PasswordRecoveryPage } from './passwordRecovery';
import { EmailConfirmationPage } from './emailConfirmation';
import { UserCoursesPage } from './course/ui';
import { UserEntity } from '@entities';

const routes = Config.routes;

export const Routing = () => {
    const { user, isLoging } = UserFeature.Auth.Model.useUser();
    if (!isLoging) {
        return <SharedUI.PageLoader />;
    }

    return (
        <Routes>
            <Route
                element={
                    <UserFeature.Auth.UI.RequiredAuth
                        needAuth={false}
                        navigateLinkIfUnavailable={routes.root.path}
                        element={<Outlet />}
                    />
                }
            >
                <Route path={routes.login.path} element={<LoginPage />} />
                <Route path={routes.registration.path} element={<RegistrationPage />} />
                <Route path={routes.passwordRecovery.path} element={<PasswordRecoveryPage />} />
                <Route path={routes.recoveryPasswordChanging.path} element={<PasswordChngingPage />} />
            </Route>
            {/* <Route
                element={
                    <UserFeature.Auth.UI.RequiredAuth
                        needAuth
                        navigateLinkIfUnavailable={routes.login.path}
                        element={<SharedUI.Header isLoged={!!user} />}
                    />
                }
            >
            </Route> */}
            <Route element={<SharedUI.Header isLoged={!!user} logout={UserEntity.Model.logoutFx} />}>
                <Route path="*" element={<Navigate to={routes.root.path} />} />
                <Route path={routes.root.path} element={<UserCoursesPage />} />
            </Route>
            <Route path={routes.emailConfiramtion.path} element={<EmailConfirmationPage />} />
        </Routes>
    );
};
