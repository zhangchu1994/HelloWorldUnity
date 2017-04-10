require "Common/define"
require "UI/MainCityUIManager"
require "UI/BattleSceneUIManager"
require "UI/BossPanel"
require "UI/BigWorld"
require "UI/SystemMainPanel"
require "UI/Login/Register"
require "UI/Login/Login"
require "UI/Role/RoleMainPanel"
require "UI/Role/RoleMeridiansPanel"
require "UI/Role/RolePanel"
require "UI/Role/RoleReLivePanel"
require "UI/Role/RoleWingsPanel"
require "UI/Role/RoleDressMainPanel"
require "UI/Role/RoleDressPanel"
require "UI/Skill/SkillBreakPanel"
require "UI/Skill/SkillMainPanel"
require "UI/Skill/SkillPanel"
require "UI/Skill/SkillSecretPanel"
require "UI/Smith/SmithMainPanel"
require "UI/Smith/SmithPanel"
require "UI/Bag/BagMainPanel"
require "UI/Bag/BagPanel"
require "UI/Shop/ShopPanel"
require "UI/Shop/ShopMainPanel"

require "UI/Common/ItemInfoPanel"

CtrlManager = {};
local this = CtrlManager;
local ctrlList = {};	--控制器列表--

local m_TableSystemInfo;


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
	ctrlList["RoleDressMainPanel"] = RoleDressMainPanel.New();
	ctrlList["RoleDressPanel"] = RoleDressMainPanel.New();

	ctrlList["SkillMainPanel"] = SkillMainPanel.New();
	ctrlList["SkillPanel"] = SkillPanel.New();
	ctrlList["SkillSecretPanel"] = SkillSecretPanel.New();
	ctrlList["SkillBreakPanel"] = SkillBreakPanel.New();

	ctrlList["SmithMainPanel"] = SmithMainPanel.New();
	ctrlList["SmithPanel"] = SmithPanel.New();

	ctrlList["BagMainPanel"] = BagMainPanel.New();
	ctrlList["BagPanel"] = BagPanel.New();

	ctrlList["ShopMainPanel"] = ShopMainPanel.New();
	ctrlList["ShopPanel"] = ShopPanel.New();

	ctrlList["BossPanel"] = BossPanel.New();
	ctrlList["BigWorld"] = BigWorld.New();


	ctrlList["ItemInfoPanel"] = ItemInfoPanel.New();

	
	m_TableSystemInfo = dofile("Table/SystemInfo");

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

function CtrlManager.GetTableSystemInfo(argIndex)
	for index,systemInfo in pairs(m_TableSystemInfo) do
		if index == argIndex then
			return systemInfo;
		end
	end
	
end

--关闭控制器--
function CtrlManager.Close()
	logWarn('CtrlManager.Close---->>>');
end