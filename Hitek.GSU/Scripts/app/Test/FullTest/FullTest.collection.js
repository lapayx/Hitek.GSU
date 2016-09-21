GSU.module("Test.FullTest", function (FullTest, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    FullTest.AnswerCollection = Backbone.Collection.extend({
        model: FullTest.AnswerModel
    });
    FullTest.QuestionCollection = Backbone.Collection.extend({
        model: FullTest.QuestionModel
    });


});
