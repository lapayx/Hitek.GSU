GSU.module("Item.UserInfo", function (UserInfo, GSU, Backbone, Marionette, $, _) {

    UserInfo._informationView = Backbone.Marionette.ItemView.extend({
        template: "info",
        initialize: function (param) {
            console.log(param);
            if (!this.model) {
                this.model = new UserInfo.model();
            }
            this.model.fetch({ data: { id: param.userId } });
            this.model.on("change", this.render);
            this.model.on("destroy", this.render);
        }
    });

    UserInfo.view = GSU.Common.SectionView.extend({
        title: "Профиль",
        param: {
            userId: 0
        },
        // itemView: Info._informationView,
        regionContent: {
            content: UserInfo._informationView
        },
        initialize: function (param) {
            this.param.userId = param.userId;

            GSU.Common.SectionView.prototype.initialize.call(this);

        },
    });



});