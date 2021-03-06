﻿var GSU = new Marionette.Application();

GSU.addRegions({
    mainRegion: "#main-region",
    modal: "#modal-window-region"

});

GSU.ApplicationPath = "/";

GSU.navigate = function (route, options) {
    options || (options = {});
    Backbone.history.navigate(route, options);
};


GSU.getCurrentRoute = function () {
    return Backbone.history.fragment
};

GSU.closeModal = function () {
    GSU.modal.reset()
};

GSU.loadMask = {


    _selector: "#loading",
    show: function () {
        $(this._selector).show();

    },
    hide: function () {
        $(this._selector).hide();
    }

}
GSU.on("modalWindow:close", function (param) {
    GSU.closeModal();
});
/*
 GSU.addInitializer(function () {
 // console.log("Запуск инициализации");

 if (this.getCurrentRoute() === "") {
 GSU.trigger("about:show");
 }

 });
 */
GSU.token = "azaza";
GSU.message = function (msg, type) {
    alert(msg);
};

GSU.cache = {};

GSU.isRun = false;

GSU.on("start", function (options) {
    
    if (GSU.isRun) {
        return;
    }
    GSU.isRun = true;
   
    if (!localStorage.getItem("token")) {
        document.location = GSU.ApplicationPath+"Home/Login";
        return;
    }
    var tObj = JSON.parse(localStorage.getItem("token"));
    GSU.token = "Bearer " + tObj.access_token;
     
    if (_.indexOf(tObj.role, "Teacher") > -1) {
        $(".role-teacher").show();
    }

    $("#gsu-user-name").text(tObj.userName);
    if (Backbone.history) {
        Backbone.history.start();
    }
    if (this.getCurrentRoute() === "") {
        GSU.trigger("Main:show");
    }



});

GSU.on("logout", function (options) {

    if (localStorage.getItem("token")) {
        localStorage.removeItem("token");
    }

    document.location = GSU.ApplicationPath+"Home/Login";


});

GSU.addInitializer(function (options) {

    console.log("Запуск инициализации");

    //GSU.cache.personel = new GSU.Common.CachePersonel();
    //GSU.cache.personel.fetch();

});


$("document").ready(function () {
    GSU.start();
})