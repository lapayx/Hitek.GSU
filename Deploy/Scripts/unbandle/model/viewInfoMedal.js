GSU.module("Info.Medal", function (Medal, GSU, Backbone, Marionette, $, _) {





 

    Medal.model = Backbone.Model.extend({
        defaults: {
            id:0,
            medalImage : "", 
            medalInfo: "",
            medalname: "",
            comment: "",
            senderImage: "",
            senderName: ""
        },
        // url: "Info/jsonInfoUser/"


    });
    Medal.collection = Backbone.Collection.extend({
        model: Medal.model,
        url: "medal/GetRepicientMedal/"

    });
    Medal._informationView = Backbone.Marionette.ItemView.extend({
        template: "infoMedal",
        events: {
            "click": "click"
        },
        click: function () {
            console.log(this.model.id)
        }
        /*initialize: function (param) {
            console.log(param);
            if (!this.model) {
                this.model = new Collectiv.model();
            }
            this.model.fetch({ data: { id: param.userId } });
            this.model.on("change", this.render);
            this.model.on("destroy", this.render);
        }*/
    });

    Medal.CollectionView = Backbone.Marionette.CollectionView.extend({
        childView: Medal._informationView,
        //collection: Collectiv.collection,
        tagName: "div",
        className: "with-block",
        initialize: function (param) {
 
            this.collection = new Medal.collection();
            this.collection.fetch({ data: { id: param.userId } } );
            this.collection.on("reset", this.render);



        },

    });

    Medal.View = GSU.Common.SectionView.extend({
        title: "полученые медали",
        param: {
            userId: 0
        },
        // itemView: Info._informationView,
        regionContent: {
            content: Medal.CollectionView
        },
        initialize: function (param) {
             this.param.userId = param.userId;

            GSU.Common.SectionView.prototype.initialize.call(this);

        },
    });



});