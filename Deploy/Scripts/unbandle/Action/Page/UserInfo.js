GSU.module("Action.Page.UserInfo", function (UserInfo, GSU, Backbone, Marionette, $, _) {
    UserInfo.Router = Marionette.AppRouter.extend({
        appRoutes: {
           // "info": "showInfo",
            "userInfo/:id": "showInfo"
        }
    });

    var API = {
        showInfo: function (id) {
            console.log("Полученеи информации о Пользователе с ID:" + id);

            var view = new Backbone.Marionette.StackView();
            GSU.mainRegion.show(view);
            view.pushView(new GSU.Item.UserInfo.view({ userId: id }));
            view.pushView(new GSU.Item.ReceivedMedal.view({ userId: id }));

        }
    };

    GSU.on("userInfo:show", function (id) {
        id = parseInt(id);
        GSU.navigate("userInfo/" + id);
        API.showInfo(id);
    });


    GSU.addInitializer(function () {
        new UserInfo.Router({
            controller: API
        });
    });
});
