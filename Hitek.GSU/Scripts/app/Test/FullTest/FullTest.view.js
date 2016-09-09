GSU.module("Test.FullTest", function (FullTest, GSU, Backbone, Marionette, $, _) {


    FullTest.AnswerView = Backbone.Marionette.ItemView.extend({
        template: "Test/FullTest/answer",
        events: {
            /*"change input": function (e) {

                //this.triggerMethod('answer:set', this.model.get("id"), e.target.checked);
                this.model.set("isAnwered", e.target.checked);
                console.log(this.model);
            }*/
        },
        checkStatusAnswer: function () {
            this.model.set("isAnswered", this.$el.find('input')[0].checked)
        }


    });

    FullTest.QuestionView = Backbone.Marionette.CompositeView.extend({
        tagName: "div",
        className: "panel-body test-cart",
        template: "Test/FullTest/question",
        childViewContainer: ".test-aswers",
        getChildView: function (item) {  if (this.model.get("isSingleAnswer")) return FullTest.AnswerView; },
        events: {
            "click .commit-answer": "onCommitAnswer"
        },
        childEvents: {
            'answer:set': "onChangeAnswer"
        },
        initialize: function () {
            console.log(this.model);
            this.collection = this.model.answers;
        },
        onChangeAnswer: function (childView, answerId, status) {
            var t = this.model.get("tempAnswer");
            if (this.model.get("isSingleAnswer")) {
                if (status)
                    this.model.set("tempAnswer", answerId);
                else
                    this.model.set("tempAnswer", null);
            }
            else {
                throw Error("Не реализовано несколько ответов");
            }
        },
        onCommitAnswer: function () {
            this.children.each(function (view) {

                view.checkStatusAnswer();

            });
           
            console.log(this.collection);

            debugger;;
            //this.triggerMethod('question:commitAnswer', this.model.get("id"), this.model.get("tempAnswer"), this.model.get("nextQuestion"));
        }

    });

    FullTest.QuestionPaginationItemView = Backbone.Marionette.ItemView.extend({


        template: "Test/FullTest/questionPaginationItem",
        events: {
            "click": "click"

        },
        click: function () {
            this.triggerMethod('show:changeQuestion', this.model.get("questionId"));
        }

    });

    FullTest.QuestionPaginationView = Backbone.Marionette.CompositeView.extend({
        template: "Test/FullTest/questionPagination",
        childViewContainer: ".question-pagination",
        childView: FullTest.QuestionPaginationItemView,
        events: {
            "click .test-complite": "onClickEndTest"
        },
        onClickEndTest: function () {
            this.triggerMethod('test:complite');
        }

    });


    FullTest.view = Backbone.Marionette.LayoutView.extend({

        template: "Test/FullTest/fullTestTemplate",
        "regions": {
            questionPagination: ".test-question-pagination",
            content: ".test-сontent"
        },
        events: {
            "click": "click"

        },
        modelEvents: {
            "sync": "onSyncModel",
            "change:currentQuestionId": "onChangeCurrentQuestionId"
        },
        childEvents: {
            'show:changeQuestion': function (childView, id) {
                this.model.answers.get(this.model.get("currentQuestionId")).set("isCurrent", false);

                this.model.set("currentQuestionId", id);
            },
            'question:commitAnswer': function (childView, id, answer, nextQuestion) {
                if (answer) {
                    this.model.answers.get(id).set("answerId", answer);
                    this.model.questions.get(id).answers.get(answer).set("isChecked", true)
                }
                this.model.answers.get(this.model.get("currentQuestionId")).set("isCurrent", false);
                if (nextQuestion)
                    this.model.set("currentQuestionId", nextQuestion);
                else
                    this.model.set("currentQuestionId", this.model.questions.at(0).get("id"));
            },

            'test:complite': "onComliteTest"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.model = new FullTest.TestModel({id: paramId.id});
            this.model.fetch();
        },

        onSyncModel: function () {
            GSU.loadMask.hide();
            if (this.model.get("id") && this.model.get("id") > 0) {
                this.render();
                if (this.model.questions && this.model.questions.length > 0) {
                    this.model.set("currentQuestionId", this.model.questions.at(0).get("id"));
                }
            }
            else {
                GSU.trigger("Error:404");
            }

        },
        onChangeCurrentQuestionId: function (model) {
            var cq = this.model.get("currentQuestionId");
            this.model.answers.get(cq).set("isCurrent", true);
            var v = new FullTest.QuestionView({model: this.model.questions.get(cq)})
            this.content.show(v);
            this.questionPagination.show(new FullTest.QuestionPaginationView({collection: this.model.answers}));
        },
        onComliteTest: function () {
            var c = new Backbone.Model();
            c.url = "Test/Check";
            c.set("idTest", this.model.get("id"));
            c.set("answers", this.model.answers.toJSON());
            c.on("sync", function (r, mod, xht) {
                debugger;
                if (mod.id) {
                    GSU.trigger("Test:showResult", mod.id);

                }
            });
            c.save();

        }
    });


});
