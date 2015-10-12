GSU.module("Test.ListTestDetail", function (ListTestDetail, GSU, Backbone, Marionette, $, _) {


    ListTestDetail.DetailModel = Backbone.Model.extend({

        // url: "api/TestHistory/",
        defaults: {
            id: 0,
            name: "Заголовок"
        }
    });
    ListTestDetail.SubjectInfoModel = Backbone.Model.extend({

        urlRoot: "api/TestSubject/",
        defaults: {
            id: 0,
            name: "Заголовок"
        }
    });


});
