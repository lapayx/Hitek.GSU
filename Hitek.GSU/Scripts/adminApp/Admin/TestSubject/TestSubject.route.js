GSU.module("Admin.TestSubject", function (TestSubject, GSU, Backbone, Marionette, $, _) {


    TestSubject.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",


            "Admin/TestSubject": "show",
            "Admin/TestSubject/new": "addSubject",
            "Admin/TestSubject/:id": "editSubject",
            /*"Test/Result": "showResultAll",
             "Test/ListDetail/:id": "showListDetail",
             "Test/List": "showList",
             "Test/:id": "show",*/
        }
    });

    var API = {
        show: function (id) {
            var view = new TestSubject.List.view();
            GSU.mainRegion.show(view);

        },
        editSubject: function (id) {
            var view = new TestSubject.Edit.view({id: id});
            GSU.mainRegion.show(view);

        },
        addSubject: function () {
            var view = new TestSubject.Edit.view({ id: 0 });
            GSU.mainRegion.show(view);

        }
        /*
         showResult: function (id) {
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

    GSU.on("Admin:TestSubject:show", function (id) {
        GSU.navigate("Admin/TestSubject");
        API.show();
    });
    GSU.on("Admin:TestSubject:edit", function (id) {
        GSU.navigate("Admin/TestSubject/" + id);
        API.editSubject(id);
    });
    GSU.on("Admin:TestSubject:add", function () {
        GSU.navigate("Admin/TestSubject/new");
        API.addSubject();
    });
    /*GSU.on("Test:showResult", function (id) {
     GSU.navigate("Test/Result/" + id);
     API.showResult(id);
     });
     GSU.on("Test:showResultAll", function () {
     GSU.navigate("Test/Result");
     API.showResultAll();
     });

     GSU.on("Test:showListDetail", function (id) {
     GSU.navigate("Test/ListDetail/" + id);
     API.showListDetail(id);
     });*/


    GSU.addInitializer(function () {
        new TestSubject.Router({
            controller: API
        });
    });


});
