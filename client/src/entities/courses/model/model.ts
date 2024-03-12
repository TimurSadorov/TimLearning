import { restore } from 'effector';
import { createGate } from 'effector-react';
import { EditableCours, UserCours } from '../types';
import { findCoursesFx, getAllCoursesFx } from './effects';
import { Api } from '@shared';

export const UserCoursesGate = createGate();
export const $userCourses = restore<UserCours[]>(getAllCoursesFx, []).reset(UserCoursesGate.close);

export const EditableCoursesGate = createGate<Api.Services.FindCoursesRequest>();
export const $editableCourses = restore<EditableCours[]>(findCoursesFx, []).reset(EditableCoursesGate.close);
