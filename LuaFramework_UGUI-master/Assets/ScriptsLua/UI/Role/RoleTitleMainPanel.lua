require "Common/define"

RoleTitleMainPanel = {};
local this = RoleTitleMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function RoleTitleMainPanel.New()

    return this;
end

function RoleTitleMainPanel.Awake()

end

function RoleTitleMainPanel.Start()

end

function RoleTitleMainPanel.Update()

end

function RoleTitleMainPanel.OnCreate(obj)
    -- log('RoleTitleMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,14,1);--1角色 2转生 3羽翼 4经脉
end

function RoleTitleMainPanel.OnDownButtonClick(go,argIndex)
    panelMgr:CreatePanel('UI/Role/RoleTitlePanel', 'UICamera/SystemCanvas/RoleTitleMainPanel','RoleTitlePanel','RoleTitlePanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
end

function RoleTitleMainPanel.InitView(obj,argIndex)
    RoleTitlePanel.OnCreate(obj);
end