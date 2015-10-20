GSU.module("Admin.TestSubject.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {


    Edit.Subject = Backbone.Model.extend({
        urlRoot: "api/TestSubject/",
        defaults: {
            isParent: false,
            name: "Заголовок"
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
