-- Index                            int                              序号
-- Id                               int                              技能ID
-- Name                             string                           技能名字
-- SkillType1                       int                              技能类型1 (0.近战  1.远程)
-- SkillType2                       int                              技能类型2 (近战 0.横扫 1.劈砍 2.跳跃 3.旋转 4.蓄力 远程 0.直线 1.抛物线 2.螺旋 3.空中打击)
-- Target                           int                              目标 (0.最近目标 1.最远目标 2.血量最多 3.血量最少 4.死亡时释放 5.选择敌方职业 6.进入战斗 7.自身血量低于一定百分比 8.敌方攻击最高)
-- Range                            int                              范围 (0.单体 1.直线 2.扇形 3.圆形 4.矩形)
-- Radius                           float                            半径

return {
	[1] = {
		Id = 1,
		Name = "横扫1",
		SkillType1 = 0,
		SkillType2 = 1,
		Target = 0,
		Range = 0,
		Radius = 3,
		EffectPath = "",
		Scale = 1,
	},
	[2] = {
		Id = 2,
		Name = "横扫2",
		SkillType1 = 0,
		SkillType2 = 1,
		Target = 0,
		Range = 0,
		Radius = 3,
		EffectPath = "",
		Scale = 1,
	},
	[3] = {
		Id = 3,
		Name = "直线1",
		SkillType1 = 1,
		SkillType2 = 6,
		Target = 0,
		Range = 0,
		Radius = 8,
		EffectPath = "Effect/ArrowFX_Fire",
		Scale = 1.5,
	},
	[4] = {
		Id = 4,
		Name = "直线2",
		SkillType1 = 1,
		SkillType2 = 6,
		Target = 0,
		Range = 0,
		Radius = 8,
		EffectPath = "Effect/CloudFlashFX",
		Scale = 2.5,
	},
	[5] = {
		Id = 5,
		Name = "法术1",
		SkillType1 = 2,
		SkillType2 = -1,
		Target = 0,
		Range = 0,
		Radius = 8,
		EffectPath = "Effect/ArrowFX_FireRain",
		Scale = 2,
	},
	[6] = {
		Id = 6,
		Name = "法术2",
		SkillType1 = 2,
		SkillType2 = -1,
		Target = 0,
		Range = 0,
		Radius = 8,
		EffectPath = "Effect/ChargeFX_Wind01",
		Scale = 2,
	},
}
