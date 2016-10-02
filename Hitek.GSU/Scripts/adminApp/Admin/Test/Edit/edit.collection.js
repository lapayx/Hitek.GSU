GSU.module("Admin.Test.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {


    /* -------------------------- COLLECTION --------------------*/

    Edit.AnswerCollection = Backbone.Collection.extend({
        model: Edit.AnswerModel,
        url: "api/TestAnswer/",
       // urlRoot: "api/TestAnswer/",
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
        url: "api/TestQuestion/",
        //url: "api/TestSubject/",
        getDataForJSON: function () {
            var res = [];
            _.each(this.models, function (c, num, collection) {
                res.push(collection[num].getDataForJSON().toJSON());

            })
            return res;
        },
        prepareCollection: function () {
            
            for( var i =0 ; i < this.models.length; i++)
            {
                var item = this.models[i];
                item.set("answers", item.answers.toJSON());
            }

        }
        
    })
});
