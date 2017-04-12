var require = {
    baseUrl: "/Frontend/src",
    deps: ["Main"],
    map: {
        '*': {
            'css': 'require-css',
            'text': 'text' 		
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
        "text": "../node_modules/requirejs-text/text",
        "knockout": "../node_modules/knockout/build/output/knockout-latest",
        "director": "../node_modules/director/build/director",
        "bootstrap": "../node_modules/bootstrap/dist/css",
        "es6-promise": "../node_modules/es6-promise/dist/es6-promise",
        "es5-shim": "../node_modules/es5-shim/es5-shim",
        "es5-shim/es5-sham": "../node_modules/es5-shim/es5-sham"
    },
    shim: {
        "director": {
            exports: 'Router'
        }
    }
};
