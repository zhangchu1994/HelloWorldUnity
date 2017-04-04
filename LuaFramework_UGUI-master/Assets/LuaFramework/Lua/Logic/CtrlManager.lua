require "Common/define"
-- require "Controller/PromptCtrl"
-- require "Controller/MessageCtrl"
require "UI/Register"
require "UI/Login"
require "UI/MainCityUIManager"
require "UI/BattleSceneUIManager"
require "UI/BossPanel"
require "UI/ShopPanel"
require "UI/BigWorld"
require "UI/SystemMainPanel"
require "UI/RoleMainPanel"
require "UI/RoleMeridiansPanel"
require "UI/RolePanel"
require "UI/RoleReLivePanel"
require "UI/RoleWingsPanel"
require "UI/SkillBreakPanel"
require "UI/SkillMainPanel"
require "UI/SkillPanel"
require "UI/SkillSecretPanel"


CtrlManager = {};
local this = CtrlManager;
local ctrlList = {};	--控制器列表--

function CtrlManager.Init()
	-- logWarn("CtrlManager.Init----->>>");
	ctrlList["Login"] = Login.New();
	ctrlList["Register"] = Register.New();
	ctrlList["MainCityUIManager"] = MainCityUIManager.New();
	ctrlList["SystemMainPanel"] = SystemMainPanel.New();

	-- setmetatable(SystemMainPanel,RoleMainPanel );
	ctrlList["RoleMainPanel"] = RoleMainPanel.New();
	ctrlList["RoleMeridiansPanel"] = RoleMeridiansPanel.New();
	ctrlList["RolePanel"] = RolePanel.New();
	ctrlList["RoleReLivePanel"] = RoleReLivePanel.New();
	ctrlList["RoleWingsPanel"] = RoleWingsPanel.New();

	ctrlList["SkillMainPanel"] = SkillMainPanel.New();
	ctrlList["SkillPanel"] = SkillPanel.New();
	ctrlList["SkillSecretPanel"] = SkillSecretPanel.New();
	ctrlList["SkillBreakPanel"] = SkillBreakPanel.New();

	ctrlList["BossPanel"] = BossPanel.New();
	ctrlList["ShopPanel"] = ShopPanel.New();
	ctrlList["BigWorld"] = BigWorld.New();

	-- setmetatable(ctrlList["SkillMainPanel"] , ctrlList["SystemMainPanel"]);

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