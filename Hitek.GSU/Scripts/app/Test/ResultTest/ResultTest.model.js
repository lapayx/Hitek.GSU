GSU.module("Test.ResultTest", function (ResultTest, GSU, Backbone, Marionette, $, _) {


    ResultTest.TestModel = Backbone.Model.extend({
        constructor: function () {
            this.questions = new ResultTest.QuestionCollection();
            Backbone.Model.apply(this, arguments);
        },
        urlRoot: "api/TestHistory/",
        defaults: {
            id: null,
            name: "Заголовок",
            currentQuestionId: 0


        },

        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.id = raw.Id;
            this.questions.reset(raw.questions, { parse: true });
            this.addNavigationQuestion();
            return raw;
        },
        addNavigationQuestion: function () {
            var prev = null,
                c = null;
            for (var i = 0; i < this.questions.length; i++) {
                
                c = this.questions.at(i);
                if (i == 0) {
                    c.set("isCurrent", true);
                }
                if (prev) {
                    prev.set("nextQuestion", c.get("id"));
                    c.set("previosQuestion", prev.get("id"));
                }
                prev = c;
            }
        }

    });

    ResultTest.QuestionModel = Backbone.Model.extend({
        constructor: function () {
            this.answers = new ResultTest.AnswerCollection();
            Backbone.Model.apply(this, arguments);
        },
        defaults: {
            id: null,
            name: "",
            text: "",
            isSingleAnswer: true,
            isCurrent: false,
            isAnswered: false,
            nextQuestion: null,
            previosQuestion: null,
            isRight:false
        },
        parse: function (raw) {
            this.answers.reset(raw.answers, { parse: true });
            raw.isRight = this.answers.where({ isRight: true }).length == this.answers.where({ isAnswered: true, isRight: true }).length;
       
            return raw;
        }
    });

    ResultTest.AnswerModel = Backbone.Model.extend({
       // url: "api/WorkTestAnswer",
        idAttribute: "id",
        defaults: {
            name: "",
            text: "",
            id: null,
            isAnswered: false,
            isRight:false
        },
        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.text = raw.Text;
            //raw.id = raw.Id;
            return raw;
        }
    });

});
