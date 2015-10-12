GSU.module("Test.ResultTest", function (ResultTest, GSU, Backbone, Marionette, $, _) {

    ResultTest.view = Backbone.Marionette.ItemView.extend({


        template: "Test/ResultTest/main",
        modelEvents: {
            "sync": "onSyncModel"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.model = new ResultTest.ResultModel({id: paramId.id});
            this.model.fetch();

        },
        onSyncModel: function () {
            GSU.loadMask.hide();
            if (this.model.get("id") && this.model.get("id") > 0) {
                this.render();
            }
            else {
                GSU.trigger("Error:404");
            }
        }

    });


});
