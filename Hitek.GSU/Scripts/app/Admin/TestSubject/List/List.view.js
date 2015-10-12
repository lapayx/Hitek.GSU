GSU.module("Admin.TestSubject.List", function (List, GSU, Backbone, Marionette, $, _) {


    List.treeView = Backbone.Marionette.CompositeView.extend({
        template: "Admin/TestSubject/List/item",
        childViewContainer: "ul",
        events: {
            "click .show-test": "click",
            "click .delete-model": "oClicknDeleteModel",
            "click .edit-model": "onClickEditModel"
        },
        initialize: function () {
            if (this.model.children)
                this.collection = this.model.children;

        },
        click: function () {
            GSU.trigger("Admin:TestBySubject", this.model.id);


        },
        oClicknDeleteModel: function () {
            console.log("delete");
            var ans = confirm("Удалить тему " + this.model.get("name") + "?");
            if (ans) {
                this.model.destroy({})
            }
        },
        onClickEditModel: function () {
            GSU.trigger("Admin:TestSubject:edit", this.model.id);

        }
    });


    List.view = Backbone.Marionette.CompositeView.extend({
        template: "Admin/TestSubject/List/main",
        childViewContainer: ".tree ul",
        childView: List.treeView,
        events: {
            "click .new-subject": "newSubject"

        },
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
            if (this.model.get("id") && this.model.get("id") > 0) {
                this.render();
            }
            else {
                GSU.trigger("Error:404");
            }
        },
        onSyncCollection: function () {
            _.each(this.collection, function (c, num, collection) {
                var m = collection.at(num);
                if (m && m.get("parentId") > 0) {
                    var t = collection.get(m.get("parentId"));
                    if (t) {
                        t.setChildren(m.clone());
                    }
                }
            });
            var forRemove = []
            _.each(this.collection, function (c, num, collection) {
                var m = collection.at(num);
                if (m && m.get("parentId") > 0) {
                    forRemove.push(m)
                }
            });
            this.collection.remove(forRemove, {silent: true});

            this.render();
            GSU.loadMask.hide();
        },

        newSubject: function () {
            GSU.trigger("Admin:TestSubject:edit", 0);

        }
    });


});
