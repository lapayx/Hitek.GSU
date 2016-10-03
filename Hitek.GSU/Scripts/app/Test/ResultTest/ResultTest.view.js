GSU.module("Test.ResultTest", function (ResultTest, GSU, Backbone, Marionette, $, _) {


    ResultTest.SingleAnswerView = Backbone.Marionette.ItemView.extend({
        template: "Test/ResultTest/singleAnswer",


    });
    ResultTest.MaltyAnswerView = Backbone.Marionette.ItemView.extend({
        template: "Test/ResultTest/multyAnswer",


    });

    ResultTest.QuestionView = Backbone.Marionette.CompositeView.extend({
        tagName: "div",
        className: "panel-body test-cart",
        template: "Test/ResultTest/question",
        childViewContainer: ".test-aswers",
        getChildView: function (item) {
            if (this.model.get("isSingleAnswer"))
            {
                return ResultTest.SingleAnswerView;
            }
            else
            {
                return ResultTest.MaltyAnswerView;
            }
        },
        events: {
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

    });

    ResultTest.QuestionPaginationItemView = Backbone.Marionette.ItemView.extend({


        template: "Test/ResultTest/questionPaginationItem",
        events: {
            "click": "click"

        },
        click: function () {
            this.triggerMethod('show:changeQuestion', this.model.get("questionId"));
        }

    });

    ResultTest.QuestionPaginationView = Backbone.Marionette.CompositeView.extend({
        template: "Test/ResultTest/questionPagination",
        childViewContainer: ".question-pagination",
        childView: ResultTest.QuestionPaginationItemView,
        

    });


    ResultTest.view = Backbone.Marionette.LayoutView.extend({

        template: "Test/ResultTest/resultTestTemplate",
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
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.model = new ResultTest.TestModel({ id: paramId.id });
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
            var v = new ResultTest.QuestionView({model: cq})
            this.content.show(v);

  
            var t = this.model.questions.map(
                function (item, key) {
                    return {
                        num: key + 1,
                        isCurrent: item.get("isCurrent"),
                        questionId: item.get("id"),
                        isRight: item.get("isRight")
                    };
                }
            );
            this.questionPagination.show(new ResultTest.QuestionPaginationView({ collection: new Backbone.Collection(t) }));
        },
       
    });


});
