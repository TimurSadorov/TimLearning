import { useGate, useUnit } from 'effector-react';
import { $userCourses, UserCoursesGate } from './model';
import { getAllCoursesFx } from './effects';

export const useUserCourses = () => {
    useGate(UserCoursesGate);
    const userCourses = useUnit($userCourses);
    const isLoading = useUnit(getAllCoursesFx.pending);

    return { userCourses, isLoading };
};
