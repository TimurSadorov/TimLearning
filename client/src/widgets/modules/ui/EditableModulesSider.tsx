import { CourseEntity } from '@entities';
import { ModuleFeature } from '@features';
import { Button, Checkbox, Layout, Tooltip } from 'antd';
import React, { useCallback } from 'react';
import { DragDropContext, Draggable, Droppable, OnDragEndResponder } from 'react-beautiful-dnd';
import { Loader } from 'shared/ui';
import styled from 'styled-components';
import { EditableModuleRecord } from './EditableModuleRecord';
import { ArrowLeftOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { Config } from '@shared';

type Props = { courseId: string };

export const EditableModulesSider = ({ courseId }: Props) => {
    const { userCourse } = CourseEntity.Model.useUserCourse(courseId);

    const { editableOrderedModules, isLoading, isDeleted, onChangeIsDeleted } =
        ModuleFeature.Model.useFilterEditableOrderedModules(courseId);

    const { changeOrder, changingOrderLoading } = ModuleFeature.Model.useСhangeModuleOrder();
    const onDragEnd = useCallback<OnDragEndResponder>(
        (result) => {
            if (!!result.destination) {
                changeOrder({ moduleId: result.draggableId, data: { order: result.destination.index + 1 } });
            }
        },
        [changeOrder],
    );

    const navigate = useNavigate();
    const toEditableCourses = useCallback(() => navigate(Config.routes.editableCourses.path), [navigate]);

    return (
        <Layout.Sider theme="light" width="400px">
            <Tooltip placement="bottom" title={'Вернуться к курсам'}>
                <BackButton icon={<ArrowLeftOutlined />} onClick={toEditableCourses} />
            </Tooltip>
            <LayoutSider>
                <SiderHeader>{userCourse?.shortName}</SiderHeader>
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
                    {isLoading && changingOrderLoading ? (
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

const BackButton = styled(Button)`
    position: absolute;
    right: -32px;
    border-radius: 0px 10px 10px 0;
    border: 0px;
    background-color: #ffffff;
`;

const LayoutSider = styled(Layout)`
    background-color: #ffffff;
    height: 100%;
`;

const SiderHeader = styled.div`
    border-bottom: 2px solid #bfdcfe;
    padding: 30px;
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
