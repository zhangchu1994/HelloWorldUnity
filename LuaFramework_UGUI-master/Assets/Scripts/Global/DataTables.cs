using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaFramework;
using LuaInterface;

namespace GlobalGame 
{
	#region Data
	public class ActorData
	{
		public int m_Id;	
		public string m_Name;
		public string m_Tip;
		public int m_Quality;
		public string m_Model;//模型位置
		public string m_Img;
		public int m_Professional;
		public float m_MaxHp;
		public float m_Attack;
		public float m_Defence;
		public float m_Range;
		public int m_Speed;//移动速度
		public int m_AttackSpeed;//普通攻击CD
		public int m_AttackDis;//攻击距离
		public string m_SkillIds;
		public float m_Crit;// 暴击
		public float m_CritHurt;//暴击伤害
		public float m_Dodge;//闪避
		public float m_InjuryFree;//免伤
		public float m_AddHp;//生命成长
		public float m_AddAttack;//攻击成长
		public float m_AddRange;//攻击距离成长
		public float m_AddAttackSpeed;//攻击速度成长
		public float m_AddSpeed;//移动速度成长
		public float m_AddDiscrit;//攻击距离成长
		public float m_AddCrit; //暴击成长
		public float m_AddCritHurt;//暴击伤害成长
		public float m_AddDodge;//闪避成长
		public float m_AddInjuryFree;//免伤成长
		public float m_SynthChipNum;//合成需要碎片数量
		public float m_GiveExp;//提供经验
		public float m_ReturnDna;//返还DNA

		//非表字段
		public float m_CurCd = 0;
		public float m_CurHp = 0;
	}

	public class SkillData
	{
		public int m_Id;
		public string m_Name;
		public string m_SkillDescription;
		public string m_SkillIcon;
		public int m_SkillLevel;
		public int m_SkillAttckType;//技能攻击类型
		public int m_SkillType;//技能分类
		public int m_Priority;//施放优先级
		public float m_CdTime;//冷却时间
		public float m_InitialCDTime;//触发几率
		public int m_MoveType;//位移类型
		public int m_MoveDistance;//位移距离
		public int m_Animation_Move;//位移动作
		public int m_Animation_AfterMove;//位移后施法动作
		public int m_MoveSpeed;//位移速度
		public int m_TargetType;//影响目标类型
		public int m_TargetChooseType;//目标选择方式
		public int m_TargetChooseConitionValue;//目标选择条件数值
		public int m_TargetEffectType;//影响范围类型
		public float m_TargetEffectArea;//影响范围
		public int m_HasBullet;//是否有子弹
		public int m_BulletId;//技能子弹
		public int m_EffectID;//子弹效果ID
		public int m_EffectTarget;//技能效果目标
		public int m_Effecttype;//技能效果类型
		public int m_EffectValue;//技能效果数值
		public int m_RelativeAttributeID;//技能效果相关属性类型
		public int m_RelativeAttributeFactor;//技能效果相关属性系数
		public int m_EffectAttributeSource;//技能效果相关属性来源
		public float m_Delay;//延迟时间
		public string m_EffectPrefab;//技能效果特效
		public int m_EffectFloatText;//技能效果提示


		//非表字段
		public List<GameObject> m_TargetObjList = new List<GameObject>();
		public List<Actor> m_TargetActorList = new List<Actor>();
	}

	public class BulletData
	{
		public int m_BulletID;	
		public int m_BulletSpeed;	
		public string m_EffectRoute;
		public int m_FlyType;
	}
														

	public class PveData
	{
		public int	m_Id;	
		public string m_Name;	
		public string m_Tip;	
		public int m_level;
		public List<string> m_MonsterPosition = new List<string>();
		public string m_Show;	
		public string m_Drop;	
		public float m_Exp;	
		public float m_Gold;	
		public int m_Monster;	
		public string m_MonsterID;	
		public int m_MonsterLevel;	
		public int m_BossOdds;	
		public int m_KillMonsterNum;	
		public string m_BossID;	
		public int m_Bosslevel;
	}

	#endregion

	public class DataTables 
	{

		private static DataTables instance=null;

		private DataTables()
		{
			
		}

		public static DataTables Instance
		{
			get
			{
				if (instance==null)
				{
					instance = new DataTables();
				}
				return instance;
			}
		}

		private static Dictionary<int, SkillData> m_SkillTable = null;
		private static Dictionary<int, ActorData> m_UserTable = null;
		private static Dictionary<int, ActorData> m_MonsterTable = null;
		private static Dictionary<int, BulletData> m_BulletTable = null;
		private static Dictionary<int, PveData> m_PveTable = null;

