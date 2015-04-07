GSU.module("Item.ReceivedMedal", function (ReceivedMedal, GSU, Backbone, Marionette, $, _) {






	ReceivedMedal.view = GSU.Common.SectionView.extend({
		title: "полученые медали",
		param: {
			userId: 0
		},
		// itemView: Info._informationView,
		regionContent: {
			content: ReceivedMedal.collection.view
		},
		initialize: function (param) {
			this.param.userId = param.userId;

			GSU.Common.SectionView.prototype.initialize.call(this);

		},
	});



});