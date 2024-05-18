import { CloseOutlined, DeleteOutlined, WarningOutlined } from '@ant-design/icons';
import { LessonFeature, StoredFileFeature } from '@features';
import { Config, SharedUI } from '@shared';
import MDEditor from '@uiw/react-md-editor';
import { Button, Card, Checkbox, Form, Input } from 'antd';
import React, { useEffect, useState } from 'react';
import styled from 'styled-components';

type Props = {
    lessonId: string;
};

export const LessonEditing = ({ lessonId }: Props) => {
    const { form, initValue, lessonName, updating, formLoading, update, updatingResult } =
        LessonFeature.Model.useLessonEditingForm(lessonId);

    const [isExercise, setIsExercise] = useState(false);
    useEffect(() => {
        setIsExercise(!!initValue.exercise);
    }, [initValue]);

    return (
        <>
            {formLoading ? (
                <SharedUI.Loader />
            ) : (
                <Page>
                    <Header>{lessonName}</Header>
                    <StyledForm
                        form={form}
                        onFinish={update}
                        disabled={updating}
                        initialValues={initValue}
                        layout="vertical"
                    >
                        <FormItem
                            validateDebounce={1000}
                            rules={[Config.RuleForm.required]}
                            name="text"
                            label="Описание"
                        >
                            <MDEditor preview="edit" data-color-mode="light" />
                        </FormItem>
                        <Checkbox checked={isExercise} onChange={(e) => setIsExercise(e.target.checked)}>
                            Практический урок
                        </Checkbox>
                        {isExercise ? (
                            <>
                                <ExerciseBlockName>Настройка упражнения</ExerciseBlockName>
                                <ExerciseBlock>
                                    <FormItem
                                        validateDebounce={1000}
                                        rules={[Config.RuleForm.required]}
                                        name={['exercise', 'appArchiveId']}
                                        label="Zip-архив приложения"
                                        tooltip="Архив должен быть не больше 200Мб и содержать dockerfile."
                                    >
                                        <StoredFileFeature.UI.UploaderInput
                                            accept=".zip"
                                            validTypes={['application/x-zip-compressed']}
                                            maxSizeMb={200}
                                        />
                                    </FormItem>
                                    <FormItem
                                        validateDebounce={1000}
                                        rules={[Config.RuleForm.required]}
                                        name={['exercise', 'relativePathToDockerfile']}
                                        label="Путь до dockerfile"
                                        tooltip="Путь до dockerfile относительно начала архива. В качестве разделителей используйте '/'.
                                         Build context будет являться также начало архива, это нужно учитывать при написании dockerfile!"
                                    >
                                        <Input />
                                    </FormItem>
                                    <AppContainerBlockName>
                                        Настройка docker контейнера приложения
                                    </AppContainerBlockName>
                                    <FormItem
                                        name={['exercise', 'appContainer', 'hostname']}
                                        label={
                                            <a
                                                href="https://docs.docker.com/network/#ip-address-and-hostname"
                                                target="_blank"
                                                rel="noreferrer"
                                            >
                                                Hostname
                                            </a>
                                        }
                                    >
                                        <Input />
                                    </FormItem>
                                    <EnvsBlockName
                                        href="https://docs.docker.com/compose/environment-variables/"
                                        target="_blank"
                                        rel="noreferrer"
                                    >
                                        Переменные окржуения
                                    </EnvsBlockName>
                                    <Form.List name={['exercise', 'appContainer', 'envs']}>
                                        {(fields, { add, remove }) => (
                                            <>
                                                {fields.map(({ key, name }) => (
                                                    <EnvsBlock key={key}>
                                                        <Form.Item
                                                            validateDebounce={1000}
                                                            rules={[Config.RuleForm.required]}
                                                            name={[name, 'name']}
                                                            label="Название"
                                                        >
                                                            <Input />
                                                        </Form.Item>
                                                        <Form.Item
                                                            validateDebounce={1000}
                                                            rules={[Config.RuleForm.required]}
                                                            name={[name, 'value']}
                                                            label="Значение"
                                                        >
                                                            <Input />
                                                        </Form.Item>
                                                        <RemoveEnvsButton
                                                            icon={<DeleteOutlined />}
                                                            onClick={() => remove(name)}
                                                        />
                                                    </EnvsBlock>
                                                ))}
                                                <Button type="dashed" onClick={() => add()} block>
                                                    + Добавить новую переменную
                                                </Button>
                                            </>
                                        )}
                                    </Form.List>
                                    <ServiceAppsName>Настройка дополнительных сервисов</ServiceAppsName>
                                    <Form.List name={['exercise', 'serviceApps']}>
                                        {(fields, { add, remove }) => (
                                            <>
                                                {fields.map(({ key, name }) => (
                                                    <ServiceAppsBlock
                                                        size="small"
                                                        title={`Сервис ${name + 1}`}
                                                        key={key}
                                                        extra={
                                                            <CloseOutlined
                                                                onClick={() => {
                                                                    remove(name);
                                                                }}
                                                            />
                                                        }
                                                    >
                                                        <Form.Item
                                                            validateDebounce={1000}
                                                            rules={[Config.RuleForm.required]}
                                                            name={[name, 'name']}
                                                            label="Название образа"
                                                        >
                                                            <Input />
                                                        </Form.Item>
                                                        <Form.Item
                                                            validateDebounce={1000}
                                                            rules={[Config.RuleForm.required]}
                                                            name={[name, 'tag']}
                                                            label="Тег образа"
                                                        >
                                                            <Input />
                                                        </Form.Item>
                                                        <ServiceContainerBlockName>
                                                            Настройка docker контейнера
                                                        </ServiceContainerBlockName>
                                                        <Form.Item
                                                            name={[name, 'container', 'hostname']}
                                                            label={
                                                                <a
                                                                    href="https://docs.docker.com/network/#ip-address-and-hostname"
                                                                    target="_blank"
                                                                    rel="noreferrer"
                                                                >
                                                                    Hostname
                                                                </a>
                                                            }
                                                        >
                                                            <Input />
                                                        </Form.Item>
                                                        <EnvsBlockName
                                                            href="https://docs.docker.com/compose/environment-variables/"
                                                            target="_blank"
                                                            rel="noreferrer"
                                                        >
                                                            Переменные окржуения
                                                        </EnvsBlockName>
                                                        <Form.List name={[name, 'container', 'envs']}>
                                                            {(fields, { add, remove }) => (
                                                                <>
                                                                    {fields.map(({ key, name }) => (
                                                                        <EnvsBlock key={key}>
                                                                            <Form.Item
                                                                                validateDebounce={1000}
                                                                                rules={[Config.RuleForm.required]}
                                                                                name={[name, 'name']}
                                                                                label="Название"
                                                                            >
                                                                                <Input />
                                                                            </Form.Item>
                                                                            <Form.Item
                                                                                validateDebounce={1000}
                                                                                rules={[Config.RuleForm.required]}
                                                                                name={[name, 'value']}
                                                                                label="Значение"
                                                                            >
                                                                                <Input />
                                                                            </Form.Item>
                                                                            <RemoveEnvsButton
                                                                                icon={<DeleteOutlined />}
                                                                                onClick={() => remove(name)}
                                                                            />
                                                                        </EnvsBlock>
                                                                    ))}
                                                                    <Button type="dashed" onClick={() => add()} block>
                                                                        + Добавить новую переменную
                                                                    </Button>
                                                                </>
                                                            )}
                                                        </Form.List>
                                                        <HealthcheckBlockName
                                                            href="https://docs.docker.com/reference/dockerfile/#healthcheck"
                                                            target="_blank"
                                                            rel="noreferrer"
                                                        >
                                                            Healthcheck
                                                        </HealthcheckBlockName>
                                                        <Form.List name={[name, 'container', 'healthcheckTest']}>
                                                            {(fields, { add, remove }) => (
                                                                <>
                                                                    {fields.map(({ key, name }) => (
                                                                        <HealthchecBlock key={key}>
                                                                            <Form.Item
                                                                                validateDebounce={1000}
                                                                                rules={[Config.RuleForm.required]}
                                                                                name={[name]}
                                                                            >
                                                                                <Input />
                                                                            </Form.Item>
                                                                            <Button
                                                                                icon={<DeleteOutlined />}
                                                                                onClick={() => remove(name)}
                                                                            />
                                                                        </HealthchecBlock>
                                                                    ))}
                                                                    <Button type="dashed" onClick={() => add()} block>
                                                                        + Добавить элемент
                                                                    </Button>
                                                                </>
                                                            )}
                                                        </Form.List>
                                                    </ServiceAppsBlock>
                                                ))}
                                                <AddServiceButton type="dashed" onClick={() => add()} block>
                                                    + Добавить сервис
                                                </AddServiceButton>
                                            </>
                                        )}
                                    </Form.List>
                                    <RelativePathToInsertCodeFormItem
                                        validateDebounce={1000}
                                        rules={[Config.RuleForm.required]}
                                        name={['exercise', 'relativePathToInsertCode']}
                                        label="Путь до файла с пользовательским кодом"
                                        tooltip="Путь до файла, в который будет вставляться тестируемый код. Путь относителен начала архива.
                                         В качестве разделителей используйте '/'."
                                    >
                                        <Input />
                                    </RelativePathToInsertCodeFormItem>
                                    <FormItem
                                        validateDebounce={1000}
                                        rules={[Config.RuleForm.required]}
                                        name={['exercise', 'insertableCode']}
                                        label="Эталонный код"
                                        tooltip="При сохранении данный код будет проверться, что он действительно является решением. Также эталонный код будет доступен для менторов."
                                    >
                                        <SharedUI.CodeEditor />
                                    </FormItem>
                                    {!!updatingResult && !updatingResult.isSuccess ? (
                                        <ErrorBlock>
                                            <ErrorStatusContainer>
                                                <WarningOutlined />
                                                <ErrorStatusText>{updatingResult?.failData?.status}</ErrorStatusText>
                                            </ErrorStatusContainer>
                                            <ErrorText>
                                                {updatingResult?.failData?.errorMessage ?? 'Текст ошибки отсутствует.'}
                                            </ErrorText>
                                        </ErrorBlock>
                                    ) : (
                                        <></>
                                    )}
                                </ExerciseBlock>
                            </>
                        ) : (
                            <></>
                        )}
                        {updating ? <SharedUI.SmalLoader /> : <SaveButton htmlType="submit">Сохранить</SaveButton>}
                    </StyledForm>
                </Page>
            )}
        </>
    );
};

