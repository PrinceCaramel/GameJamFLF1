(function(window) {
Symbol_1_instance_1 = function() {
	this.initialize();
}
Symbol_1_instance_1._SpriteSheet = new createjs.SpriteSheet({images: ["Perso_chef.png"], frames: [[0,0,296,368,0,133.25,62.85],[296,0,296,368,0,133.25,62.85],[592,0,296,368,0,133.25,62.85],[0,368,296,368,0,133.25,62.85],[296,368,296,368,0,133.25,62.85],[592,368,296,368,0,133.25,62.85],[0,736,296,368,0,133.25,62.85],[296,736,296,368,0,133.25,62.85],[592,736,296,368,0,133.25,62.85],[0,1104,296,368,0,133.25,62.85],[296,1104,296,368,0,133.25,62.85],[592,1104,296,368,0,133.25,62.85],[0,1472,296,368,0,133.25,62.85]]});
var Symbol_1_instance_1_p = Symbol_1_instance_1.prototype = new createjs.BitmapAnimation();
Symbol_1_instance_1_p.BitmapAnimation_initialize = Symbol_1_instance_1_p.initialize;
Symbol_1_instance_1_p.initialize = function() {
	this.BitmapAnimation_initialize(Symbol_1_instance_1._SpriteSheet);
	this.paused = false;
}
window.Symbol_1_instance_1 = Symbol_1_instance_1;
}(window));

