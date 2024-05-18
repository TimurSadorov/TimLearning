import React, { useCallback } from 'react';
import { CourseEntity, UserEntity } from '@entities';
import { CourseFeature } from '@features';
import { SharedUI } from '@shared';
import styled from 'styled-components';
import { Button } from 'antd';
import { useNavigate } from 'react-router-dom';
import { Config } from '@shared';

export const UserCoursesPage = () => {
    const { isLoading, userCourses } = CourseEntity.Model.useUserCourses();
    const { isInRole } = UserEntity.Model.useUser();
    const navigate = useNavigate();

    const toEditableCourses = useCallback(() => navigate(Config.routes.editableCourses.path), [navigate]);

    return (
        <CoursesPage>
            <CoursesHeaderBlock>
                <CoursesHeader>Курсы</CoursesHeader>
                {isInRole('ContentCreator') ? <Button onClick={toEditableCourses}>Режим редактирования</Button> : <></>}
            </CoursesHeaderBlock>
            {isLoading ? (
                <LoaderCourses />
            ) : (
                <CoursesContainer>
                    {userCourses.map((course) => (
                        <CourseFeature.UI.UserCourse
                            key={course.id}
                            name={course.name}
                            description={course.description}
                            onClick={() => navigate(Config.routes.userCourse.getLink(course.id))}
                        />
                    ))}
                </CoursesContainer>
            )}
        </CoursesPage>
    );
};

const CoursesPage = styled.div`
    padding: 10px 30px;
    display: flex;
    flex-direction: column;
    min-height: calc(100vh - 71px);
`;

const LoaderCourses = styled(SharedUI.Loader)`
    flex: 1;
`;

const CoursesHeaderBlock = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
`;

const CoursesHeader = styled.div`
    font-size: 1.7em;
    font-weight: 600;
    color: rgba(0, 0, 0, 0.85);
`;

const CoursesContainer = styled.div`
    margin-top: 15px;
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 10px 20px;
`;
