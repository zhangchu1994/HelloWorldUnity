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
-- synthChipNum                     int                              合成需要碎片数量
-- giveexp                          int                              提供经验
-- returndna                        int                              返还DNA

return {
	[1] = {
		id = 1,
		name = "胥博",
		tip = "巫妖王",
		quality = 1,
		animation = "1",
		img = "1",
		hp = 100,
		attack = 10,
		defence = 10,
		range = 3,
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
		synthChipNum = 10,
		giveexp = 10,
		returndna = 1,
	},
	[2] = {
		id = 2,
		name = "小桂",
		tip = "留小辫的男人",
		quality = 2,
		animation = "1",
		img = "1",
		hp = 100,
		attack = 15,
		defence = 15,
		range = 10,
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
		skillid = "3,4",
		synthChipNum = 15,
		giveexp = 15,
		returndna = 2,
	},
	[3] = {
		id = 3,
		name = "停停",
		tip = "妹子",
		quality = 3,
		animation = "1",
		img = "1",
		hp = 100,
		attack = 20,
		defence = 20,
		range = 15,
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
		skillid = "5,6",
		synthChipNum = 20,
		giveexp = 20,
		returndna = 3,
	},
}
