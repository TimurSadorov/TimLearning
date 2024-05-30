import { Button } from 'antd';
import React from 'react';
import { useActiveStudyGroup } from '../model';

interface Props {
    studyGroupId: string;
    isActive: boolean;
    className?: string;
}

export const ActiveButton = ({ studyGroupId, isActive, className }: Props) => {
    const { updateIsActive, loading } = useActiveStudyGroup(studyGroupId);

    return (
        <div className={className}>
            <Button disabled={loading} onClick={() => updateIsActive(!isActive)}>
                {isActive ? 'Деактивировать' : 'Активировать'}
            </Button>
        </div>
    );
};
