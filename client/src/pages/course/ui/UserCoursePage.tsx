import React from 'react';
import { Config, SharedUI, Utils } from '@shared';
import { Navigate, useParams } from 'react-router-dom';
import styled from 'styled-components';
import { useRedirectOnLesson } from '../model';

export const UserCoursePage = () => {
    const { courseId } = useParams<{ courseId: string }>();
    if (courseId == undefined || !Utils.isValidGuid(courseId)) {
        return <Navigate to={Config.routes.root.path} />;
    }

    useRedirectOnLesson(courseId);

    return <LoaderCourses />;
};

const LoaderCourses = styled(SharedUI.Loader)`
    min-height: calc(100vh - 51px);
`;
