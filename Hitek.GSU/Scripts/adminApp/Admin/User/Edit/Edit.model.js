GSU.module("Admin.User.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {


    Edit.TestModel = Backbone.Model.extend({

        constructor: function () {
            this.questions = new Edit.QuestionCollection();

            Backbone.Model.apply(this, arguments);
        },
        urlRoot: "Test/Edit/",
        defaults: {
            //   isParent: false,
            title: "Новый тест",
            subjectId: 0
        },
        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.id = raw.Id;
            if (raw && raw.questions) {
                this.questions.reset(raw.questions, {parse: true});
            }
            return raw;
        },
        getDataForJSON: function () {
            var res = new Backbone.Model();
            res.set("id", this.get("id"));
            res.set("title", this.get("title"));
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
        //urlRoot: "api/TestSubject/",
        defaults: {
            isRemoved: false,
            title: "",
            content: ""

        },
        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.id = raw.Id;
            this.answers.reset(raw.answers, {parse: true});

            return raw;
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

        // urlRoot: "api/TestSubject/",
        defaults: {
            isRight: false,
            content: "",
            isRemoved: false
        }

    })


});
