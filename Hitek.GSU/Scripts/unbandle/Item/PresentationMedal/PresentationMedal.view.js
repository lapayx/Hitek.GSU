GSU.module("Item.PresentationMedal", function (PresentationMedal, GSU, Backbone, Marionette, $, _) {

    PresentationMedal.model = Backbone.Model.extend({
        defaults: {
            medalId: null,
            medalImageId: null,
            medalName: "",
            info: "",
            isSelectUser: true,
            recipient: "",
            recipientId: 0,
            comment: null,
            success: false
        },
        // url: "Info/jsonInfoUser/"
        url: "Medal/SendMedal"
    });

    PresentationMedal.medalModel = Backbone.Model.extend({
        defaults: {
            name: "",
            imageId: 0
        }
        // url: "Info/jsonInfoUser/"
    });

    PresentationMedal.medalView = Backbone.Marionette.ItemView.extend({
        template: "GiveMedalItem",
        events: {
            "click": "click"
        },
        click: function () {
            this.trigger("medal:click", this.model);
        }

    });

    PresentationMedal.medalCollection = Backbone.Collection.extend({
        model: PresentationMedal.medalModel,
        url: "Medal/GetAvailableMedalForUser/"

    });

    PresentationMedal.view = Backbone.Marionette.CompositeView.extend({
        template: "GiveMedal",
        childView: PresentationMedal.medalView,
        childViewContainer: ".items",
        modelEvents: {
            "change:success": function (model) {
                if (model.get("success") == true) {
                    GSU.message("Медаль вручена");
                    GSU.trigger("ReceivedMedal:refresh");
                    GSU.trigger("modalWindow:close");
                }
               
            },
            "change": "render",

        },
        collectionEvents: {
            //"add": "modelAdded"
        },
        events: {
            "change .comment textarea": function (e) {
                this.model.set("comment", e.target.value);
            },
            "click .button": "sendMedal",
            "click .close": function () {
                GSU.trigger("modalWindow:close");
            }
        },
        initialize: function (param) {
            param.recipientId = parseInt(param.recipientId);
            this.model = new PresentationMedal.model();
            if (param.recipientId && !param.recipient) {
                var rep = GSU.cache.personel.findWhere({ id: param.recipientId });
                param.recipient = rep.get("firstName") + " " + rep.get("lastName");
            }
            
            this.model.set({ recipientId: param.recipientId,recipient: param.recipient });
           
            console.log(this.model);
            //    this.itemView = Collectiv._informationView();
            //this.collections = new PresentationMedal.collection();
            //this.collection.fetch();
            // this.collection.on("reset", this.render);
            //this.collection = new this.collection();
            this.collection = new PresentationMedal.medalCollection();
            this.collection.fetch({ data: { id: this.model.get("recipientId") } });

            this.on("childview:medal:click", function (childView, medal) {
                console.log(medal);
                this.model.set({
                    medalId: medal.get('id'),
                    medalImageId: medal.get('imageId'),
                    medalName: medal.get('name'),
                    info: medal.get('info')
                })
            });

        },

        sendMedal: function () {
            this.model.save();
        }


    });


});
