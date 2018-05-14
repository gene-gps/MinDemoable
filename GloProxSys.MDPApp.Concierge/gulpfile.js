/// <binding BeforeBuild='styles,scripts' ProjectOpened='default' />

var gulp = require('gulp'),
    sass = require('gulp-sass'),
    concat = require('gulp-concat'),
    rename = require('gulp-rename'),
    uglify = require('gulp-uglify'),
    cleanCSS = require('gulp-clean-css');
    fs = require('fs'),
    buildConfiguration = fs.readFileSync("BuildConfiguration.txt", 'ascii').split(/[\n\r] /)[0]
    ;

gulp.task('styles', function () {
    if (buildConfiguration === 'Release') {
        gulp.src('Source/css/**/*.scss')
            .pipe(sass().on('error', sass.logError))
            .pipe(concat('styles.css'))
            .pipe(cleanCSS({ rebaseTo: 'wwwroot/css' })).minify()
            .pipe(gulp.dest('wwwroot/css'))
            .pipe(rename('styles.min.css'))
                .pipe(gulp.dest('wwwroot/css'));    
    }
    else {
        gulp.src('Source/css/**/*.scss')
            .pipe(sass().on('error', sass.logError))
            .pipe(gulp.dest('wwwroot/css'));
    }
});



gulp.task('scripts', function () {
    if (buildConfiguration === 'Release') {
        gulp.src('Source/js/**/*.js')
            .pipe(concat('scripts.js'))
            .pipe(gulp.dest('wwwroot/js'))
            .pipe(rename('scripts.min.js'))
            .pipe(uglify())
            .pipe(gulp.dest('wwwroot/js'));
    }
    else {
        gulp.src('Source/js/**/*.js')
            .pipe(gulp.dest('wwwroot/js'));
    }

    gulp.src('Source/libs/**/*.js')
        .pipe(gulp.dest('wwwroot/libs'));
});

//Watch tasks
gulp.task('scss-watch', function () {
    gulp.watch('Source/css/**/*.scss', ['styles']);
});

gulp.task('scripts-watch', function () {
    gulp.watch('Source/js/**/*.js', ['scripts']);
});

//Default task
gulp.task('default', ['scss-watch', 'scripts-watch']);