GSU.module("Test.ResultTest", function (ResultTest, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    ResultTest.AnswerCollection = Backbone.Collection.extend({
        model: ResultTest.AnswerModel
    });
    ResultTest.QuestionCollection = Backbone.Collection.extend({
        model: ResultTest.QuestionModel
    });



});
