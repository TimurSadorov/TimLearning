import React from 'react';
import styled from 'styled-components';
import { Navigate, useParams } from 'react-router-dom';
import { Config, Utils } from '@shared';
import { ModuleWidget } from '@widgets';
import { Layout } from 'antd';

export const EditableModulesPage = () => {
    const { courseId } = useParams<{ courseId: string }>();
    if (!courseId || !Utils.isValidGuid(courseId)) {
        return <Navigate to={Config.routes.root.path} />;
    }

    return (
        <Page>
            <ModuleWidget.UI.EditableModulesSider courseId={courseId} />
        </Page>
    );
};

const Page = styled(Layout)`
    min-height: calc(100vh - 51px);
`;
