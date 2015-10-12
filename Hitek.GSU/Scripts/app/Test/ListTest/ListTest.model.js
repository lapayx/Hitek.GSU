GSU.module("Test.ListTest", function (ListTest, GSU, Backbone, Marionette, $, _) {


    ListTest.TreeModel = Backbone.Model.extend({

        constructor: function () {
            this.children = new ListTest.TreeCollection();
            Backbone.Model.apply(this, arguments);
        },
        // urlRoot: "api/TestSubject/",
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
