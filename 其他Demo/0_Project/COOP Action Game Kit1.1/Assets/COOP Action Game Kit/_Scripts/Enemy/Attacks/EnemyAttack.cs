using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	[HideInInspector]
	public EnemyAI eAI;
	//Intensidade que a tela deve chacoalhar quando atirarmos com esta arma
	public float ShakeIntensity=0f;
	public float WaitBeforeAttack=0.0f;
	public float WaitAfterAttack=0.0f;
	public virtual void Start() {
	}
	
	public virtual void Attack(Vector3 pos) {
		eAI.attacking=true;

		eAI.attacking=false;
	}
}
