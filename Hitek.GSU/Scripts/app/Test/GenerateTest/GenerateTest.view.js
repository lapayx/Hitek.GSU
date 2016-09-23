GSU.module("Test.GenerateTest", function (GenerateTest, GSU, Backbone, Marionette, $, _) {


    GenerateTest.view = Backbone.Marionette.ItemView.extend({


        template: "Test/GenerateTest/main",
        modelEvents: {
            "sync": "onSyncModel"
        },
        events: {
            "click .js-start-test": "startTest"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
    
            this.model = new GenerateTest.model({
                id: paramId.id
            });
            this.model.fetch({
                error: function (mod, xhr) {
                    GSU.trigger("Error",xhr.status);
                }
            });

        },
        onSyncModel: function () {
            GSU.loadMask.hide();
            this.render();
        },
        startTest: function () {
          GSU.loadMask.show();
            var c = new Backbone.Model();
            c.url = "Test/Generate/" +this.model.get("id");
            //c.set("id", this.model.get("id"));
            c.on("sync", function (r, mod, xht) {
                if (mod.id) {
                    GSU.trigger("Test:continueTest", mod.id);

                }
            });
            c.fetch();
        }
    });


});