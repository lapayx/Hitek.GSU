GSU.module("Admin.TestSubject.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {


    Edit.Subject = Backbone.Model.extend({
        urlRoot: "api/TestSubject/",
        defaults: {
            isLocked: false,
            isParent: false,
            name: "",
            parentId: 0,
            isNew: false
        }
    });

    Edit.TreeModel = Backbone.Model.extend({
        urlRoot: "api/TestSubject/",
        defaults: {
            isParent: false,
            name: "Новая тема",
            result: 0,
            parentId: 0
        }

    });


});
