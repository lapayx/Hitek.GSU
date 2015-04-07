GSU.module("Test.Main", function (FullTest, GSU, Backbone, Marionette, $, _) {


    FullTest.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",
            "Test/:id": "show"
        }
    });

    var API = {
        show: function (id) {
            var view = new FullTest.view();
            GSU.mainRegion.show(view);

        }
    };

    GSU.on("Test:show", function (id) {
        GSU.navigate("Test/"+id);
        API.show(id);
    });

    FullTest.testModel = Backbone.Model.extend({
          defaults: {
              name: "Заголовок"
             
          },
          url: "/Test/",
          parse: function (raw) {
              var res = new Object() || {};
              console.log(raw);
              raw.na2me = raw.Name;
              return raw;
          } 
          // url: "Info/jsonInfoUser/"
          
      });
    /*  PresentationMedal.model = Backbone.Model.extend({
          defaults: {
              medalId: null,
              medalImageId: null,
              medalName: "",
              info: "",
              isSelectUser: true,
              recipient: "",
              recipientId: 0,
              comment: null,
              success: false
          },
          // url: "Info/jsonInfoUser/"
          url: "Medal/SendMedal"
      });
  
      PresentationMedal.medalModel = Backbone.Model.extend({
          defaults: {
              name: "",
              imageId: 0
          }
          // url: "Info/jsonInfoUser/"
      });
  
      PresentationMedal.medalView = Backbone.Marionette.ItemView.extend({
          template: "GiveMedalItem",
          events: {
              "click": "click"
          },
          click: function () {
              this.trigger("medal:click", this.model);
          }
  
      });
  
      PresentationMedal.medalCollection = Backbone.Collection.extend({
          model: PresentationMedal.medalModel,
          url: "Medal/GetAvailableMedalForUser/"
  
      });
      */
    FullTest.view = Backbone.Marionette.ItemView.extend({
        template: "test/fullTestTemplate",

        initialize: function () {
            this.model = new FullTest.testModel();
            this.model.on("change", this.render);
            this.model.fetch();
           
        }
    });




    GSU.addInitializer(function () {
        new FullTest.Router({
            controller: API
        });
    });



});
