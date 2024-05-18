import { sample } from 'effector';
import {
    $systemLessons,
    LessonSystemDataGate,
    LessonWithExerciseGate,
    UserLessonGate,
    updateSystemLessons,
    updateUserLesson,
} from './model';
import {
    getOrderedLessonsFx,
    getDeletedLessonsFx,
    createLessonFx,
    updateLessonFx,
    moveLessonFx,
    deleteLessonFx,
    restoreLessonFx,
    getLessonWithExerciseFx,
    getUserLessonFx,
} from './effects';

sample({
    clock: [updateUserLesson, UserLessonGate.state],
    source: UserLessonGate.state,
    filter: (v) => typeof v === 'string',
    target: getUserLessonFx,
});

sample({
    clock: [LessonSystemDataGate.state, updateSystemLessons],
    source: LessonSystemDataGate.state,
    filter: (request) => request.isDeleted === false,
    fn: (request) => request.moduleId,
    target: getOrderedLessonsFx,
});

sample({
    clock: [LessonSystemDataGate.state, updateSystemLessons],
    source: LessonSystemDataGate.state,
    filter: (request) => request.isDeleted,
    fn: (request) => request.moduleId,
    target: getDeletedLessonsFx,
});

sample({
    clock: getOrderedLessonsFx.doneData,
    fn: (lessons) => (!!lessons ? lessons.map((l) => ({ ...l, isDeleted: false })) : null),
    target: $systemLessons,
});

sample({
    clock: getDeletedLessonsFx.doneData,
    fn: (lessons) => (!!lessons ? lessons.map((l) => ({ ...l, isDeleted: true })) : null),
    target: $systemLessons,
});

sample({
    clock: createLessonFx.done,
    target: updateSystemLessons,
});

sample({
    clock: updateLessonFx.done,
    target: updateSystemLessons,
});

sample({
    clock: moveLessonFx.done,
    target: updateSystemLessons,
});

sample({
    clock: deleteLessonFx.done,
    target: updateSystemLessons,
});

sample({
    clock: restoreLessonFx.done,
    target: updateSystemLessons,
});

sample({
    source: LessonWithExerciseGate.state,
    filter: (lessonId) => !!lessonId && typeof lessonId === 'string',
    target: getLessonWithExerciseFx,
});
