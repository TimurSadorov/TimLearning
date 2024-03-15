import { sample } from 'effector';
import { EditableCoursesGate, UserCoursesGate } from './model';
import { createCourseFx, findCoursesFx, getAllCoursesFx, updateCourseFx } from './effects';

sample({
    clock: UserCoursesGate.open,
    target: getAllCoursesFx,
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
