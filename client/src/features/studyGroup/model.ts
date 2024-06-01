import { StudyGroupEntity, UserEntity } from '@entities';
import { UserFeature } from '@features';
import { Api, Config, SharedUI } from '@shared';
import { useForm } from 'antd/es/form/Form';
import { createEvent, restore } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';
import { debounce, reset } from 'patronum';
import { useCallback, useEffect, useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';

const FilterStudyGroups = createGate();

const onChangeSearchName = createEvent<string>();
const $searchName = restore(onChangeSearchName, '');
const $debouncedSearchName = restore(debounce(onChangeSearchName, 300), '');

const onChangeIsActive = createEvent<boolean>();
const $isActive = restore(onChangeIsActive, true);

reset({ clock: FilterStudyGroups.close, target: [$searchName, $debouncedSearchName, $isActive] });

export const useFilterStudyGroups = () => {
    useGate(FilterStudyGroups);
    const searchName = useUnit($searchName);
    const debouncedSearchName = useUnit($debouncedSearchName);
    const isActive = useUnit($isActive);

    const { studyGroups, isLoading } = StudyGroupEntity.Model.useStudyGroups({
        searchName: debouncedSearchName,
        isActive: isActive,
    });

    return {
        studyGroups,
        isLoading,
        isActive,
        onChangeIsActive,
        searchName,
        onChangeSearchName,
    };
};

export type NewStudyGroup = Api.Services.CreateStudyGroupRequest;

export const useCreateStudyGroup = () => {
    const [form] = useForm<NewStudyGroup>();
    const [showModal, setShowModal] = useState(false);
    const onShowModal = useCallback(() => setShowModal(true), [setShowModal]);

    const onOkModal = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancelModal = useCallback(() => setShowModal(false), [setShowModal]);

    const loading = useUnit(StudyGroupEntity.Model.createStudyGroupsFx.pending);
    const create = useCallback(async (f: NewStudyGroup) => {
        await StudyGroupEntity.Model.createStudyGroupsFx(f);
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

export type UpdatedNameStudyGroup = { name: string };

export const useUpdateStudyGroup = (id: string) => {
    const [form] = useForm<UpdatedNameStudyGroup>();
    const [showModal, setShowModal] = useState(false);
    const onShowModal = useCallback(() => setShowModal(true), [setShowModal]);

    const onOkModal = useCallback(() => {
        form.submit();
    }, [form]);
    const onCancelModal = useCallback(() => setShowModal(false), [setShowModal]);

    const loading = useUnit(StudyGroupEntity.Model.updateStudyGroupFx.pending);
    const update = useCallback(
        async (f: UpdatedNameStudyGroup) => {
            await StudyGroupEntity.Model.updateStudyGroupFx({ id, ...f });
            setShowModal(false);
            form.resetFields();
        },
        [id],
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

export const useActiveStudyGroup = (id: string) => {
    const loading = useUnit(StudyGroupEntity.Model.updateStudyGroupFx.pending);
    const updateIsActive = useCallback(
        async (isActive: boolean) => {
            await StudyGroupEntity.Model.updateStudyGroupFx({ id, isActive });
        },
        [id],
    );

    return {
        updateIsActive,
        loading,
    };
};

export const useJoinToStudyGroup = () => {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const { isAuthorized } = UserEntity.Model.useUser();

    const navigateOnRoot = () => navigate(Config.routes.root.getLink(), { replace: true });

    const onSuccess = useCallback(() => {
        SharedUI.Model.Notification.notifySuccessFx('Вы успешно присоединились к группе ❤️');
        navigateOnRoot();
    }, []);

    const onFail = useCallback((error: Error) => {
        Api.Utils.notifyIfValidationErrorText(error);

        if (!Api.Utils.isApiError(error)) {
            SharedUI.Model.Notification.notifyErrorFx('Что то пошло не так, попробуйте позже снова 😔');
        }

        navigateOnRoot();
    }, []);

    const { join } = StudyGroupEntity.Model.useJoinToStudyGroup(onSuccess, onFail);

    useEffect(() => {
        if (!isAuthorized) {
            SharedUI.Model.Notification.notifyErrorFx(
                'Войдите в аккаунт, после чего снова перейдите по ссылке, чтобы присоединиться к группе 🙂',
            );

            return;
        }

        join({ id: searchParams.get('id') ?? 's', signature: searchParams.get('signature') ?? 's' });
    }, [isAuthorized, searchParams]);
};
