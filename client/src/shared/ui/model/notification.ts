import { notification } from 'antd';
import { createEffect } from 'effector';

export const notifyErrorFx = createEffect((errorMessage: string) => {
    notification.error({
        message: errorMessage,
        placement: 'bottomRight',
    });
});

export const notifySuccessFx = createEffect((message: string) => {
    notification.success({
        message: message,
        placement: 'bottomRight',
    });
});
