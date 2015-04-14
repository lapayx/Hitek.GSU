GSU.module("Test.FullTest", function (FullTest, GSU, Backbone, Marionette, $, _) {


    FullTest.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",
            "Test/:id": "show"
        }
    });

    var API = {
        show: function (id) {
            var view = new FullTest.view({ id: id });
            GSU.mainRegion.show(view);

        }
    };

    GSU.on("Test:show", function (id) {
        GSU.navigate("Test/"+id);
        API.show(id);
    });





    GSU.addInitializer(function () {
        new FullTest.Router({
            controller: API
        });
    });



});
