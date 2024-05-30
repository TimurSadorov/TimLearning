import React from 'react';
import { Select } from 'antd';
import { DefaultOptionType } from 'antd/es/select';
import { Api } from '@shared';

type Props = {
    value?: Api.Services.CodeReviewStatus[];
    onChange?: (statuses: Api.Services.CodeReviewStatus[]) => void;
    className?: string;
};

const options: DefaultOptionType[] = Api.Model.AllReviewStatuses.map<DefaultOptionType>((c) => ({
    value: c,
    label: Api.Model.getReviewStatusName(c),
}));

export const StatusSelector = ({ onChange, value, className }: Props) => {
    return (
        <Select
            className={className}
            mode="tags"
            placeholder="Выберите статусы"
            value={value}
            onChange={onChange}
            options={options}
        />
    );
};
