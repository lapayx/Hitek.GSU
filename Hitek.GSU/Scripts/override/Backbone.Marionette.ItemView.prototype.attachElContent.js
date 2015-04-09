Backbone.Marionette.ItemView.prototype.attachElContent = function (html) {
   
    this.$el.html(html);
    //debugger;
    if (this.tagName.toLowerCase() == Backbone.Marionette.View.prototype.tagName.toLowerCase()) {
        // Unwrap the element to prevent infinitely 
        // nesting elements during re-render.

        var child = this.el.children[0];
        if (this.el.tagName.toLowerCase() == Backbone.Marionette.View.prototype.tagName.toLowerCase()) {
            this.setElement(child);
        } else {
            child.style.border = "1px solid red";
            //  this.el.tagName = child.tagName;
            if (child.className) {
                if (this.className) {
                    this.el.className = this.className + " " + child.className;
                }
                else {
                    this.el.className = child.className;
                }

            }
            if (child.className && !this.id) {
                this.el.id = child.id;
            }
            // debugger;
            this.$el.html(child.innerHTML);
        }
        
    } 
    return this;
}

