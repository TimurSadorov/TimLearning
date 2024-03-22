import { Api } from '@shared';
import { createEffect } from 'effector';

export const getAllUserCoursesFx = createEffect(async () => await Api.Services.CourseService.getUserCourses());

export const getUserCourseFx = createEffect(async (courseId: string) => {
    const courses = await Api.Services.CourseService.getUserCourses(courseId);
    return courses.at(0) ?? null;
});

export const findCoursesFx = createEffect(
    async (requestBody: Api.Services.FindCoursesRequest = {}) =>
        await Api.Services.CourseService.findCourses(requestBody),
);

export const getEditableCourseFx = createEffect(async (courseId: string) => {
    const courses = await Api.Services.CourseService.findCourses({ id: courseId });
    return courses.at(0) ?? null;
});

export const createCourseFx = createEffect(
    async (request: Api.Services.CreateCourseRequest) => await Api.Services.CourseService.createCourse(request),
);

export const updateCourseFx = createEffect(
    async (request: { courseId: string; data: Api.Services.UpdateCourseRequest }) =>
        await Api.Services.CourseService.updateCourse(request.courseId, request.data),
);