		private LuaManager m_luaManager;

		public void Init()
		{
			GameObject obj = GameObject.Find ("GameManager");
			m_luaManager = obj.GetComponent<LuaManager> ();

			if (m_SkillTable == null)
			{
				ReadSkillTable ();
//				yield return Utility.CoroutineContainer.StartCoroutine(ReadSkillTable());
			}
			if (m_UserTable == null)
			{
				ReadUserTable ();
//				yield return Utility.CoroutineContainer.StartCoroutine(ReadUserTable());
			}
			if (m_MonsterTable == null) 
			{
				ReadMonsterTable ();
			}

			if (m_BulletTable == null)
			{
				ReadBulletTable ();
//				yield return Utility.CoroutineContainer.StartCoroutine(ReadUserTable());
			}

			if (m_PveTable == null)
			{
				ReadPveTable ();
			}

//			Debug.Log ("Init_____________________________________");
		}

		private void ReadSkillTable()
		{
			object[] list = m_luaManager.GetLuaTable ("Skill.lua");
			m_SkillTable = new Dictionary<int, SkillData>();

			for (int i = 0; i < list.Length; i++)
			{
				LuaTable row = (LuaTable)list [i];
				SkillData data = new SkillData();

				data.m_Id = int.Parse (row ["skillID"].ToString ());
				data.m_Name = row ["skillName"].ToString ();
				data.m_SkillDescription = row ["skillDescription"].ToString ();
				data.m_SkillIcon = row ["skillIcon"].ToString ();
				data.m_SkillLevel = int.Parse(row ["skillLevel"].ToString ());
				data.m_SkillAttckType = int.Parse(row ["SkillAttckType"].ToString ());
				data.m_SkillType = int.Parse(row ["skillType"].ToString ());
				data.m_Priority = int.Parse(row ["priority"].ToString ());
				data.m_CdTime = float.Parse(row ["cdTime"].ToString ());
				data.m_InitialCDTime = float.Parse(row ["initialCDTime"].ToString ());
				data.m_MoveDistance = int.Parse(row ["moveDistance"].ToString ());
				data.m_Animation_Move = int.Parse(row ["animation_Move"].ToString ());
				data.m_Animation_AfterMove = int.Parse(row ["animation_AfterMove"].ToString ());
				data.m_MoveSpeed = int.Parse(row ["moveSpeed"].ToString ());
				data.m_MoveType = int.Parse(row ["moveType"].ToString ());
				data.m_TargetType = int.Parse(row ["targetType"].ToString ());
				data.m_TargetChooseType = int.Parse(row ["targetChooseType"].ToString ());
				data.m_TargetChooseConitionValue = int.Parse(row ["targetChooseConitionValue"].ToString ());
				data.m_TargetEffectType = int.Parse(row ["targetEffectType"].ToString ());
				data.m_TargetEffectArea = int.Parse(row ["targetEffectArea"].ToString ());
				data.m_HasBullet = int.Parse(row ["hasBullet"].ToString ());
				data.m_BulletId = int.Parse(row ["bulletId"].ToString ());
				data.m_EffectTarget = int.Parse(row ["effectTarget"].ToString ());
				data.m_Effecttype = int.Parse(row ["effecttype"].ToString ());
				data.m_EffectValue = int.Parse(row ["effectValue"].ToString ());
				data.m_RelativeAttributeID = int.Parse(row ["relativeAttributeID"].ToString ());
				data.m_RelativeAttributeFactor = int.Parse(row ["relativeAttributeFactor"].ToString ());
				data.m_EffectAttributeSource = int.Parse(row ["effectAttributeSource"].ToString ());
				data.m_Delay = float.Parse(row ["delay"].ToString ());
				data.m_EffectPrefab = row ["effectPrefab"].ToString ();
				data.m_EffectFloatText = int.Parse(row ["effectFloatText"].ToString ());

				m_SkillTable[data.m_Id] = data;
			}
		}


		static public SkillData GetSkillData(int Id)
		{
			if (m_SkillTable != null && Id > 0)
			{

				SkillData sd;
				if (m_SkillTable.TryGetValue(Id, out sd))
				{
					return sd;
				}
				return null;

			}
			return null;
		}

