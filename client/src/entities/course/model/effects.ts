import { Api } from '@shared';
import { createEffect } from 'effector';

export const getAllUserCoursesFx = createEffect(async () => await Api.Services.CourseService.getUserCourses());

export const findCoursesFx = createEffect(
    async (request: Api.Services.FindCoursesQueryParams = {}) =>
        await Api.Services.CourseService.findCourses(
            request.id,
            request.searchName,
            request.isDraft,
            request.isDeleted,
        ),
);

export const getUserCourseFx = createEffect(async (courseId: string) => {
    return await Api.Services.CourseService.getUserCourseAllData(courseId);
});

export const getEditableCourseFx = createEffect(async (courseId: string) => {
    const courses = await Api.Services.CourseService.findCourses(courseId);
    return courses.at(0) ?? null;
});

export const createCourseFx = createEffect(
    async (request: Api.Services.CreateCourseRequest) => await Api.Services.CourseService.createCourse(request),
);

export const updateCourseFx = createEffect(
    async (request: { courseId: string; data: Api.Services.UpdateCourseRequest }) =>
        await Api.Services.CourseService.updateCourse(request.courseId, request.data),
);
