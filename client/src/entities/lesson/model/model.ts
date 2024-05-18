import { createEvent, createStore, restore } from 'effector';
import { createGate } from 'effector-react';
import { LessonSystemData, UserExerciseTestingResult, UserLesson } from '../types';
import {
    getDeletedLessonsFx,
    getLessonWithExerciseFx,
    getOrderedLessonsFx,
    getUserLessonFx,
    testUserLessonExerciseFx,
} from './effects';
import { restoreFail } from '@shared';
import { or } from 'patronum';

export const UserLessonGate = createGate<string>();
export const updateUserLesson = createEvent();
export const $userLesson = restore<UserLesson>(getUserLessonFx, null).reset(UserLessonGate.close);

export type SystemLessonsFilters = { moduleId: string; isDeleted: boolean };

export const updateSystemLessons = createEvent();
export const LessonSystemDataGate = createGate<SystemLessonsFilters>();

export const $systemLessons = createStore<LessonSystemData[] | null>(null).reset(LessonSystemDataGate.close);
export const $systemLessonsLoading = or(getOrderedLessonsFx.pending, getDeletedLessonsFx.pending);
export const $errorOnGetSystemLessons = restoreFail<Error | null>(null, getOrderedLessonsFx, getDeletedLessonsFx).reset(
    LessonSystemDataGate.close,
);

export const LessonWithExerciseGate = createGate<string>();
export const $lessonWithExercise = restore(getLessonWithExerciseFx, null).reset(LessonSystemDataGate.close);

export const UserExerciseTestingResultGate = createGate<UserExerciseTestingResult>();
export const $userExerciseTestingResult = restore(testUserLessonExerciseFx, null).reset(
    UserExerciseTestingResultGate.close,
);
