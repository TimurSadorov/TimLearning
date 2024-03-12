import { useGate, useUnit } from 'effector-react';
import { $editableCourses, $userCourses, EditableCoursesGate, UserCoursesGate } from './model';
import { findCoursesFx, getAllCoursesFx } from './effects';
import { Api } from '@shared';

export const useUserCourses = () => {
    useGate(UserCoursesGate);
    const userCourses = useUnit($userCourses);
    const isLoading = useUnit(getAllCoursesFx.pending);

    return { userCourses, isLoading };
};

export const useEditableCourses = (request: Api.Services.FindCoursesRequest) => {
    useGate(EditableCoursesGate, request);
    const editableCourses = useUnit($editableCourses);
    const isLoading = useUnit(findCoursesFx.pending);

    return { editableCourses, isLoading };
};
