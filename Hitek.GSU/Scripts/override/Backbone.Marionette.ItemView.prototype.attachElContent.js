Backbone.Marionette.ItemView.prototype.attachElContent = function (html) {
    this.$el.html(html);
    if (this.el.tagName.toLowerCase == Backbone.Marionette.View.prototype.tagName.toLowerCase) {
        // Unwrap the element to prevent infinitely 
        // nesting elements during re-render.
        var child = this.el.children[0];
        if (this.className) {
            child.className = this.className + ' ' + child.className;
        }
        if (this.id) {
            child.id = this.id;
        }
        this.setElement(child);
    }
    return this;
}

