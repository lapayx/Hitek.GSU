GSU.module("Admin.Test.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    Edit.AnswerCollection = Backbone.Collection.extend({
        model: Edit.AnswerModel,
        //url: "api/TestSubject/"
     });

    Edit.QuestionCollection = Backbone.Collection.extend({
        model: Edit.QuestionModel,
        //url: "api/TestSubject/"
     })
});
