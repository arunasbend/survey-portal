const HtmlWebpackPlugin = require('html-webpack-plugin');
const path = require('path');

const appDir = path.resolve(__dirname, 'app/');
const buildDir = path.resolve(__dirname, 'wwwroot/dist/');

module.exports = {
  entry: {
    main: path.resolve(appDir, 'index.jsx'),
  },
  output: {
    filename: 'bundle.js',
    path: buildDir,
    publicPath: '/dist/',
  },
  module: {
    rules: [
      {
        test: /\.json$/,
        loader: 'json-loader',
      },
      {
        enforce: 'pre',
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        loader: 'eslint-loader',
        options: {
          failOnError: true,
          configFile: './.eslintrc.json',
        },
      },
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        loader: 'babel-loader',
        query: {
          presets: ['es2015', 'react'],
          plugins: ['transform-object-rest-spread'],
        },
      },
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader'],
      },
      {
        test: /\.(ttf|woff|jpeg|jpg|png|gif|svg|eot|woff2)$/,
        use: [
          {
            loader: 'file-loader',
            options: {
              outputPath: path.join('assets', '/'),
              publicPath: '/dist/assets/',
              name: '[name]--[hash:base64:5].[ext]',
            },
          },
        ],
      },
    ],
  },
  devtool: 'source-map',
  resolve: {
    extensions: ['.js', '.jsx'],
  },
  devServer: {
    inline: true,
    contentBase: 'app',
    historyApiFallback: {
      index: 'index.html',
    },
  },
  plugins: [new HtmlWebpackPlugin({
    template: path.resolve(appDir, 'index.html'),
  })],
};
