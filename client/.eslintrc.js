module.exports = {
    parser: '@typescript-eslint/parser',
    parserOptions: {
        createDefaultProgram: true,
        ecmaFeatures: {
            jsx: true,
        },
        ecmaVersion: 2018,
        tsconfigRootDir: __dirname,
        sourceType: 'module',
        project: './tsconfig.json',
    },
    extends: [
        'plugin:@typescript-eslint/recommended',
        'plugin:react/recommended',
        'plugin:boundaries/recommended',
        'plugin:prettier/recommended', // Make sure this is always the last element in the array.
    ],
    settings: {
        react: {
            version: 'detect',
        },
        'boundaries/elements': [
            {
                type: 'app',
                pattern: 'src/app/*',
            },
            {
                type: 'page',
                pattern: 'src/pages/*',
            },
            {
                type: 'widget',
                pattern: 'src/widgets/*',
            },
            {
                type: 'feature',
                pattern: 'src/features/*',
            },
            {
                type: 'entity',
                pattern: 'src/entities/*',
            },
            {
                type: 'shared',
                pattern: 'src/shared/*',
            },
        ],
    },
    plugins: ['prettier', 'boundaries'],
    env: {
        browser: true,
        amd: true,
        node: true,
    },
    globals: {
        Atomics: 'readonly',
        SharedArrayBuffer: 'readonly',
    },
    rules: {
        'linebreak-style': 'off',
        'prettier/prettier': [
            'error',
            {
                endOfLine: 'auto',
            },
            {
                usePrettierrc: true,
            },
        ],
        'no-console': 'warn',
        '@typescript-eslint/no-empty-function': 'off',
        'no-eval': 'off',
        '@typescript-eslint/explicit-module-boundary-types': 'off',
        '@typescript-eslint/no-non-null-assertion': 'off',
        '@typescript-eslint/no-explicit-any': 'off',
        'react/jsx-key': 0,
        'boundaries/element-types': [
            2,
            {
                default: 'disallow',
                message: '${file.type} is not allowed to import ${dependency.type}',
                rules: [
                    {
                        from: ['app'],
                        allow: ['app', 'page', 'widget', 'feature', 'entity', 'shared'],
                    },
                    {
                        from: ['page'],
                        allow: ['page', 'widget', 'feature', 'entity', 'shared'],
                    },
                    {
                        from: ['widget'],
                        allow: ['widget', 'feature', 'entity', 'shared'],
                    },
                    {
                        from: ['feature'],
                        allow: ['feature', 'entity', 'shared'],
                    },
                    {
                        from: ['entity'],
                        allow: ['entity', 'shared'],
                    },
                    {
                        from: ['shared'],
                        allow: ['shared'],
                    },
                ],
            },
        ],
    },
};
