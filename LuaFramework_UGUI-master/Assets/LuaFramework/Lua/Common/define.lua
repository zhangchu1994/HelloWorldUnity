--协议类型--
ProtocalType = {
	BINARY = 0,
	PB_LUA = 1,
	PBC = 2,
	SPROTO = 3,
}
--当前使用的协议类型--
TestProtoType = ProtocalType.BINARY;

Util = LuaFramework.Util;
AppConst = LuaFramework.AppConst;
LuaHelper = LuaFramework.LuaHelper;
ByteBuffer = LuaFramework.ByteBuffer;
MySceneManager = LuaFramework.MySceneManager;
JsonObject = SimpleJson.JsonObject;

resMgr = LuaHelper.GetResManager();
panelMgr = LuaHelper.GetPanelManager();
soundMgr = LuaHelper.GetSoundManager();
networkMgr = LuaHelper.GetWebManager();
sceneMgr = LuaHelper.GetSceneManager();

WWW = UnityEngine.WWW;
GameObject = UnityEngine.GameObject;
Application = UnityEngine.Application;
SceneManager = UnityEngine.SceneManagement.SceneManager;
Space = UnityEngine.Space;
NavMeshPathStatus = UnityEngine.AI.NavMeshPathStatus;
Resources = UnityEngine.Resources;
SkinnedMeshRenderer = UnityEngine.SkinnedMeshRenderer;
Transform = UnityEngine.Transform;
CombineInstance = UnityEngine.CombineInstance;
Material = UnityEngine.Material;
Shader = UnityEngine.Shader;
Texture2D = UnityEngine.Texture2D;
TextureFormat = UnityEngine.TextureFormat;
Mesh = UnityEngine.Mesh;
Text = UnityEngine.UI.Text;


-- Actor1 = GlobalGame.Actor1;
CameraControl = GlobalGame.CameraControl;

