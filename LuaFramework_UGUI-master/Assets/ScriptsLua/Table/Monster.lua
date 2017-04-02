-- Index                            int                              Index
-- id                               int                              ID
-- name                             string                           名字
-- tip                              string                           描述
-- quality                          int                              品质
-- animation                        string                           模型
-- img                              string                           图片
-- hp                               float                            生命
-- attack                           float                            攻击
-- defence                          float                            防御
-- range                            float                            攻击距离
-- attackSpeed                      float                            攻击速度
-- speed                            float                            移动速度
-- crit                             float                            暴击
-- crithurt                         float                            暴击伤害
-- dodge                            float                            闪避
-- injury_free                      float                            免伤
-- addhp                            float                            生命成长
-- addattack                        float                            攻击成长
-- addrange                         float                            攻击距离成长
-- addattackSpeed                   float                            攻击速度成长
-- addspeed                         float                            移动速度成长
-- adddiscrit                       float                            攻击距离成长
-- addcrit                          float                            暴击成长
-- addcrithurt                      float                            暴击伤害成长
-- adddodge                         float                            闪避成长
-- addinjury_free                   float                            免伤成长
-- skillid                          string                           技能ID

return {
	[1] = {
		id = 1,
		name = "巫妖博",
		tip = "大boss",
		quality = 1,
		animation = "enemy/Boss",
		img = "1",
		hp = 10,
		attack = 10,
		defence = 10,
		range = 50,
		attackSpeed = 100,
		speed = 100,
		crit = 0.9,
		crithurt = 1.2,
		dodge = 0,
		injury_free = 0,
		addhp = 5,
		addattack = 5,
		addrange = 0,
		addattackSpeed = 0,
		addspeed = 0,
		adddiscrit = 0,
		addcrit = 0,
		addcrithurt = 0,
		adddodge = 0,
		addinjury_free = 0,
		skillid = "1,2",
	},
	[2] = {
		id = 2,
		name = "巫妖桂",
		tip = "左护法",
		quality = 2,
		animation = "enemy/01-FlowerMonster-Blue",
		img = "1",
		hp = 15,
		attack = 15,
		defence = 15,
		range = 150,
		attackSpeed = 100,
		speed = 100,
		crit = 1,
		crithurt = 1.3,
		dodge = 0,
		injury_free = 0,
		addhp = 5,
		addattack = 5,
		addrange = 0,
		addattackSpeed = 0,
		addspeed = 0,
		adddiscrit = 0,
		addcrit = 0,
		addcrithurt = 0,
		adddodge = 0,
		addinjury_free = 0,
		skillid = "1,2",
	},
	[3] = {
		id = 3,
		name = "巫妖停",
		tip = "右护法",
		quality = 3,
		animation = "enemy/03-MaskedOrc-Grey",
		img = "1",
		hp = 20,
		attack = 20,
		defence = 20,
		range = 200,
		attackSpeed = 100,
		speed = 100,
		crit = 0,
		crithurt = 1.4,
		dodge = 0,
		injury_free = 0,
		addhp = 5,
		addattack = 5,
		addrange = 0,
		addattackSpeed = 0,
		addspeed = 0,
		adddiscrit = 0,
		addcrit = 0,
		addcrithurt = 0,
		adddodge = 0,
		addinjury_free = 0,
		skillid = "1,2",
	},
}
