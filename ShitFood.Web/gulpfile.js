'use strict';

var { series, src, dest } = require('gulp');
var rename = require('gulp-rename');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var cssPurge = require('gulp-css-purge');
var sourcemap = require('gulp-sourcemaps');
var uglify = require('gulp-uglify');

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

function compileJs() {
  return src('js/get-restaurants.js')
      .pipe(uglify())
      .pipe(rename("get-restaurants.min.js"))
      .pipe(dest('../docs/js/'))
}

exports.default = series(compileScss, compileJs);
