'use strict';

var { series, src, dest } = require('gulp');
var rename = require('gulp-rename');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var cssPurge = require('gulp-css-purge');
var sourcemap = require('gulp-sourcemaps');

sass.compiler = require('sass');

function compileScss() {
  return src('scss/main.scss')
      .pipe(sourcemap.init())
      .pipe(sass({outputStyle: 'compressed'}).on('error', sass.logError))
      .pipe(autoprefixer())
      .pipe(cssPurge())
      .pipe(rename("styles.min.css"))
      .pipe(sourcemap.write())
      .pipe(dest('../docs/css/'));
}

exports.default = series(compileScss);
