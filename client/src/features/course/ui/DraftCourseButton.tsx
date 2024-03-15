import { FileSearchOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import React from 'react';
import { useDraftCourse } from '../model';

interface Props {
    courseId: string;
    className?: string;
}

export const DraftCourseButton = ({ courseId, className }: Props) => {
    const { toDraft, loading } = useDraftCourse(courseId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Перенести в черновик'}>
                <Button icon={<FileSearchOutlined />} disabled={loading} onClick={toDraft} />
            </Tooltip>
        </div>
    );
};
