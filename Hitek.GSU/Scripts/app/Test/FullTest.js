GSU.module("Test.Main", function (FullTest, GSU, Backbone, Marionette, $, _) {


    FullTest.Router = Marionette.AppRouter.extend({
        appRoutes: {
            // "info": "showInfo",
            "Test/:id": "show"
        }
    });

    var API = {
        show: function (id) {
            var view = new FullTest.view({ id: id });
            GSU.mainRegion.show(view);

        }
    };

    GSU.on("Test:show", function (id) {
        GSU.navigate("Test/"+id);
        API.show(id);
    });






    FullTest.TestModel = Backbone.Model.extend({
        constructor: function () {
            this.questions = new FullTest.QuestionCollection();
            Backbone.Model.apply(this, arguments);
        },
        url: "/Test/",
        defaults: {
            id: 0,
            name: "Заголовок",
             
        },
          
        parse: function (raw) {
            raw.name = raw.Name;
            raw.id = raw.Id;
            this.questions.reset(raw.Questions, {parse:true});
            return raw;
        } 
       
    });

    FullTest.QuestionModel = Backbone.Model.extend({
        constructor: function () {
            this.answers = new FullTest.AnswerCollection();
            Backbone.Model.apply(this, arguments);
        },
        defaults: {
            id:0,
            name: "",
            text:""
        },
        parse: function (raw) {
            this.answers.reset(raw.Answers, {parse:true});
            raw.name = raw.Name;
            raw.id = raw.Id;
            raw.text = raw.Text;
            return raw;
        }
    });
    FullTest.AnswerModel = Backbone.Model.extend({
        defaults: {
            name: "",
            text: "",
            id: null
        },
        parse: function (raw) {
            raw.name = raw.Name;
            raw.text = raw.Text;
            raw.id = raw.Id;
            return raw;
        }
    });
    FullTest.AnswerView = Backbone.Marionette.ItemView.extend({
        template: "test/answer"
    });
     FullTest.AnswerCollection = Backbone.Collection.extend({
         model: FullTest.AnswerModel
     });
     FullTest.QuestionCollection = Backbone.Collection.extend({
         model: FullTest.QuestionModel
     });

    FullTest.QuestionView = Backbone.Marionette.CompositeView.extend({
        tagName:"div",
        template: "test/question",
        childViewContainer: ".test-aswers",
        childView: FullTest.AnswerView,

        initialize: function () {
            console.log(this.model);
            this.collection = this.model.answers;
        },
        
      });




    FullTest.view = Backbone.Marionette.LayoutView.extend({
        template: "test/fullTestTemplate",
        regions: {
          //  menu: "#menu",
            content: ".test-сontent"
        },
        initialize: function (paramId) {
           // debugger;
            this.model = new FullTest.TestModel();
            this.model.on("change", this.render);
            this.model.fetch({ data: { id: paramId.id } });
           
            
           
        },
        onRender: function () {
          //  debugger;
            if (this.model.get("Questions")) {
                var v = new FullTest.QuestionView({ model: this.model.questions.at(0) })
                this.content.show(v);
            }

        }
    });




    GSU.addInitializer(function () {
        new FullTest.Router({
            controller: API
        });
    });



});
