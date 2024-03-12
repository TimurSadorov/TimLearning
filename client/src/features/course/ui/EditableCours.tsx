import { CourseEntity } from '@entities';
import React from 'react';
import styled from 'styled-components';

type CourseProps = Omit<CourseEntity.Type.EditableCours, 'id'>;

export const EditableCours = (course: CourseProps) => {
    return (
        <CourseBlock>
            <CoursePropertyBlock>
                <CoursePropertyName>Название:</CoursePropertyName>
                <div>{course.name}</div>
            </CoursePropertyBlock>
            <CoursePropertyBlock>
                <CoursePropertyName>Короткое название:</CoursePropertyName>
                <div>{course.shortName}</div>
            </CoursePropertyBlock>
            <CoursePropertyBlock>
                <CoursePropertyName>Описание:</CoursePropertyName>
                <div>{course.description}</div>
            </CoursePropertyBlock>
            <CoursePropertyBlock>
                <CoursePropertyName>Черновик:</CoursePropertyName>
                <div>{course.isDraft ? 'Да' : 'Нет'}</div>
            </CoursePropertyBlock>
            <CoursePropertyBlock>
                <CoursePropertyName>Удален:</CoursePropertyName>
                <div>{course.isDeleted ? 'Да' : 'Нет'}</div>
            </CoursePropertyBlock>
        </CourseBlock>
    );
};

const CourseBlock = styled.div`
    border: solid 1px;
    border-radius: 10px;
    padding: 10px 15px;
    display: flex;
    flex-direction: column;
    row-gap: 10px;
`;

const CoursePropertyBlock = styled.div`
    display: flex;
    flex-direction: column;
`;

const CoursePropertyName = styled.div`
    font-weight: 600;
    font-size: 1em;
`;
