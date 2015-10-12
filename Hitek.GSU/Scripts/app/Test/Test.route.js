GSU.module("Test", function (Test, GSU, Backbone, Marionette, $, _) {


    Test.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",


            "Test/Result/:id": "showResult",
            "Test/Result": "showResultAll",
            "Test/ListDetail/:id": "showListDetail",
            "Test/List": "showList",
            "Test/:id": "show",
        }
    });

    var API = {
        show: function (id) {
            debugger;
            var view = new Test.FullTest.view({id: id});
            GSU.mainRegion.show(view);

        },
        showResultAll: function () {
            var view = new Test.ResultTestAll.view({id: 0});
            GSU.mainRegion.show(view);

        },
        showResult: function (id) {
            var view = new Test.ResultTest.view({id: id});
            GSU.mainRegion.show(view);

        },
        showList: function (id) {
            var view = new Test.ListTest.view({id: id});
            GSU.mainRegion.show(view);

        },
        showListDetail: function (id) {
            var view = new Test.ListTestDetail.view({id: id});
            GSU.mainRegion.show(view);

        }
    };

    GSU.on("Test:showTest", function (id) {
        GSU.navigate("Test/" + id);
        API.show(id);
    });
    GSU.on("Test:showResult", function (id) {
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
    });


    GSU.addInitializer(function () {
        new Test.Router({
            controller: API
        });
    });


});
