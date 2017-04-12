var fs = require('fs'), vm = require('vm'), merge = require('deeply'), chalk = require('chalk'), es = require('event-stream');

var gulp = require('gulp'), rjs = require('gulp-requirejs-bundler'), concat = require('gulp-concat'), clean = require('gulp-clean'),
    replace = require('gulp-replace'), uglify = require('gulp-uglify'), htmlreplace = require('gulp-html-replace'), typescript = require('gulp-tsc'),
    useref = require('gulp-useref'), minifyCss = require('gulp-minify-css');

// Config
var requireJsRuntimeConfig = vm.runInNewContext(fs.readFileSync('require.config.js') + '; require;');
    requireJsOptimizerConfig = merge(requireJsRuntimeConfig, {
        out: 'shortener.js',
        baseUrl: './src',
        name: 'Main',
        paths: {
            requireLib: '../node_modules/requirejs/require'
        },
        include: [
            'requireLib',
        ],
        insertRequire: ['Main'],
        bundles: {
        }
    });

// Compile all .ts files, producing .js and source map files alongside them
gulp.task('ts', function() {
    return gulp.src(['src/**/*.ts','typings/*.d.ts'])
        .pipe(typescript({
            module: 'amd',
            target: "es3",
            noImplicitAny: true,
            noImplicitReturns: true,
            inlineSourceMap: true,
            experimentalDecorators: true,
            outDir: './'
        }))
        .pipe(gulp.dest('./src'));
});

// Discovers all AMD dependencies, concatenates together all required .js files, minifies them
gulp.task('js', ['ts'], function () {
    return rjs(requireJsOptimizerConfig)
        .pipe(uglify({ preserveComments: 'some' }))
        .pipe(gulp.dest('./.deploy/'));
});

// Concatenates CSS files, rewrites relative paths to Bootstrap fonts, copies Bootstrap fonts
gulp.task('css', function () {  
        appCss = gulp.src(['./node_modules/bootstrap/dist/css/bootstrap.css',
 			   './node_modules/bootstrap/dist/css/bootstrap-theme.css']);
	return es.concat(appCss)
			.pipe(minifyCss({compatibility: 'ie8'}))
			.pipe(concat('shortener.css'))
			.pipe(gulp.dest('./.deploy/'));
});

gulp.task('clean', function() {
    var distContents = gulp.src('./.deploy/**/*', { read: false }),
        generatedJs = gulp.src(['app/**/*.js'], { read: false })
            .pipe(es.mapSync(function(data) {
                // Include only the .js/.js.map files that correspond to a .ts file
                return fs.existsSync(data.path.replace(/\.js(\.map)?$/, '.ts')) ? data : undefined;
            }));
    return es.merge(distContents, generatedJs).pipe(clean());
});

gulp.task('default', ['js', 'css'], function(callback) {
    callback();
    console.log('\nPlaced optimized files in ' + chalk.magenta('.deploy/\n'));
});
