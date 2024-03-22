import { Card } from 'antd';
import React from 'react';
import styled from 'styled-components';

interface UserCourseProps {
    name: string;
    description: string;
}

export const UserCourse = ({ name, description }: UserCourseProps) => {
    return (
        <Card title={name} hoverable>
            <CourseDescription>{description}</CourseDescription>
        </Card>
    );
};

const CourseDescription = styled.div`
    font-size: 1em;
`;
