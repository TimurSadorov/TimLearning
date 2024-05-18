import { LessonEntity } from '@entities';
import { Api } from '@shared';
import { useCallback, useEffect, useState } from 'react';

export type TestingResult = { isSuccess: boolean; error?: { status: string; text?: string | null } };

const getErrorStatusText = (status: Api.Services.UserExerciseTestingStatus) => {
    switch (status) {
        case Api.Services.UserExerciseTestingStatus.OK:
            throw new Error('Testing status not is error status.');
        case Api.Services.UserExerciseTestingStatus.SERVER_ERROR:
            return 'Задание сконфигурировано неверно, обратитесь в техподдержку!';
        case Api.Services.UserExerciseTestingStatus.USER_ERROR:
            return 'Ошибка проверки!';
        default:
            const exhaustiveCheck: never = status;
            throw new Error(exhaustiveCheck);
    }
};

export const useCodeForm = (
    lesson: LessonEntity.Type.UserLesson,
    onComplete?: (lesson: LessonEntity.Type.UserLesson) => void,
) => {
    const lastUserSolutionCode = lesson.exercise?.lastUserSolutionCode ?? undefined;
    const [code, setCode] = useState<string | undefined>(lastUserSolutionCode ?? undefined);
    useEffect(() => {
        setCode(lastUserSolutionCode);
    }, [lastUserSolutionCode]);
    const [lastSubmitedCode, setLastSubmitedCode] = useState<string>();

    const [isExerciseCompleted, setIsExerciseCompleted] = useState(false);
    useEffect(() => {
        setIsExerciseCompleted(!!lastUserSolutionCode);
    }, [lastUserSolutionCode]);
    const onTryAgain = useCallback(() => {
        setIsExerciseCompleted(false);
    }, [setIsExerciseCompleted]);

    const [formIsValid, setFormIsValid] = useState<boolean>(true);
    useEffect(() => {
        setFormIsValid(!!code);
    }, [code]);

    const { test, isTestingProcess, userExerciseTestingResult } = LessonEntity.Model.useUserLessonExerciseTesting(
        lesson.id,
    );

    const [testingResult, setTestingResult] = useState<TestingResult | null>(null);
    useEffect(() => {
        if (!userExerciseTestingResult) {
            return;
        }

        const isSuccess = userExerciseTestingResult.status === Api.Services.UserExerciseTestingStatus.OK;
        const result: TestingResult = {
            isSuccess: isSuccess,
            error: isSuccess
                ? undefined
                : {
                      status: getErrorStatusText(userExerciseTestingResult.status),
                      text: userExerciseTestingResult.errorMessage,
                  },
        };
        setTestingResult(result);

        if (isSuccess) {
            setIsExerciseCompleted(true);
            setCode(lastSubmitedCode);
            onComplete?.(lesson);
        }
    }, [userExerciseTestingResult, onComplete, setIsExerciseCompleted, setTestingResult, lesson]);

    const submit = useCallback(() => {
        if (!!code) {
            setTestingResult(null);
            setLastSubmitedCode(code);
            test(code);
        }
    }, [code, test]);

    return { code, setCode, submit, isTestingProcess, testingResult, formIsValid, isExerciseCompleted, onTryAgain };
};
