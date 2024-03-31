import React from 'react';
import { DeleteOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import { useDeleteLesson } from '../model';

interface Props {
    lessonId: string;
    className?: string;
}

export const DeleteLessonButton = ({ lessonId, className }: Props) => {
    const { deleteLesson, loading } = useDeleteLesson(lessonId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Удалить'}>
                <Button danger icon={<DeleteOutlined />} disabled={loading} onClick={deleteLesson} />
            </Tooltip>
        </div>
    );
};