const Page = styled.div`
    margin: 30px 0 40px 0;
`;

const Header = styled.div`
    font-size: 2em;
    font-weight: 600;
`;

const StyledForm = styled(Form<LessonFeature.Model.LessonEditingForm>)`
    display: flex;
    flex-direction: column;
    margin-top: 20px;
`;

const FormItem = styled(Form.Item<LessonFeature.Model.LessonEditingForm>)`
    width: 100%;
`;

const ExerciseBlock = styled.div`
    margin-top: 10px;
`;

const ExerciseBlockName = styled.div`
    font-size: 1.5em;
    font-weight: 600;
    margin-top: 25px;
`;

const AppContainerBlockName = styled.div`
    font-size: 1.2em;
    font-weight: 600;
`;

const EnvsBlockName = styled.a``;

const EnvsBlock = styled.div`
    display: grid;
    grid-template-columns: 45% 45% 10%;
    grid-column-gap: 10px;
    align-items: center;
`;

const RemoveEnvsButton = styled(Button)`
    margin-top: 6px;
`;

const ServiceAppsName = styled.div`
    font-size: 1.2em;
    font-weight: 600;
    margin-top: 25px;
`;

const ServiceAppsBlock = styled(Card)`
    margin-top: 10px;
`;

const AddServiceButton = styled(Button)`
    margin-top: 10px;
`;

