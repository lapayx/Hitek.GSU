Backbone.Marionette.StackView = Backbone.Marionette.LayoutView.extend({

    template: "none",  
    _i: 0,
    /*initialize: function (param) {
        for (var i = 0 ; i < this.views.length ; i++) {
            this.$el.append("<div class='stack-view-" + i + "'></div>");
            this.addRegion("region-" + i, ".stack-view-" + i);
            var v = this.views[i];
            this["region-" + i].show(new v(param));
        }
    },*/
    /*
    onRender:function (param){
        for (var i = 0 ; i < this.views.length ; i++) {
            this.$el.append("<div id='stack-view-" + i + "'></div>");
             var v = new this.views[i](param); 
            this.addRegion("region-" + i, "#stack-view-" + i);
              
            
          //  this["region-" + i].show(v);;
        }
    },
    onBeforeShow: function (param) {
        for (var i = 0 ; i < this.views.length ; i++) {
            //this.$el.append("<div id='stack-view-" + i + "'></div>");
             var v = new this.views[i](param); 
            //this.addRegion("region-" + i, "#stack-view-" + i);
              
            
           // this["region-" + i].show(v);;
        }
    }
    ,*/
    pushView: function (view) {
        this.$el.append("<div class='stack-view-" + this._i + "'></div>");
        this.addRegion("region-" + this._i, ".stack-view-" + this._i);
        this["region-" + this._i].show(view);
        this._i++;
    }


})