require "Common/define"

EncounterMainPanel = {};
local this = EncounterMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function EncounterMainPanel.New()

    return this;
end

function EncounterMainPanel.Awake()

end

function EncounterMainPanel.Start()

end

function EncounterMainPanel.Update()

end

function EncounterMainPanel.OnCreate(obj)
    -- log('EncounterMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,11,0);--1角色 2转生 3羽翼 4经脉
end

function EncounterMainPanel.OnDownButtonClick(go,argIndex)
    if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Encounter/MineEncounterPanel', 'UICamera/SystemCanvas/EncounterMainPanel','MineEncounterPanel','MineEncounterPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 2) then
        panelMgr:CreatePanel('UI/Encounter/WildEncounterPanel', 'UICamera/SystemCanvas/EncounterMainPanel','WildEncounterPanel','WildEncounterPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    end
end

function EncounterMainPanel.InitView(obj,argIndex)
    if (argIndex == 1) then
        MineEncounterPanel.OnCreate(obj);
    elseif (argIndex == 2) then
        WildEncounterPanel.OnCreate(obj);
    end
end