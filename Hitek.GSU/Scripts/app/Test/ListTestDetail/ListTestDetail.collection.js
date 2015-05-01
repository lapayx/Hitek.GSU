GSU.module("Test.ListTestDetail", function (ListTestDetail, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    ListTestDetail.DetailCollection = Backbone.Collection.extend({
        model: ListTestDetail.DetailModel,
        url: function () { return "api/TestSubject/" + this.id + "/Test"; }
     });
  
});
