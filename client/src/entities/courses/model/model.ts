import { restore } from 'effector';
import { createGate } from 'effector-react';
import { UserCourses } from '../types';
import { getAllCoursesFx } from './effects';

export const UserCoursesGate = createGate();
export const $userCourses = restore<UserCourses[]>(getAllCoursesFx, []).reset(UserCoursesGate.close);
