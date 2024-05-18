import { SharedUI } from '@shared';
import React from 'react';
import styled from 'styled-components';

export interface Props {
    name: string;
    completionPercentage: number;
    isActive: boolean;
    onClick?: React.MouseEventHandler<HTMLDivElement>;
}

export const UserModuleRecord = ({ name, completionPercentage, isActive, onClick }: Props) => {
    return (
        <ModuleContainer $isActive={isActive} onClick={onClick}>
            <ModuleName>{name}</ModuleName>
            <ProgressBar type="circle" size={40} percent={completionPercentage} />
        </ModuleContainer>
    );
};

const ModuleContainer = styled.div<{ $isActive: boolean }>`
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 15px 10px 15px 30px;
    border-radius: 10px;
    cursor: pointer;
    background-color: ${(props) => (props.$isActive ? '#e7e7e7' : '#ffffff')};
    transition: background-color 0.3s;
    &:hover {
        background-color: #bfdcfe;
    }
`;

const ModuleName = styled.div`
    font-weight: 600;
    font-size: 1.2em;
`;

const ProgressBar = styled(SharedUI.ProgressBar)``;
