GSU.module("Admin.User.List", function (List, GSU, Backbone, Marionette, $, _) {


    List.DetailModel = Backbone.Model.extend({

       // urlRoot: "Test/",
        defaults: {
            id: 0,
            userName: "Имя"
        }
    });
    List.SubjectInfoModel = Backbone.Model.extend({

      //  urlRoot: "api/TestSubject/",
        defaults: {
            id: 0,
            name: "Заголовок"
        }
    });


});
