import React, { forwardRef, useCallback } from 'react';
import { ModuleEntity } from '@entities';
import { DraggableProvidedDragHandleProps, DraggableProvidedDraggableProps } from 'react-beautiful-dnd';
import styled from 'styled-components';
import { ModuleFeature } from '@features';

export interface Props {
    module: ModuleEntity.Type.EditableModule;
    isActive: boolean;
    isEditable: boolean;
    draggableProps?: DraggableProvidedDraggableProps;
    dragHandleProps?: DraggableProvidedDragHandleProps | null;
    onClick?: React.MouseEventHandler<HTMLDivElement>;
}

export const EditableModuleRecord = forwardRef<HTMLDivElement, Props>(function EditableModuleRecord(
    { module, isActive, isEditable, dragHandleProps, draggableProps, onClick },
    ref,
) {
    const onBottonsClick = useCallback<React.MouseEventHandler<HTMLElement>>((e) => {
        e.stopPropagation();
    }, []);

    return (
        <ModuleContainer $isActive={isActive} onClick={onClick} ref={ref} {...draggableProps} {...dragHandleProps}>
            <ModuleName> {module.name}</ModuleName>
            {isEditable ? (
                <Buttons onClick={onBottonsClick}>
                    <ModuleFeature.UI.UpdateModuleButton module={module} />
                    {module.isDraft ? (
                        <ModuleFeature.UI.PublishModuleButton moduleId={module.id} />
                    ) : (
                        <ModuleFeature.UI.DraftModuleButton moduleId={module.id} />
                    )}
                    {module.isDeleted ? (
                        <ModuleFeature.UI.RestoreModuleButton moduleId={module.id} />
                    ) : (
                        <ModuleFeature.UI.DeleteModuleButton moduleId={module.id} />
                    )}
                </Buttons>
            ) : (
                <></>
            )}
        </ModuleContainer>
    );
});

const ModuleContainer = styled.div<{ $isActive: boolean }>`
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

const ModuleName = styled.div`
    font-weight: 600;
    font-size: 1.2em;
`;

const Buttons = styled.div`
    display: flex;
    margin-right: 10px;
    grid-column-gap: 5px;
`;
