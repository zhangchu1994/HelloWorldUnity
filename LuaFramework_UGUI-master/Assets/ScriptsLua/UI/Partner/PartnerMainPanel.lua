require "Common/define"

PartnerMainPanel = {};
local this = PartnerMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function PartnerMainPanel.New()

    return this;
end

function PartnerMainPanel.Awake()

end

function PartnerMainPanel.Start()

end

function PartnerMainPanel.Update()

end

function PartnerMainPanel.OnCreate(obj)
    -- log('PartnerMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,7,0);--1角色 2转生 3羽翼 4经脉
end

function PartnerMainPanel.OnDownButtonClick(go,argIndex)
    -- if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Partner/PartnerPanel', 'UICamera/SystemCanvas/PartnerMainPanel','PartnerPanel','PartnerPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    -- elseif (argIndex == 2) then
    --     panelMgr:CreatePanel('UI/Role/RoleReLivePanel', 'UICamera/SystemCanvas/PartnerMainPanel','RoleReLivePanel','RoleReLivePanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    -- elseif (argIndex == 3) then
    --     panelMgr:CreatePanel('UI/Role/RoleWingsPanel', 'UICamera/SystemCanvas/PartnerMainPanel','RoleWingsPanel','RoleWingsPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    -- elseif (argIndex == 4) then
    --     panelMgr:CreatePanel('UI/Role/RoleMeridiansPanel', 'UICamera/SystemCanvas/PartnerMainPanel','RoleMeridiansPanel','RoleMeridiansPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    -- end
end

function PartnerMainPanel.InitView(obj,argIndex)
    -- if (argIndex == 1) then
        PartnerPanel.OnCreate(obj);
    -- elseif (argIndex == 2) then
    --     RoleReLivePanel.OnCreate(obj);
    -- elseif (argIndex == 3) then
    --     RoleWingsPanel.OnCreate(obj);
    -- elseif (argIndex == 4) then
    --     RoleMeridiansPanel.OnCreate(obj);
    -- end
end