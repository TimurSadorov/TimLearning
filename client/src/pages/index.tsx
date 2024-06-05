import React from 'react';
import { Route, Routes, Navigate, Outlet } from 'react-router-dom';
import { Config, SharedUI } from '@shared';
import { UserFeature } from '@features';
import { LoginPage } from './login';
import { RegistrationPage } from './registration';
import { PasswordChngingPage, PasswordRecoveryPage } from './passwordRecovery';
import { EmailConfirmationPage } from './emailConfirmation';
import { EditableCourses, UserCoursePage, UserCoursesPage } from './course/ui';
import { TemplateWidget } from '@widgets';
import styled from 'styled-components';
import { EditableModulesPage } from './module';
import { EditableLessonPage, EditableLessonsPage, UserLessonPage } from './lesson';
import { StudyGroupsPage } from './studyGroup/ui/StudyGroupsPage';
import { StudyGroupPage } from './studyGroup/ui/StudyGroupPage';
import { JoiningGroupPage } from './studyGroup/ui/JoiningGroupPage';
import { CodeReviewPage } from './codeReview';

const routes = Config.routes;

export const Routing = () => {
    const { isLoging } = UserFeature.Auth.Model.useUser();
    if (!isLoging) {
        return <Loader />;
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
            <Route
                element={
                    <UserFeature.Auth.UI.RequiredAuth
                        needAuth
                        navigateLinkIfUnavailable={routes.login.path}
                        requiredRole="ContentCreator"
                        element={<TemplateWidget.UI.Header />}
                    />
                }
            >
                <Route path={routes.editableCourses.path} element={<EditableCourses />} />
                <Route path={routes.editableModules.path} element={<EditableModulesPage />} />
                <Route path={routes.editableLessons.path} element={<EditableLessonsPage />} />
                <Route path={routes.editableLesson.path} element={<EditableLessonPage />} />
            </Route>
            <Route
                element={
                    <UserFeature.Auth.UI.RequiredAuth
                        needAuth
                        navigateLinkIfUnavailable={routes.login.path}
                        element={<TemplateWidget.UI.Header />}
                    />
                }
            >
                <Route path={routes.userCourse.path} element={<UserCoursePage />} />
                <Route path={routes.userLesson.path} element={<UserLessonPage />} />
            </Route>
            <Route
                element={
                    <UserFeature.Auth.UI.RequiredAuth
                        needAuth
                        navigateLinkIfUnavailable={routes.login.path}
                        requiredRole="Mentor"
                        element={<TemplateWidget.UI.Header />}
                    />
                }
            >
                <Route path={routes.studyGroups.path} element={<StudyGroupsPage />} />
                <Route path={routes.studyGroup.path} element={<StudyGroupPage />} />
                <Route path={routes.userCodeReview.path} element={<CodeReviewPage />} />
            </Route>
            <Route element={<TemplateWidget.UI.Header />}>
                <Route path="*" element={<Navigate to={routes.root.path} />} />
                <Route path={routes.root.path} element={<UserCoursesPage />} />
            </Route>
            <Route path={routes.emailConfiramtion.path} element={<EmailConfirmationPage />} />
            <Route path={routes.joiningGroup.path} element={<JoiningGroupPage />} />
        </Routes>
    );
};

const Loader = styled(SharedUI.Loader)`
    min-height: 100vh;
`;
