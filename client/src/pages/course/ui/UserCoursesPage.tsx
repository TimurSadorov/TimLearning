import React, { useCallback } from 'react';
import { CourseEntity, UserEntity } from '@entities';
import { CourseFeature } from '@features';
import { PageLoader } from 'shared/ui';
import styled from 'styled-components';
import { Button } from 'antd';
import { useNavigate } from 'react-router-dom';
import { Config } from '@shared';

export const UserCoursesPage = () => {
    const { isLoading, userCourses } = CourseEntity.Model.useUserCourses();
    const { isInRole } = UserEntity.Model.useUser();
    const navigate = useNavigate();

    const toEditableCourses = useCallback(() => navigate(Config.routes.editableCourses.path), [navigate]);

    if (isLoading) {
        return <PageLoader />;
    }

    return (
        <CoursesPage>
            <CoursesHeaderBlock>
                <CoursesHeader>Курсы</CoursesHeader>
                {isInRole('ContentCreator') ? <Button onClick={toEditableCourses}>Режим редактирования</Button> : <></>}
            </CoursesHeaderBlock>
            <CoursesContainer>
                {userCourses.map((course) => (
                    <CourseFeature.UI.UserCourse key={course.id} name={course.name} description={course.description} />
                ))}
            </CoursesContainer>
        </CoursesPage>
    );
};

const CoursesHeaderBlock = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
`;

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
