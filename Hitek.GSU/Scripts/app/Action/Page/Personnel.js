GSU.module("Action.Page.Personnel", function (Personnel, GSU, Backbone, Marionette, $, _) {

    Personnel.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",
            "personnel": "showPersonnel"
        }
    });

    var API = {
        showPersonnel: function () {
            var view = new Backbone.Marionette.StackView();
            GSU.mainRegion.show(view);
            view.pushView(new GSU.Item.Personnel.view());
        }
    };




    GSU.on("personnel:show", function () {
        GSU.navigate("personnel");
        API.showPersonnel();
    });

    GSU.addInitializer(function () {
        new Personnel.Router({
            controller: API
        });
    });

});
