-- Index                            int                              Index
-- id                               int                              ID
-- name                             string                           名字

return {
	[1] = {
		Index = 1,
		Name = "人物",
		SubSystem = {[1]="角色",[2]="转生",[3]="羽翼",[4]="经脉"},
	},
	[2] = {
		Index = 2,
		name = "技能",
		SubSystem = {[1]="技能",[2]="秘籍",[3]="突破"},
	},
	[3] = {
		Index = 3,
		name = "锻造",
		SubSystem = {[1]="强化",[2]="宝石",[3]="注灵"},
	},
	[4] = {
		Index = 4,
		name = "背包",
		SubSystem = {[1]="装备",[2]="道具"},
	},
	[5] = {
		Index = 5,
		name = "商城",
		SubSystem = {[1]="神秘商店",[2]="积分商店",[3]="道具商城",[4]="功勋商店"},
	},
}
