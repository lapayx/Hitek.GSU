GSU.module("Admin.TestSubject.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {

    Edit.view = Backbone.Marionette.ItemView.extend({


        template: "Admin/TestSubject/Edit/main",
        modelEvents: {
            "sync": "onSyncModel"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.model = new Edit.Subject({ id: paramId.id });
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
