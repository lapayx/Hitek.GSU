GSU.module("Error", function (Error, GSU, Backbone, Marionette, $, _) {


    Error.Error404View = Backbone.Marionette.ItemView.extend({
        template: "Error/404"

    });


    var API = {
        error404: function (id) {
            var view = new Error.Error404View();
            GSU.mainRegion.show(view);

        }
    };

    GSU.on("Error:404", function () {

        API.error404();
    });


});
