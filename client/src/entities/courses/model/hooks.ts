import { useGate, useUnit } from 'effector-react';
import {
    $editableCourses,
    $userCourse,
    $userCourses,
    EditableCoursesGate,
    UserCourseGate,
    UserCoursesGate,
} from './model';
import { findCoursesFx, getAllUserCoursesFx, getUserCourseFx } from './effects';
import { Api } from '@shared';

export const useUserCourses = () => {
    useGate(UserCoursesGate);
    const userCourses = useUnit($userCourses);
    const isLoading = useUnit(getAllUserCoursesFx.pending);

    return { userCourses, isLoading };
};

export const useUserCourse = (courseId: string) => {
    useGate(UserCourseGate, courseId);
    const userCourse = useUnit($userCourse);
    const isLoading = useUnit(getUserCourseFx.pending);

    return { userCourse, isLoading };
};

export const useEditableCourses = (request: Api.Services.FindCoursesRequest) => {
    useGate(EditableCoursesGate, request);
    const editableCourses = useUnit($editableCourses);
    const isLoading = useUnit(findCoursesFx.pending);

    return { editableCourses, isLoading };
};
