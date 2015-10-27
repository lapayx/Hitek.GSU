GSU.module("Admin.User.List", function (List, GSU, Backbone, Marionette, $, _) {


    List.item = Backbone.Marionette.ItemView.extend({
        template: "Admin/User/List/item",
        events: {
            "click .js-add-role-admin": "addRoleAdmin",
            "click .js-add-role-teacher": "addRoleTeacher",
            "click .js-remove-all-role":"removeAllRole",
        },
        addRoleAdmin: function () {
            var c = new Backbone.Model();
            c.url = "UserManager/AddRole";
            c.set("userId", this.model.get("id"));
            c.set("role", "Admin");
            c.on("sync", function (r, mod, xht) {
                if (mod.success) {
                    alert("Роль Админа добавлена");
                } else {
                    alert("Роль Админа НЕ добавлена");
                }
            });
            c.save();

        },
        addRoleTeacher: function () {
            var c = new Backbone.Model();
            c.url = "UserManager/AddRole";
            c.set("userId", this.model.get("id"));
            c.set("role", "Teacher");
            c.on("sync", function (r, mod, xht) {
                if (mod.success) {
                    alert("Роль Учитель добавлена");
                }
                else {
                    alert("Роль Учитель НЕ добавлена");
                }
            });
            c.save();

        },
        removeAllRole: function () {
            var ans = confirm("Удалить все роли у пользователя  " + this.model.get("userName") + "?");
            if (ans) {
                var c = new Backbone.Model();
                c.url = "UserManager/RemoveAllRole";
                c.set("userId", this.model.get("id"));
                c.on("sync", function (r, mod, xht) {
                    if (mod.success) {
                        alert("Роли удалены");
                    }
                    else {
                        alert("Роли  НЕ удалены");
                    }
                });
                c.save();
            }

        }
    });
    List.noItem = Backbone.Marionette.ItemView.extend({
        template: "Admin/User/List/noItem"
    });


    List.view = Backbone.Marionette.CompositeView.extend({
        template: "Admin/User/List/main",
        childViewContainer: ".test-lists",
        childView: List.item,
        emptyView: List.noItem,

        events: {
        },
        modelEvents: {
            "sync": "onSyncModel"
        },
        collectionEvents: {
            "sync": "onSyncCollection"
        },
        initialize: function (paramId) {
            GSU.loadMask.show();
            this.model = new List.SubjectInfoModel({ id: paramId.id });
            // this.model.fetch();
            this.collection = new List.DetailCollection();
            this.collection.id = paramId.id;
            //this.colectgion.url = "api/subject/"+paramId.id +"/Test";
            this.collection.fetch();

        },
        onSyncModel: function () {
            GSU.loadMask.hide();
            if (this.model.id && this.model.id > 0) {
                this.render();
            }
            else {
                GSU.trigger("Error:404");
            }
        },
        onSyncCollection: function () {
            GSU.loadMask.hide();
            this.render();
        }

        

    });


});
