GSU.module("Item.InfoOnMainPage", function (InfoOnMainPage, GSU, Backbone, Marionette, $, _) {






    InfoOnMainPage.view = GSU.Common.SectionView.extend({
		title: "Полезная информация",
		param: {
			userId: 0
		},
		// itemView: Info._informationView,
		regionContent: {
		    content: Backbone.Marionette.ItemView.extend({
		        template: "InformationOnMainPage",
		        className: "informationOnMainPage"
		                })
		},
		initialize: function (param) {

			GSU.Common.SectionView.prototype.initialize.call(this);

		},
	});



});