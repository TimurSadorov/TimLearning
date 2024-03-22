import { restore } from 'effector';
import { createGate } from 'effector-react';
import { WithCourseId, findOrderedModulesFx } from './effects';
import { EditableModule } from '../types';
import { Api } from '@shared';

export const EditableModulesGate = createGate<WithCourseId<Api.Services.FindOrderedModulesRequest>>();
export const $editableOrderedModules = restore<EditableModule[]>(findOrderedModulesFx, []).reset(
    EditableModulesGate.close,
);