const ServiceContainerBlockName = styled.div`
    font-size: 1.1em;
    font-weight: 600;
`;

const HealthcheckBlockName = styled.a`
    display: block;
    margin-top: 20px;
`;

const HealthchecBlock = styled.div`
    display: grid;
    grid-template-columns: 90% 10%;
    grid-column-gap: 10px;
    align-items: start;
`;

const RelativePathToInsertCodeFormItem = styled(FormItem)`
    margin-top: 10px;
`;

const ErrorBlock = styled.div`
    margin-top: 10px;
    display: flex;
    flex-direction: column;
`;

const ErrorStatusContainer = styled.div`
    background-color: #e02b2b;
    border: 2px solid #e02b2b;
    border-radius: 10px 10px 0 0;
    display: flex;
    color: white;
    font-size: 1.1em;
    padding: 7px 15px;
    grid-column-gap: 10px;
    font-weight: 500;
`;

const ErrorStatusText = styled.span`
    display: block;
`;

const ErrorText = styled.div`
    white-space: pre-line;
    border: 2px solid #e02b2b;
    border-radius: 0 0 0 0;
    font-size: 1em;
    padding: 7px 15px;
    max-height: 500px;
    overflow: auto;
`;

const SaveButton = styled(Button)`
    margin-top: 20px;
`;
