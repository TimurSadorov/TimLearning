import React from 'react';
import { CourseFeature } from '@features';
import { Loader } from 'shared/ui';
import styled from 'styled-components';
import { Button, Checkbox, Input } from 'antd';
import { CourseWidget } from '@widgets';
import { useNavigate } from 'react-router-dom';
import { Config } from '@shared';

export const EditableCourses = () => {
    const {
        isLoading,
        editableCourses,
        searchName,
        onChangeSearchName,
        isDraft,
        onChangeIsDraft,
        isDeleted,
        onChangeIsDeleted,
        draftFilterIsDisabled,
    } = CourseFeature.Model.useFilterEditableCourses();

    const navigate = useNavigate();

    return (
        <CoursesPage>
            <CoursesHeaderBlock>
                <FilterBlock>
                    <Search
                        placeholder="Поиск"
                        onChange={(e) => onChangeSearchName(e.target.value)}
                        value={searchName}
                    />
                    <FilterCheckbox
                        onChange={(e) => onChangeIsDraft(e.target.checked)}
                        checked={isDraft}
                        disabled={draftFilterIsDisabled}
                    >
                        Черновик
                    </FilterCheckbox>
                    <FilterCheckbox onChange={(e) => onChangeIsDeleted(e.target.checked)} checked={isDeleted}>
                        Удален
                    </FilterCheckbox>
                </FilterBlock>
                <Buttons>
                    <CreateCourseButton />
                    <ExitButton onClick={() => navigate(Config.routes.root.path)}>Пользовательский режим</ExitButton>
                </Buttons>
            </CoursesHeaderBlock>
            {isLoading ? (
                <LoaderCourses />
            ) : (
                <CoursesContainer>
                    {editableCourses.map((course) => (
                        <CourseWidget.UI.EditableCours key={course.id} course={course} />
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

const LoaderCourses = styled(Loader)`
    flex: 1;
`;

const CoursesHeaderBlock = styled.div`
    display: grid;
    grid-template-columns: repeat(2, 1fr);
`;

const FilterBlock = styled.div``;

const FilterCheckbox = styled(Checkbox)`
    margin-left: 10px;
`;

const Search = styled(Input)`
    max-width: 30%;
`;

const Buttons = styled.div`
    display: flex;
    justify-content: end;
`;

const CreateCourseButton = styled(CourseFeature.UI.CreateCourseButton)`
    margin-right: 10px;
`;

const ExitButton = styled(Button)`
    max-width: 30%;
    justify-content: space-around;
    display: flex;
`;

const CoursesContainer = styled.div`
    margin-top: 15px;
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 10px 20px;
`;
