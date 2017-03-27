using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GlobalGame 
{
	public class GlobalBattle 
	{
		public static GameObject GetCloseMonsterObj(GameObject selfObj,Actor.ActorType actorType)
		{
			List<GameObject> objList = null;
			List<Actor> actorList = null;

			if (actorType == Actor.ActorType.Actor) 
			{
				objList = BattleScene.Active.m_actorObjList;
				actorList = BattleScene.Active.m_actorList;
			} 
			else if (actorType == Actor.ActorType.Monster) 
			{
				objList = BattleScene.Active.m_monsterObjList;
				actorList = BattleScene.Active.m_monsterList;
			}

			GameObject closeMonster = null;
			float closeDis = 100000;
			for (int i = 0; i < objList.Count; i++) //m_actorList.Count
			{
				GameObject monsterObj = objList [i];
				Actor monster = actorList [i];
				if (monsterObj == null || monster.IsActorStatus (Actor.ActorStatus.Dead) == true)
					continue;

				float dis = Vector3.Distance (selfObj.transform.position,monsterObj.transform.position);

				if (dis < closeDis) 
				{
					closeMonster = monsterObj;
					closeDis = dis;
				}

			}
			return closeMonster;
		}









	}
}
