GSU.module("Test.ResultTestAll", function (ResultTestAll, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    ResultTestAll.ResultCollection = Backbone.Collection.extend({
        model: ResultTestAll.ResultModel,
        url: "api/TestHistory/"
     });
  
});
