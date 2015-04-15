﻿GSU.module("Test.ResultTest", function (ResultTest, GSU, Backbone, Marionette, $, _) {

    ResultTest.view = Backbone.Marionette.ItemView.extend({


        template: "Test/ResultTest/main",
        modelEvents: {
            "sync": "onSyncModel"
        },
        initialize: function (paramId) {
            this.model = new ResultTest.ResultModel();
            this.model.fetch({ data: { id: paramId.id } });

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