﻿GSU.module("Admin.TestSubject.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    Edit.TreeCollection = Backbone.Collection.extend({
        model: Edit.TreeModel,
        url: "api/TestSubject/"
    });

});
