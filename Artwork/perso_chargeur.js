(function(window) {
chargeur_instance_1 = function() {
	this.initialize();
}
chargeur_instance_1._SpriteSheet = new createjs.SpriteSheet({images: ["perso_chargeur.png"], frames: [[0,0,218,157,0,106,60.05],[218,0,218,157,0,106,60.05],[436,0,218,157,0,106,60.05],[218,0,218,157,0,106,60.05],[654,0,218,157,0,106,60.05],[0,157,218,157,0,106,60.05],[218,157,218,157,0,106,60.05],[436,157,218,157,0,106,60.05],[654,157,218,157,0,106,60.05],[0,314,218,157,0,106,60.05],[218,314,218,157,0,106,60.05],[436,314,218,157,0,106,60.05],[654,314,218,157,0,106,60.05],[0,471,218,157,0,106,60.05],[218,471,218,157,0,106,60.05],[436,471,218,157,0,106,60.05],[654,471,218,157,0,106,60.05],[0,628,218,157,0,106,60.05],[218,628,218,157,0,106,60.05],[436,628,218,157,0,106,60.05],[654,628,218,157,0,106,60.05],[0,785,218,157,0,106,60.05]]});
var chargeur_instance_1_p = chargeur_instance_1.prototype = new createjs.BitmapAnimation();
chargeur_instance_1_p.BitmapAnimation_initialize = chargeur_instance_1_p.initialize;
chargeur_instance_1_p.initialize = function() {
	this.BitmapAnimation_initialize(chargeur_instance_1._SpriteSheet);
	this.paused = false;
}
window.chargeur_instance_1 = chargeur_instance_1;
}(window));

