import React from 'react';
import { Progress } from 'antd';
import { ProgressProps } from 'antd/lib';

type Props = Omit<ProgressProps, 'strokeColor'>;

export const ProgressBar = (props: Props) => (
    <Progress
        {...props}
        strokeColor={{
            '0%': '#108ee9',
            '100%': '#87d068',
        }}
    />
);
