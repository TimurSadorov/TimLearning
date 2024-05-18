import { Card } from 'antd';
import React from 'react';
import styled from 'styled-components';

interface UserCourseProps {
    name: string;
    description: string;
    onClick?: () => void;
}

export const UserCourse = ({ name, description, onClick }: UserCourseProps) => {
    return (
        <Card title={name} hoverable onClick={onClick}>
            <CourseDescription>{description}</CourseDescription>
        </Card>
    );
};

const CourseDescription = styled.div`
    font-size: 1em;
`;
