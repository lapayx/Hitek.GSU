GSU.module("Admin.Test", function (Test, GSU, Backbone, Marionette, $, _) {


    Test.Router = Marionette.AppRouter.extend({
		appRoutes: {
			// "info": "showInfo",


			"Admin/Test/:id": "show",
			/*"Test/Result": "showResultAll",
			"Test/ListDetail/:id": "showListDetail",
			"Test/List": "showList",
			"Test/:id": "show",*/
		}
	});

	var API = {
		show: function (id) {
		    var view = new Test.List.view({ id: id });
			GSU.mainRegion.show(view);

		},
		/*editSunbject: function (id) {
		    var view = new Test.Edit.view({ id: id });
			GSU.mainRegion.show(view);

		},
        
		showResult: function (id) {
			var view = new TestSubject.ResultTest.view({ id: id });
			GSU.mainRegion.show(view);

		},
		showList: function (id) {
			var view = new TestSubject.ListTest.view({ id: id });
			GSU.mainRegion.show(view);

		},
		showListDetail: function (id) {
			var view = new TestSubject.ListTestDetail.view({ id: id });
			GSU.mainRegion.show(view);

		}*/
	};
	
	GSU.on("Admin:Test", function (id) {
	    GSU.navigate("Admin/Test/"+id);
	    API.show(id);
	});
	/*GSU.on("Test:showResult", function (id) {
		GSU.navigate("Test/Result/" + id);
		API.showResult(id);
	});
	GSU.on("Test:showResultAll", function () {
		GSU.navigate("Test/Result");
		API.showResultAll();
	});

	GSU.on("Test:showListDetail", function (id) {
		GSU.navigate("Test/ListDetail/" + id);
		API.showListDetail(id);
	});*/


	GSU.addInitializer(function () {
		new Test.Router({
			controller: API
		});
	});



});
