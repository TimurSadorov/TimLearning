import { CourseEntity } from '@entities';
import { CourseFeature } from '@features';
import React from 'react';
import styled from 'styled-components';

type Props = { course: CourseEntity.Type.EditableCours };

export const EditableCours = ({ course }: Props) => {
    return (
        <CourseBlock>
            <PropertyWithBittons>
                <CoursePropertyBlock>
                    <CoursePropertyName>Название:</CoursePropertyName>
                    <div>{course.name}</div>
                </CoursePropertyBlock>
                <Buttons>
                    <CourseFeature.UI.UpdateCourseButton course={course} />
                    {course.isDraft ? (
                        <CourseFeature.UI.PublishCourseButton courseId={course.id} />
                    ) : (
                        <CourseFeature.UI.DraftCourseButton courseId={course.id} />
                    )}
                    {course.isDeleted ? (
                        <CourseFeature.UI.RecoverCourseButton courseId={course.id} />
                    ) : (
                        <CourseFeature.UI.DeleteCourseButton courseId={course.id} />
                    )}
                </Buttons>
            </PropertyWithBittons>
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

const PropertyWithBittons = styled.div`
    display: flex;
    justify-content: space-between;
`;

const Buttons = styled.div`
    display: flex;
    column-gap: 10px;
`;

const CoursePropertyBlock = styled.div`
    display: flex;
    flex-direction: column;
`;

const CoursePropertyName = styled.div`
    font-weight: 600;
    font-size: 1em;
`;
