import { Api } from '@shared';
import { createEffect } from 'effector';

export type WithCourseId<TData> = { courseId: string; data: TData };

export const findOrderedModulesFx = createEffect(
    async (request: WithCourseId<Api.Services.FindOrderedModulesQueryParams>) => {
        if (!request.courseId) {
            return null;
        }
        return await Api.Services.ModuleService.findOrderedModules(
            request.courseId,
            request.data.isDeleted,
            request.data.isDraft,
        );
    },
);

export const getModuleAllDataFx = createEffect(async (moduleId: string) => {
    return await Api.Services.ModuleService.getModuleAllData(moduleId);
});

export const createModuleFx = createEffect(
    async (request: WithCourseId<Api.Services.CreateModuleRequest>) =>
        await Api.Services.ModuleService.createModule(request.courseId, request.data),
);

export type WithModuleId<TData> = { moduleId: string; data: TData };

export const changeModuleOrderFx = createEffect(
    async (request: WithModuleId<Api.Services.ChangeModuleOrderRequest>) => {
        if (!request.moduleId) {
            return [];
        }
        return await Api.Services.ModuleService.changeModuleOrder(request.moduleId, request.data);
    },
);

export const updateModuleFx = createEffect(
    async (request: WithModuleId<Api.Services.UpdateModuleRequest>) =>
        await Api.Services.ModuleService.updateModule(request.moduleId, request.data),
);

export const deleteModuleFx = createEffect(
    async (moduleId: string) => await Api.Services.ModuleService.deleteModule(moduleId),
);

export const restoreModuleFx = createEffect(
    async (moduleId: string) => await Api.Services.ModuleService.restoreModule(moduleId),
);
