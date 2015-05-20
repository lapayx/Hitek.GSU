GSU.module("Admin.Test.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {



    Edit.AnswerView = Backbone.Marionette.ItemView.extend({
        template: "Admin/Test/Edit/answer",
        
        events: {
            "change #editTest-NameTest": "changeName",
            "change #editTest-SubjectId": "changeParent",

        },
        modelEvents: {
            "sync": "onSyncModel"
        },


    });

    Edit.QuestionView = Backbone.Marionette.CompositeView.extend({
        template: "Admin/Test/Edit/question",
        childViewContainer: ".js-test-answer-section",
        childView: Edit.AnswerView,
        events: {
            "change #editTest-NameTest": "changeName",
            "change #editTest-SubjectId": "changeParent",
            "click .js-test-add-answer": "addAnswer"

        },
        modelEvents: {
          //  "sync": "onSyncModel"
        },

        collectionEvents: {
            "sync": "onSyncCollection"
        },

        initialize: function () {
            this.collection = this.model.answerns;
        },

        addAnswer: function (event) {
            console.log(this.model);
            this.model.answers.add({});
            event.preventDefault();

        }

    });


    Edit.view = Backbone.Marionette.CompositeView.extend({


        template: "Admin/Test/Edit/main",

        childViewContainer: ".js-test-question-section",
        childView: Edit.QuestionView,
        events: {
            "submit": "onSubmit",
            "change #editTest-NameTest": "changeName",
            "change #editTest-SubjectId": "changeParent",
            "click .js-test-add-question": "addQuestion"

        },
        modelEvents: {
            "sync": "onSyncModel"
        },
        
        collectionEvents: {
            "sync": "onSyncCollection"
        },

        initialize: function (paramId) {
            /*GSU.loadMask.show();
            if (paramId.id > 0) {
                this.model = new Edit.Subject({ id: paramId.id });
                this.model.fetch();
            }
            else
                this.model = new Edit.Subject();
            
            this.collection = new Edit.TreeCollection();
            this.collection.fetch();
           */
            paramId.id = 0;
            if (paramId.id > 0) {
                this.model = new Edit.TestModel({ id: paramId.id });
                this.model.fetch();
            }
            else
                this.model = new Edit.TestModel();
            this.model.questions.add([{},{},{}]);
            this.collection = this.model.questions;

            //this.render();

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

        },
        addQuestion: function () {
            this.model.questions.add({});
        }

    });
    


});
