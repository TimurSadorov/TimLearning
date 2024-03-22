import { restore } from 'effector';
import { createGate } from 'effector-react';
import { EditableCours, UserCours, UserCoursFullData } from '../types';
import { findCoursesFx, getAllUserCoursesFx, getUserCourseFx } from './effects';
import { Api } from '@shared';

export const UserCoursesGate = createGate();
export const $userCourses = restore<UserCours[]>(getAllUserCoursesFx, []).reset(UserCoursesGate.close);

export const UserCourseGate = createGate<string>();
export const $userCourse = restore<UserCoursFullData>(getUserCourseFx, null).reset(UserCourseGate.close);

export const EditableCoursesGate = createGate<Api.Services.FindCoursesRequest>();
export const $editableCourses = restore<EditableCours[]>(findCoursesFx, []).reset(EditableCoursesGate.close);