		private void ReadUserTable()
		{
			object[] list = m_luaManager.GetLuaTable ("Actor.lua");
			m_UserTable = new Dictionary<int, ActorData>();

			for (int i = 0; i < list.Length; i++)
			{
				LuaTable row = (LuaTable)list [i];
				ActorData data = new ActorData();

				data.m_Id = int.Parse (row ["id"].ToString ());
				data.m_Name = (string)row ["name"];
				data.m_Tip = (string)row ["tip"];
				data.m_Quality = int.Parse (row ["quality"].ToString());
				data.m_Model = (string)row ["animation"];
				data.m_Img = (string)row ["img"];	
				data.m_MaxHp = int.Parse (row ["hp"].ToString());
				data.m_Attack =int.Parse (row ["attack"].ToString());
				data.m_Range = float.Parse(row ["range"].ToString());
				data.m_Defence = int.Parse (row ["defence"].ToString());
				data.m_Speed = int.Parse (row ["speed"].ToString());
				data.m_AttackSpeed = int.Parse (row ["attackSpeed"].ToString());
				data.m_AttackDis = int.Parse (row ["range"].ToString());
				data.m_SkillIds = row ["skillid"].ToString ();
				data.m_Crit = float.Parse (row ["crit"].ToString());
				data.m_Dodge = float.Parse (row ["dodge"].ToString());
				data.m_InjuryFree = float.Parse (row ["injury_free"].ToString());
				data.m_AddHp = float.Parse (row ["addhp"].ToString());
				data.m_AddAttack = float.Parse (row ["addattack"].ToString());
				data.m_AddRange = float.Parse (row ["addrange"].ToString());
				data.m_AddAttackSpeed = float.Parse (row ["addattackSpeed"].ToString());
				data.m_AddSpeed = float.Parse (row ["addspeed"].ToString());
				data.m_AddDiscrit = float.Parse (row ["adddiscrit"].ToString());
				data.m_AddCrit = float.Parse (row ["addcrit"].ToString());
				data.m_AddCritHurt = float.Parse (row ["addcrithurt"].ToString());
				data.m_AddDodge = float.Parse (row ["adddodge"].ToString());
				data.m_AddInjuryFree = float.Parse (row ["addinjury_free"].ToString());
				data.m_SynthChipNum = float.Parse (row ["synthChipNum"].ToString());
				data.m_GiveExp = float.Parse (row ["giveexp"].ToString());
				data.m_ReturnDna = float.Parse (row ["returndna"].ToString());

				data.m_CurHp = data.m_MaxHp;
				m_UserTable [data.m_Id] = data;
			}
		}

		static public ActorData GetUserData(int Id)
		{
			if (m_UserTable != null && Id > 0)
			{

				ActorData sd;
				if (m_UserTable.TryGetValue(Id, out sd))
				{
					return sd;
				}
				return null;

			}
			return null;
		}


		private void ReadMonsterTable()
		{
			object[] list = m_luaManager.GetLuaTable ("Monster.lua");
			m_MonsterTable = new Dictionary<int, ActorData>();

			for (int i = 0; i < list.Length; i++)
			{
				LuaTable row = (LuaTable)list [i];
				ActorData data = new ActorData();

				data.m_Id = int.Parse (row ["id"].ToString ());
				data.m_Name = (string)row ["name"];
				data.m_Tip = (string)row ["tip"];
				data.m_Quality = int.Parse (row ["quality"].ToString());
				data.m_Model = (string)row ["animation"];
				data.m_Img = (string)row ["img"];	
				data.m_MaxHp = int.Parse (row ["hp"].ToString());
				data.m_Attack =int.Parse (row ["attack"].ToString());
				data.m_Range = float.Parse(row ["range"].ToString());
				data.m_Defence = int.Parse (row ["defence"].ToString());
				data.m_Speed = int.Parse (row ["speed"].ToString());
				data.m_AttackSpeed = int.Parse (row ["attackSpeed"].ToString());
				data.m_AttackDis = int.Parse (row ["range"].ToString());
				data.m_SkillIds = row ["skillid"].ToString ();
				data.m_Crit = float.Parse (row ["crit"].ToString());
				data.m_Dodge = float.Parse (row ["dodge"].ToString());
				data.m_InjuryFree = float.Parse (row ["injury_free"].ToString());
				data.m_AddHp = float.Parse (row ["addhp"].ToString());
				data.m_AddAttack = float.Parse (row ["addattack"].ToString());
				data.m_AddRange = float.Parse (row ["addrange"].ToString());
				data.m_AddAttackSpeed = float.Parse (row ["addattackSpeed"].ToString());
				data.m_AddSpeed = float.Parse (row ["addspeed"].ToString());
				data.m_AddDiscrit = float.Parse (row ["adddiscrit"].ToString());
				data.m_AddCrit = float.Parse (row ["addcrit"].ToString());
				data.m_AddCritHurt = float.Parse (row ["addcrithurt"].ToString());
				data.m_AddDodge = float.Parse (row ["adddodge"].ToString());
				data.m_AddInjuryFree = float.Parse (row ["addinjury_free"].ToString());
//				data.m_SynthChipNum = float.Parse (row ["synthChipNum"].ToString());
//				data.m_GiveExp = float.Parse (row ["giveexp"].ToString());
//				data.m_ReturnDna = float.Parse (row ["returndna"].ToString());

				data.m_CurHp = data.m_MaxHp;
				m_MonsterTable [data.m_Id] = data;
			}
		}


