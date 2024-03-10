import React from 'react';
import styled from 'styled-components';

interface UserCourseProps {
    name: string;
    description: string;
}

export const UserCourse = ({ name, description }: UserCourseProps) => {
    return (
        <CourseBlock>
            <CourseName>{name}</CourseName>
            <CourseDescription>
                <DescriptionWord>Описание: </DescriptionWord>
                {description}
            </CourseDescription>
        </CourseBlock>
    );
};

const CourseBlock = styled.div`
    border: solid 1px;
    border-radius: 10px;
    padding: 10px 15px;
    display: flex;
    flex-direction: column;
`;

const CourseName = styled.div`
    font-size: 1.4em;
    font-weight: 600;
`;

const DescriptionWord = styled.span`
    font-weight: 600;
`;

const CourseDescription = styled.header`
    font-size: 1em;
    margin-top: 10px;
`;
