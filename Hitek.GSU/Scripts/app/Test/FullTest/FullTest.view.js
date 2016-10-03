GSU.module("Test.FullTest", function (FullTest, GSU, Backbone, Marionette, $, _) {


    FullTest.SingleAnswerView = Backbone.Marionette.ItemView.extend({
        template: "Test/FullTest/singleAnswer",
        checkStatusAnswer: function () {
            var isAnswer = this.$el.find('input')[0].checked;
            this.model.set("isAnswered", isAnswer);
            return isAnswer;
        }


    });
    FullTest.MaltyAnswerView = Backbone.Marionette.ItemView.extend({
        template: "Test/FullTest/multyAnswer",
        checkStatusAnswer: function () {
            var isAnswer = this.$el.find('input')[0].checked;
            this.model.set("isAnswered", isAnswer);
            return isAnswer;
        }


    });

    FullTest.QuestionView = Backbone.Marionette.CompositeView.extend({
        tagName: "div",
        className: "panel-body test-cart",
        template: "Test/FullTest/question",
        childViewContainer: ".test-aswers",
        getChildView: function (item) {
            if (this.model.get("isSingleAnswer"))
            {
                return FullTest.SingleAnswerView;
            }
            else
            {
                return FullTest.MaltyAnswerView;
            }
        },
        events: {
            "click .commit-answer": "onCommitAnswer",
            "click .js-next-question": "showNextQuestion",
            "click .js-preview-question": "showPreviewQuestion"
        },
        childEvents: {
            //'answer:set': "onChangeAnswer"
        },
        initialize: function () {
          
            this.collection = this.model.answers;
        },
        showNextQuestion: function () {
            this.triggerMethod('question:nextQuestion', this.model, this.model.get("nextQuestion"));
        },
        showPreviewQuestion: function () {
            this.triggerMethod('question:nextQuestion', this.model, this.model.get("previosQuestion"));
        },
        onCommitAnswer: function () {
            var isAnswered = false;
            this.children.each(function (view) {
                isAnswered = view.checkStatusAnswer() || isAnswered;
            });
           
            this.collection.each(
                function (e) {
                    e.save();
                }
            );
            this.model.set("isAnswered", isAnswered);
            //debugger;;
            this.triggerMethod('question:nextQuestion', this.model, this.model.get("nextQuestion"));
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
               // this.model.answers.get(this.model.get("currentQuestionId")).set("isCurrent", false);
                this.model.questions.get(this.model.get("currentQuestionId")).set("isCurrent", false);
                this.model.set("currentQuestionId", id);
            },
            'question:nextQuestion': function (childView, currentQuestion,nextQuestion) {
               
                currentQuestion.set("isCurrent", false);
                if (nextQuestion)
                    this.model.set("currentQuestionId", nextQuestion);
                else
                    this.model.set("currentQuestionId", this.model.questions.at(0).get("id"));
            },

            'test:complite': "onComliteTest"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.model = new FullTest.TestModel({ id: paramId.id });
            this.model.fetch({ 
                data: { exist: paramId.exist},
                processData: true,
                error: function (model, xhr, req) {     
                    GSU.trigger("Error", xhr.status);
                }
            });
        },

        onSyncModel: function () {
            
        
                this.render();
                if (this.model.questions && this.model.questions.length > 0) {
                     this.model.set("currentQuestionId", this.model.questions.at(0).get("id"));
                }
            
           GSU.loadMask.hide();

        },
        onChangeCurrentQuestionId: function (model) {
            var cq = this.model.questions.get(this.model.get("currentQuestionId"));
            cq.set("isCurrent", true);
            var v = new FullTest.QuestionView({model: cq})
            this.content.show(v);

  
            var t = this.model.questions.map(
                function (item, key) {
                    return {
                        num: key + 1,
                        isAnswered: item.get("isAnswered"),
                        isCurrent: item.get("isCurrent"),
                        questionId: item.get("id")
                    };
                }
            );
            this.questionPagination.show(new FullTest.QuestionPaginationView({ collection: new Backbone.Collection(t) }));
        },
        onComliteTest: function () {
            var c = new Backbone.Model();
            c.url = "api/Test/Check";
            c.set("idTest", this.model.get("id"));
            c.on("sync", function (r, mod, xht) {
                GSU.trigger("Test:showResultAll");
            });
            c.save();

        }
    });


});
