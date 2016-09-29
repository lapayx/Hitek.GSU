GSU.module("Admin.Test.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {
"use strict";

    Edit.AnswerView = Backbone.Marionette.ItemView.extend({
        template: "Admin/Test/Edit/answer",

        events: {
            "keyup .js-test-edit-answer-content": "changeContent",
            "click .js-test-edit-answer-isRight": "toggleIsRight",
            "click .js-test-edit-answer-delete": "deleteAnswer",

        },
        modelEvents: {
            //     "sync": "onSyncModel"
        },
        initialize: function () {
        },


        changeContent: function (event) {
            this.model.set("text", event.target.value);
        },

        toggleIsRight: function (e) {
            e.preventDefault();
            var oldState = this.model.get("isRight");
            this.model.set("isRight", !oldState);
            var btn = $(e.target);
            btn.toggleClass("btn-success");
            btn.toggleClass("btn-danger");
            if (!oldState) {
                btn.text("Верный");
            } else {
                btn.text("Неверный");
            }
        },

        deleteAnswer: function () {
            if (this.model.isNew()) {
                this.model.destroy();
            } else {
                this.model.set("isRemoved", true);
                this.model.collection.trigger("update");
               // this.render();
            }

        }


    });

    Edit.QuestionView = Backbone.Marionette.CompositeView.extend({
        template: "Admin/Test/Edit/question",
        childViewContainer: ".js-test-answer-section",
        childView: Edit.AnswerView,
        events: {
            "click .js-test-add-answer": "addAnswer",
            "keyup .js-test-edit-question-title": "changeTitle",
            "keyup .js-test-edit-question-content": "changeContent",
            "click .js-test-edit-question-delete": "deleteQuestion"


        },
        modelEvents: {
            //  "sync": "onSyncModel"
        },

        collectionEvents: {
            // "sync": "onSyncCollection"
        },

        initialize: function () {
            this.collection = this.model.answers;
        },

        addAnswer: function (event) {
            event.preventDefault();
            this.collection.add({});
        },
        changeTitle: function (event) {
            this.model.set("title", event.target.value);
        },

        changeContent: function (event) {
            this.model.set("content", event.target.value);
        },

        deleteQuestion: function () {

            if (!this.model.id) {
                this.model.destroy();
            } else {
                this.model.set("isRemoved", true);
                this.render();
            }

        }

    });


    Edit.view = Backbone.Marionette.CompositeView.extend({


        template: "Admin/Test/Edit/main",

        childViewContainer: ".js-test-question-section",
        childView: Edit.QuestionView,
        events: {
            "submit": "onSubmit",
            "keyup #test-edit-title": "changeTitle",
            "change #test-edit-subjectId": "changeSubjectId",
            "keyup #СountQuestion": "changeСountQuestion",
            "click .js-test-add-question": "addQuestion"

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

                this.model = new Edit.TestModel({id: paramId.id});
                this.model.fetch();
            }
            else {
                this.model = new Edit.TestModel();
                GSU.loadMask.hide();
            }

            this.collection = this.model.questions;

            this.SubjectTest = new Backbone.Collection();
            this.SubjectTest.url = "api/TestSubject/";
            this.SubjectTest.on("sync", this.renderTestSubjectSelect, this);
            this.SubjectTest.fetch();
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

        changeTitle: function (event) {
            this.model.set("title", event.target.value);

        },
        changeSubjectId: function (event) {
            this.model.set("subjectId", parseInt(event.target.value));

        },
        changeСountQuestion: function (event) {
            this.model.set("countQuestion", event.target.value);

        },
        onSubmit: function (event) {
            event.preventDefault()




            this.model.questions.forEach(function (item, index, collection) {
                item.answers.forEach(function (it) {
                    if (it.isNew() || it.changedAttributes()) {
                        if (it.isNew()) {
                            it.set("testQuestionId", item.id);
                        }
                        it.save();
                    }
                })
            })

            return;
            //console.log(this.model.getDataForJSON().toJSON());
            var c = this.model.getDataForJSON();
            
            c.url = "api/Test/Edit";
            //c.set("idTest", this.model.get("id"));
            //c.set("answers", this.model.answers.toJSON());
            c.on("sync", function (r, mod, xht) {

                GSU.trigger("Admin:TestSubject:show");


            });
            c.save();


        },
        addQuestion: function () {
            this.model.questions.add({});
        },
        renderTestSubjectSelect: function () {
            console.log(this);
            var tesSubjectId = this.model.get("subjectId");
            var inputSelect = this.$el.find("#test-edit-subjectId");
            _.each(this.SubjectTest, function (c, num, collection) {
                var model = collection.at(num);
                var s =
                    inputSelect.append($("<option value='" + model.id + "' " + ((tesSubjectId == model.id) ? "selected" : "") + " >" + model.get("name") + "</option>"))

            })

        }

    });


});
