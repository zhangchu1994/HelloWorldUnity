require "Common/define"

RoleDressMainPanel = {};
local this = RoleDressMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function RoleDressMainPanel.New()

    return this;
end

function RoleDressMainPanel.Awake()

end

function RoleDressMainPanel.Start()

end

function RoleDressMainPanel.Update()

end

function RoleDressMainPanel.OnCreate(obj)
    -- log('RoleDressMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,6,1);
end

function RoleDressMainPanel.OnDownButtonClick(go,argIndex)
    if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Role/RoleDressPanel', 'UICamera/SystemCanvas/RoleDressMainPanel','RoleDressPanel','RoleDressRolePanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 2) then
        panelMgr:CreatePanel('UI/Role/RoleDressPanel', 'UICamera/SystemCanvas/RoleDressMainPanel','RoleDressPanel','RoleDressWeaponPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 3) then
        panelMgr:CreatePanel('UI/Role/RoleDressPanel', 'UICamera/SystemCanvas/RoleDressMainPanel','RoleDressPanel','RoleDressWingPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    end
end

function RoleDressMainPanel.InitView(obj,argIndex)
    RoleDressPanel.OnCreate(obj,argIndex);
    -- if (argIndex == 1) then
    --     RolePanel.OnCreate(obj);
    -- elseif (argIndex == 2) then
    --     RoleReLivePanel.OnCreate(obj);
    -- elseif (argIndex == 3) then
    --     RoleWingsPanel.OnCreate(obj);
    -- elseif (argIndex == 4) then
    --     RoleMeridiansPanel.OnCreate(obj);
    -- end
end