﻿GSU.module("Admin.User", function (User, GSU, Backbone, Marionette, $, _) {


    User.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",


            "Admin/User": "show",
           // "Admin//Edit/:id": "editTest",
            /*"Test/ListDetail/:id": "showListDetail",
             "Test/List": "showList",
             "Test/:id": "show",*/
        }
    });

    var API = {
        show: function (id) {
            var view = new User.List.view({id: id});
            GSU.mainRegion.show(view);

        },
        editTest: function (id) {
            var view = new User.Edit.view({id: id});
            GSU.mainRegion.show(view);

        },

        /*showResult: function (id) {
         var view = new TestSubject.ResultTest.view({ id: id });
         GSU.mainRegion.show(view);

         },
         showList: function (id) {
         var view = new TestSubject.ListTest.view({ id: id });
         GSU.mainRegion.show(view);

         },
         showListDetail: function (id) {
         var view = new TestSubject.ListTestDetail.view({ id: id });
         GSU.mainRegion.show(view);

         }*/
    };

    GSU.on("Admin:User", function (id) {
        GSU.navigate("Admin/User" + id);
        API.show(id);
    });
    /*
    GSU.on("Admin:Test:edit", function (id) {
        GSU.navigate("Admin/Test/Edit/" + id);
        API.editTest(id);
    });
    GSU.on("Admin:Test:add", function (id) {
        GSU.navigate("Admin/Test/Edit/" + 0);
        API.editTest(id);
    });*/
    /*GSU.on("Test:showResultAll", function () {
     GSU.navigate("Test/Result");
     API.showResultAll();
     });

     GSU.on("Test:showListDetail", function (id) {
     GSU.navigate("Test/ListDetail/" + id);
     API.showListDetail(id);
     });*/


    GSU.addInitializer(function () {
        new User.Router({
            controller: API
        });
    });


});
