import React from 'react';
import { StudyGroupFeature } from '@features';
import styled from 'styled-components';
import { Checkbox, Input } from 'antd';
import { useNavigate } from 'react-router-dom';
import { Config, SharedUI } from '@shared';
import { CourseEntity } from '@entities';

export const StudyGroupsPage = () => {
    const { isLoading, studyGroups, isActive, onChangeIsActive, searchName, onChangeSearchName } =
        StudyGroupFeature.Model.useFilterStudyGroups();

    const { isLoading: coursesIsLoading, userCourses } = CourseEntity.Model.useUserCourses();

    const navigate = useNavigate();

    return (
        <Page>
            <StudyGroupsHeaderBlock>
                <FilterBlock>
                    <Search
                        placeholder="Поиск"
                        onChange={(e) => onChangeSearchName(e.target.value)}
                        value={searchName}
                    />
                    <FilterCheckbox onChange={(e) => onChangeIsActive(e.target.checked)} checked={isActive}>
                        Активные
                    </FilterCheckbox>
                </FilterBlock>
                <Buttons>
                    <CreateStudyGroupButton />
                </Buttons>
            </StudyGroupsHeaderBlock>
            {isLoading || !studyGroups || coursesIsLoading ? (
                <Loader />
            ) : (
                <StudyGroupsContainer>
                    {studyGroups.map((studyGroup) => (
                        <StudyGroupFeature.UI.StudyGroupRecord
                            key={studyGroup.id}
                            courseName={
                                userCourses.find((c) => c.id == studyGroup.courseId)?.name ?? 'Имя курса не известно'
                            }
                            isActive={studyGroup.isActive}
                            name={studyGroup.name}
                            onClick={() => navigate(Config.routes.studyGroup.getLink(studyGroup.id))}
                        />
                    ))}
                </StudyGroupsContainer>
            )}
        </Page>
    );
};

const Page = styled.div`
    padding: 10px 30px;
    display: flex;
    flex-direction: column;
    min-height: calc(100vh - 71px);
`;

const Loader = styled(SharedUI.Loader)`
    flex: 1;
`;

const StudyGroupsHeaderBlock = styled.div`
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

const CreateStudyGroupButton = styled(StudyGroupFeature.UI.CreateStudyGroupButton)`
    margin-right: 10px;
`;

const StudyGroupsContainer = styled.div`
    margin-top: 15px;
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 10px 20px;
`;
