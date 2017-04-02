using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using SimpleJson;
using UniLinq;
using System.Xml;

namespace GlobalGame 
{
	public class MessageInfo{
		public string onMessage;//1ScriptFunction
		public object sender;
		public Message msg;
		public JsonObject jsonobj;
	}

	public class OpenInfo{
		public WebManager.OpenHandler onOpen;
		public object sender;
		public System.EventArgs e;
		public bool isOpen = false;
	}


	public class WebManager : Manager
	{
		public WebSocket ws;
		public delegate void MessageHandler(object sender, JsonObject data);
		public delegate void OpenHandler(object sender, System.EventArgs e);

		public Dictionary<string, string> _OnCmds = new Dictionary<string, string> ();//2ScriptFunction
		public Queue<MessageInfo> _toDoMessage = new Queue<MessageInfo> ();

		public delegate void voidDelegate();
		public delegate void goDelegate(GameObject go);
		public delegate void comDelegate(GameObject go, string com);//3ScriptObject


		public OpenInfo _toDoOpen = new OpenInfo();

		public XmlElement root = null;

		public MessageInfo cmdInfo = null;

		private bool isConnecting = false;
		private bool loadingCheck = false;
		private bool needReconnect = false;

		public bool reconnect = false;

		public bool disconnect = false;


		void OnApplicationQuit() {
		}

		void OnApplicationFocus(bool focusStatus) 
		{
			
		}

		// Use this for initialization
		void Start () {
			XmlDocument xmldoc = new XmlDocument();
			string localResUrl = "";
			#if UNITY_ANDROID 
			localResUrl = "jar:file://" + Application.dataPath + "!/assets/";
			#elif UNITY_IPHONE
			localResUrl = "file://" + Application.streamingAssetsPath + "/";
			#endif

			#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
			localResUrl = "file://" + Application.dataPath + "/StreamingAssets/";
			#elif UNITY_STANDALONE_OSX
			localResUrl = "file://" + Application.dataPath + "/Data/StreamingAssets/";
			#endif
			Connect(SelectUrl(), null, true);
		}

		public void InitProtocol(string text)
		{
			XmlDocument xmldoc = new XmlDocument();
			System.IO.TextReader tr = new System.IO.StringReader(text);
			xmldoc.Load(tr);
			root = xmldoc.DocumentElement;
		}

		// Update is called once per frame
		void Update () {	

			if (isConnecting) {
				return;		
			}
			if (_toDoOpen.isOpen) {
				_toDoOpen.onOpen(_toDoOpen.sender,_toDoOpen.e);
				_toDoOpen = new OpenInfo();
			}

			while (_toDoMessage.Count > 0) {
				MessageInfo todo = _toDoMessage.Dequeue();
//				((ScriptFunction)todo.onMessage).call(todo.jsonobj);//4
			}
		}

		public void ReconnectServer(){				
//			ScriptTable scriptPlayer = ResManager.Instance.GetStaticScript("ScriptPlayer");
//			var userInfoMsg = scriptPlayer.GetValue ("_PlayerLoginInfo").ObjectValue as JsonObject;
//			string selectId = userInfoMsg.TryGet ("secretId").ToString ();
//			string  ip = userInfoMsg.TryGet("ip").ToString();
//			string  port = userInfoMsg.TryGet("port").ToString();
//			ResManager.Instance.SwitchLogin((ResManager.voidDelegate)delegate{
//
//				Connect(ip + ":" + port, (OpenHandler)delegate(object sender, System.EventArgs e) {
//					scriptPlayer.GetValue("EnterGame").Call();
//					JsonObject json = new JsonObject ();
//					json.TrySet("secretId",selectId );
//					json.TrySet("language", 0);
//					CMD("CS_LoginMsg", json); 
//					reconnect = true;
//				}, false);							
//			});		
		}


		string MyEscapeURL (string url)
		{
			return WWW.EscapeURL(url).Replace("+","%20");
		}


		public void Send(string str){
			if (ws != null) {
				ws.Send (str);
			}else {

			}
		}


		public void CMD(string cmdName, JsonObject cmdjson)
		{
			if (ws == null)
				return;
			Message sendmsg = new Message();

			if(!sendmsg.Write(cmdName,cmdjson))
			{
				Debug.Log("CMD_Error "+cmdName+"," + cmdjson);
				return;
			}

			NeedLoading();
			if (ws.ReadyState == WebSocketState.Open) {

				ws.Send (sendmsg.buf.ToArray ());
			}
		}

