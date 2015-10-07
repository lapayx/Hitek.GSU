GSU.module("Action.Page.LastAction", function (LastAction, GSU, Backbone, Marionette, $, _) {

    LastAction.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",
            "lastAction": "show"
        }
    });

    var API = {
        show: function () {
            var view = new Backbone.Marionette.StackView();
            GSU.mainRegion.show(view);
            view.pushView(new GSU.Item.InfoOnMainPage.view());
            view.pushView(new GSU.Item.LastAction.view());
        }
    };

    GSU.on("lastAction:show", function (id) {
        GSU.navigate("lastAction");
        API.show(id);
    });


  
    GSU.addInitializer(function () {
        new LastAction.Router({
            controller: API
        });
    });

});
