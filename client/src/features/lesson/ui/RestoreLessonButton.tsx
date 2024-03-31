import React from 'react';
import { ToTopOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import styled from 'styled-components';
import { useRestoreLesson } from '../model';

interface Props {
    lessonId: string;
    className?: string;
}

export const RestoreLessonButton = ({ lessonId, className }: Props) => {
    const { restoreLesson, loading } = useRestoreLesson(lessonId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Восстановить'}>
                <RecoverButton icon={<ToTopOutlined />} disabled={loading} onClick={restoreLesson} />
            </Tooltip>
        </div>
    );
};

const RecoverButton = styled(Button)`
    color: #4dbc4d;
`;
