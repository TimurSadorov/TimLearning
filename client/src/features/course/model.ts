import { CourseEntity } from '@entities';
import { createEvent, restore } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';
import { debounce, reset } from 'patronum';

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
