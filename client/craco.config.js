const path = require(`path`);

module.exports = {
    webpack: {
        alias: {
            '@app': path.resolve(__dirname, 'src/app'),
            '@pages': path.resolve(__dirname, 'src/pages'),
            '@pages': path.resolve(__dirname, 'src/pages'),
            '@features': path.resolve(__dirname, 'src/features'),
            '@entities': path.resolve(__dirname, 'src/entities'),
            '@shared': path.resolve(__dirname, 'src/shared'),
        }
    },
};