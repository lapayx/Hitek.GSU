GSU.module("Admin.User.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    Edit.AnswerCollection = Backbone.Collection.extend({
        model: Edit.AnswerModel,
        //url: "api/TestSubject/",
        getDataForJSON: function () {
            var res = [];
            _.each(this.models, function (c, num, collection) {
                res.push(collection[num].toJSON());
            })
            return res;
        }
    });

    Edit.QuestionCollection = Backbone.Collection.extend({
        model: Edit.QuestionModel,
        //url: "api/TestSubject/",
        getDataForJSON: function () {
            var res = [];
            _.each(this.models, function (c, num, collection) {
                res.push(collection[num].getDataForJSON().toJSON());

            })
            return res;
        }
    })
});
