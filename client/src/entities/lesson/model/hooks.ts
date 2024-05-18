import { useGate, useUnit } from 'effector-react';
import {
    $errorOnGetSystemLessons,
    $lessonWithExercise,
    $systemLessons,
    $systemLessonsLoading,
    $userExerciseTestingResult,
    $userLesson,
    LessonSystemDataGate,
    LessonWithExerciseGate,
    SystemLessonsFilters,
    UserExerciseTestingResultGate,
    UserLessonGate,
    updateUserLesson,
} from './model';
import {
    getLessonWithExerciseFx,
    getUserLessonExerciseAppFile,
    getUserLessonFx,
    testUserLessonExerciseFx,
} from './effects';
import { useCallback } from 'react';

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

export const useUserLesson = (lessonId: string) => {
    useGate(UserLessonGate, lessonId);
    const userLesson = useUnit($userLesson);
    const isLoading = useUnit(getUserLessonFx.pending);

    return { userLesson, isLoading, updateUserLesson };
};

export const useUserLessonExerciseAppDownloading = (lessonId: string) => {
    const downloadApp = useCallback(async () => {
        const file = await getUserLessonExerciseAppFile(lessonId);
        window.location.href = file.downloadingUrl;
    }, [getUserLessonExerciseAppFile, lessonId]);

    return { downloadApp };
};

export const useUserLessonExerciseTesting = (lessonId: string) => {
    useGate(UserExerciseTestingResultGate);

    const userExerciseTestingResult = useUnit($userExerciseTestingResult);
    const isTestingProcess = useUnit(testUserLessonExerciseFx.pending);

    const test = useCallback(
        async (code: string) => {
            await testUserLessonExerciseFx({ code, lessonId });
        },
        [testUserLessonExerciseFx, lessonId],
    );

    return { test, isTestingProcess, userExerciseTestingResult };
};
