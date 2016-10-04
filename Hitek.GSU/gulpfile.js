/// <binding />

"use strict";

var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    inject = require("gulp-inject"),
   // uglify = require("gulp-uglify"),
    clean = require('gulp-clean');

/* watch = require("gulp-watch"),
 prefixer = require("gulp-autoprefixer"),
 uglify = require("gulp-uglify"),
//  sass = require("gulp-sass"),
 sourcemaps = require("gulp-sourcemaps"),
 rigger = require("gulp-rigger"),
//  cssmin = require("gulp-minify-css"),
 imagemin = require("gulp-imagemin"),
 pngquant = require("imagemin-pngquant"),
 rimraf = require("rimraf"),
 browserSync = require("browser-sync"),
 inject = require("gulp-inject"),*/
//plumber = require("gulp-plumber");

;

var path = {
    build: { //Тут мы укажем куда складывать готовые после сборки файлы
        html: "wwwroot/",
        json: "wwwroot/api/",
        js: "wwwroot/js/",
        css: "wwwroot/css/",
        img: "wwwroot/images/",
        fonts: "wwwroot/fonts/",
        app: "wwwroot/js/app",
        override: "wwwroot/js/override"
    },
    src: { //Пути откуда брать исходники
        json: "src/api/**/*",
        html: "src/*.html", //Синтаксис src/*.html говорит gulp что мы хотим взять все файлы с расширением .html
        js: "src/js/**/*.js",//В стилях и скриптах нам понадобятся только main файлы
        css: "src/css/**/*.css",
        img: "src/images/**/*.*", //Синтаксис img/**/*.* означает - взять все файлы всех расширений из папки и из вложенных каталогов
        fonts: "src/fonts/**/*.*"
    },
    watch: { //Тут мы укажем, за изменением каких файлов мы хотим наблюдать
        html: "src/**/*.html",
        js: "src/js/*.js",
        style: "src/css/**/*.css",
        img: "src/img/**/*.*",
        fonts: "src/fonts/**/*.*",
        app: "src/js/app/**/*.js"
    },
    clean: "./wwwroot"
};

//var config = {
//    server: {
//        baseDir: "./wwwroot"
//    },
//    tunnel: false,
//    host: "localhost",
//    port: 9000,
//    logPrefix: "Frontend_Devil"
//};


gulp.task("html:js", function () {
    return gulp.src(path.src.js) //Выберем файлы по нужному пути
        .pipe(gulp.dest(path.build.js)); //Выплюнем их в папку build
    //.pipe(reload({stream: true})); //И перезагрузим наш сервер для обновлений
});
gulp.task("html:html", function () {
    return gulp.src(path.src.html) //Выберем файлы по нужному пути
        .pipe(gulp.dest(path.build.html)); //Выплюнем их в папку build
    //.pipe(reload({stream: true})); //И перезагрузим наш сервер для обновлений
});
gulp.task("html:css", function () {
    return gulp.src(path.src.css) //Выберем файлы по нужному пути
      .pipe(gulp.dest(path.build.css)); //Выплюнем их в папку build
    //.pipe(reload({stream: true})); //И перезагрузим наш сервер для обновлений
});
gulp.task("html:fonts", function () {
    return gulp.src(path.src.fonts) //Выберем файлы по нужному пути
        .pipe(gulp.dest(path.build.fonts)); //Выплюнем их в папку build
    //.pipe(reload({stream: true})); //И перезагрузим наш сервер для обновлений
});
gulp.task("html:image", function () {
    return gulp.src(path.src.img) //Выберем файлы по нужному пути
        .pipe(gulp.dest(path.build.img)); //Выплюнем их в папку build
    //.pipe(reload({stream: true})); //И перезагрузим наш сервер для обновлений
});

gulp.task("html:json", function () {
    return gulp.src(path.src.json) //Выберем файлы по нужному пути
         .pipe(gulp.dest(path.build.json)); //Выплюнем их в папку build
    //.pipe(reload({stream: true})); //И перезагрузим наш сервер для обновлений
});