		static public ActorData GetMonsterData(int Id)
		{
			if (m_MonsterTable != null && Id > 0)
			{

				ActorData sd;
				if (m_MonsterTable.TryGetValue(Id, out sd))
				{
					return sd;
				}
				return null;

			}
			return null;
		}

		private void ReadBulletTable()
		{
			object[] list = m_luaManager.GetLuaTable ("SkillBullet.lua");
			m_BulletTable = new Dictionary<int, BulletData> ();

			for (int i = 0; i < list.Length; i++) 
			{
				LuaTable row = (LuaTable)list [i];
				BulletData data = new BulletData ();
				data.m_BulletID = int.Parse (row ["bulletID"].ToString());
				data.m_BulletSpeed = int.Parse (row ["bulletSpeed"].ToString());
//				data.m_EffectID = int.Parse (row ["effectID"].ToString());
				data.m_EffectRoute = row ["effectRoute"].ToString();
				data.m_FlyType = int.Parse (row ["FlyType"].ToString());		
				m_BulletTable [data.m_BulletID] = data;
			}
		}

		static public BulletData GetBulletData(int Id)
		{
			if (m_BulletTable != null && Id > 0)
			{

				BulletData sd;
				if (m_BulletTable.TryGetValue(Id, out sd))
				{
					return sd;
				}
				return null;

			}
			return null;
		}

		private void ReadPveTable()
		{
			object[] list = m_luaManager.GetLuaTable ("Pve.lua");
			m_PveTable = new Dictionary<int, PveData> ();

			for (int i = 0; i < list.Length; i++) 
			{
				LuaTable row = (LuaTable)list [i];
				PveData data = new PveData ();

				data.m_Id = int.Parse (row ["id"].ToString());	
				data.m_Name = row ["name"].ToString();	
				data.m_Tip = row ["tip"].ToString();	
				data.m_level = int.Parse (row ["level"].ToString());
				data.m_MonsterPosition.Add(row ["monsterPosition1"].ToString());
				data.m_MonsterPosition.Add(row ["monsterPosition2"].ToString());
				data.m_MonsterPosition.Add(row ["monsterPosition3"].ToString());
				data.m_MonsterPosition.Add(row ["monsterPosition4"].ToString());
				data.m_MonsterPosition.Add(row ["monsterPosition5"].ToString());
				data.m_MonsterPosition.Add(row ["monsterPosition6"].ToString());
				data.m_MonsterPosition.Add(row ["monsterPosition7"].ToString());
				data.m_MonsterPosition.Add(row ["monsterPosition8"].ToString());
				data.m_Show = row ["show"].ToString();	
				data.m_Drop = row ["drop"].ToString();	
				data.m_Exp = float.Parse (row ["exp"].ToString());	
				data.m_Gold = float.Parse (row ["gold"].ToString());	
				data.m_Monster = int.Parse (row ["monster"].ToString());	
				data.m_MonsterID = row ["monsterID"].ToString();	
				data.m_MonsterLevel = int.Parse (row ["monsterLevel"].ToString());	
				data.m_BossOdds = int.Parse (row ["bossOdds"].ToString());	
				data.m_KillMonsterNum = int.Parse (row ["killMonsterNum"].ToString());	
				data.m_BossID = row ["bossID"].ToString();	
				data.m_Bosslevel = int.Parse (row ["bosslevel"].ToString());

				m_PveTable [data.m_Id] = data;
			}
		}

		static public PveData GetPveData(int Id)
		{
			if (m_PveTable != null && Id > 0)
			{

				PveData sd;
				if (m_PveTable.TryGetValue(Id, out sd))
				{
					return sd;
				}
				return null;
			}
			return null;
		}
	}
}
