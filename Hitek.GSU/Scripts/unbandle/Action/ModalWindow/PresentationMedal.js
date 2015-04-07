GSU.module("Action.ModalWindow.PresentationMedal", function (PresentationMedal, GSU, Backbone, Marionette, $, _) {

    var API = {
        showModalWindow: function (par) {
            param = {
                recipientId: par.userId,
                recipient: par.name
            }
            var v = new GSU.Item.PresentationMedal.view(param);
            GSU.modal.show(v);
        }
    };

    GSU.on("PresentationMedal:show", function (param) {
        //  GSU.navigate("collectiv");
        API.showModalWindow(param);
    });


});
