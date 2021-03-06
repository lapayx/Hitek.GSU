﻿GSU.module("Admin.Test.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {


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
                subjectId: -41,
                countQuestion: 10,
                isCanShowResultAnswer:false
            }
        }, 
        save: function (attrs, options) {
    
            var o = options || {};
            var a =  _.clone(this.attributes);

            // Filter the data to send to the server
            a.questions=null;

            //options.data = JSON.stringify(attrs);

            // Proxy the call to the original save function
            return Backbone.Model.prototype.save.call(this, a, o);
        },
        parse: function (raw) {
            /*debugger;
            var newRaw = { 
                countQuestion: raw.countQuestion,
                id: raw.id,
                subjectId: raw.subjectId,
                title: raw.title,
                isCanShowResultAnswer: raw.isCanShowResultAnswer


            };
            // raw.name = raw.Name;
            // raw.id = raw.Id;
            */
            if (raw && raw.questions) {
                this.questions.reset(raw.questions, { parse: true });
                raw.questions = null;
            }

            if (this.previousAttributes() && this.previousAttributes().questions) {
                this.questions.reset(this.previousAttributes().questions, { parse: true });
            }

           
            return raw;
        },
        getDataForJSON: function () {
            var res = this.toJSON();
            res.questions = this.questions.getDataForJSON();
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
        save: function (attrs, options) {

            var o = options || {};
            var a = _.clone(this.attributes);

            // Filter the data to send to the server
            a.answers = null;

            //options.data = JSON.stringify(attrs);

            // Proxy the call to the original save function
            return Backbone.Model.prototype.save.call(this, a, o);
        },
        parse: function (raw) {
      
            if (raw) {
                
                this.answers.reset(raw.answers, { parse: true });
                delete raw.answers;
            }
            if (this.previousAttributes() && this.previousAttributes().answers) {
                this.answers.reset(this.previousAttributes().answers, { parse: true });
            }

            return raw
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
