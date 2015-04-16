GSU.module("Test.ResultTestAll", function (ResultTestAll, GSU, Backbone, Marionette, $, _) {


    ResultTestAll.ItemView = Backbone.Marionette.ItemView.extend({
        template: "Test/ResultTestAll/item",
        events: {
            "click button":"showDeteil"

        },
        showDeteil: function () {
            GSU.trigger("Test:showResult", this.model.get("id"));

        }
    });

    ResultTestAll.view = Backbone.Marionette.CompositeView.extend({
        template: "Test/ResultTestAll/main",
        childViewContainer: ".test-result",
        childView: ResultTestAll.ItemView,
        modelEvents: {
            "sync": "onSyncModel"
        },
        initialize: function (paramId) {
            this.collection = new ResultTestAll.ResultCollection();
            this.collection.fetch({ data: { id: paramId.id } });

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
