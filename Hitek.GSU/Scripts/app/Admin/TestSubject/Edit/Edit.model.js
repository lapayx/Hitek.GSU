GSU.module("Admin.TestSubject.Edit", function (Edit, GSU, Backbone, Marionette, $, _) {



    Edit.Subject = Backbone.Model.extend({
        urlRoot: "api/TestSubject/",
        defaults: {
            isParent: false,
            name: "Заголовок"             
        }
    });

 
   


});
