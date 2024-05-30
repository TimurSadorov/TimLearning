import React from 'react';
import { Select } from 'antd';
import { useUserCourses } from '../model';
import { useMemo } from 'react';
import { DefaultOptionType } from 'antd/es/select';

type Props = {
    value?: string;
    onChange?: (course: string) => void;
};

export const CreateStudyGroup = ({ onChange, value }: Props) => {
    const { isLoading, userCourses } = useUserCourses();
    const courses = useMemo(
        () =>
            userCourses.map<DefaultOptionType>((c) => ({
                value: c.id,
                label: c.name,
            })),
        [userCourses],
    );

    return (
        <Select placeholder="Выберите курс" value={value} onChange={onChange} options={courses} loading={isLoading} />
    );
};
