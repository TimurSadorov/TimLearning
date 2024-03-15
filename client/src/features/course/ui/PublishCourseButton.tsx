import { SendOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import React from 'react';
import { usePublishCourse } from '../model';

interface Props {
    courseId: string;
    className?: string;
}

export const PublishCourseButton = ({ courseId, className }: Props) => {
    const { publishCourse, loading } = usePublishCourse(courseId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Опубликовать'}>
                <Button icon={<SendOutlined />} disabled={loading} onClick={publishCourse} />
            </Tooltip>
        </div>
    );
};
