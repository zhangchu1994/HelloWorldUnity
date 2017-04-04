require "Logic/LuaClass"
require "Logic/CtrlManager"
require "Common/functions"

--管理器--
Game = {};
local this = Game;

local game; 
local transform;
local gameObject;
local WWW = UnityEngine.WWW;

function Game.OnInitOK()
    CtrlManager.Init();
 
    local ctrl = CtrlManager.GetCtrl("Login");
    local scene = SceneManager.GetActiveScene();
    if ctrl ~= nil and AppConst.ExampleMode == 1 and scene.name == "Login" then

        local transform =  GameObject.Find("LoginCanvas").transform;
        local InfoPanel = transform:FindChild("Bg/InfoPanel").gameObject;
        InfoPanel.SetActive(InfoPanel,false);

        panelMgr:CreatePanel('UI/Login/LoginPanel', 'UICamera/LoginCanvas/Bg','Login', Login.OnCreate);
    end
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end
