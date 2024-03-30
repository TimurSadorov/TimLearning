import React from 'react';
import { Config, Utils } from '@shared';
import { ModuleWidget } from '@widgets';
import { Layout } from 'antd';
import { Navigate, useParams } from 'react-router-dom';
import styled from 'styled-components';

export const EditableLessonsPage = () => {
    const { courseId, moduleId } = useParams<{ courseId: string; moduleId: string }>();
    if (
        courseId == undefined ||
        !Utils.isValidGuid(courseId) ||
        moduleId == undefined ||
        !Utils.isValidGuid(moduleId)
    ) {
        return <Navigate to={Config.routes.root.path} />;
    }

    return (
        <Page>
            <ModuleWidget.UI.SystemModulesSider courseId={courseId} selectedModuleId={moduleId} />
        </Page>
    );
};

const Page = styled(Layout)`
    min-height: calc(100vh - 51px);
`;
