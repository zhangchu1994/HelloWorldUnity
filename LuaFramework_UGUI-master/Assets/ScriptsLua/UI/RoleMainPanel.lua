require "Common/define"

RoleMainPanel = {};
local this = RoleMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function RoleMainPanel.New()

    return this;
end

function RoleMainPanel.Awake()

end

function RoleMainPanel.Start()

end

function RoleMainPanel.Update()

end

function RoleMainPanel.OnCreate(obj)
    -- log('RoleMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,4);--1角色 2转生 3羽翼 4经脉
end

function RoleMainPanel.OnDownButtonClick(go,argIndex)
    if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Role/RolePanel', 'UICamera/Canvas/RoleMainPanel','RolePanel', m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 2) then
        panelMgr:CreatePanel('UI/Role/RoleReLivePanel', 'UICamera/Canvas/RoleMainPanel','RoleReLivePanel', m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 3) then
        panelMgr:CreatePanel('UI/Role/RoleWingsPanel', 'UICamera/Canvas/RoleMainPanel','RoleWingsPanel', m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 4) then
        panelMgr:CreatePanel('UI/Role/RoleMeridiansPanel', 'UICamera/Canvas/RoleMainPanel','RoleMeridiansPanel', m_SystemMainPanel.OnSubViewCreate);
    end
end

function RoleMainPanel.InitView(obj,argIndex)
    if (argIndex == 1) then
        RolePanel.OnCreate(obj);
    elseif (argIndex == 2) then
        RoleReLivePanel.OnCreate(obj);
    elseif (argIndex == 3) then
        RoleWingsPanel.OnCreate(obj);
    elseif (argIndex == 4) then
        RoleMeridiansPanel.OnCreate(obj);
    end
end