GSU.module("Item.ReceivedMedal.collection", function (Collection, GSU, Backbone, Marionette, $, _) {



    Collection.model = Backbone.Model.extend({
        defaults: {
            id: 0,
            medalImage: "",
            medalInfo: "",
            medalname: "",
            comment: "",
            senderImage: "",
            senderName: "",
            medalCount: 1
        },
        // url: "Info/jsonInfoUser/"
    });


});