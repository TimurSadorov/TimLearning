import { LessonEntity } from '@entities';
import { Api, Config, SharedUI } from '@shared';
import { useForm } from 'antd/lib/form/Form';
import { createEvent, restore } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';
import { reset } from 'patronum';
import { useCallback, useMemo, useState } from 'react';

const FilterLessonsSystemDataGate = createGate();

const onChangeIsDeleted = createEvent<boolean>();
const $isDeleted = restore(onChangeIsDeleted, false);

reset({ clock: FilterLessonsSystemDataGate.close, target: [$isDeleted] });

export const useFilterLessonsSystemData = (moduleId: string) => {
    useGate(FilterLessonsSystemDataGate);
    const isDeleted = useUnit($isDeleted);

    const request = useMemo<LessonEntity.Model.SystemLessonsFilters>(
        () => ({ moduleId, isDeleted }),
        [moduleId, isDeleted],
    );

    const { lessons, isLoading, error } = LessonEntity.Model.useLessonsSystemData(request);

    Api.Model.useNotFoundEntity(error, 'Модуль не найден 😕', Config.routes.editableCourses.path);

    return {
        lessons,
        isLoading,
        isDeleted,
        onChangeIsDeleted,
    };
};

export type NewLesson = Api.Services.CreateLessonRequest;

export const useCreateLessonModal = (moduleId: string) => {
    const [form] = useForm<NewLesson>();
    const [showModal, setShowModal] = useState(false);
    const onShowModal = useCallback(() => setShowModal(true), [setShowModal]);

    const onOkModal = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancelModal = useCallback(() => setShowModal(false), [setShowModal]);

    const loading = useUnit(LessonEntity.Model.createLessonFx.pending);
    const create = useCallback(
        async (formData: NewLesson) => {
            await LessonEntity.Model.createLessonFx({ moduleId, ...formData });
            form.resetFields();
            setShowModal(false);
            SharedUI.Model.Notification.notifySuccessFx('Урок успешно создан 😉');
        },
        [form],
    );

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

export type LessonUpdating = Pick<Api.Services.UpdateLessonRequest, 'name'>;

export const useUpdateLessonModal = (lessonId: string) => {
    const [form] = useForm<LessonUpdating>();
    const [showModal, setShowModal] = useState(false);
    const onShowModal = useCallback(() => setShowModal(true), [setShowModal]);

    const onOkModal = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancelModal = useCallback(() => {
        setShowModal(false);
        form.resetFields();
    }, [form, setShowModal]);

    const loading = useUnit(LessonEntity.Model.updateLessonFx.pending);
    const update = useCallback(
        async (formData: LessonUpdating) => {
            await LessonEntity.Model.updateLessonFx({ lessonId, data: formData });
            setShowModal(false);
            SharedUI.Model.Notification.notifySuccessFx('Урок успешно обнавлен 😉');
        },
        [lessonId],
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

export const useMoveLesson = () => {
    const lessonMovingLoading = useUnit(LessonEntity.Model.moveLessonFx.pending);

    const moveLesson = useCallback(async (data: LessonEntity.Model.WithLessonId<Api.Services.MoveLessonRequest>) => {
        await LessonEntity.Model.moveLessonFx(data);
    }, []);

    return {
        lessonMovingLoading,
        moveLesson,
    };
};

export const useDeleteLesson = (lessonId: string) => {
    const loading = useUnit(LessonEntity.Model.deleteLessonFx.pending);
    const deleteLesson = useCallback(async () => {
        await LessonEntity.Model.deleteLessonFx(lessonId);
    }, [lessonId]);

    return {
        loading,
        deleteLesson,
    };
};

export const useRestoreLesson = (lessonId: string) => {
    const loading = useUnit(LessonEntity.Model.restoreLessonFx.pending);
    const restoreLesson = useCallback(async () => {
        await LessonEntity.Model.restoreLessonFx(lessonId);
    }, [lessonId]);

    return {
        loading,
        restoreLesson,
    };
};

export const usePublishLesson = (lessonId: string) => {
    const loading = useUnit(LessonEntity.Model.updateLessonFx.pending);
    const publishLesson = useCallback(async () => {
        await LessonEntity.Model.updateLessonFx({ lessonId, data: { isDraft: false } });
    }, [lessonId]);

    return {
        loading,
        publishLesson,
    };
};

export const useDraftLesson = (lessonId: string) => {
    const loading = useUnit(LessonEntity.Model.updateLessonFx.pending);
    const draftLesson = useCallback(async () => {
        await LessonEntity.Model.updateLessonFx({ lessonId, data: { isDraft: true } });
    }, [lessonId]);

    return {
        loading,
        draftLesson,
    };
};
