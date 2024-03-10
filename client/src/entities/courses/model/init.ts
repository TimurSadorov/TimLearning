import { sample } from 'effector';
import { UserCoursesGate } from './model';
import { getAllCoursesFx } from './effects';

sample({
    clock: UserCoursesGate.open,
    target: getAllCoursesFx,
});
