import { Api } from '@shared';
import { createEffect } from 'effector';

export const getAllCoursesFx = createEffect(async () => await Api.Services.CourseService.getAllCourses());

export const findCoursesFx = createEffect(
    async (requestBody: Api.Services.FindCoursesRequest = {}) =>
        await Api.Services.CourseService.findCourses(requestBody),
);

export const createCourseFx = createEffect(
    async (request: Api.Services.CreateCourseRequest) => await Api.Services.CourseService.createCourse(request),
);
