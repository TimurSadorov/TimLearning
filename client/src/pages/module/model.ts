import { createStore, sample } from 'effector';
import { createGate, useGate } from 'effector-react';
import { useParams } from 'react-router-dom';

export const CourseIdGate = createGate<string | null>();

export const $courseId = createStore<string | null>(null).reset(CourseIdGate.close);
sample({
    clock: CourseIdGate.state,
    filter: (state) => typeof state === 'string',
    target: $courseId,
});

export const useCourseIdParams = () => {
    const { courseId } = useParams<{ courseId: string }>();
    useGate(CourseIdGate, courseId ?? '');
};
