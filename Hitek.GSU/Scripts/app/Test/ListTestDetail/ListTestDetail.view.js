GSU.module("Test.ListTestDetail", function (ListTestDetail, GSU, Backbone, Marionette, $, _) {


    ListTestDetail.item = Backbone.Marionette.ItemView.extend({
        template: "Test/ListTestDetail/item",
        events: {
            "click": "goToTest"
        },
        goToTest: function () {
            debugger
            GSU.trigger("Test:showTest", this.model.id);

        }
    });
    ListTestDetail.noItem = Backbone.Marionette.ItemView.extend({
        template: "Test/ListTestDetail/noItem"
    });


    ListTestDetail.view = Backbone.Marionette.CompositeView.extend({
        template: "Test/ListTestDetail/main",
        childViewContainer: ".test-lists",
        childView: ListTestDetail.item,
        emptyView: ListTestDetail.noItem,
        modelEvents: {
            "sync": "onSyncModel"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.model = new ListTestDetail.SubjectInfoModel({id: paramId.id});
            this.model.fetch();
            this.collection = new ListTestDetail.DetailCollection();
            this.collection.id = paramId.id;
            //this.colectgion.url = "api/subject/"+paramId.id +"/Test";
            this.collection.fetch();

        },
        onSyncModel: function () {
            GSU.loadMask.hide();
            if (this.model.id && this.model.id > 0) {
                this.render();
            }
            else {
                GSU.trigger("Error:404");
            }
        }

    });


});
