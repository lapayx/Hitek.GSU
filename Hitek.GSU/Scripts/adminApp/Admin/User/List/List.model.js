GSU.module("Admin.User.List", function (List, GSU, Backbone, Marionette, $, _) {


    List.DetailModel = Backbone.Model.extend({

       // urlRoot: "Test/",
        defaults: {
            id: 0,
            userName: "Имя",
            lastLoginDateString: "-",
            registrationDateString: "-"
            
        },
        parse: function (raw) {
            // raw.name = raw.Name;
            // raw.id = raw.Id;
            
            if (raw.registrationDate) {
                var d = new Date(Date.parse(raw.registrationDate));
                raw.registrationDate = d;
                raw.registrationDateString = d.getDate() + "." + (d.getMonth() + 1) + "." + d.getFullYear() + " "
                    + d.getHours() + ":" + d.getMinutes();
            }
            if (raw.lastLoginDate) {
                var d = new Date(Date.parse(raw.lastLoginDate));
                raw.lastLoginDate = d;
                raw.lastLoginDateString = d.getDate() + "." + (d.getMonth() + 1) + "." + d.getFullYear() + " "
                    + d.getHours() + ":" + d.getMinutes();
            }
            console.log(raw.endDate)
            return raw;
        }
    });
    List.SubjectInfoModel = Backbone.Model.extend({

      //  urlRoot: "api/TestSubject/",
        defaults: {
            id: 0,
            name: "Заголовок"
        }
    });


});
