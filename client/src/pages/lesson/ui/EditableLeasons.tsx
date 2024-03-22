import { Config, Utils } from '@shared';
import { ModuleWidget } from '@widgets';
import { Layout } from 'antd';
import React from 'react';
import { Navigate, useParams } from 'react-router-dom';
import styled from 'styled-components';

export const EditableLeasons = () => {
    const { courseId } = useParams<{ courseId: string }>();

    if (courseId == undefined || !Utils.isValidGuid(courseId)) {
        return <Navigate to={Config.routes.root.path} />;
    }

    return (
        <LeasonsPage>
            <ModuleWidget.UI.EditableModulesSider courseId={courseId} />
        </LeasonsPage>
    );
};

const LeasonsPage = styled(Layout)`
    min-height: calc(100vh - 51px);
`;
