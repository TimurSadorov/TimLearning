import { useGate, useUnit } from 'effector-react';
import { WithCourseId, findOrderedModulesFx } from './effects';
import { $editableOrderedModules, $errorOnFindOrderedModules, EditableModulesGate } from './model';
import { Api } from '@shared';

export const useEditableOrderedModules = (request: WithCourseId<Api.Services.FindOrderedModulesQueryParams>) => {
    useGate(EditableModulesGate, request);
    const editableOrderedModules = useUnit($editableOrderedModules);
    const isLoading = useUnit(findOrderedModulesFx.pending);
    const error = useUnit($errorOnFindOrderedModules);

    return { editableOrderedModules, isLoading, error };
};
