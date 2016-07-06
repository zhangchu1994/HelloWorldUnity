using UnityEngine;
using System.Collections;

public class MenuPickInput : MonoBehaviour {
	
	public PlayerInput.InputMethod currentInput;
	

	public TextMesh Tell;
	public TextMesh Tell2;

	public int Operation;
	
	[HideInInspector]
	public int NumberOfPlayers=1;
	
	PlayerInput.InputMethod myInput;
	
	public int PlayerNumber;
	
	public bool Receiver=false;
	
	//Esta função será chamada por outras classes que irão usa-la para mudar o método de input.
	public void SetInput(PlayerInput.InputMethod newIM)
	{		
		//Mudar as strings de input com base no método recebido.
		myInput=newIM;
		
		switch (myInput)
		{
			case PlayerInput.InputMethod.SingleplayerKeyboard:
					Tell.text="Singleplayer Keyboard";
					Tell2.text="ASDW to Move, Arrow Keys to Shoot";
				break;
			case PlayerInput.InputMethod.SingleplayerMouse:
					Tell.text="Singleplayer Keyboard&Mouse";
					Tell2.text="ASDW or ArrowKeys to Move, mouse to shoot";
				break;
			case PlayerInput.InputMethod.mpKeyboard1:
					Tell.text="ASDW Local Coop Keyboard";
					Tell2.text="ASDW to Move, UHJK to Shoot";
				break;
			case PlayerInput.InputMethod.mpKeyboard2:
					Tell.text="Arrow keys Local Coop Keyboard";
					Tell2.text="Arrows to Move, Numpad 4568 to Shoot";
				break;
			case PlayerInput.InputMethod.Gamepad1:
					Tell.text="GamePad1";
					Tell2.text="";
				break;
			case PlayerInput.InputMethod.Gamepad2:
					Tell.text="GamePad2";
					Tell2.text="";
				break;
			case PlayerInput.InputMethod.Gamepad3:
					Tell.text="GamePad3";
					Tell2.text="";
				break;
			case PlayerInput.InputMethod.Gamepad4:
					Tell.text="GamePad4";
					Tell2.text="";
				break;
		}
		
		GameManager.instance.playerInfo[PlayerNumber].pInput=myInput;
	}
	
	public void CheckInput(int operation)
	{		
		//Mudar as strings de input com base no método recebido.
		NumberOfPlayers=operation;
		switch (operation)
		{
			case 1:
					SetInput(PlayerInput.InputMethod.SingleplayerKeyboard);
				break;
			case 2:
					SetInput(PlayerInput.InputMethod.SingleplayerMouse);
				break;
			case 3:
					SetInput(PlayerInput.InputMethod.mpKeyboard1);
				break;
			case 4:
					SetInput(PlayerInput.InputMethod.mpKeyboard2);
				break;
			case 5:
					SetInput(PlayerInput.InputMethod.Gamepad1);
				break;
			case 6:
					SetInput(PlayerInput.InputMethod.Gamepad2);
				break;
			case 7:
					SetInput(PlayerInput.InputMethod.Gamepad3);
				break;
			case 8:
					SetInput(PlayerInput.InputMethod.Gamepad4);
				break;
		}
	}
	
	public void ReceiveUpdate (int operation) {
		//Debug.Log("Operation "+operation+" NumbeROfPla" + NumberOfPlayers );
		if(NumberOfPlayers==8&&operation==1)
			return;
		if(NumberOfPlayers==1&&operation==0)
			return;
		
		if(operation==0) {
			NumberOfPlayers--;
			StartCoroutine(transform.Shake(0.02f,0.3f));
		}
		
		if(operation==1) {
			NumberOfPlayers++;
			StartCoroutine(transform.Shake(0.02f,0.3f));
		}
		
		//Tell.text=""+NumberOfPlayers;
		CheckInput(NumberOfPlayers);
	}
	
	void ButtonClick(int Operation) {
		//Debug.Log("Operation "+Operation);
		Tell.GetComponent<MenuPickInput>().ReceiveUpdate(Operation);
	}
}
