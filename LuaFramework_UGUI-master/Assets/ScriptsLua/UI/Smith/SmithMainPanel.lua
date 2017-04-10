require "Common/define"

SmithMainPanel = {};
local this = SmithMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function SmithMainPanel.New()

    return this;
end

function SmithMainPanel.Awake()

end

function SmithMainPanel.Start()

end

function SmithMainPanel.Update()

end

function SmithMainPanel.OnCreate(obj)
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,3,0);
end

function SmithMainPanel.OnDownButtonClick(go,argIndex)
    if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Smith/SmithPanel', 'UICamera/SystemCanvas/SmithMainPanel',"SmithPanel",'StrengthPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 2) then
        panelMgr:CreatePanel('UI/Smith/SmithPanel', 'UICamera/SystemCanvas/SmithMainPanel',"SmithPanel",'GemPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 3) then
        panelMgr:CreatePanel('UI/Smith/SmithPanel', 'UICamera/SystemCanvas/SmithMainPanel',"SmithPanel",'AddSpiritPanel',Vector3.New(0,0,0), m_SystemMainPanel.OnSubViewCreate);
    end
end

function SmithMainPanel.InitView(obj,argIndex)
    SmithPanel.OnCreate(obj,argIndex);
    -- if (argIndex == 1) then
    --     RoleMeridiansPanel.OnCreate(obj);
    -- elseif (argIndex == 4) then
    --     RoleMeridiansPanel.OnCreate(obj);
    -- end
end