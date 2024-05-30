import React from 'react';
import { CodeReviewFeature } from '@features';
import styled from 'styled-components';
import { useNavigate } from 'react-router-dom';
import { Config, SharedUI } from '@shared';
import { CodeReviewEntity } from '@entities';

type Props = {
    studyGroupId: string;
    className?: string;
};

export const CodeReviewsFilterList = ({ studyGroupId, className }: Props) => {
    const { codeReviews, isLoading, onChangeStatuses, statuses } =
        CodeReviewFeature.Model.useFilterCodeReviews(studyGroupId);

    const navigate = useNavigate();

    return (
        <ListBlock className={className}>
            <HeaderBlock>
                <SelectorName>Статусы: </SelectorName>
                <StatusSelector value={statuses} onChange={(value) => onChangeStatuses(value)} />
            </HeaderBlock>
            {isLoading || !codeReviews ? (
                <Loader />
            ) : (
                <ListRecords>
                    {codeReviews.map((codeReview) => (
                        <CodeReviewFeature.UI.CodeReviewRecord key={codeReview.id} codeReview={codeReview} />
                    ))}
                </ListRecords>
            )}
        </ListBlock>
    );
};

const ListBlock = styled.div`
    display: flex;
    flex-direction: column;
    flex: 1;
`;

const Loader = styled(SharedUI.Loader)`
    flex: 1;
`;

const HeaderBlock = styled.div`
    display: flex;
    align-items: center;
    flex-direction: row;
`;

const SelectorName = styled.div`
    font-size: 1em;
`;

const StatusSelector = styled(CodeReviewEntity.UI.StatusSelector)`
    margin-left: 10px;
    min-width: 10%;
`;

const ListRecords = styled.div`
    margin-top: 15px;
    display: grid;
    grid-template-columns: repeat(5, 1fr);
    gap: 10px 20px;
`;
