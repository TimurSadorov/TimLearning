import { CourseEntity } from '@entities';
import { Config, SiteLocalStorage } from '@shared';
import { sample } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';
import { useCallback, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const UserCourseGate = createGate<string>();

sample({
    clock: UserCourseGate.open,
    target: CourseEntity.Model.getUserCourseFx,
});

export const useRedirectOnLesson = (courseId: string) => {
    const navigate = useNavigate();
    const toLesson = useCallback(
        (lessonId: string) => navigate(Config.routes.userLesson.getLink(lessonId), { replace: true }),
        [navigate],
    );

    useGate(UserCourseGate, courseId);
    const userCourse = useUnit(CourseEntity.Model.$userCourse);

    useEffect(() => {
        if (!!userCourse) {
            const cachedSelectedLessonsId = SiteLocalStorage.getSelectedLessonIdInCourse(courseId);
            if (
                !!cachedSelectedLessonsId &&
                userCourse.modules.some((m) => m.lessons.some((l) => l.id == cachedSelectedLessonsId))
            ) {
                toLesson(cachedSelectedLessonsId);
                navigate(Config.routes.userLesson.getLink(cachedSelectedLessonsId), { replace: true });
                return;
            }

            const lessonId = userCourse.modules.find((m) => m.lessons.length !== 0)!.lessons[0].id;
            toLesson(lessonId);
        }
    }, [userCourse]);

    return { userCourse };
};
