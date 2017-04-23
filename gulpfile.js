var gulp = require('gulp');
var cleanCss = require('gulp-clean-css');
var concat = require('gulp-concat');
var sass = require('gulp-sass');
var sourcemaps = require('gulp-sourcemaps');
var uglify = require('gulp-uglify');
var gutil = require('gulp-util');

var production = gutil.env.production ? true : false;

if (production) {
    process.env.NODE_ENV = 'production';
}

const paths = {
    js: {
        files: 'js/*.js',
        concat: 'app.js',
        dest: 'build'
    },
    sass: {
        files: 'scss/*.scss',
        dest: 'build'
    }
};


function compileSass() {
    return gulp.src(paths.sass.files)
      .pipe(production ? gutil.noop() : sourcemaps.init())
      .pipe(sass().on('error', sass.logError))
      .pipe(production ? cleanCss() : gutil.noop())
      .pipe(production ? gutil.noop() : sourcemaps.write())
      .pipe(gulp.dest(paths.sass.dest));
}

gulp.task('sass-compile', compileSass);
gulp.task('sass-compile-build', gulp.series(compileSass));
gulp.task('sass-watch', function () {
    gulp.watch(paths.sass.files, { usePolling: true }, gulp.series('sass-compile-build'));
});
gulp.task('sass', gulp.series('sass-compile', 'sass-watch'));


function compileJs() {
    return gulp.src(paths.js.files)
      .pipe(production ? gutil.noop() : sourcemaps.init())
      .pipe(concat(paths.js.concat))
      .pipe(production ? uglify() : gutil.noop())
      .pipe(production ? gutil.noop() : sourcemaps.write())
      .pipe(gulp.dest(paths.js.dest));
}

gulp.task('js-compile', gulp.series(compileJs));
gulp.task('js-compile-build', gulp.series(compileJs));
gulp.task('js-watch', function () {
    gulp.watch(paths.js.files, { usePolling: true }, gulp.series('js-compile-build'));
});
gulp.task('js', gulp.series('js-compile', 'js-watch'));


gulp.task('watch',
  gulp.series(
    gulp.parallel('js-compile', 'sass-compile'),
    gulp.parallel('js-watch', 'sass-watch')
  )
);


gulp.task('default', gulp.series('js-compile', 'sass-compile'));
