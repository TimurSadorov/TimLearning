import { Api } from '@shared';
import { createEffect } from 'effector';

export const getAllUserCoursesFx = createEffect(async () => await Api.Services.CourseService.getAllUserCourses());

export const getUserCourseFx = createEffect(
    async (courseId: string) => await Api.Services.CourseService.getUserCourse(courseId),
);

export const findCoursesFx = createEffect(
    async (requestBody: Api.Services.FindCoursesRequest = {}) =>
        await Api.Services.CourseService.findCourses(requestBody),
);

export const createCourseFx = createEffect(
    async (request: Api.Services.CreateCourseRequest) => await Api.Services.CourseService.createCourse(request),
);

export const updateCourseFx = createEffect(
    async (request: { courseId: string; data: Api.Services.UpdateCourseRequest }) =>
        await Api.Services.CourseService.updateCourse(request.courseId, request.data),
);
