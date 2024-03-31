import { useGate, useUnit } from 'effector-react';
import {
    $editableCourse,
    $editableCourses,
    $userCourse,
    $userCourses,
    EditableCourseGate,
    EditableCoursesGate,
    UserCourseGate,
    UserCoursesGate,
} from './model';
import { findCoursesFx, getAllUserCoursesFx, getEditableCourseFx, getUserCourseFx } from './effects';
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

export const useEditableCourses = (request: Api.Services.FindCoursesQueryParams) => {
    useGate(EditableCoursesGate, request);
    const editableCourses = useUnit($editableCourses);
    const isLoading = useUnit(findCoursesFx.pending);

    return { editableCourses, isLoading };
};

export const useEditableCourse = (courseId: string) => {
    useGate(EditableCourseGate, courseId);
    const editableCourse = useUnit($editableCourse);
    const isLoading = useUnit(getEditableCourseFx.pending);

    return { editableCourse, isLoading };
};
