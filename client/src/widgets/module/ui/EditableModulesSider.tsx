import { CourseEntity } from '@entities';
import { ModuleFeature } from '@features';
import { Checkbox, Layout, Skeleton } from 'antd';
import React, { useCallback } from 'react';
import { DragDropContext, Draggable, Droppable, OnDragEndResponder } from 'react-beautiful-dnd';
import { Loader } from 'shared/ui';
import styled from 'styled-components';
import { EditableModuleRecord } from './EditableModuleRecord';
import { Link, useNavigate } from 'react-router-dom';
import { Config } from '@shared';

type Props = { courseId: string };

export const EditableModulesSider = ({ courseId }: Props) => {
    const { editableCourse, isLoading: courseLoading } = CourseEntity.Model.useEditableCourse(courseId);

    const { editableOrderedModules, isLoading, isDeleted, onChangeIsDeleted } =
        ModuleFeature.Model.useFilterEditableOrderedModules(courseId);

    const { changeOrder, changingOrderLoading } = ModuleFeature.Model.useChangeModuleOrder();
    const onDragEnd = useCallback<OnDragEndResponder>(
        (result) => {
            if (!!result.destination && result.source.index !== result.destination.index) {
                changeOrder({ moduleId: result.draggableId, data: { order: result.destination.index + 1 } });
            }
        },
        [changeOrder],
    );

    const navigate = useNavigate();
    const toEditableLessons = useCallback(
        (moduleId: string) => {
            navigate(Config.routes.editableLessons.getLink(moduleId));
        },
        [navigate],
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
                    <CreateModuleButton courseId={courseId} />
                </FeaturesBlock>
                <DragDropContext enableDefaultSensors onDragEnd={onDragEnd}>
                    {!editableOrderedModules || isLoading || changingOrderLoading ? (
                        <Loader />
                    ) : (
                        <Droppable droppableId="modules">
                            {(provided) => (
                                <ModulesList ref={provided.innerRef} {...provided.droppableProps}>
                                    {editableOrderedModules.map((module, index) => (
                                        <Draggable
                                            isDragDisabled={isDeleted}
                                            key={module.id}
                                            draggableId={module.id}
                                            index={index}
                                        >
                                            {(provided) => (
                                                <EditableModuleRecord
                                                    module={module}
                                                    isActive={false}
                                                    isEditable={true}
                                                    onClick={() => {
                                                        toEditableLessons(module.id);
                                                    }}
                                                    ref={provided.innerRef}
                                                    draggableProps={provided.draggableProps}
                                                    dragHandleProps={provided.dragHandleProps}
                                                />
                                            )}
                                        </Draggable>
                                    ))}
                                    {provided.placeholder}
                                </ModulesList>
                            )}
                        </Droppable>
                    )}
                </DragDropContext>
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
    font-size: 1.8em;
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

const FilterCheckbox = styled(Checkbox)`
    margin-left: 10px;
    margin-top: 2px;
`;

const CreateModuleButton = styled(ModuleFeature.UI.CreateModuleButton)`
    margin-right: 10px;
`;

const ModulesList = styled.div`
    background-color: #ffffff;
    display: flex;
    flex-direction: column;
`;
