GSU.module("Item.UserInfo", function (UserInfo, GSU, Backbone, Marionette, $, _) {


    UserInfo.model = Backbone.Model.extend({
        defaults: {
            id: 0,
            name: "",
            imageId: 0,
            email: "",
            creationDate: "",
            birthday: null,
            svnInfo: 0,
            gitInfo: 0
        },
        url: "Info/jsonInfoUser/"
    });
   

});