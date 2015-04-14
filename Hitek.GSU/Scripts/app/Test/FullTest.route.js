GSU.module("Test.FullTest", function (FullTest, GSU, Backbone, Marionette, $, _) {


    FullTest.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",
            "Test/:id": "show",
            "Test/result/:id": "showResult"
        }
    });

    var API = {
        show: function (id) {
            var view = new FullTest.view({ id: id });
            GSU.mainRegion.show(view);

        },
        showResult: function (id) {
            var view = new FullTest.Result.view({ id: id });
            GSU.mainRegion.show(view);

        }
    };

    GSU.on("Test:show", function (id) {
        GSU.navigate("Test/"+id);
        API.show(id);
    });
    GSU.on("Test:showResult", function (id) {
        GSU.navigate("Test/result/" + id);
        API.showResult(id);
    });




    GSU.addInitializer(function () {
        new FullTest.Router({
            controller: API
        });
    });



});
