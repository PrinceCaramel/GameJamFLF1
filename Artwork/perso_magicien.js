(function(window) {
mage_instance_1 = function() {
	this.initialize();
}
mage_instance_1._SpriteSheet = new createjs.SpriteSheet({images: ["perso_magicien.png"], frames: [[0,0,222,246,0,40.65,65.65],[222,0,222,246,0,40.65,65.65],[444,0,222,246,0,40.65,65.65],[666,0,222,246,0,40.65,65.65],[0,246,222,246,0,40.65,65.65],[222,246,222,246,0,40.65,65.65],[444,246,222,246,0,40.65,65.65],[666,246,222,246,0,40.65,65.65],[0,492,222,246,0,40.65,65.65],[222,492,222,246,0,40.65,65.65],[444,492,222,246,0,40.65,65.65],[666,492,222,246,0,40.65,65.65],[0,738,222,246,0,40.65,65.65],[222,738,222,246,0,40.65,65.65],[444,738,222,246,0,40.65,65.65],[666,738,222,246,0,40.65,65.65],[0,984,222,246,0,40.65,65.65],[222,984,222,246,0,40.65,65.65],[444,984,222,246,0,40.65,65.65],[666,984,222,246,0,40.65,65.65],[0,1230,222,246,0,40.65,65.65],[222,1230,222,246,0,40.65,65.65],[444,1230,222,246,0,40.65,65.65],[666,1230,222,246,0,40.65,65.65],[0,1476,222,246,0,40.65,65.65]]});
var mage_instance_1_p = mage_instance_1.prototype = new createjs.BitmapAnimation();
mage_instance_1_p.BitmapAnimation_initialize = mage_instance_1_p.initialize;
mage_instance_1_p.initialize = function() {
	this.BitmapAnimation_initialize(mage_instance_1._SpriteSheet);
	this.paused = false;
}
window.mage_instance_1 = mage_instance_1;
}(window));

