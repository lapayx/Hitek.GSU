﻿GSU.module("Test.ResultTest", function (ResultTest, GSU, Backbone, Marionette, $, _) {



    ResultTest.ResultModel = Backbone.Model.extend({
        url: "api/TestHistory/",
        defaults: {
            id: 0,
            name: "Заголовок",
            result: 0
            
             
        },
        parse: function (raw) {
          //  raw.name = raw.Name;
           // raw.id = raw.Id;
            raw.result = raw.result*100;
            
            return raw;
        }
       
    });

 
   


});
