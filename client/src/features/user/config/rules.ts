import { Rule } from 'antd/es/form';

export const emailRules: Rule[] = [
    {
        required: true,
        message: 'Почта должна быть заполнен.',
    },
    {
        pattern: /^[A-Z0-9._%+-]+@[A-Z0-9-]+.+.[A-Z]{2,4}$/i,
        message: 'Невалидная почта.',
    },
];

export const passwordRules: Rule[] = [
    {
        required: true,
        message: 'Пароль должна быть заполнен.',
    },
    {
        min: 8,
        message: 'Пароль должен содержать минимум 8 символов.',
    },
];
