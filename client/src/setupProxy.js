const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    '/api/seasons',
    '/api/characters',
    '/api/quotes',
    '/api/episodes'
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7021',
        secure: false
    });

    app.use(appProxy);
};
