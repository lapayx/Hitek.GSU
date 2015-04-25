GSU.module("Test.ListTest", function (ListTest, GSU, Backbone, Marionette, $, _) {



    ListTest.TreeModel = Backbone.Model.extend({
        constructor: function () {
            this.children = new ListTest.TreeCollection();
            
            Backbone.Model.apply(this, arguments);
        },
       // url: "api/TestHistory/",
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
