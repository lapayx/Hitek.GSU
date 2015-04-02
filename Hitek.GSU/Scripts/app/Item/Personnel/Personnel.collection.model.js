GSU.module("Item.Personnel.collection", function (Collection, GSU, Backbone, Marionette, $, _) {



    Collection.model = Backbone.Model.extend({
        defaults: {
            name: "",
            imageId: 0
        },
        // url: "Info/jsonInfoUser/"
    });



});
