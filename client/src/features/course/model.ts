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
const $isDraft = restore(onChangeIsDraft, false);

const onChangeIsDeleted = createEvent<boolean>();
const $isDeleted = restore(onChangeIsDeleted, false);

const $draftFilterIsDisabled = $isDeleted;

reset({ clock: FilterEditableCourses.close, target: [$searchName, $debouncedSearchName, $isDraft, $isDeleted] });

export const useFilterEditableCourses = () => {
    useGate(FilterEditableCourses);
    const searchName = useUnit($searchName);
    const debouncedSearchName = useUnit($debouncedSearchName);
    const isDraft = useUnit($isDraft);
    const isDeleted = useUnit($isDeleted);
    const draftFilterIsDisabled = useUnit($draftFilterIsDisabled);

    const { isLoading, editableCourses } = CourseEntity.Model.useEditableCourses({
        searchName: debouncedSearchName,
        isDraft: draftFilterIsDisabled ? undefined : isDraft,
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
        draftFilterIsDisabled,
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
    const create = useCallback(async (f: NewCourse) => {
        await CourseEntity.Model.createCourseFx(f);
        setShowModal(false);
        form.resetFields();
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

export const useDeleteCourse = (courseId: string) => {
    const loading = useUnit(CourseEntity.Model.updateCourseFx.pending);
    const deleteCourse = useCallback(async () => {
        await CourseEntity.Model.updateCourseFx({ courseId: courseId, data: { isDeleted: true } });
    }, [courseId]);

    return {
        loading,
        deleteCourse,
    };
};

export const useRecoverCourse = (courseId: string) => {
    const loading = useUnit(CourseEntity.Model.updateCourseFx.pending);
    const recoverCourse = useCallback(async () => {
        await CourseEntity.Model.updateCourseFx({ courseId: courseId, data: { isDeleted: false } });
    }, [courseId]);

    return {
        loading,
        recoverCourse,
    };
};

export const usePublishCourse = (courseId: string) => {
    const loading = useUnit(CourseEntity.Model.updateCourseFx.pending);
    const publishCourse = useCallback(async () => {
        await CourseEntity.Model.updateCourseFx({ courseId: courseId, data: { isDraft: false } });
    }, [courseId]);

    return {
        loading,
        publishCourse,
    };
};

export const useDraftCourse = (courseId: string) => {
    const loading = useUnit(CourseEntity.Model.updateCourseFx.pending);
    const toDraft = useCallback(async () => {
        await CourseEntity.Model.updateCourseFx({ courseId: courseId, data: { isDraft: true } });
    }, [courseId]);

    return {
        loading,
        toDraft,
    };
};

export type CourseUpdating = Pick<Api.Services.UpdateCourseRequest, 'name' | 'shortName' | 'description'>;

export const useUpdateCourseModal = (courseId: string) => {
    const [form] = useForm<CourseUpdating>();
    const [showModal, setShowModal] = useState(false);
    const onShowModal = useCallback(() => setShowModal(true), [setShowModal]);

    const onOkModal = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancelModal = useCallback(() => {
        setShowModal(false);
        form.resetFields();
    }, [form, setShowModal]);

    const loading = useUnit(CourseEntity.Model.updateCourseFx.pending);
    const update = useCallback(
        async (formData: CourseUpdating) => {
            await CourseEntity.Model.updateCourseFx({ courseId, data: formData });
            setShowModal(false);
        },
        [courseId],
    );

    return {
        form,
        loading,
        update,
        showModal,
        onShowModal,
        onOkModal,
        onCancelModal,
    };
};
