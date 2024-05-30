import { CodeReviewEntity } from '@entities';
import { Api } from '@shared';
import { createEvent, restore } from 'effector';
import { createGate, useGate, useUnit } from 'effector-react';
import { debounce, reset } from 'patronum';
import { useMemo } from 'react';

const FilterCodeReviews = createGate();

const onChangeStatuses = createEvent<Api.Services.CodeReviewStatus[]>();
const $statuses = restore(onChangeStatuses, [Api.Services.CodeReviewStatus.PENDING]);
const $debouncedStatuses = restore(debounce(onChangeStatuses, 800), [Api.Services.CodeReviewStatus.PENDING]);

reset({ clock: FilterCodeReviews.close, target: [$statuses, $debouncedStatuses] });

export const useFilterCodeReviews = (studyGroupId: string) => {
    useGate(FilterCodeReviews);
    const statuses = useUnit($statuses);
    const debouncedStatuses = useUnit($debouncedStatuses);

    const request = useMemo(
        () => ({
            studyGroupId,
            statuses: debouncedStatuses,
        }),
        [studyGroupId, debouncedStatuses],
    );

    const { codeReviews, isLoading } = CodeReviewEntity.Model.useCodeReviews(request);

    return {
        codeReviews,
        isLoading,
        statuses,
        onChangeStatuses,
    };
};
