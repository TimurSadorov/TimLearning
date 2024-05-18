import { createEvent, restore } from 'effector';
import { createGate } from 'effector-react';
import { EditableCours, UserCours, UserCoursAllData } from '../types';
import { findCoursesFx, getAllUserCoursesFx, getEditableCourseFx, getUserCourseFx } from './effects';
import { Api } from '@shared';

export const UserCoursesGate = createGate();
export const $userCourses = restore<UserCours[]>(getAllUserCoursesFx, []).reset(UserCoursesGate.close);

export const resetUserCourse = createEvent();
export const updateUserCourse = createEvent<string>();
export const $userCourse = restore<UserCoursAllData>(getUserCourseFx, null).reset(resetUserCourse);

export const EditableCoursesGate = createGate<Api.Services.FindCoursesQueryParams>();
export const $editableCourses = restore<EditableCours[]>(findCoursesFx, []).reset(EditableCoursesGate.close);

export const EditableCourseGate = createGate<string>();
export const $editableCourse = restore<EditableCours | null>(getEditableCourseFx, null).reset(EditableCourseGate.close);
