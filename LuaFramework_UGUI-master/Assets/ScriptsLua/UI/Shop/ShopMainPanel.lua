require "Common/define"

ShopMainPanel = {};
local this = ShopMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_SystemMainPanel;

function ShopMainPanel.New()

    return this;
end

function ShopMainPanel.Awake()

end

function ShopMainPanel.Start()

end

function ShopMainPanel.Update()

end

function ShopMainPanel.OnCreate(obj)
    -- log('ShopMainPanel.OnCreate____________');
    m_SystemMainPanel = CtrlManager.GetCtrl("SystemMainPanel");
    m_SystemMainPanel.InitView(obj,this,5);--1角色 2转生 3羽翼 4经脉
end

function ShopMainPanel.OnDownButtonClick(go,argIndex)
    if (argIndex == 1) then
        panelMgr:CreatePanel('UI/Shop/ShopPanel', 'UICamera/Canvas/ShopMainPanel','ShopPanel','SecretShopPanel', m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 2) then
        panelMgr:CreatePanel('UI/Shop/ShopPanel', 'UICamera/Canvas/ShopMainPanel','ShopPanel','IntegralShopPanel', m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 3) then
        panelMgr:CreatePanel('UI/Shop/ShopPanel', 'UICamera/Canvas/ShopMainPanel','ShopPanel','ItemShopPanel', m_SystemMainPanel.OnSubViewCreate);
    elseif (argIndex == 4) then
        panelMgr:CreatePanel('UI/Shop/ShopPanel', 'UICamera/Canvas/ShopMainPanel','ShopPanel','FeatsShopPanel', m_SystemMainPanel.OnSubViewCreate);
    end
end

function ShopMainPanel.InitView(obj,argIndex)
    ShopPanel.OnCreate(obj,argIndex);
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