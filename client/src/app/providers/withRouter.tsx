import React from 'react';
import { FC, Suspense } from 'react';
import { BrowserRouter } from 'react-router-dom';

// eslint-disable-next-line react/display-name
export const withRouter = (Component: FC) => () => (
    <BrowserRouter>
        <Suspense fallback="Loading...">
            <Component />
        </Suspense>
    </BrowserRouter>
);
