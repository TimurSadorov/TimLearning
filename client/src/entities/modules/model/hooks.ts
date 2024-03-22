import { useGate, useUnit } from 'effector-react';
import { WithCourseId, findOrderedModulesFx } from './effects';
import { $editableOrderedModules, EditableModulesGate } from './model';
import { Api } from '@shared';

export const useEditableOrderedModules = (request: WithCourseId<Api.Services.FindOrderedModulesRequest>) => {
    useGate(EditableModulesGate, request);
    const editableOrderedModules = useUnit($editableOrderedModules);
    const isLoading = useUnit(findOrderedModulesFx.pending);

    return { editableOrderedModules, isLoading };
};
