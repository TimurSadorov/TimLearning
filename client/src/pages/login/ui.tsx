import React from 'react';
import { Button, Form, Input } from 'antd';
import styled from 'styled-components';

interface AuthForm {
    email: string;
    password: string;
}

export const LoginPage = () => {
    const [form] = Form.useForm();
    // const [submit, setSubmit] = useState(false);

    // useEffect(() => {
    //     form.setFields([
    //         {
    //             name: 'username',
    //             errors: ['Ошибка с бека!'],
    //         },
    //     ]);
    // }, [submit]);

    const onFinish = (form: AuthForm) => {
        console.log(form);
    };

    return (
        <PageContainer>
            <FormBock>
                <Header>Авторизация</Header>
                <StyledForm onFinish={onFinish} form={form}>
                    <Form.Item
                        validateFirst
                        rules={[{ required: true, message: 'Почта должна быть заполнен!' }]}
                        name="username"
                    >
                        <Input placeholder="Почта" />
                    </Form.Item>
                    <Form.Item
                        validateFirst="parallel"
                        rules={[{ required: true, message: 'Пароль должна быть заполнен!' }]}
                        name="password"
                    >
                        <Input placeholder="Пароль" />
                    </Form.Item>
                    <SubmitButton htmlType="submit">Войти</SubmitButton>
                </StyledForm>
            </FormBock>
        </PageContainer>
    );
};

const PageContainer = styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
`;

const FormBock = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
`;

const Header = styled.div`
    font-size: 1.7em;
    font-weight: 500;
    margin: 20px 0;
`;

const StyledForm = styled(Form<AuthForm>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const SubmitButton = styled(Button)`
    width: 50%;
    margin: -10px 0;
`;
