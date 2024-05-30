import { StudyGroupEntity } from '@entities';
import { Api } from '@shared';
import { useForm } from 'antd/es/form/Form';
import { createEvent, restore } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';
import { debounce, reset } from 'patronum';
import { useCallback, useState } from 'react';

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
