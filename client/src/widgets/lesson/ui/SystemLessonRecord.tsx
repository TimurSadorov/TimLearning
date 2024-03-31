import React, { forwardRef, useCallback } from 'react';
import { LessonEntity } from '@entities';
import { DraggableProvidedDragHandleProps, DraggableProvidedDraggableProps } from 'react-beautiful-dnd';
import styled from 'styled-components';
import { LessonFeature } from '@features';

export interface Props {
    lesson: LessonEntity.Type.LessonSystemData;
    isActive: boolean;
    isEditable: boolean;
    draggableProps?: DraggableProvidedDraggableProps;
    dragHandleProps?: DraggableProvidedDragHandleProps | null;
    onClick?: React.MouseEventHandler<HTMLDivElement>;
}

export const SystemLessonRecord = forwardRef<HTMLDivElement, Props>(function SystemLessonRecord(
    { lesson, isActive, isEditable, dragHandleProps, draggableProps, onClick },
    ref,
) {
    const onBottonsClick = useCallback<React.MouseEventHandler<HTMLElement>>((e) => {
        e.stopPropagation();
    }, []);

    return (
        <LessonContainer $isActive={isActive} onClick={onClick} ref={ref} {...draggableProps} {...dragHandleProps}>
            <LessonName> {lesson.name}</LessonName>
            {isEditable ? (
                <Buttons onClick={onBottonsClick}>
                    <LessonFeature.UI.UpdateLessonButton lesson={lesson} />
                    {lesson.isDraft ? (
                        <LessonFeature.UI.PublishLessonButton lessonId={lesson.id} />
                    ) : (
                        <LessonFeature.UI.DraftLessonButton lessonId={lesson.id} />
                    )}
                    {lesson.isDeleted ? (
                        <LessonFeature.UI.RestoreLessonButton lessonId={lesson.id} />
                    ) : (
                        <LessonFeature.UI.DeleteLessonButton lessonId={lesson.id} />
                    )}
                </Buttons>
            ) : (
                <></>
            )}
        </LessonContainer>
    );
});

const LessonContainer = styled.div<{ $isActive: boolean }>`
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 15px 0px 15px 30px;
    border-radius: 10px;
    cursor: pointer;
    background-color: ${(props) => (props.$isActive ? '#e7e7e7' : '#ffffff')};
    transition: background-color 0.3s;
    &:hover {
        background-color: #bfdcfe;
    }
`;

const LessonName = styled.div`
    font-weight: 600;
    font-size: 1.2em;
`;

const Buttons = styled.div`
    display: flex;
    margin-right: 10px;
    grid-column-gap: 5px;
`;