		public void Connect(string url, object onOpen = null, bool isLogin = false)
		{

			string str = "ws://" + url + "//websocket";
			ws = new WebSocket (str);
			ws.OnMessage += OnMessages;
			ws.OnOpen += OnOpens;
			if(!isLogin)
			{
				ws.OnClose += OnClose;
				ws.OnError += OnError;
			}

			OpenHandler tmp = null;
			if(onOpen == null){
				tmp = null;
			}
			else if(onOpen.GetType() == typeof(OpenHandler))
			{
				tmp = (OpenHandler)onOpen;
			}
//			else if(onOpen.GetType() == typeof(ScriptFunction))//4
//			{
//				tmp = delegate(object sender, System.EventArgs e) 
//				{
//					((ScriptFunction)onOpen).call(sender, e);
//				};
//			}
			else
			{
				tmp = null;
			}
			_toDoOpen = new OpenInfo ();
			_toDoOpen.onOpen = tmp;

			isConnecting = true;
			NeedLoading ();

			ws.ConnectAsync ();
		}



		public void Disconnect(ushort code){
			CloseWebSocket (code);

			_toDoMessage.Clear ();

			_OnCmds.Clear ();
			_toDoOpen = new OpenInfo ();
		}

		public void CloseWebSocket(ushort code){
			if (ws != null && ws.ReadyState == WebSocketState.Open) {
				ws.Close(code);
			}
			ws = null;
		}

		public void ShowBackToLoginConfirm()
		{
		
		}


		public void NeedLoading()
		{
			loadingCheck = true;
		}

		public void LoadingCheck(){
			if (loadingCheck) {
				bool tmpBool = true;
				if(_toDoMessage.Count > 0){
					tmpBool = false;
				}
				if(_toDoOpen.onOpen != null){
					tmpBool = false;
				}

				if(_OnCmds.Count > 0){
					tmpBool = false;
				}

				if(tmpBool){
					loadingCheck = false;
				}
			}
		}

		public string SelectUrl()
		{
			return "120.92.63.207:8003";
//			if (StaticDataManager.Instance.Get ("url") == null) 
//			{
//				Debug.Log("StaticData Error: No Url Info!");		
//				return "";
//			}
//			JsonObject urlInfo = (JsonObject)StaticDataManager.Instance.Get ("url");
//			int cnt = urlInfo.Count;
//
//			int index = UnityEngine.Random.Range (0, cnt);
//
//			if (urlInfo.TryGet (index.ToString (), "url") != null) 
//			{
//				return urlInfo.TryGet (index.ToString (), "url").ToString();
//			}
//			return "";	
		}

		public void AddCmdHandler(string cmdName, string fun)//5
		{
			if(_OnCmds.ContainsKey(cmdName))
			{
				//Debug.Log("AddCmdHandler repeat "+cmdName);
			}
			_OnCmds[cmdName] = fun;
		}

		void OnMessages(object sender, MessageEventArgs e)
		{
			Message readmsg = new Message();
			readmsg.Read(e.RawData);
			if(_OnCmds.ContainsKey(readmsg.cmdName))
			{
				MessageInfo cmdInfo = new MessageInfo();
				cmdInfo.onMessage = _OnCmds[readmsg.cmdName];
				cmdInfo.sender = sender;
				cmdInfo.jsonobj = readmsg.mJson;
				Debug.Log ("__________________________ json = "+readmsg.mJson.ToString());
				cmdInfo.msg = readmsg;
				_toDoMessage.Enqueue(cmdInfo);

				JsonObject newDataJson =  cmdInfo.jsonobj;
				JsonObject diffData = new JsonObject ();
			}
		}

		void OnOpens(object sender, System.EventArgs e)
		{
			isConnecting = false;
			if (_toDoOpen.onOpen != null) 
			{
				_toDoOpen.isOpen = true;
				_toDoOpen.sender = sender;
				_toDoOpen.e = e;
			}
			//		ws.OnClose -= OnClose;
			//		ws.OnError -= OnError;
		}

		void OnClose(object sender, CloseEventArgs e)
		{
			loadingCheck = false;
			//UIManager.Instance.HideUI("WebLoadingUI");

			//void OnClose(object sender, System.EventArgs e){
			isConnecting = false;
			//		if (e.Code == 4001) {
			//			//Disconnect ();		
			//		} else if (e.Code != 1005) {
			//			needReconnect = true;
			//		}
			//needReconnect = true;
			//Connect ("115.29.208.202:8010");
			//ReconnectServer();
			disconnect = true;
		}

		void OnError(object sender, ErrorEventArgs e)
		{
			//SuperSocket.ClientEngine.ErrorEventArgs msg = (SuperSocket.ClientEngine.ErrorEventArgs)e;
		}
	}

	
}
