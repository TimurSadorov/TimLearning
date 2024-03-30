import { ModuleEntity } from '@entities';
import { Api, Config, SharedUI } from '@shared';
import { useForm } from 'antd/lib/form/Form';
import { createEvent, restore } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';
import { WithModuleId } from 'entities/modules/model';
import { reset } from 'patronum';
import { useCallback, useEffect, useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const FilterEditableOrderedModules = createGate();

const onChangeIsDeleted = createEvent<boolean>();
const $isDeleted = restore(onChangeIsDeleted, false);

reset({ clock: FilterEditableOrderedModules.close, target: [$isDeleted] });

export const useFilterEditableOrderedModules = (cousreId: string) => {
    const navigate = useNavigate();
    useGate(FilterEditableOrderedModules);
    const isDeleted = useUnit($isDeleted);

    const request = useMemo(
        () => ({
            courseId: cousreId,
            data: {
                isDeleted,
            },
        }),
        [cousreId, isDeleted],
    );

    const { isLoading, editableOrderedModules, error } = ModuleEntity.Model.useEditableOrderedModules(request);

    useEffect(() => {
        if (!error) {
            return;
        }

        if (Api.Utils.isNotFoundApiError(error)) {
            SharedUI.Model.Notification.notifyErrorFx('Курс не найден 😕');
            navigate(Config.routes.editableCourses.path);
        }
    }, [error]);

    return {
        editableOrderedModules,
        isLoading,
        isDeleted,
        onChangeIsDeleted,
    };
};

export type NewModule = Api.Services.CreateModuleRequest;

export const useCreateModuleModal = (courseId: string) => {
    const [form] = useForm<NewModule>();
    const [showModal, setShowModal] = useState(false);
    const onShowModal = useCallback(() => setShowModal(true), [setShowModal]);

    const onOkModal = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancelModal = useCallback(() => setShowModal(false), [setShowModal]);

    const loading = useUnit(ModuleEntity.Model.createModuleFx.pending);
    const create = useCallback(
        async (formData: NewModule) => {
            await ModuleEntity.Model.createModuleFx({ courseId: courseId, data: formData });
            form.resetFields();
            setShowModal(false);
            SharedUI.Model.Notification.notifySuccessFx('Модуль успешно создан 😉');
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

export type ModuleUpdating = Pick<Api.Services.UpdateModuleRequest, 'name'>;

export const useUpdateModuleModal = (moduleId: string) => {
    const [form] = useForm<ModuleUpdating>();
    const [showModal, setShowModal] = useState(false);
    const onShowModal = useCallback(() => setShowModal(true), [setShowModal]);

    const onOkModal = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancelModal = useCallback(() => {
        setShowModal(false);
        form.resetFields();
    }, [form, setShowModal]);

    const loading = useUnit(ModuleEntity.Model.updateModuleFx.pending);
    const update = useCallback(
        async (formData: ModuleUpdating) => {
            await ModuleEntity.Model.updateModuleFx({ moduleId, data: formData });
            setShowModal(false);
            SharedUI.Model.Notification.notifySuccessFx('Модуль успешно обнавлен 😉');
        },
        [moduleId],
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

export const useСhangeModuleOrder = () => {
    const changingOrderLoading = useUnit(ModuleEntity.Model.changeModuleOrderFx.pending);

    const changeOrder = useCallback(async (data: WithModuleId<Api.Services.ChangeModuleOrderRequest>) => {
        await ModuleEntity.Model.changeModuleOrderFx(data);
    }, []);

    return {
        changingOrderLoading,
        changeOrder,
    };
};

export const useDeleteModule = (moduleId: string) => {
    const loading = useUnit(ModuleEntity.Model.deleteModuleFx.pending);
    const deleteModule = useCallback(async () => {
        await ModuleEntity.Model.deleteModuleFx(moduleId);
    }, [moduleId]);

    return {
        loading,
        deleteModule,
    };
};

export const useRestoreModule = (moduleId: string) => {
    const loading = useUnit(ModuleEntity.Model.restoreModuleFx.pending);
    const restoreModule = useCallback(async () => {
        await ModuleEntity.Model.restoreModuleFx(moduleId);
    }, [moduleId]);

    return {
        loading,
        restoreModule,
    };
};

export const usePublishModule = (moduleId: string) => {
    const loading = useUnit(ModuleEntity.Model.updateModuleFx.pending);
    const publishModule = useCallback(async () => {
        await ModuleEntity.Model.updateModuleFx({ moduleId: moduleId, data: { isDraft: false } });
    }, [moduleId]);

    return {
        loading,
        publishModule,
    };
};

export const useDraftModule = (moduleId: string) => {
    const loading = useUnit(ModuleEntity.Model.updateModuleFx.pending);
    const draftModule = useCallback(async () => {
        await ModuleEntity.Model.updateModuleFx({ moduleId: moduleId, data: { isDraft: true } });
    }, [moduleId]);

    return {
        loading,
        draftModule,
    };
};
