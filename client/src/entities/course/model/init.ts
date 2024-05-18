import { sample } from 'effector';
import { EditableCourseGate, EditableCoursesGate, UserCoursesGate, updateUserCourse } from './model';
import {
    createCourseFx,
    findCoursesFx,
    getAllUserCoursesFx,
    getEditableCourseFx,
    getUserCourseFx,
    updateCourseFx,
} from './effects';

sample({
    clock: UserCoursesGate.open,
    target: getAllUserCoursesFx,
});

sample({
    clock: updateUserCourse,
    target: getUserCourseFx,
});

sample({
    clock: EditableCoursesGate.state,
    target: findCoursesFx,
});

sample({
    clock: EditableCourseGate.open,
    target: getEditableCourseFx,
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
