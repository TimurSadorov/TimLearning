import { Config, Utils } from '@shared';
import { ModuleWidget } from '@widgets';
import { Layout } from 'antd';
import { useUnit } from 'effector-react';
import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import styled from 'styled-components';
import { $courseId } from './model';

export const EditableModulesSider = () => {
    const courseId = useUnit($courseId);

    if (courseId !== null && !Utils.isValidGuid(courseId)) {
        return <Navigate to={Config.routes.root.path} />;
    }

    return (
        <LeasonsPage>
            {courseId === null ? <></> : <ModuleWidget.UI.EditableModulesSider courseId={courseId} />}
            <Outlet />
        </LeasonsPage>
    );
};

const LeasonsPage = styled(Layout)`
    min-height: calc(100vh - 51px);
    display: flex;
    flex-direction: row;
`;
