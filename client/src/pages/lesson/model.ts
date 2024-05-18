import { CourseEntity, LessonEntity, UserProgressEntity } from '@entities';
import { SiteLocalStorage } from '@shared';
import { LessonWidget } from '@widgets';
import { sample } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';
import { useCallback, useEffect, useMemo, useState } from 'react';

const UserCourseGate = createGate();

sample({
    clock: UserCourseGate.close,
    target: CourseEntity.Model.resetUserCourse,
});

export const useUserLessonPage = (lessonId: string) => {
    const { userLesson, isLoading: userLessonLoading, updateUserLesson } = LessonEntity.Model.useUserLesson(lessonId);

    useGate(UserCourseGate);
    const userCourse = useUnit(CourseEntity.Model.$userCourse);

    useEffect(() => {
        if (!userCourse && !!userLesson) {
            CourseEntity.Model.getUserCourseFx(userLesson.courseId);
        }
    }, [userLesson]);

    const onCompleteLesson = useCallback(
        async (lesson: LessonEntity.Type.UserLesson) => {
            CourseEntity.Model.updateUserCourse(lesson.courseId);
        },
        [CourseEntity.Model.updateUserCourse, updateUserLesson],
    );

    const currentModule = useMemo(
        () => userCourse?.modules.find((m) => m.lessons.some((l) => l.id === userLesson?.id)),
        [userCourse, userLesson?.id],
    );

    const visitLesson = useCallback(
        async (lesson: LessonEntity.Type.UserLesson) => {
            SiteLocalStorage.setSelectedLessonInCourse(lesson.courseId, lesson.id);

            const currentLesson = currentModule?.lessons.find((l) => l.id === lesson?.id);
            if (!!currentLesson && !currentLesson.userProgress) {
                await UserProgressEntity.Model.visitLesson(lesson.id);
                CourseEntity.Model.updateUserCourse(lesson.courseId);
            }
        },
        [UserProgressEntity.Model.visitLesson, CourseEntity.Model.updateUserCourse, currentModule],
    );

    const [selectedNavigationModuleId, setSelectedNavigationModuleId] = useState<string>();
    useEffect(() => {
        setSelectedNavigationModuleId(currentModule?.id);
    }, [currentModule?.id, userLesson]);

    const selectedNavigationModule = useMemo(
        () => userCourse?.modules.find((m) => m.id === selectedNavigationModuleId),
        [userCourse, selectedNavigationModuleId],
    );

    const currentLessonIndex = useMemo(() => {
        const index = currentModule?.lessons.findIndex((l) => l.id === userLesson?.id);
        return index === -1 ? undefined : index;
    }, [currentModule, userLesson?.id]);
    const previousLesson = useMemo<LessonWidget.UI.UserLessonContentProps['previousLesson'] | undefined>(() => {
        if (!userCourse || !userLesson?.id || currentLessonIndex === undefined || !currentModule) {
            return undefined;
        }

        if (currentLessonIndex !== 0) {
            return { inCurrentModule: true, lessonId: currentModule.lessons[currentLessonIndex - 1].id };
        }

        const previousModule = userCourse.modules.find(
            (_, index, modules) => modules.at(index + 1)?.id === currentModule.id,
        );
        return !previousModule
            ? null
            : { inCurrentModule: false, lessonId: previousModule.lessons[previousModule.lessons.length - 1].id };
    }, [userCourse, currentModule, currentLessonIndex, userLesson?.id]);

    const nextLesson = useMemo<LessonWidget.UI.UserLessonContentProps['nextLesson'] | undefined>(() => {
        if (!userCourse || !userLesson?.id || currentLessonIndex === undefined || !currentModule) {
            return undefined;
        }

        if (currentLessonIndex + 1 < currentModule.lessons.length) {
            return { inCurrentModule: true, lessonId: currentModule.lessons[currentLessonIndex + 1].id };
        }

        const nextModule = userCourse.modules.find(
            (_, index, modules) => index > 0 && modules.at(index - 1)?.id === currentModule.id,
        );
        return !nextModule ? null : { inCurrentModule: false, lessonId: nextModule.lessons[0].id };
    }, [userCourse, currentModule, currentLessonIndex, userLesson?.id]);

    return {
        userCourse,
        userLesson,
        userLessonLoading,
        currentModule,
        selectedNavigationModule,
        setSelectedNavigationModuleId,
        previousLesson,
        nextLesson,
        visitLesson,
        onCompleteLesson,
    };
};

type SiderType = 'modules' | 'lessons';

export const useSiderSwitch = () => {
    const [siderType, setSiderType] = useState<SiderType>('lessons');

    return { siderType, setSiderType };
};
