import { Api } from '@shared';
import { createEffect } from 'effector';
import { UserLessonExerciseTesting } from '../types';

export type WithModuleId<TData> = { moduleId: string; data: TData };

export const getUserLessonFx = createEffect(async (lessonId: string) => {
    return await Api.Services.LessonService.getUserLesson(lessonId);
});

export const getOrderedLessonsFx = createEffect(async (moduleId: string) => {
    if (!moduleId) {
        return null;
    }

    return await Api.Services.LessonService.getOrderedLessons(moduleId);
});

export const getUserLessonExerciseAppFile = createEffect(async (lessonId: string) => {
    return await Api.Services.LessonService.getUserLessonExerciseAppFile(lessonId);
});

export const getDeletedLessonsFx = createEffect(async (moduleId: string) => {
    if (!moduleId) {
        return null;
    }

    return await Api.Services.LessonService.getDeletedLessons(moduleId);
});

export const getLessonWithExerciseFx = createEffect(async (lessonId: string) => {
    return await Api.Services.LessonService.getLessonWithExercise(lessonId);
});

export const createLessonFx = createEffect(
    async (request: Api.Services.CreateLessonRequest & { moduleId: string }) =>
        await Api.Services.LessonService.createLesson(request.moduleId, { name: request.name }),
);

export type WithLessonId<TData> = { lessonId: string; data: TData };

export const updateLessonFx = createEffect(
    async (request: WithLessonId<Api.Services.UpdateLessonRequest>) =>
        await Api.Services.LessonService.updateLesson(request.lessonId, request.data),
);

export const moveLessonFx = createEffect(async (request: WithLessonId<Api.Services.MoveLessonRequest>) => {
    return await Api.Services.LessonService.moveLesson(request.lessonId, request.data);
});

export const deleteLessonFx = createEffect(
    async (lessonId: string) => await Api.Services.LessonService.deleteLesson(lessonId),
);

export const restoreLessonFx = createEffect(
    async (lessonId: string) => await Api.Services.LessonService.restoreLesson(lessonId),
);

export const testUserLessonExerciseFx = createEffect(
    async (tesingData: UserLessonExerciseTesting) =>
        await Api.Services.LessonService.testUserLessonExercise(tesingData.lessonId, { code: tesingData.code }),
);
