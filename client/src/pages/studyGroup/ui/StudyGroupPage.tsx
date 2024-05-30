import React from 'react';
import styled from 'styled-components';
import { Navigate, useParams } from 'react-router-dom';
import { Config, Utils } from '@shared';
import { CodeReviewWidget, StudyGroupWidget } from '@widgets';
import { Divider } from 'antd';

export const StudyGroupPage = () => {
    const { studyGroupId } = useParams<{ studyGroupId: string }>();

    if (studyGroupId == undefined || !Utils.isValidGuid(studyGroupId)) {
        return <Navigate to={Config.routes.root.getLink()} />;
    }

    return (
        <Page>
            <StudyGroupWidget.UI.StugyGroupHeader studyGroupId={studyGroupId} />
            <ReviewsDivider orientation="left">
                <ReviewsDividerText>Ревью кода</ReviewsDividerText>
            </ReviewsDivider>
            <ReviewsList studyGroupId={studyGroupId} />
        </Page>
    );
};

const Page = styled.div`
    padding: 10px 30px;
    display: flex;
    flex-direction: column;
    min-height: calc(100vh - 71px);
`;

const ReviewsDivider = styled(Divider)`
    padding-top: 25px;
`;

const ReviewsDividerText = styled.div`
    font-size: 1.2em;
`;

const ReviewsList = styled(CodeReviewWidget.UI.CodeReviewsFilterList)`
    margin-top: 5px;
`;
