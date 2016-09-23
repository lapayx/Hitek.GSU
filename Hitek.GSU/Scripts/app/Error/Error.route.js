GSU.module("Error", function (Error, GSU, Backbone, Marionette, $, _) {


    Error.Error404View = Backbone.Marionette.ItemView.extend({
        template: "Error/404"

    });

    Error.ErrorView = Backbone.Marionette.ItemView.extend({
        template: "Error/error"

    });


    var API = {
        error: function (error) {
            var model = new Backbone.Model({status:error})
            var view = new Error.ErrorView({ model: model });
            GSU.mainRegion.show(view);
            GSU.loadMask.hide();

        },
        error404: function (id) {
            var view = new Error.Error404View();
            GSU.mainRegion.show(view);
            GSU.loadMask.hide();

        }
    };

    GSU.on("Error", function (error) {

        API.error(error);
    });
    GSU.on("Error:404", function () {

        API.error404();
    });


});
