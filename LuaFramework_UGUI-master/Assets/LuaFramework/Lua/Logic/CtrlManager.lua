require "Common/define"
require "UI/MainCityUIManager"
require "UI/BattleSceneUIManager"
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
require "UI/Role/RoleTitleMainPanel"
require "UI/Role/RoleTitlePanel"
require "UI/Role/RoleInfoPanel"
require "UI/Role/RoleList"
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
require "UI/Partner/PartnerMainPanel"
require "UI/Partner/PartnerPanel"
require "UI/Instance/ChallengeInstancePanel"
require "UI/Instance/InstanceMainPanel"
require "UI/Instance/MaterialInstancePanel"
require "UI/Encounter/EncounterMainPanel"
require "UI/Encounter/MineEncounterPanel"
require "UI/Encounter/WildEncounterPanel"
require "UI/Boss/BossMainPanel"
require "UI/Boss/PersonalBossPanel"
require "UI/Boss/ReLiveBossPanel"
require "UI/Boss/WorldBossPanel"
require "UI/Common/ItemInfoPanel"
require "Logic/GameClient"
require "Logic/StringTable"

CtrlManager = {};
local this = CtrlManager;
local ctrlList = {};	--控制器列表--

local m_TableSystemInfo;


function CtrlManager.Init()
	log("CtrlManager.Init______________________________________");
	ctrlList["Login"] = Login.New();
	ctrlList["Register"] = Register.New();
	ctrlList["MainCityUIManager"] = MainCityUIManager.New();
	ctrlList["SystemMainPanel"] = SystemMainPanel.New();

	ctrlList["RoleMainPanel"] = RoleMainPanel.New();
	ctrlList["RoleMeridiansPanel"] = RoleMeridiansPanel.New();
	ctrlList["RolePanel"] = RolePanel.New();
	ctrlList["RoleReLivePanel"] = RoleReLivePanel.New();
	ctrlList["RoleWingsPanel"] = RoleWingsPanel.New();
	ctrlList["RoleDressMainPanel"] = RoleDressMainPanel.New();
	ctrlList["RoleDressPanel"] = RoleDressPanel.New();
	ctrlList["RoleTitleMainPanel"] = RoleTitleMainPanel.New();
	ctrlList["RoleTitlePanel"] = RoleTitlePanel.New();
	ctrlList["RoleInfoPanel"] = RoleInfoPanel.New();
	ctrlList["RoleList"] = RoleList.New();

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



	ctrlList["PartnerMainPanel"] = PartnerMainPanel.New();
	ctrlList["PartnerPanel"] = PartnerPanel.New();

	ctrlList["ChallengeInstancePanel"] = ChallengeInstancePanel.New();
	ctrlList["MaterialInstancePanel"] = MaterialInstancePanel.New();
	ctrlList["InstanceMainPanel"] = InstanceMainPanel.New();

	ctrlList["MineEncounterPanel"] = MineEncounterPanel.New();
	ctrlList["WildEncounterPanel"] = WildEncounterPanel.New();
	ctrlList["EncounterMainPanel"] = EncounterMainPanel.New();

	ctrlList["BossMainPanel"] = BossMainPanel.New();
	ctrlList["PersonalBossPanel"] = PersonalBossPanel.New();
	ctrlList["ReLiveBossPanel"] = ReLiveBossPanel.New();
	ctrlList["WorldBossPanel"] = WorldBossPanel.New();

	ctrlList["BigWorld"] = BigWorld.New();
	ctrlList["ItemInfoPanel"] = ItemInfoPanel.New();

	ctrlList["GameClient"] = GameClient.New();

	
	m_TableSystemInfo = dofile("Table/SystemInfo");

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