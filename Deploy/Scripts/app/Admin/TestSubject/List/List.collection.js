GSU.module("Admin.TestSubject.List", function (List, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    List.TreeCollection = Backbone.Collection.extend({
        model: List.TreeModel,
        url: "api/TestSubject/"
     });
  
});
