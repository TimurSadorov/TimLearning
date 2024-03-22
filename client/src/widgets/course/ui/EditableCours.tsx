import React from 'react';
import { ArrowRightOutlined } from '@ant-design/icons';
import { CourseEntity } from '@entities';
import { CourseFeature } from '@features';
import { Config } from '@shared';
import { Button, Card, Descriptions, Tooltip } from 'antd';
import { useNavigate } from 'react-router-dom';
import styled from 'styled-components';

type Props = { course: CourseEntity.Type.EditableCours };

export const EditableCours = ({ course }: Props) => {
    const navigate = useNavigate();
    return (
        <CourseCard
            actions={[
                <CourseFeature.UI.UpdateCourseButton course={course} />,
                course.isDraft ? (
                    <CourseFeature.UI.PublishCourseButton courseId={course.id} />
                ) : (
                    <CourseFeature.UI.DraftCourseButton courseId={course.id} />
                ),
                course.isDeleted ? (
                    <CourseFeature.UI.RecoverCourseButton courseId={course.id} />
                ) : (
                    <CourseFeature.UI.DeleteCourseButton courseId={course.id} />
                ),
                <Tooltip placement="top" title={'Перейти к редактировнию модулей'}>
                    <Button
                        icon={<ArrowRightOutlined />}
                        onClick={() => navigate(Config.routes.editableModules.getLink(course.id))}
                    />
                </Tooltip>,
            ]}
        >
            <Descriptions
                items={[
                    { key: '1', label: 'Название', children: course.name, span: 2 },
                    { key: '2', label: 'Короткое название', children: course.shortName },
                    { key: '3', label: 'Описание', children: course.description, span: 3 },
                    { key: '4', label: 'Черновик', children: course.isDraft ? 'Да' : 'Нет', span: 2 },
                    { key: '5', label: 'Удален', children: course.isDeleted ? 'Да' : 'Нет' },
                ]}
                layout="vertical"
            />
        </CourseCard>
    );
};

const CourseCard = styled(Card)`
    justify-content: space-between;
    display: flex;
    flex-direction: column;
`;
