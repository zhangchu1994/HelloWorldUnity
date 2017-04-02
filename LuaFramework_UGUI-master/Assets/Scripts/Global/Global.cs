using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LuaFramework;
using SimpleJson;

namespace GlobalGame 
{
	public class Global 
	{
		public static string ABInfoKey_Type = "ABType";
		public static string ABInfoKey_ABName = "ABName";
		public static string ABInfoKey_AssetName = "AssetName";
		public static string ABInfoKey_Index = "Index";

		public static string TagName_Enemy = "Enemy";
		public static string TagName_Ground = "Ground";
		public static string TagName_Actor = "Actor";

		public static Dictionary<string,string> CreateABInfo(string ABName,string AssetName,AssetType Type,int Index)
		{
			Dictionary<string,string> info1 = new Dictionary<string,string>();//abFullName, assetName
			int type = (int)Type;
			info1.Add(ABInfoKey_Type,type.ToString());
			info1.Add(ABInfoKey_ABName,ABName);
			info1.Add(ABInfoKey_AssetName,AssetName);
			info1.Add(ABInfoKey_Index,Index.ToString());
			return info1;
		}

		public static string GetAssetName(string path)
		{
			string[] names = path.Split ('/');
			return names[names.Length-1];
		}

		public static string getDicStrVaule(Dictionary<string,string> info,string Key)
		{
			string vaule = "";
			info.TryGetValue (Key, out vaule);
			return vaule;
		}

		public static GameObject getDicObjVaule(Dictionary<string,GameObject> info,string Key)
		{
			GameObject vaule = null;
			info.TryGetValue (Key, out vaule);
			return vaule;
		}

//		public static string ActorName = "Ian1970";

		public enum BattleAnimationType
		{
			Stand = 0,
			Run = 1,
			Hurt = 2,
			Attack = 3,
			Dead = 10,
		}

		public static string Stand = "stand";
		public static string Run = "run";
		public static string Hurt = "hurt";
		public static string Attack1 = "attack1";
//		public static string Stand = "4",
//		public static string Stand = "5",
//		public static string Stand = "6",
//		public static string Stand = "7",
//		public static string Stand = "8",
//		public static string Stand = "9",
		public static string Die = "die";

		public static ArrayList m_ActorNameList = new ArrayList(){"Actor1","Actor2","Actor3","Actor4","Actor5"};
		public static ArrayList m_MonsterNameList = new ArrayList(){"Monster1","Monster2","Monster3","Monster4","Monster5"};
		public static ArrayList m_AnimationNameList = new ArrayList(){Stand,Run,Hurt,Attack1,"4","5","6","7","8","9",Die};

		public static List<BattleAnimationType> GetAnimRestoreList(BattleAnimationType argType)
		{
			List<BattleAnimationType> animationList = new List<BattleAnimationType> ();
			animationList.Add(argType);
			animationList.Add(BattleAnimationType.Stand);

			return animationList;
		}

		public static string GetActorNmae(int index)
		{
			return (string)m_ActorNameList[index];
		}

		public static string GetMonsterNmae(int index)
		{
			return (string)m_MonsterNameList[index];
		}

		public static string GetAnimation(BattleAnimationType argType)
		{
			return (string)m_AnimationNameList[(int)argType];
		}

		//缩放粒子及模型
		public static void ChangeParticleScale(GameObject Obj,float SetScale)
		{
			if (SetScale > 0) 
			{
				ParticleSystem[] systems = Obj.GetComponentsInChildren<ParticleSystem> ();

				for (int i = 0; i < systems.Length; i++ )
				{
					ParticleSystem system = systems [i];
					system.startSize = system.startSize * SetScale;
					system.startSpeed = system.startSpeed * SetScale;
				}

				Obj.transform.localScale *= SetScale;
			}
		}

		public static List<int> GetRandomNumber(int Range,int count)
		{
			List<int> list = new List<int>();
			while(list.Count < count)
			{
				int num = UnityEngine.Random.Range( 0,Range);
				if (list.Contains (num) == false)
					list.Add (num);
			}
			return list;
		}

		public static bool IsRateTrigger(float rate)
		{
			bool isRate = true;
			int rand = UnityEngine.Random.Range( 0,100);
			if (rand <= rate)
				isRate = false;
			return isRate;
		}

		public static int[] StringArrayToIntArray(string[] strArray)
		{
			int[] intArray  = new int [strArray.Length] ;
			for (int i = 0; i < strArray.Length; i++) 
			{
				intArray [i] = int.Parse (strArray [i]);
			}
			return intArray;
		}

//		public static T Clone<T>(T source)
//		{
//			var serialized = JsonConvert.SerializeObject(source);
//			return JsonConvert.DeserializeObject<T>(serialized);
//		}

		public static void BattleLog(Actor actor,string argString)
		{
			if (actor != null && actor.name == "Actor1")
				Debug.Log (" ___________ "+argString);
		}

		public static bool isMobile()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
				return true;
			else
				return false;
		}

		//------------------------------------------------------
		public static JsonObject StrToJson(string str){
			object a;
			SimpleJson.SimpleJson.TryDeserializeObject (str, out a);
			if (a != null) {
				return (JsonObject)a;	
			}

			return null;
		}
	}
}
