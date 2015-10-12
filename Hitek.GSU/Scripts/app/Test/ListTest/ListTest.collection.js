GSU.module("Test.ListTest", function (ListTest, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    ListTest.TreeCollection = Backbone.Collection.extend({
        model: ListTest.TreeModel,
        url: "api/TestSubject/"
    });

});
