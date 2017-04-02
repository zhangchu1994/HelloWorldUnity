-- Index                            int                              序号
-- bulletID                         int                              子弹ID
-- name                             string                           子弹名称
-- bulletSpeed                      int                              子弹飞行速度
-- effectRoute                      string                           子弹效果路径
-- FlyType                          int                              运动轨迹

return {
	[1] = {
		bulletID = 1,
		name = "弓箭",
		bulletSpeed = 15,
		effectRoute = "Effect/ArrowFX_Fire",
		FlyType = 6,
	},
	[2] = {
		bulletID = 2,
		name = "寒冰箭",
		bulletSpeed = 15,
		effectRoute = "Effect/CloudFlashFX",
		FlyType = 6,
	},
	[3] = {
		bulletID = 3,
		name = "火球",
		bulletSpeed = 15,
		effectRoute = "Effect/CloudFlashFX",
		FlyType = 6,
	},
}
