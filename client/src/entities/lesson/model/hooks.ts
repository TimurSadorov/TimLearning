import { useGate, useUnit } from 'effector-react';
import {
    $errorOnGetSystemLessons,
    $systemLessons,
    $systemLessonsLoading,
    LessonSystemDataGate,
    SystemLessonsFilters,
} from './model';

export const useLessonsSystemData = (request: SystemLessonsFilters) => {
    useGate(LessonSystemDataGate, request);
    const lessons = useUnit($systemLessons);
    const isLoading = useUnit($systemLessonsLoading);
    const error = useUnit($errorOnGetSystemLessons);

    return { lessons, isLoading, error };
};
