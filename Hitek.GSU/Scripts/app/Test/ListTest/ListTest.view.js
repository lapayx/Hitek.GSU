GSU.module("Test.ListTest", function (ListTest, GSU, Backbone, Marionette, $, _) {


    ListTest.treeView = Backbone.Marionette.CompositeView.extend({
        template: "Test/ListTest/item",
        childViewContainer: "ul",
        initialize: function () {
            if(this.model.children)
                this.collection = this.model.children;

        },
        
    });



    ListTest.view = Backbone.Marionette.CompositeView.extend({
        template: "Test/ListTest/main",
        childViewContainer: ".tree ul",
        childView: ListTest.treeView,
        modelEvents: {
            "sync": "onSyncModel"
        },
        initialize: function (paramId) {
            this.collection = new ListTest.TreeCollection([{ name: 2, childrens: [{ name: 6 }, { Name: 65 }] }, { name: 23 }], { parse: true });

           // this.collection.fetch();

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
