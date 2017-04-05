require "Common/define"

BagMainPanel = {};
local this = BagMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function BagMainPanel.New()

    return this;
end

function BagMainPanel.Awake()

end

function BagMainPanel.Start()

end

function BagMainPanel.Update()

end

function BagMainPanel.OnCreate(obj)
    -- log('BagMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,4);--1角色 2转生 3羽翼 4经脉
end

function BagMainPanel.OnDownButtonClick(go,argIndex)
    if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Bag/BagPanel', 'UICamera/Canvas/BagMainPanel','BagPanel',"EquipmentPanel", m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 2) then
        panelMgr:CreatePanel('UI/Bag/BagPanel', 'UICamera/Canvas/BagMainPanel','BagPanel',"ItemPanel", m_SystemMainPanel.OnSubViewCreate);
    end
end

function BagMainPanel.InitView(obj,argIndex)
    BagPanel.OnCreate(obj,argIndex);
    -- if (argIndex == 1) then
    --     RolePanel.OnCreate(obj);
    -- elseif (argIndex == 2) then
    --     RoleReLivePanel.OnCreate(obj);
    -- end
end