gulp.task("build", [
    //"html:css",
    //"html:image",
    //"html:fonts",
    //"html:js",
    //"html:html"
   // "html:json"
   "Template"
]);
var isRelease = true;
if (process.env.NODE_ENV && process.env.NODE_ENV !== 'Release') {
    isRelease = false;
    console.log("no");
}
gulp.task("Template", function () {
    var templatePath = "Content/tpl/";

    return gulp.src('Views/Shared/_Layout.cshtml') //Выберем файлы по нужному пути
         .pipe(inject(gulp.src([templatePath + "**/*.html"]), {

             starttag: '<!-- inject:template -->',
             transform: function (filepath, file) {
                 console.log(filepath);
                 var fileName = filepath.replace("/" + templatePath, "");
                 fileName = fileName.replace(".html", "");
                 fileName = fileName.replace(/\//g, "-");
                 return ' <script type="text/template" id="' + fileName + '"> ' + file.contents.toString('utf8').replace(/\n/g, "").replace(/\r/g, "") + ' </script>';
             }
         }))
        .pipe(inject(gulp.src(["Scripts/override/Backbone.Marionette.TemplateCache.prototype.loadTemplate.Relise"]), {

            starttag: '<!-- inject:js-relise -->',
            transform: function (filepath, file) {
                console.log('ddd');
                var fileName = filepath.replace("/" + templatePath, "");
                fileName = fileName.replace(".html", "");
                fileName = fileName.replace(/\//g, "-");
                return ' <script> ' + file.contents.toString('utf8').replace(/\n/g, "").replace(/\r/g, "") + '</script>';
            }
        }))
         .pipe(gulp.dest('Views/Shared/')) //Выплюнем их в папку build
    // .pipe(reload({stream: true})); //И перезагрузим наш сервер для обновлений
});


gulp.task("html:jsapp", ["build"], function () {
    return gulp.src(path.build.html + '*.html') //Выберем файлы по нужному пути
       //  .pipe(plumber())
         .pipe(inject(gulp.src([
             "wwwroot/lib/jquery/dist/jquery.min.js",
             "wwwroot/lib/underscore/underscore.js",
             "wwwroot/lib/bootstrap/dist/js/bootstrap.min.js",
             "wwwroot/lib/backbone/backbone.js",
             "wwwroot/lib/backbone.wreqr/lib/backbone.wreqr.js",
             "wwwroot/lib/backbone.babysitter/lib/backbone.babysitter.js",
             "wwwroot/lib/backbone.marionette/lib/backbone.marionette.js"]
             ),
             { name: 'vendor', relative: true }))
         .pipe(inject(
           gulp.src([
             /*"wwwroot/lib/jquery/dist/jquery.min.js",
             "wwwroot/lib/underscore/underscore.js",
             "wwwroot/lib/bootstrap/dist/js/bootstrap.min.js",
             "wwwroot/lib/backbone/backbone.js",
             "wwwroot/lib/backbone.wreqr/lib/backbone.wreqr.js",
             "wwwroot/lib/backbone.babysitter/lib/backbone.babysitter.js",
             "wwwroot/lib/backbone.marionette/lib/backbone.marionette.js",
             */
             "wwwroot/js/override/*.js",
             "wwwroot/js/app/app.js",
             "wwwroot/js/app/**/*.model.js",
             "wwwroot/js/app/**/*.collection.js",
             "wwwroot/js/app/**/*.view.js",
             "wwwroot/js/app/**/*.route.js",
             "!wwwroot/js/override/Backbone.Marionette.TemplateCache.prototype.loadTemplate.js"

           ]),
           { relative: true }
         ))

         .pipe(inject(gulp.src(["./src/tpl/**/*.html"]), {

             starttag: '<!-- inject:template -->',
             transform: function (filepath, file) {
                 var fileName = filepath.replace("/src/tpl/", "");
                 fileName = fileName.replace(".html", "");
                 fileName = fileName.replace(/\//g, "-");
                 return ' <script type="text/template" id="' + fileName + '">\n' + file.contents.toString('utf8').replace(/\n/g, "").replace(/\r/g, "") + '\n</script>';
             }
         }))
         .pipe(gulp.dest(path.build.html)) //Выплюнем их в папку build
    // .pipe(reload({stream: true})); //И перезагрузим наш сервер для обновлений
});
gulp.task('clean', function () {
    var templatePath = "Content/tpl/";
    return gulp.src('Views/Shared/_Layout.cshtml') //Выберем файлы по нужному пути
        .pipe(inject(gulp.src([templatePath + "**/*.html"]), {

            starttag: '<!-- inject:template -->',
            transform: function (filepath, file) {
                return ' <!-- nothink -->';
            }
        }))
       .pipe(inject(gulp.src(["Scripts/override/Backbone.Marionette.TemplateCache.prototype.loadTemplate.Relise"]), {

           starttag: '<!-- inject:js-relise -->',
           transform: function (filepath, file) {
               
               return ' <!-- nothink -->';
           }
       }))
        .pipe(gulp.dest('Views/Shared/'))

    return gulp.src([path.build.js, path.build.css, path.build.html + "*.html"], { read: false }).pipe(clean());
})

//gulp.task("clean", function (cb) {
//     rimraf(path.build.css, cb);
//    rimraf(path.build.js, cb);
//    rimraf(path.build.img, cb);
//    rimraf( path.build.override, cb);
//});

//gulp.task("webserver", function () {
//    browserSync(config);
//});

gulp.task("default", ["clean"], function () {
    return gulp.start("html:jsapp");
});

gulp.task("watch", function () {
    //return gulp.watch(["src/**/*.*"], function(event, cb) {
    //    return gulp.start("html:jsapp");
    //});

    return gulp.watch(["src/api/**/*"], ["html:json"]);

    /*  watch([path.watch.style], function(event, cb) {
          gulp.start("style:build");
      });
      watch([path.watch.js], function(event, cb) {
          gulp.start("js:build");;
      });
      watch([path.watch.img], function(event, cb) {
          gulp.start("image:build");
      });
      watch([path.watch.fonts], function(event, cb) {
          gulp.start("fonts:build");
      });*/
});
