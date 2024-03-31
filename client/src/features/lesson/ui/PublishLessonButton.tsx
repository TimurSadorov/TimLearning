import React from 'react';
import { SendOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import { usePublishLesson } from '../model';

interface Props {
    lessonId: string;
    className?: string;
}

export const PublishLessonButton = ({ lessonId, className }: Props) => {
    const { publishLesson, loading } = usePublishLesson(lessonId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Опубликовать'}>
                <Button icon={<SendOutlined />} disabled={loading} onClick={publishLesson} />
            </Tooltip>
        </div>
    );
};
