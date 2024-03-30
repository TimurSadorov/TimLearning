import React, { useCallback } from 'react';
import { CourseEntity } from '@entities';
import { ModuleFeature } from '@features';
import { Button, Checkbox, Layout, Skeleton, Tooltip } from 'antd';
import { Loader } from 'shared/ui';
import styled from 'styled-components';
import { EditableModuleRecord } from './EditableModuleRecord';
import { EditOutlined } from '@ant-design/icons';
import { Link, useNavigate } from 'react-router-dom';
import { Config } from '@shared';

type Props = { courseId: string; selectedModuleId: string };

export const SystemModulesSider = ({ courseId, selectedModuleId }: Props) => {
    const { editableCourse, isLoading: courseLoading } = CourseEntity.Model.useEditableCourse(courseId);

    const {
        editableOrderedModules,
        isLoading: modulesLoading,
        isDeleted,
        onChangeIsDeleted,
    } = ModuleFeature.Model.useFilterEditableOrderedModules(courseId);

    const navigate = useNavigate();
    const toEditableModules = useCallback(() => navigate(Config.routes.editableModules.getLink(courseId)), [navigate]);
    const toEditableLessons = useCallback(
        (moduleId: string) => {
            navigate(Config.routes.editableLessons.getLink(courseId, moduleId));
        },
        [navigate, selectedModuleId, courseId],
    );

    return (
        <Layout.Sider theme="light" width="400px">
            <LayoutSider>
                <SiderHeader>
                    <CoursesLink to={Config.routes.editableCourses.path}>Вернуться к курсам</CoursesLink>
                    {courseLoading ? <Skeleton.Input active /> : <HeaderText>{editableCourse?.shortName}</HeaderText>}
                </SiderHeader>
                <FeaturesBlock>
                    <FiltersBlock>
                        <FilterWord>Фильтры:</FilterWord>
                        <FilterCheckbox onChange={(e) => onChangeIsDeleted(e.target.checked)} checked={isDeleted}>
                            Удален
                        </FilterCheckbox>
                    </FiltersBlock>
                    <ToEditableButton>
                        <Tooltip placement="top" title={'Режим редактирования'}>
                            <Button icon={<EditOutlined />} onClick={toEditableModules} />
                        </Tooltip>
                    </ToEditableButton>
                </FeaturesBlock>
                {!editableOrderedModules || modulesLoading ? (
                    <Loader />
                ) : (
                    <ModulesList>
                        {editableOrderedModules.map((module) => (
                            <EditableModuleRecord
                                key={module.id}
                                module={module}
                                isActive={selectedModuleId === module.id}
                                isEditable={false}
                                onClick={() => toEditableLessons(module.id)}
                            />
                        ))}
                    </ModulesList>
                )}
            </LayoutSider>
        </Layout.Sider>
    );
};

const LayoutSider = styled(Layout)`
    background-color: #ffffff;
    height: 100%;
`;
const SiderHeader = styled.div`
    border-bottom: 2px solid #bfdcfe;
    padding: 20px 30px 30px 30px;
    flex-direction: column;
    align-items: start;
    display: flex;
    row-gap: 10px;
`;

const CoursesLink = styled(Link)`
    font-size: 0.95em;
`;

const HeaderText = styled.div`
    font-size: 1.7em;
    font-weight: 600;
`;

const FeaturesBlock = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 15px 0px 15px 30px;
    border-bottom: 1px solid #e1e1e1;
`;

const FilterWord = styled.div`
    font-size: 1.1em;
    font-weight: 600;
`;

const FiltersBlock = styled.div`
    display: flex;
    align-items: center;
`;

const ToEditableButton = styled.div`
    margin-right: 10px;
`;

const FilterCheckbox = styled(Checkbox)`
    margin-left: 10px;
    margin-top: 2px;
`;

const ModulesList = styled.div`
    background-color: #ffffff;
    display: flex;
    flex-direction: column;
`;
