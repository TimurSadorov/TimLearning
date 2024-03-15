import { ToTopOutlined } from '@ant-design/icons';
import { Button, Tooltip } from 'antd';
import React from 'react';
import { useRecoverCourse } from '../model';
import styled from 'styled-components';

interface Props {
    courseId: string;
    className?: string;
}

export const RecoverCourseButton = ({ courseId, className }: Props) => {
    const { recoverCourse, loading } = useRecoverCourse(courseId);

    return (
        <div className={className}>
            <Tooltip placement="top" title={'Восстановить'}>
                <RecoverButton icon={<ToTopOutlined />} disabled={loading} onClick={recoverCourse} />
            </Tooltip>
        </div>
    );
};

const RecoverButton = styled(Button)`
    color: #4dbc4d;
`;
