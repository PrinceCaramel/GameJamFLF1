(function(window) {
Symbol_1_instance_1 = function() {
	this.initialize();
}
Symbol_1_instance_1._SpriteSheet = new createjs.SpriteSheet({images: ["perso_pnj.png"], frames: [[5,5,142,184,0,0,37.05],[152,5,142,184,0,0,37.05],[5,194,142,184,0,0,37.05],[152,194,142,184,0,0,37.05]]});
var Symbol_1_instance_1_p = Symbol_1_instance_1.prototype = new createjs.BitmapAnimation();
Symbol_1_instance_1_p.BitmapAnimation_initialize = Symbol_1_instance_1_p.initialize;
Symbol_1_instance_1_p.initialize = function() {
	this.BitmapAnimation_initialize(Symbol_1_instance_1._SpriteSheet);
	this.paused = false;
}
window.Symbol_1_instance_1 = Symbol_1_instance_1;
}(window));

