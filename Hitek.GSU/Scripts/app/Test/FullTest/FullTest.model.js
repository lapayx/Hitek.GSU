GSU.module("Test.FullTest", function (FullTest, GSU, Backbone, Marionette, $, _) {


    FullTest.TestModel = Backbone.Model.extend({
        constructor: function () {
            this.questions = new FullTest.QuestionCollection();
            this.answers = new FullTest.AnswerForViewCollection();
            Backbone.Model.apply(this, arguments);
        },
        urlRoot: "Test/Exist/",
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

    FullTest.QuestionModel = Backbone.Model.extend({
        constructor: function () {
            this.answers = new FullTest.AnswerCollection();
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
            previosQuestion: null
        },
        parse: function (raw) {
            this.answers.reset(raw.answers, { parse: true });
            raw.isAnswered = !!this.answers.models.find(function (item) { return item.get("isAnswered") === true; });

            //raw.name = raw.Name;
            // raw.id = raw.Id;
            // raw.text = raw.Text;
            return raw;
        }
    });

    FullTest.AnswerModel = Backbone.Model.extend({
        urlRoot: "api/WorkTestAnswer/",
       // url: "api/WorkTestAnswer",
        idAttribute: "id",
        defaults: {
            name: "",
            text: "",
            id: null,
            isAnswered: false
        },
        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.text = raw.Text;
            //raw.id = raw.Id;
            return raw;
        }
    });

    FullTest.AnswerForSyncModel = Backbone.Model.extend({
        defaults: {
            questionId: 0,
            answerId: 0
        }
    });

    FullTest.AnswerForViewModel = Backbone.Model.extend({
        defaults: {
            id: 0,
            questionId: null,
            answerId: null,
            num: 0,
            isCurrent: false
        }
    });


});
