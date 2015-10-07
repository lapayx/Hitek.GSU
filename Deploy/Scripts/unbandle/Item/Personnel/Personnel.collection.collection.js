GSU.module("Item.Personnel.collection", function (Collection, GSU, Backbone, Marionette, $, _) {

    Collection.collection = Backbone.Collection.extend({
        model: Collection.model,
        url: "Info/jsonFullList/"

    });



});
