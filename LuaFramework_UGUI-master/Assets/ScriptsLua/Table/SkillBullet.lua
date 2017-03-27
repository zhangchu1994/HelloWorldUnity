-- Index                            int                              序号
-- bulletID                         int                              子弹ID
-- bulletSpeed                      int                              子弹飞行速度
-- effectID                         int                              子弹效果ID
-- effectRoute                      string                           子弹效果路径
-- FlyType                          int                              运动轨迹

return {
	[1] = {
		bulletID = 1,
		bulletSpeed = 15,
		effectID = 10,
		effectRoute = "Effect/ArrowFX_Fire",
		FlyType = 6,
	},
	[2] = {
		bulletID = 2,
		bulletSpeed = 15,
		effectID = 10,
		effectRoute = "Effect/CloudFlashFX",
		FlyType = 6,
	},
}
