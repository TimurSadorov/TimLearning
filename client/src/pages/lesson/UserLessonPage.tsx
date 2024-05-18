import React, { useCallback } from 'react';
import { Config, SharedUI, Utils } from '@shared';
import { LessonWidget, ModuleWidget } from '@widgets';
import { Layout } from 'antd';
import { Navigate, useParams } from 'react-router-dom';
import styled from 'styled-components';
import { useSiderSwitch, useUserLessonPage } from './model';

export const UserLessonPage = () => {
    const { lessonId } = useParams<{ lessonId: string }>();
    if (lessonId == undefined || !Utils.isValidGuid(lessonId)) {
        return <Navigate to={Config.routes.root.path} />;
    }

    const {
        userCourse,
        userLesson,
        userLessonLoading,
        currentModule,
        selectedNavigationModule,
        setSelectedNavigationModuleId,
        nextLesson,
        previousLesson,
        visitLesson,
        onCompleteLesson,
    } = useUserLessonPage(lessonId);
    const { siderType, setSiderType } = useSiderSwitch();

    const onClickModuleRecord = useCallback(
        (moduleId: string) => {
            setSiderType('lessons');
            setSelectedNavigationModuleId(moduleId);
        },
        [setSelectedNavigationModuleId],
    );

    const onClickModuleBack = useCallback(() => {
        setSiderType('modules');
    }, [setSelectedNavigationModuleId]);

    const onToLesson = useCallback(() => {
        setSiderType('lessons');
    }, [setSiderType]);

    return (
        <Page>
            {siderType === 'modules' ? (
                <ModuleWidget.UI.UserModulesSider
                    courseCompletionPercentage={userCourse?.completionPercentage}
                    courseShortName={userCourse?.shortName}
                    modules={userCourse?.modules}
                    selectedModuleId={currentModule?.id}
                    onClickModuleRecord={onClickModuleRecord}
                />
            ) : (
                <LessonWidget.UI.UserLessonSider
                    lessons={selectedNavigationModule?.lessons}
                    moduleCompletionPercentage={selectedNavigationModule?.completionPercentage}
                    moduleName={selectedNavigationModule?.name}
                    selectedLessonId={userLesson?.id}
                    onClickModuleBack={onClickModuleBack}
                />
            )}
            <LessonContainer>
                <LessonContent>
                    {!userLessonLoading && !!userLesson && nextLesson !== undefined && previousLesson !== undefined ? (
                        <LessonWidget.UI.UserLessonContent
                            lesson={userLesson}
                            previousLesson={previousLesson}
                            nextLesson={nextLesson}
                            onVisit={visitLesson}
                            onToLesson={onToLesson}
                            onLessonComplete={onCompleteLesson}
                        />
                    ) : (
                        <SharedUI.Loader />
                    )}
                </LessonContent>
            </LessonContainer>
        </Page>
    );
};

const Page = styled(Layout)`
    min-height: calc(100vh - 51px);
`;

const LessonContainer = styled.div`
    flex: 1;
    justify-content: center;
    display: flex;
`;

const LessonContent = styled.div`
    width: 55vw;
`;
