import { Api } from '@shared';
import { Card } from 'antd';
import React from 'react';
import styled from 'styled-components';

type Props = {
    codeReview: Api.Services.GetStudyGroupCodeReviewsResponse;
    onClick?: () => void;
};

export const CodeReviewRecord = ({ codeReview, onClick }: Props) => {
    return (
        <Card title={`${codeReview.moduleName} -> ${codeReview.lessonName}`} hoverable onClick={onClick}>
            <CodeReviewDescription>
                <FieldDescription>
                    <FieldNameDescription>Пользователь: </FieldNameDescription>
                    <FieldValueDescription>{codeReview.userEmail}</FieldValueDescription>
                </FieldDescription>
                <FieldDescription>
                    <FieldNameDescription>Статус: </FieldNameDescription>
                    <FieldValueDescription>{Api.Model.getReviewStatusName(codeReview.status)}</FieldValueDescription>
                </FieldDescription>
            </CodeReviewDescription>
        </Card>
    );
};

const CodeReviewDescription = styled.div`
    display: flex;
    flex-direction: column;
    font-size: 1em;
    gap: 10px 0;
`;

const FieldDescription = styled.div``;

const FieldNameDescription = styled.span`
    font-weight: 600;
`;

const FieldValueDescription = styled.span``;
