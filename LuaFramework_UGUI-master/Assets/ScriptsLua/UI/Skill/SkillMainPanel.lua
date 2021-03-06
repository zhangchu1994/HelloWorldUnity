require "Common/define"

SkillMainPanel = {};
local this = SkillMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function SkillMainPanel.New()

    return this;
end

function SkillMainPanel.Awake()

end

function SkillMainPanel.Start()

end

function SkillMainPanel.Update()

end

function SkillMainPanel.OnCreate(obj)
    -- log('SkillMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,2,0);--1角色 2转生 3羽翼 4经脉
end

function SkillMainPanel.OnDownButtonClick(go,argIndex)
    if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Skill/SkillPanel', 'UICamera/SystemCanvas/SkillMainPanel','SkillPanel','SkillPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 2) then
        panelMgr:CreatePanel('UI/Skill/SkillSecretPanel', 'UICamera/SystemCanvas/SkillMainPanel','SkillSecretPanel','SkillSecretPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 3) then
        panelMgr:CreatePanel('UI/Skill/SkillBreakPanel', 'UICamera/SystemCanvas/SkillMainPanel','SkillBreakPanel','SkillBreakPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    end
end

function SkillMainPanel.InitView(obj,argIndex)
    if (argIndex == 1) then
        SkillPanel.OnCreate(obj);
    elseif (argIndex == 2) then
        SkillSecretPanel.OnCreate(obj);
    elseif (argIndex == 3) then
        SkillBreakPanel.OnCreate(obj);
    end
end