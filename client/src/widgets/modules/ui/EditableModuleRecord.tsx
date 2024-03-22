import React, { forwardRef } from 'react';
import { ModuleEntity } from '@entities';
import { DraggableProvidedDragHandleProps, DraggableProvidedDraggableProps } from 'react-beautiful-dnd';
import styled from 'styled-components';
import { ModuleFeature } from '@features';

export interface Props {
    module: ModuleEntity.Type.EditableModule;
    draggableProps?: DraggableProvidedDraggableProps;
    dragHandleProps?: DraggableProvidedDragHandleProps | null;
}

export const EditableModuleRecord = forwardRef<HTMLDivElement, Props>(function EditableModuleRecord(
    { module, dragHandleProps, draggableProps },
    ref,
) {
    return (
        <ModuleContainer ref={ref} {...draggableProps} {...dragHandleProps}>
            <ModuleName> {module.name}</ModuleName>
            <Buttons>
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
        </ModuleContainer>
    );
});

const ModuleContainer = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 15px 0px 15px 30px;
    border-radius: 10px;

    cursor: pointer;
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
