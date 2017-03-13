using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaFramework;
using LuaInterface;

namespace GlobalGame 
{
	public class UserData
	{
		public int m_Id;	
		public string m_Name;	
		public int m_Professional;
		public int m_Life;
		public int m_Attack;
		public int m_Defence;
		public int m_Speed;
		public int m_AttackSpeed;	
		public int m_AttackDis;
	}

	public class SkillData
	{
		public int m_Id;	
		public string m_Name;	
		public int m_SkillType1;	
		public int m_SkillType2;	
		public int m_Target;	
		public int m_Range;
		public float m_Radius;

		//非表字段
		public List<GameObject> m_TargetObjList = new List<GameObject>();
		public List<Actor> m_TargetActorList = new List<Actor>();
	}


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
		private static Dictionary<int, UserData> m_UserTable = null;
		private LuaManager luaManager;

		public void Init()
		{
			GameObject obj = GameObject.Find ("GameManager");
			luaManager = obj.GetComponent<LuaManager> ();

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
			Debug.Log ("Init_____________________________________");
		}

		private void ReadSkillTable()
		{
			object[] list = luaManager.GetLuaTable ("Skill.lua");
			m_SkillTable = new Dictionary<int, SkillData>();

			for (int i = 0; i < list.Length; i++)
			{
				LuaTable row = (LuaTable)list [i];
				SkillData data = new SkillData();
				Debugger.Log ("{0},{1},{2}",row ["Id"],row ["Name"],row ["SkillType1"]);

				data.m_Id = int.Parse (row ["Id"].ToString ());
				data.m_Name = row ["Name"].ToString ();
				data.m_SkillType1 = int.Parse(row ["SkillType1"].ToString ());
				data.m_SkillType2 = int.Parse(row ["SkillType2"].ToString ());
				data.m_Target = int.Parse(row ["Target"].ToString ());
				data.m_Range = int.Parse(row ["Range"].ToString ());
				data.m_Radius = float.Parse(row ["Radius"].ToString ());

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
			object[] list = luaManager.GetLuaTable ("User.lua");
			m_UserTable = new Dictionary<int, UserData>();

			for (int i = 0; i < list.Length; i++)
			{
				LuaTable row = (LuaTable)list [i];
				UserData data = new UserData();

				data.m_Id = int.Parse (row ["Id"].ToString());
				data.m_Name = (string)row ["Name"];	
				data.m_Professional = int.Parse (row ["Professional"].ToString());
				data.m_Life = int.Parse (row ["Life"].ToString());
				data.m_Attack =int.Parse (row ["Attack"].ToString());
				data.m_Defence = int.Parse (row ["Defence"].ToString());
				data.m_Speed = int.Parse (row ["Speed"].ToString());
				data.m_AttackSpeed = int.Parse (row ["AttackSpeed"].ToString());
				data.m_AttackDis = int.Parse (row ["AttackDis"].ToString());

				m_UserTable [data.m_Id] = data;
			}
		}

		static public UserData GetUserData(int Id)
		{
			if (m_UserTable != null && Id > 0)
			{

				UserData sd;
				if (m_UserTable.TryGetValue(Id, out sd))
				{
					return sd;
				}
				return null;

			}
			return null;
		}

	}
}
