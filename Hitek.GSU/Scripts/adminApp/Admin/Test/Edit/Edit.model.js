GSU.module("Admin.Test.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {


    Edit.TestModel = Backbone.Model.extend({

        constructor: function () {
            this.questions = new Edit.QuestionCollection();

            Backbone.Model.apply(this, arguments);
        },
        urlRoot: "api/Test/Edit/",
        idAttribute :"id",
        defaults: function(){
            return {
                //   isParent: false,
                title: "Новый тест",
                subjectId: 0,
                countQuestion: 10,
                isCanShowResultAnswer:false
            }
        },
        parse: function (raw) {
            var newRaw = { 
                countQuestion: raw.countQuestion,
                id: raw.id,
                subjectId: raw.subjectId,
                title: raw.title,
                isCanShowResultAnswer: raw.isCanShowResultAnswer


            };
            // raw.name = raw.Name;
            // raw.id = raw.Id;

            if (raw && raw.questions) {
                this.questions.reset(raw.questions, {parse: true});
            }
            return newRaw;
        },
        getDataForJSON: function () {
            var res = new Backbone.Model();
            res.set("id", this.get("id"));
            res.set("title", this.get("title"));
            res.set("countQuestion", this.get("countQuestion"));
            res.set("subjectId", this.get("subjectId"));
            res.set("questions", this.questions.getDataForJSON());

            return res;
        }

    });

    Edit.QuestionModel = Backbone.Model.extend({
        constructor: function () {
            this.answers = new Edit.AnswerCollection();

            Backbone.Model.apply(this, arguments);
        },
        urlRoot: "api/TestQuestion/",
        idAttribute: "id",
        defaults: {
            isRemoved: false,
            name: "",
            text: "",
            testId: 0

        },
        parse: function (raw) {
           var  n = {};
            if (raw) {
                
                n.name = raw.name;
                n.id = raw.id;
                n.text = raw.text;
                n.testId = raw.testId;
                this.answers.reset(raw.answers, { parse: true });
               
            }
            if (this.previousAttributes() && this.previousAttributes().answers) {
                this.answers.reset(this.previousAttributes().answers, { parse: true });
            }

            return n;
        },
        getDataForJSON: function () {
            var res = new Backbone.Model();
            res.set("id", this.get("id"));
            res.set("title", this.get("title"));
            res.set("content", this.get("content"));
            res.set("answers", this.answers.getDataForJSON());

            return res;
        }

    });

    Edit.AnswerModel = Backbone.Model.extend({

        urlRoot: "api/TestAnswer/",
        //url: "api/TestAnswer/",
        idAttribute: "id",
        defaults: {
            isRight: false,
            text: "",
            isRemoved: false,
            testQuestionId: -1
        },
        onDestroy: function () {
            console.log(arguments);
        }

    })


});
