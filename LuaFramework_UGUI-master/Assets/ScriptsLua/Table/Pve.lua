-- Index                            int                              序号
-- id                               int                              ID
-- name                             string                           关卡名称
-- tip                              string                           关卡描述
-- level                            int                              关卡等级
-- monsterPosition1                 string                           位置点1
-- monsterPosition2                 string                           位置点2
-- monsterPosition3                 string                           位置点3
-- monsterPosition4                 string                           位置点4
-- monsterPosition5                 string                           位置点5
-- monsterPosition6                 string                           位置点6
-- monsterPosition7                 string                           位置点7
-- monsterPosition8                 string                           位置点8
-- show                             string                           关卡奖励显示
-- drop                             string                           关卡奖励
-- exp                              float                            每分钟获取经验
-- gold                             float                            每分钟获取金币
-- monster                          int                              普通怪物出现个数
-- monsterID                        string                           普通怪物ID
-- monsterLevel                     int                              普通怪物等级
-- bossOdds                         int                              击杀普通怪物数量出现boss
-- killMonsterNum                   int                              BOSS怪物出现个数
-- bossID                           string                           BOSS怪物ID
-- bosslevel                        int                              BOSS怪物等级

return {
	[1] = {
		id = 1,
		name = "第一关",
		tip = "关卡1",
		level = 1,
		monsterPosition1 = "-16,3,3",
		monsterPosition2 = "10,0,8",
		monsterPosition3 = "14,0,29",
		monsterPosition4 = "5,3,54",
		monsterPosition5 = "-40,-6,86",
		monsterPosition6 = "5,3,54",
		monsterPosition7 = "14,0,29",
		monsterPosition8 = "10,0,8",
		show = "1,2,3,4",
		drop = "200,240",
		exp = 1.2,
		gold = 1.2,
		monster = 4,
		monsterID = "1,2,3",
		monsterLevel = 1,
		bossOdds = 10,
		killMonsterNum = 1,
		bossID = "1,2,3",
		bosslevel = 1,
	},
}
