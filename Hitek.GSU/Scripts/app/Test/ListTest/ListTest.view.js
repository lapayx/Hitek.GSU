GSU.module("Test.ListTest", function (ListTest, GSU, Backbone, Marionette, $, _) {


    ListTest.treeView = Backbone.Marionette.CompositeView.extend({
        template: "Test/ListTest/item",
        childViewContainer: "ul",
        events: {
            "click":"click"
        },
        initialize: function () {
            if(this.model.children)
                this.collection = this.model.children;

        },
        click: function () {
            if (!this.model.get("isParent")) {
                GSU.trigger("Test:showListDetail", this.model.id);

            }

        }
    });



    ListTest.view = Backbone.Marionette.CompositeView.extend({
        template: "Test/ListTest/main",
        childViewContainer: ".tree ul",
        childView: ListTest.treeView,
        modelEvents: {
            "sync": "onSyncModel"
        },
        initialize: function (paramId) {
            this.collection = new ListTest.TreeCollection();//[{ id:1,name: 2, childrens: [{ id:3,name: 6 }, { Name: 65,id:9 }] }, { name: 23,id:56 }], { parse: true });
           this.collection.fetch();

        },
        onSyncModel: function () {
            if (this.model.get("id") && this.model.get("id")>0) {
                this.render();
            }
            else {
                GSU.trigger("Error:404");
            }
        }

    });
    


});
