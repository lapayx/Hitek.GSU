GSU.module("Admin.TestSubject.List", function (List, GSU, Backbone, Marionette, $, _) {


    List.treeView = Backbone.Marionette.CompositeView.extend({
        template: "Admin/TestSubject/List/item",
        childViewContainer: "ul",
        events: {
            "click span":"click"
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



    List.view = Backbone.Marionette.CompositeView.extend({
        template: "Admin/TestSubject/List/main",
        childViewContainer: ".tree ul",
        childView: List.treeView,
        modelEvents: {
            "sync": "onSyncModel"
        },
        collectionEvents: {
            "sync": "onSyncCollection"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.collection = new List.TreeCollection();//[{ id:1,name: 2, childrens: [{ id:3,name: 6 }, { Name: 65,id:9 }] }, { name: 23,id:56 }], { parse: true });
            this.collection.fetch();

        },
        onSyncModel: function () {
            if (this.model.get("id") && this.model.get("id")>0) {
                this.render();
            }
            else {
                GSU.trigger("Error:404");
            }
        },
        onSyncCollection: function () {
            GSU.loadMask.hide();
        }


    });
    


});
