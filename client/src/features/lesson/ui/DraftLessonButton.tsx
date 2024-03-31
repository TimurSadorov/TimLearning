import React from 'react';
import { FileSearchOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import { useDraftLesson } from '../model';

interface Props {
    lessonId: string;
    className?: string;
}

export const DraftLessonButton = ({ lessonId, className }: Props) => {
    const { draftLesson, loading } = useDraftLesson(lessonId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Перенести в черновик'}>
                <Button icon={<FileSearchOutlined />} disabled={loading} onClick={draftLesson} />
            </Tooltip>
        </div>
    );
};
