GSU.module("Pages.Main", function (Main, GSU, Backbone, Marionette, $, _) {


    Main.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",
            "Main": "show"
        }
    });

    var API = {
        show: function () {
            var view = new Main.view();
            GSU.mainRegion.show(view);

        }
    };

    GSU.on("Main:show", function (id) {
        GSU.navigate("Main");
        API.show(id);
    });

    /*  PresentationMedal.model = Backbone.Model.extend({
     defaults: {
     medalId: null,
     medalImageId: null,
     medalName: "",
     info: "",
     isSelectUser: true,
     recipient: "",
     recipientId: 0,
     comment: null,
     success: false
     },
     // url: "Info/jsonInfoUser/"
     url: "Medal/SendMedal"
     });

     PresentationMedal.medalModel = Backbone.Model.extend({
     defaults: {
     name: "",
     imageId: 0
     }
     // url: "Info/jsonInfoUser/"
     });

     PresentationMedal.medalView = Backbone.Marionette.ItemView.extend({
     template: "GiveMedalItem",
     events: {
     "click": "click"
     },
     click: function () {
     this.trigger("medal:click", this.model);
     }

     });

     PresentationMedal.medalCollection = Backbone.Collection.extend({
     model: PresentationMedal.medalModel,
     url: "Medal/GetAvailableMedalForUser/"

     });
     */
    Main.view = Backbone.Marionette.ItemView.extend({
        template: "main"
    });


    GSU.addInitializer(function () {
        new Main.Router({
            controller: API
        });
    });


});
