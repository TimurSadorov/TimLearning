import { Api } from '@shared';
import { createEffect } from 'effector';

export const getAllCoursesFx = createEffect(async () => await Api.Services.CourseService.getAllCourses());
