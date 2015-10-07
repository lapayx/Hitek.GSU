GSU.module("Admin.Test.List", function (List, GSU, Backbone, Marionette, $, _) {



    List.DetailModel = Backbone.Model.extend({

        urlRoot: "api/Test/",
        defaults: {
            id:0,
            name: "Заголовок"
        }       
    });
    List.SubjectInfoModel = Backbone.Model.extend({

        urlRoot: "api/TestSubject/",
        defaults: {
            id: 0,
            name: "Заголовок"
        }
    });

 
   


});
