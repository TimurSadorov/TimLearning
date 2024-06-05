import React from 'react';
import styled from 'styled-components';
import { Navigate, useParams } from 'react-router-dom';
import { Config, Utils } from '@shared';
import { CodeReviewWidget } from '@widgets';

export const CodeReviewPage = () => {
    const { reviewId } = useParams<{ reviewId: string }>();

    if (reviewId == undefined || !Utils.isValidGuid(reviewId)) {
        return <Navigate to={Config.routes.root.getLink()} />;
    }

    return (
        <Page>
            <CodeReviewWidget.UI.CodeReview reviewId={reviewId} />
        </Page>
    );
};

const Page = styled.div`
    padding: 0 200px;
    background-color: #f5f5f5;
    display: flex;
    flex-direction: column;
    min-height: calc(100vh - 71px);
`;
