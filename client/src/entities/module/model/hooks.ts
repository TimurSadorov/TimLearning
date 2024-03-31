import { useGate, useUnit } from 'effector-react';
import { WithCourseId, findOrderedModulesFx, getModuleAllDataFx } from './effects';
import {
    $editableOrderedModules,
    $errorOnFindOrderedModules,
    $moduleAllData,
    EditableModulesGate,
    ModuleAllDataGate,
} from './model';
import { Api } from '@shared';

export const useEditableOrderedModules = (request: WithCourseId<Api.Services.FindOrderedModulesQueryParams>) => {
    useGate(EditableModulesGate, request);
    const editableOrderedModules = useUnit($editableOrderedModules);
    const isLoading = useUnit(findOrderedModulesFx.pending);
    const error = useUnit($errorOnFindOrderedModules);

    return { editableOrderedModules, isLoading, error };
};

export const useModuleAllData = (moduleId: string) => {
    useGate(ModuleAllDataGate, moduleId);
    const moduleAllData = useUnit($moduleAllData);
    const isLoading = useUnit(getModuleAllDataFx.pending);

    return { moduleAllData, isLoading };
};
