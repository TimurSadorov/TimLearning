import { restore } from 'effector';
import { createGate } from 'effector-react';
import { WithCourseId, findOrderedModulesFx, getModuleAllDataFx } from './effects';
import { EditableModule } from '../types';
import { Api, restoreFail } from '@shared';

export const EditableModulesGate = createGate<WithCourseId<Api.Services.FindOrderedModulesQueryParams>>();
export const $editableOrderedModules = restore<EditableModule[] | null>(findOrderedModulesFx, null).reset(
    EditableModulesGate.close,
);
export const $errorOnFindOrderedModules = restoreFail(null, findOrderedModulesFx).reset(EditableModulesGate.close);

export const ModuleAllDataGate = createGate<string>();
export const $moduleAllData = restore(getModuleAllDataFx, null).reset(ModuleAllDataGate.close);
