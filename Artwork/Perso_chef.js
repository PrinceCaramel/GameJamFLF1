(function(window) {
Symbol_1_instance_1 = function() {
	this.initialize();
}
Symbol_1_instance_1._SpriteSheet = new createjs.SpriteSheet({images: ["Perso_chef.png"], frames: [[0,0,302,401,0,133.5,95.15],[302,0,302,401,0,133.5,95.15],[604,0,302,401,0,133.5,95.15],[0,401,302,401,0,133.5,95.15],[302,401,302,401,0,133.5,95.15],[604,401,302,401,0,133.5,95.15],[0,802,302,401,0,133.5,95.15],[302,802,302,401,0,133.5,95.15],[604,802,302,401,0,133.5,95.15],[0,1203,302,401,0,133.5,95.15],[302,1203,302,401,0,133.5,95.15],[604,1203,302,401,0,133.5,95.15],[0,1604,302,401,0,133.5,95.15],[302,1604,302,401,0,133.5,95.15]]});
var Symbol_1_instance_1_p = Symbol_1_instance_1.prototype = new createjs.BitmapAnimation();
Symbol_1_instance_1_p.BitmapAnimation_initialize = Symbol_1_instance_1_p.initialize;
Symbol_1_instance_1_p.initialize = function() {
	this.BitmapAnimation_initialize(Symbol_1_instance_1._SpriteSheet);
	this.paused = false;
}
window.Symbol_1_instance_1 = Symbol_1_instance_1;
}(window));

