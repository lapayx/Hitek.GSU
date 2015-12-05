GSU.module("Test.ResultTestAll", function (ResultTestAll, GSU, Backbone, Marionette, $, _) {


    ResultTestAll.ResultModel = Backbone.Model.extend({
        url: "TestHistory/",
        defaults: {
            id: 0,
            name: "Заголовок",
            result: 0,
            date: new Date(),
            dateString : "-"
        },
        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.id = raw.Id;
            raw.result = Math.round(raw.result * 1000) / 10;
            if (raw.date) {
                var d =  new Date(Date.parse(raw.date));
                raw.date = d;
                raw.dateString = d.getDate() + "." + (d.getMonth()+1) + "." + d.getFullYear() + " "
                    + d.getHours() + ":" + d.getMinutes();
            }
            return raw;
        }

    });


});
