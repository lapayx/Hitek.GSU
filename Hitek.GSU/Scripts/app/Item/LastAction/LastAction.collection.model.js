GSU.module("Item.LastAction.collection", function (Collection, GSU, Backbone, Marionette, $, _) {



    Collection.model = Backbone.Model.extend({
        defaults: {
            id: 0,
            medalImage: "",
            medalInfo: "",
            medalname: "",
            comment: "",
            senderImage: "",
            senderName: ""
        },
        // url: "Info/jsonInfoUser/"
    });


});