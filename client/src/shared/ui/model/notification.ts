import { notification } from 'antd';
import { createEffect } from 'effector';

export const notifyErrorFx = createEffect((errorMessage: string) => {
    notification.error({
        message: errorMessage,
        placement: 'bottomRight',
    });
});
