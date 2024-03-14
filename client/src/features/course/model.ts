import { CourseEntity } from '@entities';
import { Api } from '@shared';
import { useForm } from 'antd/es/form/Form';
import { createEvent, restore } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';
import { debounce, reset } from 'patronum';
import { useCallback, useState } from 'react';

const FilterEditableCourses = createGate();

const onChangeSearchName = createEvent<string>();
const $searchName = restore(onChangeSearchName, '');
const $debouncedSearchName = restore(debounce(onChangeSearchName, 300), '');

const onChangeIsDraft = createEvent<boolean>();
const $isDraft = restore(onChangeIsDraft, true);

const onChangeIsDeleted = createEvent<boolean>();
const $isDeleted = restore(onChangeIsDeleted, false);

reset({ clock: FilterEditableCourses.close, target: [$searchName, $isDraft, $isDeleted] });

export const useFilterEditableCourses = () => {
    useGate(FilterEditableCourses);
    const searchName = useUnit($searchName);
    const debouncedSearchName = useUnit($debouncedSearchName);
    const isDraft = useUnit($isDraft);
    const isDeleted = useUnit($isDeleted);

    const { isLoading, editableCourses } = CourseEntity.Model.useEditableCourses({
        searchName: debouncedSearchName,
        isDraft,
        isDeleted,
    });

    return {
        editableCourses,
        isLoading,
        onChangeSearchName,
        onChangeIsDraft,
        onChangeIsDeleted,
        searchName,
        isDraft,
        isDeleted,
    };
};

export type NewCourse = Api.Services.CreateCourseRequest;

export const useCreateCourseModal = () => {
    const [form] = useForm<NewCourse>();
    const [showModal, setShowModal] = useState(false);
    const onShowModal = useCallback(() => setShowModal(true), [setShowModal]);

    const onOkModal = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancelModal = useCallback(() => setShowModal(false), [setShowModal]);

    const loading = useUnit(CourseEntity.Model.createCourseFx.pending);
    const create = useCallback(async (form: NewCourse) => {
        await CourseEntity.Model.createCourseFx(form);
        setShowModal(false);
    }, []);

    return {
        form,
        loading,
        create,
        showModal,
        onShowModal,
        onOkModal,
        onCancelModal,
    };
};
