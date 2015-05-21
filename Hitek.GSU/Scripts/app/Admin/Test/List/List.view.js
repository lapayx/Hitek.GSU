GSU.module("Admin.Test.List", function (List, GSU, Backbone, Marionette, $, _) {


    List.item = Backbone.Marionette.ItemView.extend({
        template: "Admin/Test/List/item",
        events:{
            "click .js-button-show": "goToTest",
            "click .js-button-edit": "editTest",
            "click .js-button-remove": "deleteTest"
        },
        goToTest: function () {
            GSU.trigger("Test:showTest", this.model.id);

        },
        editTest: function () {
            GSU.trigger("Admin:Test:edit", this.model.id);

        },
        deleteTest: function () {
            var ans = confirm("Удалить тест " + this.model.get("name") + "?");
            if (ans) {
                this.model.destroy({});
            }

        }
    });
    List.noItem = Backbone.Marionette.ItemView.extend({
        template: "Admin/Test/List/noItem"
    });


    List.view = Backbone.Marionette.CompositeView.extend({
        template: "Admin/Test/List/main",
        childViewContainer: ".test-lists",
        childView: List.item,
        emptyView:List.noItem,

        events: {
            "click .js-test-list-addTest": "addNewTest"
        },
        modelEvents: {
            "sync": "onSyncModel"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.model = new List.SubjectInfoModel({ id: paramId.id });
            this.model.fetch();
            this.collection = new List.DetailCollection();
            this.collection.id = paramId.id;
            //this.colectgion.url = "api/subject/"+paramId.id +"/Test";
            this.collection.fetch();

        },
        onSyncModel: function () {
            GSU.loadMask.hide();
            if (this.model.id && this.model.id>0) {
                this.render();
            }
            else {
                GSU.trigger("Error:404");
            }
        },
        addNewTest: function () {
            GSU.trigger("Admin:Test:add");
        }

    });
    


});
