require "Common/define"
require "Controller/PromptCtrl"
require "Controller/MessageCtrl"
require "Login"
require "MainCityScene"
require "Actor"
require "FirstBattleScene"
require "MainCityUIManager"
require "BattleSceneUIManager"
require "BossPanel"
require "ShopPanel"


CtrlManager = {};
local this = CtrlManager;
local ctrlList = {};	--控制器列表--

CtrlNames = 
{
	Prompt = "PromptCtrl",
	Message = "MessageCtrl",
	Login = "Login",
	MainCityScene = "MainCityScene",
	Actor = "Actor",
	FirstBattleScene = "FirstBattleScene",
	MainCityUIManager = "MainCityUIManager",
	BattleSceneUIManager = "BattleSceneUIManager",
	BossPanel = "BossPanel",
	ShopPanel = "ShopPanel",
}

function CtrlManager.Init()
	logWarn("CtrlManager.Init----->>>");
	ctrlList[CtrlNames.Prompt] = PromptCtrl.New();
	ctrlList[CtrlNames.Message] = MessageCtrl.New();
	ctrlList[CtrlNames.Login] = Login.New();
	ctrlList[CtrlNames.MainCityScene] = MainCityScene.New();
	ctrlList[CtrlNames.FirstBattleScene] = FirstBattleScene.New();
	ctrlList[CtrlNames.MainCityUIManager] = MainCityUIManager.New();
	ctrlList[CtrlNames.BossPanel] = BossPanel.New();
	ctrlList[CtrlNames.ShopPanel] = ShopPanel.New();
	return this;
end

--添加控制器--
function CtrlManager.AddCtrl(ctrlName, ctrlObj)
	ctrlList[ctrlName] = ctrlObj;
end

--获取控制器--
function CtrlManager.GetCtrl(ctrlName)
	return ctrlList[ctrlName];
end

--移除控制器--
function CtrlManager.RemoveCtrl(ctrlName)
	ctrlList[ctrlName] = nil;
end

--关闭控制器--
function CtrlManager.Close()
	logWarn('CtrlManager.Close---->>>');
end