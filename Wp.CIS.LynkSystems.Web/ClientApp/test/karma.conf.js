// Karma configuration file, see link for more information
// https://karma-runner.github.io/0.13/config/configuration-file.html
// https://stackoverflow.com/questions/19228209/javascript-internationalization-api-is-not-supported-by-phantomjs

module.exports = function (config) {
    config.set({
        basePath: '.',
        frameworks: ['intl-shim', 'jasmine'],
        files: [
            '../../node_modules/Intl/locale-data/jsonp/en-US.js',
            '../../wwwroot/dist/vendor.js',
            './globals-for-tests.js',
            './boot-tests.ts'
        ],
        preprocessors: {
            './boot-tests.ts': ['webpack']
        },
        reporters: ['progress'],
        port: 9876,
        colors: true,
        logLevel: config.LOG_INFO,
        autoWatch: true,
        browsers: ['ChromeHeadless'],//,'Chrome','PhantomJS'
        mime: { 'application/javascript': ['ts','tsx'] },
        singleRun: false,
        webpack: require('../../webpack.config.js')().filter(config => config.target !== 'node'), // Test against client bundle, because tests run in a browser
        webpackMiddleware: { stats: 'errors-only' }
    });
};
