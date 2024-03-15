import { DeleteOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import React from 'react';
import { useDeleteCourse } from '../model';

interface Props {
    courseId: string;
    className?: string;
}

export const DeleteCourseButton = ({ courseId, className }: Props) => {
    const { deleteCourse, loading } = useDeleteCourse(courseId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Удалить'}>
                <Button danger icon={<DeleteOutlined />} disabled={loading} onClick={deleteCourse} />
            </Tooltip>
        </div>
    );
};
