GSU.module("Admin.TestSubject.List", function (List, GSU, Backbone, Marionette, $, _) {


    List.TreeModel = Backbone.Model.extend({
        constructor: function () {
            this.children = new List.TreeCollection();

            Backbone.Model.apply(this, arguments);
        },
        // url: "api/TestHistory/",
        urlRoot: "api/TestSubject/",
        defaults: {
            isParent: false,
            name: "Заголовок",
            result: 0,
            parentId: 0


        },
        setChildren: function (child) {
            this.children.add(child);
            this.set("isParent", true);
        }

    });


});
