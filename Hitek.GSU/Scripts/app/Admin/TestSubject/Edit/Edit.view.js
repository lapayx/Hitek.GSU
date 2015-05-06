GSU.module("Admin.TestSubject.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {

    Edit.Option = Backbone.Marionette.ItemView.extend({
        template: "Admin/TestSubject/Edit/select",
    });


    Edit.view = Backbone.Marionette.CompositeView.extend({


        template: "Admin/TestSubject/Edit/main",

        childViewContainer: "select",
        childView: Edit.Option,
        events: {
            "submit": "onSubmit",
            "change #InputName": "changeName",
            "change #parentSubject": "changeParent"

        },
        modelEvents: {
            "sync": "onSyncModel"
        },
        
        collectionEvents: {
            "sync": "onSyncCollection"
        },

        initialize: function (paramId) {
            GSU.loadMask.show();
            if (paramId.id > 0) {
                this.model = new Edit.Subject({ id: paramId.id });
                this.model.fetch();
            }
            else
                this.model = new Edit.Subject();
            
            this.collection = new Edit.TreeCollection();
            this.collection.fetch();
           

        },
        onSyncModel: function () {
            GSU.loadMask.hide();
            if (this.model.get("id") && this.model.get("id") > 0) {
                this.render();
            }
            else {
                GSU.trigger("Error:404");
            }
        },
        onSyncCollection: function () {
            GSU.loadMask.hide();
            this.render();
           
        },

        changeName: function (event) {
            this.model.set("name", event.target.value);

        },
        changeParent: function (event) {
            this.model.set("parentId", parseInt(event.target.value));

        },
        onSubmit: function (event) {
            event.preventDefault()
            this.model.save();

        }

    });
    


});
