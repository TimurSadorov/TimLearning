import { createEvent, createStore, restore } from 'effector';
import { createGate } from 'effector-react';
import { LessonSystemData } from '../types';
import { getDeletedLessonsFx, getLessonWithExerciseFx, getOrderedLessonsFx } from './effects';
import { restoreFail } from '@shared';
import { or } from 'patronum';

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
