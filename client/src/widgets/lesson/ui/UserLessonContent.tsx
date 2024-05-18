import { LessonEntity } from '@entities';
import { Config, SharedUI } from '@shared';
import MDEditor from '@uiw/react-md-editor';
import { Alert, Button } from 'antd';
import React, { useCallback, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import styled from 'styled-components';
import { useCodeForm } from '../model';

type LessonItem = {
    lessonId: string;
    inCurrentModule: boolean;
};

export type UserLessonContentProps = {
    lesson: LessonEntity.Type.UserLesson;
    nextLesson: LessonItem | null;
    previousLesson: LessonItem | null;
    onVisit: (lesson: LessonEntity.Type.UserLesson) => void;
    onToLesson: (lessonId: string) => void;
    onLessonComplete?: (lesson: LessonEntity.Type.UserLesson) => void;
};

export const UserLessonContent = ({
    nextLesson,
    previousLesson,
    lesson,
    onVisit,
    onToLesson,
    onLessonComplete,
}: UserLessonContentProps) => {
    const navigate = useNavigate();
    const toUserLesson = useCallback(
        (lessonId?: string) => {
            if (!!lessonId) {
                navigate(Config.routes.userLesson.getLink(lessonId));
                onToLesson(lessonId);
            }
        },
        [navigate],
    );

    useEffect(() => {
        onVisit(lesson);
    }, [onVisit]);

    const { downloadApp } = LessonEntity.Model.useUserLessonExerciseAppDownloading(lesson.id);

    const { code, setCode, isTestingProcess, submit, testingResult, formIsValid, isExerciseCompleted, onTryAgain } =
        useCodeForm(lesson, onLessonComplete);

    return (
        <Page>
            <Header>{lesson.name}</Header>
            <ContentContainer data-color-mode="light">
                <MDEditor.Markdown skipHtml={true} source={lesson.text} />
                {!lesson.exercise ? (
                    <></>
                ) : (
                    <ExerciseContainer>
                        <FileDownloadingContainer>
                            <FileDownloadingRecord onClick={downloadApp}>Скачать архив проекта</FileDownloadingRecord>
                        </FileDownloadingContainer>
                        <CodeEditorContainer>
                            <SharedUI.CodeEditor
                                value={code}
                                onChange={setCode}
                                readonly={isExerciseCompleted}
                                theme={isExerciseCompleted ? 'light' : 'dark'}
                            />
                        </CodeEditorContainer>
                        {isExerciseCompleted ? (
                            <CompletedAlert
                                message="Все тесты пройдены, задача выполнена!"
                                type="success"
                                showIcon
                                description="Вы можете повторно отправить свое новое решение."
                            />
                        ) : (
                            <></>
                        )}
                        <FormButtonsContainer>
                            {isExerciseCompleted ? (
                                <FormButton type="primary" onClick={onTryAgain}>
                                    Попробовать еще раз
                                </FormButton>
                            ) : (
                                <FormButton
                                    type="primary"
                                    disabled={!formIsValid || isTestingProcess}
                                    loading={isTestingProcess}
                                    onClick={submit}
                                >
                                    Отправить
                                </FormButton>
                            )}
                        </FormButtonsContainer>
                        {testingResult != undefined && !testingResult.isSuccess ? (
                            <ErrorAlert
                                type="error"
                                message={testingResult.error!.status}
                                description={
                                    <ErrorText>{testingResult.error?.text ?? 'Описание ошибки отсутствует.'}</ErrorText>
                                }
                                showIcon
                            />
                        ) : (
                            <></>
                        )}
                    </ExerciseContainer>
                )}
            </ContentContainer>
            <NavigationButtons>
                <PreviousLessonButton
                    onClick={() => toUserLesson(previousLesson?.lessonId)}
                    size="large"
                    disabled={!previousLesson}
                >
                    {!previousLesson || previousLesson.inCurrentModule ? 'Назад' : 'Предыдущий модуль'}
                </PreviousLessonButton>
                <NextLessonButton
                    type="primary"
                    onClick={() => toUserLesson(nextLesson?.lessonId)}
                    size="large"
                    disabled={!nextLesson}
                >
                    {!nextLesson || nextLesson.inCurrentModule ? 'Далее' : 'Следующий модуль'}
                </NextLessonButton>
            </NavigationButtons>
        </Page>
    );
};

const Page = styled.div`
    margin: 30px 0 40px 0;
    display: flex;
    flex-direction: column;
    align-items: center;
`;

const Header = styled.div`
    font-size: 2em;
    font-weight: 600;
`;

const ContentContainer = styled.div`
    display: flex;
    flex-direction: column;
    margin-top: 30px;
    padding: 40px 35px 50px 35px;
    width: 100%;
    background-color: white;
    border-radius: 10px;
`;

const ExerciseContainer = styled.div`
    display: flex;
    flex-direction: column;
    margin-top: 20px;
    width: 100%;
`;

const FileDownloadingContainer = styled.div`
    display: flex;
`;

const FileDownloadingRecord = styled.span`
    cursor: pointer;
    font-size: 1.2em;
    color: #3fbafd;
    transition: color;
    &:hover {
        color: #05a7ff;
        text-decoration: underline;
    }
`;

const CodeEditorContainer = styled.div`
    margin-top: 10px;
`;

const CompletedAlert = styled(Alert)`
    margin-top: 20px;
`;

const FormButtonsContainer = styled.div`
    display: flex;
    margin-top: 20px;
`;

const FormButton = styled(Button)``;

const ErrorAlert = styled(Alert)`
    margin-top: 20px;
`;

const ErrorText = styled.div`
    white-space: pre-line;
`;

const NavigationButtons = styled.div`
    display: flex;
    margin-top: 30px;
    width: 100%;
    justify-content: space-between;
`;

const PreviousLessonButton = styled(Button)``;

const NextLessonButton = styled(Button)``;
