require "Common/define"

InstanceMainPanel = {};
local this = InstanceMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function InstanceMainPanel.New()

    return this;
end

function InstanceMainPanel.Awake()

end

function InstanceMainPanel.Start()

end

function InstanceMainPanel.Update()

end

function InstanceMainPanel.OnCreate(obj)
    -- log('InstanceMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,12,0);--1角色 2转生 3羽翼 4经脉
end

function InstanceMainPanel.OnDownButtonClick(go,argIndex)
    if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Instance/MaterialInstancePanel', 'UICamera/SystemCanvas/InstanceMainPanel','MaterialInstancePanel','MaterialInstancePanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 2) then
        panelMgr:CreatePanel('UI/Instance/ChallengeInstancePanel', 'UICamera/SystemCanvas/InstanceMainPanel','ChallengeInstancePanel','ChallengeInstancePanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    end
end

function InstanceMainPanel.InitView(obj,argIndex)
    if (argIndex == 1) then
        MaterialInstancePanel.OnCreate(obj);
    elseif (argIndex == 2) then
        ChallengeInstancePanel.OnCreate(obj);
    end
end