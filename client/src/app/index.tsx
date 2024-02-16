import React from 'react';
import { Routing } from 'pages';
import withProviders from './providers';
import './index.css';

const App = () => {
    return <Routing />;
};

export default withProviders(App);
