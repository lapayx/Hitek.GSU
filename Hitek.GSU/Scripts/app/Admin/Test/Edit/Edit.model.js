GSU.module("Admin.Test.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {



    Edit.TestModel = Backbone.Model.extend({

        constructor: function () {
            this.questions = new Edit.QuestionCollection();

            Backbone.Model.apply(this, arguments);
        },
        urlRoot: "api/TestSubject/",
        defaults: {
         //   isParent: false,
            name: "Заголовок"             
        },
        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.id = raw.Id;
            if (raw && raw.questions) {
                this.questions.reset(raw.questions, { parse: true });
            }
            return raw;
        },

    });

    Edit.QuestionModel = Backbone.Model.extend({
        constructor: function () {
            this.answers = new Edit.AnswerCollection();

            Backbone.Model.apply(this, arguments);
        },
       //urlRoot: "api/TestSubject/",
        defaults: {
            isParent: false,
            name: "Новая тема",
            result: 0,
            parentId: 0
        },
        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.id = raw.Id;
            this.answers.reset(raw.answers, { parse: true });

            return raw;
        },

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
