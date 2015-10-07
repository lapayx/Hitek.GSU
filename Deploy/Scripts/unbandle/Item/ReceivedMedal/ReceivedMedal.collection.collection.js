GSU.module("Item.ReceivedMedal.collection", function (Collection, GSU, Backbone, Marionette, $, _) {


    Collection.collection = Backbone.Collection.extend({
        model: Collection.model,
        url: "Medal/GetRepicientMedal/"
    });


});