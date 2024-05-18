import { CodeOutlined, FileTextOutlined } from '@ant-design/icons';
import { Api } from '@shared';
import React from 'react';
import styled from 'styled-components';

export type Props = Omit<Api.Services.UserProgressInLessonResponse, 'id'> & {
    isFirst: boolean;
    isLast: boolean;
    isActive: boolean;
    previousLessonHasProgress: boolean;
    onClick?: () => void;
};

export const UserLessonRecord = ({
    name,
    isPractical,
    userProgress,
    isActive,
    onClick,
    isFirst,
    isLast,
    previousLessonHasProgress,
}: Props) => {
    return (
        <LessonContainer $isActive={isActive} onClick={onClick}>
            <ProgressContainer
                $isFirst={isFirst}
                $isLast={isLast}
                $currentLessonHasProgress={userProgress != undefined}
                $previousLessonHasProgress={previousLessonHasProgress}
            >
                <ProgressPoint $userProgress={userProgress} />
            </ProgressContainer>
            <IconContainer>{isPractical ? <CodeOutlined /> : <FileTextOutlined />}</IconContainer>
            <LessonName>{name}</LessonName>
        </LessonContainer>
    );
};

const LessonContainer = styled.div<{ $isActive: boolean }>`
    display: flex;
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

const ProgressContainer = styled.div<{
    $isFirst: boolean;
    $isLast: boolean;
    $currentLessonHasProgress: boolean;
    $previousLessonHasProgress: boolean;
}>`
    height: 100%;
    margin: -15px 0px 15px 0px;

    &::before {
        content: '';
        display: block;
        background-color: ${(props) =>
            props.$isFirst ? 'none' : props.$previousLessonHasProgress ? '#3f9726' : '#989898'};
        height: calc(50% + 11px);
        width: 1px;
    }
    &::after {
        content: '';
        display: block;
        background-color: ${(props) =>
            props.$isLast ? 'none' : props.$currentLessonHasProgress ? '#3f9726' : '#989898'};
        height: calc(50% + 9px);
        width: 1px;
    }
`;

const ProgressPoint = styled.span<{ $userProgress: Props['userProgress'] }>`
    background-color: ${(props) =>
        !props.$userProgress
            ? '#989898'
            : props.$userProgress === Api.Services.UserProgressType.COMPLETED
              ? '#3f9726'
              : '#F69C00'};
    display: block;
    width: 10px;
    height: 10px;
    border-radius: 50%;
    margin-left: -4px;
`;

const IconContainer = styled.div`
    margin-left: 15px;
    color: #868686;
    font-size: 1.1em;
`;

const LessonName = styled.div`
    font-size: 1.2em;
    margin-left: 8px;
`;
