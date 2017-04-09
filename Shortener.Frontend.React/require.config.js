requirejs.config({
    baseUrl: "src",
    deps: ["Main"],
    map: {
        '*': {
            'css': 'require-css'
        }
    },
    packages: [{
	name: 'moment',
	location: '../node_modules/moment',
	main: 'moment'
    }, {
	name: 'moment-locale-ru',
	location: '../node_modules/moment/locale',
	main: 'ru'
    }],
    paths: {
        "require-css": "../node_modules/require-css/css",
        "react": "../node_modules/react/dist/react",
        "react-dom": "../node_modules/react-dom/dist/react-dom",
        "react-router": "../node_modules/react-router/umd/ReactRouter",
        "bootstrap": "../node_modules/bootstrap/dist/css",
        "react-bootstrap": "../node_modules/react-bootstrap/dist/react-bootstrap",

        "history": "../node_modules/history/umd/History",
        "es6-promise": "../node_modules/es6-promise/dist/es6-promise",
        "es5-shim": "../node_modules/es5-shim/es5-shim",
        "es5-shim/es5-sham": "../node_modules/es5-shim/es5-sham",
        "lodash": "../node_modules/lodash-compat/lodash",
        "classnames": "../node_modules/classnames/index",
        "jquery": "../node_modules/jquery/dist/jquery",
        "microevent": "../node_modules/microevent/microevent"
    }
});
