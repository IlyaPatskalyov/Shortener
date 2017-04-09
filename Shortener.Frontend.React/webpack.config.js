var path = require('path');
var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var failPlugin = require('webpack-fail-plugin');
 
module.exports = {
    context: path.join(__dirname, '/src'),
    entry: {
        shortener: "./Main"
    },
    output: {
        path: path.resolve(__dirname, '.deploy'),
        filename: '[name].js'
    },
    devtool: 'source-map',
    resolveLoader: { 
	root: [ __dirname, path.resolve('./node_modules') ]
    },		
    resolve: {
	root: [ __dirname, path.resolve('./node_modules') ],
        extensions: ['', '.webpack.js', '.web.js', '.tsx',  '.ts', '.js'],
        alias: {
            "lodash": "lodash-compat",
            "moment": path.join(__dirname, "node_modules/moment/moment"),
            "moment-locale-ru": path.join(__dirname, "node_modules/moment/locale/ru")
        }
    },
    module: {
        loaders: [
            {
                test: /\.ts(x?)$/,
                loader: 'ts?configFileName=webpack.tsconfig.json'
            },
            {
                test: /\.(styl)$/,
                loader: ExtractTextPlugin.extract('style', 'css?sourceMap!postcss!stylus?sourceMap')
            },
            {
                test: /\.(css)$/,
                loader: ExtractTextPlugin.extract('style', 'css?sourceMap!postcss?sourceMap')
            },
            {
                test: /\.(gif|png|woff|woff2|eot|ttf|svg)$/,
                loader: "file?name=shortener_[hash:base64:8].[ext]"
            }
        ]
    },
    plugins: [
        failPlugin,
        new webpack.optimize.OccurenceOrderPlugin(),
        new webpack.NormalModuleReplacementPlugin(/css\!.*/, function(result) {
            if (result.request.startsWith("css!bootstrap"))
                result.request = path.join(__dirname, "node_modules/bootstrap/dist/css", result.request.replace(/css\!bootstrap\/(.*)/, '$1.css'));
            else
                result.request = result.request.replace(/css\!(.*)/, '$1.styl');
        }),
        new webpack.DefinePlugin({
            'process.env.NODE_ENV': JSON.stringify(process.env.NODE_ENV)
        }),
        new ExtractTextPlugin("shortener.css")
    ]
};

if (process.env.NODE_ENV === 'production') {
    module.exports.plugins.push(
        new webpack.optimize.UglifyJsPlugin({
            compress: {
                screw_ie8: true
            }
        })
    );
}
