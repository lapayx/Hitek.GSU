GSU.module("Item.LastAction", function (LastAction, GSU, Backbone, Marionette, $, _) {






    LastAction.view = GSU.Common.SectionView.extend({
		title: "Последние события на сайте",
		param: {
			userId: 0
		},
		// itemView: Info._informationView,
		regionContent: {
		    content: LastAction.collection.view
		},
		initialize: function (param) {
			this.param.userId = param.userId;

			GSU.Common.SectionView.prototype.initialize.call(this);

		},
	});



});