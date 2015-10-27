GSU.module("Admin.User.List", function (List, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    List.DetailCollection = Backbone.Collection.extend({
        model: List.DetailModel,
        url: function () {
            return "UserManager/";
        }
    });

});
