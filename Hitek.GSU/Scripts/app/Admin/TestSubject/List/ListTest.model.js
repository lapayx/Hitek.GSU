GSU.module("Admin.TestSubject.List", function (List, GSU, Backbone, Marionette, $, _) {



    List.TreeModel = Backbone.Model.extend({
        constructor: function () {
            this.children = new List.TreeCollection();
            
            Backbone.Model.apply(this, arguments);
        },
        // url: "api/TestHistory/",
        urlRoot: "api/TestSubject/",
        defaults: {
            isParent: false,
            name: "Заголовок",
            result: 0
            
             
        },
        parse: function (raw) {
            console.log(raw.childrens);
            this.children.reset(raw.childrens, { parse: true });
            if (this.children.length > 0) {
                raw.isParent = true;
            }
           return raw;
        }
       
    });

 
   


});
