const path = require('path');
const CopyPlugin = require("copy-webpack-plugin");

module.exports = {
	mode: 'production',
	entry: ['./src/js/site.js'],
	plugins: [
		new CopyPlugin({
			patterns: [
				{ from: path.join(__dirname, 'node_modules/govuk-frontend/govuk/assets'), to: path.join(__dirname, 'assets') },
				{ from: path.resolve(__dirname, 'node_modules/@ministryofjustice/frontend/moj/assets'), to: path.resolve(__dirname, 'assets') },
				{ from: path.resolve(__dirname, 'node_modules/jquery/dist'), to: path.resolve(__dirname, 'dist/vendor') },
				{ from: path.resolve(__dirname, 'node_modules/@ministryofjustice/frontend/moj'), to: path.resolve(__dirname, 'dist/moj') },
				{ from: path.resolve(__dirname, 'src/js'), to: path.resolve(__dirname, 'dist') },
			]
		}),
	],
};