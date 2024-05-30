import { Card, Descriptions } from 'antd';
import React from 'react';

interface UserCourseProps {
    name: string;
    courseName: string;
    isActive: boolean;
    onClick?: () => void;
}

export const StudyGroupRecord = ({ name, isActive, courseName, onClick }: UserCourseProps) => {
    return (
        <Card title={name} hoverable onClick={onClick}>
            <Descriptions
                items={[
                    { key: '1', label: 'Курс', children: courseName, span: 3 },
                    { key: '2', label: 'Активный', children: isActive ? 'Да' : 'Нет', span: 3 },
                ]}
                layout="vertical"
            />
        </Card>
    );
};
