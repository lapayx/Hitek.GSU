GSU.module("Test.ResultTest", function (ResultTest, GSU, Backbone, Marionette, $, _) {



    ResultTest.ResultModel = Backbone.Model.extend({
        url: "/Test/TestResultById",
        defaults: {
            id: 0,
            name: "Заголовок",
            result: 0
            
             
        },
        parse: function (raw) {
            raw.name = raw.Name;
            raw.id = raw.Id;
            raw.result = raw.Result*100;
            
            return raw;
        }
       
    });

 
   


});
