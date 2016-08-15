GSU.module("Test.ListTestDetail", function (ListTestDetail, GSU, Backbone, Marionette, $, _) {


    ListTestDetail.item = Backbone.Marionette.ItemView.extend({
        template: "Test/ListTestDetail/item",
        events: {
            "click": "goToTest"
        },
        goToTest: function () {
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
        collectionEvents: {
            "sync": "onSyncCollection"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.model = new ListTestDetail.SubjectInfoModel({id: paramId.id});
            
            this.collection = new ListTestDetail.DetailCollection();
            this.collection.id = paramId.id;
            var self = this;
            this.model.fetch();
            //this.colectgion.url = "api/subject/"+paramId.id +"/Test";
            //this.collection.fetch();

        },
        onSyncModel: function () {
            if (this.model.id && this.model.id > 0) {
                this.collection.fetch({ reset: true });
            }
            else {
                GSU.trigger("Error:404");
            }
        },

        onSyncCollection: function () {
            this.render();
            GSU.loadMask.hide();
        },

        

    });


});
