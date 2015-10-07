GSU.module("Item.ReceivedMedal.collection", function (Collection, GSU, Backbone, Marionette, $, _) {



    Collection._informationView = Backbone.Marionette.ItemView.extend({
        template: "infoReceivedMedal"
    });
    Collection._emptyReceivedMedal = Backbone.Marionette.ItemView.extend({
        template: "emptyReceivedMedal",

        events: {
            "click" : "click"
        },
        initialize: function(options){
            this.userId = options.userId;
        },
        click: function () {
            console.log(arguments);
            GSU.trigger("PresentationMedal:show", { userId: this.userId});
        }
    });


    Collection.view = Backbone.Marionette.CollectionView.extend({
        childView: Collection._informationView,
        //collection: Collectiv.collection,
        tagName: "div",
        className: "with-block",
        emptyView: Collection._emptyReceivedMedal,
        _userId: 0,
       
        initialize: function (param) {
            this._userId = parseInt(param.userId);
            this.userId = param.userId;
            this.emptyViewOptions= {
                userId: this._userId
            };
            console.log(param);
            this.collection = new Collection.collection();
            this.collection.fetch({ data: { id: this._userId } });
            this.collection.on("reset", this.render);

            var self = this;
            GSU.on("ReceivedMedal:refresh", function () { self.refreshCollection();});



        },
        refreshCollection: function () {
            this.collection.fetch({ data: { id: this._userId },reset: true} );
        }

    });



});