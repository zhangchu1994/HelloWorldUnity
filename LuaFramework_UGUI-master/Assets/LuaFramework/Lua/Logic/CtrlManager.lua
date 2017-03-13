require "Common/define"
-- require "Controller/PromptCtrl"
-- require "Controller/MessageCtrl"
require "UI/Login"
require "UI/MainCityUIManager"
require "UI/BattleSceneUIManager"
require "UI/BossPanel"
require "UI/ShopPanel"
require "UI/BigWorld"


CtrlManager = {};
local this = CtrlManager;
local ctrlList = {};	--控制器列表--

function CtrlManager.Init()
	logWarn("CtrlManager.Init----->>>");
	-- ctrlList["PromptCtrl"] = PromptCtrl.New();
	-- ctrlList["MessageCtrl"] = MessageCtrl.New();
	ctrlList["Login"] = Login.New();
	-- ctrlList["MainCityScene"] = MainCityScene.New();
	-- ctrlList["FirstBattleScene"] = FirstBattleScene.New();
	ctrlList["MainCityUIManager"] = MainCityUIManager.New();
	ctrlList["BossPanel"] = BossPanel.New();
	ctrlList["ShopPanel"] = ShopPanel.New();
	ctrlList["BigWorld"] = BigWorld.New();
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