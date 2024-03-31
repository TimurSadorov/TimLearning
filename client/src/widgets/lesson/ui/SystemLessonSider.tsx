import React, { useCallback } from 'react';
import { ModuleEntity } from '@entities';
import { LessonFeature } from '@features';
import { Button, Checkbox, Layout, Skeleton, Tooltip } from 'antd';
import { DragDropContext, Draggable, Droppable, OnDragEndResponder } from 'react-beautiful-dnd';
import styled from 'styled-components';
import { Link, useNavigate } from 'react-router-dom';
import { Config, SharedUI } from '@shared';
import { SystemLessonRecord } from './SystemLessonRecord';
import { EditOutlined } from '@ant-design/icons';

type Props = { moduleId: string; isEditable: boolean; selectedLessonId?: string };

export const SystemLessonSider = ({ moduleId, isEditable, selectedLessonId }: Props) => {
    const { moduleAllData, isLoading: moduleLoading } = ModuleEntity.Model.useModuleAllData(moduleId);

    const { lessons, isLoading, isDeleted, onChangeIsDeleted } =
        LessonFeature.Model.useFilterLessonsSystemData(moduleId);

    const { moveLesson, lessonMovingLoading } = LessonFeature.Model.useMoveLesson();
    const onDragEnd = useCallback<OnDragEndResponder>(
        (result) => {
            if (!!result.destination && !!lessons && result.source.index !== result.destination.index) {
                const newNextLessonIndex =
                    result.source.index > result.destination.index
                        ? result.destination.index
                        : result.destination.index + 1;
                const newNextLesson = lessons.at(newNextLessonIndex);
                moveLesson({ lessonId: result.draggableId, data: { nextLessonId: newNextLesson?.id } });
            }
        },
        [moveLesson, lessons],
    );

    const navigate = useNavigate();
    const toEditableLessons = useCallback(() => navigate(Config.routes.editableLessons.getLink(moduleId)), [navigate]);
    const toEditableLesson = useCallback(
        (lessonId: string) => {
            navigate(Config.routes.editableLesson.getLink(moduleId, lessonId));
        },
        [navigate],
    );

    return (
        <Layout.Sider theme="light" width="400px">
            <LayoutSider>
                <SiderHeader>
                    {!moduleAllData || moduleLoading ? (
                        <Skeleton.Input active />
                    ) : (
                        <>
                            <BackLink to={Config.routes.editableModules.getLink(moduleAllData.courseId)}>
                                Вернуться к модулям
                            </BackLink>
                            <HeaderText>{moduleAllData.name}</HeaderText>
                        </>
                    )}
                </SiderHeader>
                <FeaturesBlock>
                    <FiltersBlock>
                        <FilterWord>Фильтры:</FilterWord>
                        <FilterCheckbox onChange={(e) => onChangeIsDeleted(e.target.checked)} checked={isDeleted}>
                            Удален
                        </FilterCheckbox>
                    </FiltersBlock>
                    {isEditable ? (
                        <CreateLessonButton moduleId={moduleId} />
                    ) : (
                        <ToEditableButton>
                            <Tooltip placement="top" title={'Режим редактирования'}>
                                <Button icon={<EditOutlined />} onClick={toEditableLessons} />
                            </Tooltip>
                        </ToEditableButton>
                    )}
                </FeaturesBlock>
                <DragDropContext enableDefaultSensors onDragEnd={onDragEnd}>
                    {!lessons || isLoading || lessonMovingLoading ? (
                        <SharedUI.Loader />
                    ) : (
                        <Droppable droppableId="lessons">
                            {(provided) => (
                                <LessonsList ref={provided.innerRef} {...provided.droppableProps}>
                                    {lessons.map((lesson, index) => (
                                        <Draggable
                                            isDragDisabled={!isEditable || isDeleted}
                                            key={lesson.id}
                                            draggableId={lesson.id}
                                            index={index}
                                        >
                                            {(provided) => (
                                                <SystemLessonRecord
                                                    lesson={lesson}
                                                    isActive={selectedLessonId === lesson.id}
                                                    isEditable={isEditable}
                                                    onClick={() => {
                                                        toEditableLesson(lesson.id);
                                                    }}
                                                    ref={provided.innerRef}
                                                    draggableProps={provided.draggableProps}
                                                    dragHandleProps={provided.dragHandleProps}
                                                />
                                            )}
                                        </Draggable>
                                    ))}
                                    {provided.placeholder}
                                </LessonsList>
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

const BackLink = styled(Link)`
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

const CreateLessonButton = styled(LessonFeature.UI.CreateLessonButton)`
    margin-right: 10px;
`;

const ToEditableButton = styled.div`
    margin-right: 10px;
`;

const LessonsList = styled.div`
    background-color: #ffffff;
    display: flex;
    flex-direction: column;
`;
