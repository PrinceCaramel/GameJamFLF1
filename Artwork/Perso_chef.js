(function(window) {
Symbol_1_instance_1 = function() {
	this.initialize();
}
Symbol_1_instance_1._SpriteSheet = new createjs.SpriteSheet({images: ["Perso_chef.png"], frames: [[0,0,296,369,0,133.3,62.9],[296,0,296,369,0,133.3,62.9],[592,0,296,369,0,133.3,62.9],[0,369,296,369,0,133.3,62.9],[296,369,296,369,0,133.3,62.9],[592,369,296,369,0,133.3,62.9],[0,738,296,369,0,133.3,62.9],[296,738,296,369,0,133.3,62.9],[592,738,296,369,0,133.3,62.9],[0,1107,296,369,0,133.3,62.9],[296,1107,296,369,0,133.3,62.9],[592,1107,296,369,0,133.3,62.9],[0,1476,296,369,0,133.3,62.9],[296,1476,296,369,0,133.3,62.9]]});
var Symbol_1_instance_1_p = Symbol_1_instance_1.prototype = new createjs.BitmapAnimation();
Symbol_1_instance_1_p.BitmapAnimation_initialize = Symbol_1_instance_1_p.initialize;
Symbol_1_instance_1_p.initialize = function() {
	this.BitmapAnimation_initialize(Symbol_1_instance_1._SpriteSheet);
	this.paused = false;
}
window.Symbol_1_instance_1 = Symbol_1_instance_1;
}(window));

