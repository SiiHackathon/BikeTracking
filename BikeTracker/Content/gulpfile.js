var gulp = require('gulp');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var watch = require('gulp-watch');
var rename = require('gulp-rename');

gulp.task('sass', function() {
    return gulp.src('scss/Site.scss')
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9', 'opera 12.1', 'ios 6', 'android 4'))
        .pipe(rename('Site.css'))
        .pipe(gulp.dest('.'));
});

gulp.task('watch', function() {
    gulp.watch('scss/**/*.scss', ['sass']);
});

gulp.task('build', ['sass']);
