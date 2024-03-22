import { sample } from 'effector';
import { EditableCoursesGate, UserCourseGate, UserCoursesGate } from './model';
import { createCourseFx, findCoursesFx, getAllUserCoursesFx, getUserCourseFx, updateCourseFx } from './effects';

sample({
    clock: UserCoursesGate.open,
    target: getAllUserCoursesFx,
});

sample({
    clock: UserCourseGate.open,
    target: getUserCourseFx,
});

sample({
    clock: EditableCoursesGate.state,
    target: findCoursesFx,
});

sample({
    clock: createCourseFx.done,
    source: EditableCoursesGate.state,
    target: findCoursesFx,
});

sample({
    clock: updateCourseFx.done,
    source: EditableCoursesGate.state,
    target: findCoursesFx,
});
