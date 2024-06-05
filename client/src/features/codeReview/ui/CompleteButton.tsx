import React from 'react';
import { CheckOutlined, CloseOutlined } from '@ant-design/icons';
import { Button } from 'antd';
import { useCompleteReview } from '../model';
import styled from 'styled-components';

interface Props {
    reviewId: string;
    isSuccess: boolean;
    className?: string;
}

export const CompleteButton = ({ reviewId, isSuccess, className }: Props) => {
    const { completeCodeReview, loading } = useCompleteReview(reviewId, isSuccess);

    return (
        <div className={className}>
            <ColorButton
                icon={isSuccess ? <CheckOutlined /> : <CloseOutlined />}
                disabled={loading}
                onClick={completeCodeReview}
                $isSuccess={isSuccess}
            >
                {isSuccess ? 'Ревью пройдено' : 'Отправить на доработку'}
            </ColorButton>
        </div>
    );
};

const ColorButton = styled(Button)<{ $isSuccess: boolean }>`
    border-color: ${(props) => (props.$isSuccess ? '#35c258' : '#ff5a5d')};
    color: ${(props) => (props.$isSuccess ? '#35c258' : '#ff5a5d')};
    transition: 0.3s;
    &:hover {
        border-color: ${(props) => (props.$isSuccess ? '#4dea74' : '#ff0000')} !important;
        color: ${(props) => (props.$isSuccess ? '#4dea74' : '#ff0000')} !important;
    }
`;
