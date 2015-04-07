GSU.module("Item.Personnel", function (Personnel, GSU, Backbone, Marionette, $, _) {
    Personnel.view = GSU.Common.SectionView.extend({
        title: "Коллектив",
        param: {
            userId: 0
        },
        regionContent: {
            content: Personnel.collection.view
        },
        initialize: function () {
            GSU.Common.SectionView.prototype.initialize.call(this);
        }
    });


});
