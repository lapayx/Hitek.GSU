GSU.module("Test.GenerateTest", function (GenerateTest, GSU, Backbone, Marionette, $, _) {


    GenerateTest.model = Backbone.Model.extend({
        urlRoot: "api/Test/",
        idAttribute: "id",
        defaults: {
            id: 0,
            name: "Заголовок",
            testSubjectName:""
        }
    });

});
