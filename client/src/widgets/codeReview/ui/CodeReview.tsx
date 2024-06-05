import { CodeReviewEntity, LessonEntity } from '@entities';
import { CodeReviewFeature } from '@features';
import { Api, Config, SharedUI } from '@shared';
import MDEditor from '@uiw/react-md-editor';
import { CodeReviewNoteCommentWidget } from '@widgets';
import { Alert } from 'antd';
import React, { useEffect, useMemo } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';

export type Props = {
    className?: string;
    reviewId: string;
};

export const CodeReview = ({ className, reviewId }: Props) => {
    const { codeReviewsWithUserSolution, isLoading } = CodeReviewEntity.Model.useCodeReviewsWithUserSolution(reviewId);
    const reviewCompleted = useMemo(
        () =>
            codeReviewsWithUserSolution?.status === Api.Services.CodeReviewStatus.REJECTED ||
            codeReviewsWithUserSolution?.status === Api.Services.CodeReviewStatus.COMPLETED,
        [codeReviewsWithUserSolution?.status],
    );

    const { downloadApp } = LessonEntity.Model.useUserLessonExerciseAppDownloading(
        codeReviewsWithUserSolution?.lesson.id ?? '',
    );

    useEffect(() => {
        if (codeReviewsWithUserSolution?.status === Api.Services.CodeReviewStatus.PENDING) {
            CodeReviewEntity.Model.startCodeReviewFx(reviewId);
        }
    }, [codeReviewsWithUserSolution?.status, reviewId]);

    return (
        <Page className={className}>
            {!codeReviewsWithUserSolution || isLoading ? (
                <Loader />
            ) : (
                <>
                    <Header>Код ревью</Header>
                    <ContentContainer>
                        <ReviewDescription>
                            <FieldDescription>
                                <FieldName>Статус:</FieldName>
                                <FieldValue>
                                    {Api.Model.getReviewStatusName(codeReviewsWithUserSolution.status)}
                                </FieldValue>
                            </FieldDescription>
                            <FieldDescription>
                                <FieldName>Пользователь:</FieldName>
                                <FieldValue>{codeReviewsWithUserSolution.userEmail}</FieldValue>
                            </FieldDescription>
                            <FieldDescription>
                                <FieldName>Название урока:</FieldName>
                                <LessonValue
                                    to={Config.routes.userLesson.getLink(codeReviewsWithUserSolution.lesson.id)}
                                    target="_blank"
                                >
                                    {codeReviewsWithUserSolution.lesson.name}
                                </LessonValue>
                            </FieldDescription>
                        </ReviewDescription>
                        <LessonDescriptionName>Описание урока</LessonDescriptionName>
                        <LessonDescriptionContainer data-color-mode="light">
                            <MDEditor.Markdown skipHtml={true} source={codeReviewsWithUserSolution.lesson.text} />
                        </LessonDescriptionContainer>
                        <FileDownloadingContainer>
                            <FileDownloadingRecord onClick={downloadApp}>Скачать архив проекта</FileDownloadingRecord>
                        </FileDownloadingContainer>
                        <UserSolutionContainer>
                            <UserSolutionName>Решение пользователя:</UserSolutionName>
                            <CodeReviewNoteCommentWidget.UI.CodeViewerWithComments
                                canStartNewComments={reviewCompleted}
                                code={codeReviewsWithUserSolution.solution.code}
                                reviewId={reviewId}
                            />
                        </UserSolutionContainer>
                        {!reviewCompleted ? (
                            <Buttons>
                                <CompleteButton isSuccess reviewId={reviewId} />
                                <CompleteButton isSuccess={false} reviewId={reviewId} />
                            </Buttons>
                        ) : (
                            <ReviewCompletedAlert
                                message="Решение проверено!"
                                type="info"
                                showIcon
                            ></ReviewCompletedAlert>
                        )}
                        <StandardCodeContainer>
                            <StandardCodeName>Эталонное решение:</StandardCodeName>
                            <SharedUI.CodeEditor value={codeReviewsWithUserSolution.standardCode} readonly />
                        </StandardCodeContainer>
                    </ContentContainer>
                </>
            )}
        </Page>
    );
};

const Page = styled.div`
    padding: 20px 0 40px 0;
    display: flex;
    flex-direction: column;
    align-items: center;
    flex: 1;
`;

const Loader = styled(SharedUI.Loader)`
    flex: 1;
`;

const Header = styled.div`
    font-size: 2em;
    font-weight: 600;
`;

const ContentContainer = styled.div`
    margin-top: 20px;
    display: flex;
    flex-direction: column;
    padding: 40px 35px 50px 35px;
    width: 100%;
    background-color: white;
    border-radius: 10px;
`;

const ReviewDescription = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: end;
    gap: 10px 0;
`;

const FieldDescription = styled.div`
    display: flex;
    align-items: end;
    gap: 0 5px;
`;

const FieldName = styled.span`
    font-size: 1em;
    font-weight: 600;
`;

const FieldValue = styled.span`
    font-size: 1em;
`;

const LessonValue = styled(Link)`
    font-size: 1em;
    color: #3fbafd;
    transition: color;
    text-decoration: none;
    &:hover {
        color: #05a7ff;
    }
`;

const LessonDescriptionName = styled.div`
    margin-top: 20px;
    font-size: 1.4em;
    font-weight: 600;
    align-self: center;
`;

const LessonDescriptionContainer = styled.div`
    margin-top: 20px;
`;

const FileDownloadingContainer = styled.div`
    display: flex;
    margin-top: 10px;
`;

const FileDownloadingRecord = styled.span`
    cursor: pointer;
    font-size: 1.1em;
    color: #3fbafd;
    transition: color;
    &:hover {
        color: #05a7ff;
        text-decoration: underline;
    }
`;

const UserSolutionContainer = styled.div`
    margin-top: 10px;
    display: flex;
    flex-direction: column;
    gap: 10px 0;
`;

const UserSolutionName = styled.div`
    font-size: 1.05em;
    font-weight: 600;
`;

const Buttons = styled.div`
    margin-top: 20px;
    display: flex;
    gap: 0 15px;
`;

const CompleteButton = styled(CodeReviewFeature.UI.CompleteButton)``;

const ReviewCompletedAlert = styled(Alert)`
    margin-top: 20px;
`;

const StandardCodeContainer = styled.div`
    margin-top: 20px;
    display: flex;
    flex-direction: column;
    gap: 10px 0;
`;

const StandardCodeName = styled.div`
    font-size: 1.05em;
    font-weight: 600;
`;
