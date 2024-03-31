import React from 'react';
import { Config, Utils } from '@shared';
import { LessonWidget } from '@widgets';
import { Layout } from 'antd';
import { Navigate, useParams } from 'react-router-dom';
import styled from 'styled-components';

export const EditableLessonsPage = () => {
    const { moduleId } = useParams<{ moduleId: string }>();
    if (moduleId == undefined || !Utils.isValidGuid(moduleId)) {
        return <Navigate to={Config.routes.root.path} />;
    }

    return (
        <Page>
            <LessonWidget.UI.SystemLessonSider moduleId={moduleId} isEditable />
        </Page>
    );
};

const Page = styled(Layout)`
    min-height: calc(100vh - 51px);
`;
