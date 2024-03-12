import { Api } from '@shared';
import { createEffect } from 'effector';

export const getAllCoursesFx = createEffect(async () => await Api.Services.CourseService.getAllCourses());

export const findCoursesFx = createEffect(
    async (requestBody: Api.Services.FindCoursesRequest = {}) =>
        await Api.Services.CourseService.findCourses(requestBody),
);
