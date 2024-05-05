import { useGate, useUnit } from 'effector-react';
import {
    $errorOnGetSystemLessons,
    $lessonWithExercise,
    $systemLessons,
    $systemLessonsLoading,
    LessonSystemDataGate,
    LessonWithExerciseGate,
    SystemLessonsFilters,
} from './model';
import { getLessonWithExerciseFx } from './effects';

export const useLessonsSystemData = (request: SystemLessonsFilters) => {
    useGate(LessonSystemDataGate, request);
    const lessons = useUnit($systemLessons);
    const isLoading = useUnit($systemLessonsLoading);
    const error = useUnit($errorOnGetSystemLessons);

    return { lessons, isLoading, error };
};

export const useLessonWithExercise = (lessonId: string) => {
    useGate(LessonWithExerciseGate, lessonId);
    const lessonWithExercise = useUnit($lessonWithExercise);
    const isLoading = useUnit(getLessonWithExerciseFx.pending);

    return { lessonWithExercise, isLoading };
};
