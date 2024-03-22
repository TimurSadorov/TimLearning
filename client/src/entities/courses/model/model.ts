import { restore } from 'effector';
import { createGate } from 'effector-react';
import { EditableCours, UserCours } from '../types';
import { findCoursesFx, getAllUserCoursesFx, getEditableCourseFx, getUserCourseFx } from './effects';
import { Api } from '@shared';

export const UserCoursesGate = createGate();
export const $userCourses = restore<UserCours[]>(getAllUserCoursesFx, []).reset(UserCoursesGate.close);

export const UserCourseGate = createGate<string>();
export const $userCourse = restore<UserCours | null>(getUserCourseFx, null).reset(UserCourseGate.close);

export const EditableCoursesGate = createGate<Api.Services.FindCoursesRequest>();
export const $editableCourses = restore<EditableCours[]>(findCoursesFx, []).reset(EditableCoursesGate.close);

export const EditableCourseGate = createGate<string>();
export const $editableCourse = restore<EditableCours | null>(getEditableCourseFx, null).reset(EditableCourseGate.close);
