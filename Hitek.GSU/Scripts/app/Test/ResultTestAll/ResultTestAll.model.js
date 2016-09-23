GSU.module("Test.ResultTestAll", function (ResultTestAll, GSU, Backbone, Marionette, $, _) {


    ResultTestAll.ResultModel = Backbone.Model.extend({
        url: "api/TestHistory/",
        defaults: {
            id: 0,
            name: "Заголовок",
            result: 0,
            startDate: new Date(),
            startSateString: "-",
            endDate: undefined,
            endSateString: "-",
        },
        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.id = raw.Id;
            raw.result = Math.round(raw.result * 1000) / 10;
            if (raw.startDate) {
                var d = new Date(Date.parse(raw.startDate));
                raw.startDate = d;
                raw.startSateString = d.getDate() + "." + (d.getMonth() + 1) + "." + d.getFullYear() + " "
                    + d.getHours() + ":" + d.getMinutes();
            }
            if (raw.endDate) {
                var d = new Date(Date.parse(raw.endDate));
                raw.endDate = d;
                raw.endSateString = d.getDate() + "." + (d.getMonth() + 1) + "." + d.getFullYear() + " "
                    + d.getHours() + ":" + d.getMinutes();
            }
            console.log(raw.endDate)
            return raw;
        }

    });


});
