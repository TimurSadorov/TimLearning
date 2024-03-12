import { sample } from 'effector';
import { EditableCoursesGate, UserCoursesGate } from './model';
import { findCoursesFx, getAllCoursesFx } from './effects';

sample({
    clock: UserCoursesGate.open,
    target: getAllCoursesFx,
});

sample({
    clock: EditableCoursesGate.state,
    target: findCoursesFx,
});
