import { useGate, useUnit } from 'effector-react';
import {
    $editableCourse,
    $editableCourses,
    $userCourses,
    EditableCourseGate,
    EditableCoursesGate,
    UserCoursesGate,
} from './model';
import { findCoursesFx, getAllUserCoursesFx, getEditableCourseFx } from './effects';
import { Api } from '@shared';

export const useUserCourses = () => {
    useGate(UserCoursesGate);
    const userCourses = useUnit($userCourses);
    const isLoading = useUnit(getAllUserCoursesFx.pending);

    return { userCourses, isLoading };
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
