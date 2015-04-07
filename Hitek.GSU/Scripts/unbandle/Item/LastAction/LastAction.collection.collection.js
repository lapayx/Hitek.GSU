GSU.module("Item.LastAction.collection", function (Collection, GSU, Backbone, Marionette, $, _) {


    Collection.collection = Backbone.Collection.extend({
        model: Collection.model,
        url: "Medal/GetLastPresentMedal/"
    });


});