GSU.module("Admin.Test.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {
    "use strict";

    Edit.AnswerView = Backbone.Marionette.ItemView.extend({
        template: "Admin/Test/Edit/answer",

        events: {
            "change .js-test-edit-answer-content": "changeContent",
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
            event.preventDefault()
            if (this.model.isNew()) {
                this.model.destroy();
            } else {
                this.model.set("isRemoved", true);
                this.$el.hide();
            }

        }


    });

    Edit.QuestionView = Backbone.Marionette.CompositeView.extend({
        template: "Admin/Test/Edit/question",
        childViewContainer: ".js-test-answer-section",
        childView: Edit.AnswerView,
        events: {
            "click .js-test-add-answer": "addAnswer",
            "change .js-test-edit-question-title": "changeTitle",
            "change .js-test-edit-question-content": "changeContent",
            "click .js-test-edit-question-delete": "deleteQuestion"


        },
        childEvents: {

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
            this.model.set("name", event.target.value);
        },

        changeContent: function (event) {
            this.model.set("text", event.target.value);
        },

        deleteQuestion: function () {

            /*          if (!this.model.id) {
                          this.model.destroy();
                      } else {
                          this.model.set("isRemoved", true);
                          this.render();
                      }
          */
            if (this.model.isNew()) {
                this.model.destroy();
            } else {
                this.model.set("isRemoved", true);
                this.$el.hide();
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
            "change #СountQuestion": "changeСountQuestion",
            "change #isCanShowResultAnswer": "changeIsCanShowResultAnswer",
            "click .js-test-add-question": "addQuestion"

        },
        childEvents: {
            'questions:rerender': "rerenderChild"
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

                this.model = new Edit.TestModel({ id: paramId.id });
                this.model.fetch({
                    error: function (model, xhr, req) {
                        GSU.trigger("Error", xhr.status);
                    }
                });
            }
            else {
                this.model = new Edit.TestModel();
                GSU.loadMask.hide();
            }

            this.collection = this.model.questions;

            this.SubjectTest = new Backbone.Collection();
            this.SubjectTest.url = "api/TestSubject/";
            this.SubjectTest.on("sync", this.onRender, this);
            this.SubjectTest.fetch();
            //this.render();
        },

        onSyncModel: function () {
            GSU.loadMask.hide();
            this.render();

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
        changeIsCanShowResultAnswer: function () {
            this.model.set("isCanShowResultAnswer", event.target.checked);
        },
        onSubmit: function (event) {
            event.preventDefault()

            var updateAnswerCallback = function (item) {
                for (var i = 0; i < item.answers.models.length; i++) {
                    var it = item.answers.models[i];

                    if (it.isNew() || it.changedAttributes()) {
                        if (it.isNew()) {
                            it.set("testQuestionId", item.id);
                        }
                        if (it.get("isRemoved")) {
                            it.destroy();
                        } else {
                            it.save({});
                        }
                    }
                }

            };
            var updateQuestionCallback = function (itemTest) {
                for (var i = 0; i < itemTest.questions.models.length; i++) {
                    var item = itemTest.questions.models[i];

                    if (item.isNew() || item.changedAttributes()) {

                        if (item.isNew()) {
                            item.set("testId", itemTest.id);
                        }

                        if (item.get("isRemoved")) {
                            item.answers.forEach(function (it) {
                                it.destroy()
                            });
                            item.destroy();
                            continue

                        }
                        else {
                            item.set("answers", item.answers.toJSON())
                            item.save({}, { success: updateAnswerCallback });
                        }
                    } else {
                        updateAnswerCallback(item);
                    }


                }
            };
            var item = this.model;

            if (item.isNew() || item.changedAttributes()) {



                if (item.get("isRemoved")) {
                    item.questions.forEach(function (it) {

                        it.destroy()
                    });
                    item.destroy();
                }
                else {
                    item.questions.prepareCollection();
                    item.set("questions", item.questions.toJSON())
                    item.save({}, { success: updateQuestionCallback });
                }
            } else {
                updateQuestionCallback(item);
            }


        },

        addQuestion: function () {
            this.model.questions.add({});
        },
        onRender: function () {
            var tesSubjectId = this.model.get("subjectId");
            var inputSelect = this.$el.find("#test-edit-subjectId");
            var res = "";
            for (var i = 0; i < this.SubjectTest.length ; i++) {
                var model = this.SubjectTest.models[i];
                res += "<option value='" + model.id + "' " + ((tesSubjectId == model.id) ? "selected" : "") + " >" + model.get("name") + "</option>";

            }
            inputSelect.append($(res));

        }

    });


});
