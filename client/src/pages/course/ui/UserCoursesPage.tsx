import React from 'react';
import { CourseEntity } from '@entities';
import { CourseFeature } from '@features';
import { PageLoader } from 'shared/ui';
import styled from 'styled-components';

export const UserCoursesPage = () => {
    const { isLoading, userCourses } = CourseEntity.Model.useUserCourses();

    if (isLoading) {
        return <PageLoader />;
    }

    return (
        <CoursesPage>
            <CoursesHeader>Курсы</CoursesHeader>
            <CoursesContainer>
                {userCourses.map((course) => (
                    <CourseFeature.UI.UserCourse key={course.id} name={course.name} description={course.description} />
                ))}
            </CoursesContainer>
        </CoursesPage>
    );
};

const CoursesHeader = styled.div`
    font-size: 1.8em;
`;

const CoursesPage = styled.div`
    padding: 10px 20px;
    display: flex;
    flex-direction: column;
`;

const CoursesContainer = styled.div`
    margin-top: 15px;
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 10px 20px;
`;
