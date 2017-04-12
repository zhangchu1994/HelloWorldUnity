require "Common/define"

BossMainPanel = {};
local this = BossMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function BossMainPanel.New()

    return this;
end

function BossMainPanel.Awake()

end

function BossMainPanel.Start()

end

function BossMainPanel.Update()

end

function BossMainPanel.OnCreate(obj)
    -- log('BossMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,10,0);--1角色 2转生 3羽翼 4经脉
end

function BossMainPanel.OnDownButtonClick(go,argIndex)
    if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Boss/PersonalBossPanel', 'UICamera/SystemCanvas/BossMainPanel','PersonalBossPanel','PersonalBossPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 2) then
        panelMgr:CreatePanel('UI/Boss/WorldBossPanel', 'UICamera/SystemCanvas/BossMainPanel','WorldBossPanel','WorldBossPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 3) then
        panelMgr:CreatePanel('UI/Boss/ReLiveBossPanel', 'UICamera/SystemCanvas/BossMainPanel','ReLiveBossPanel','ReLiveBossPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    end
end

function BossMainPanel.InitView(obj,argIndex)
    if (argIndex == 1) then
        PersonalBossPanel.OnCreate(obj);
    elseif (argIndex == 2) then
        WorldBossPanel.OnCreate(obj);
    elseif (argIndex == 3) then
        ReLiveBossPanel.OnCreate(obj);
    end
end