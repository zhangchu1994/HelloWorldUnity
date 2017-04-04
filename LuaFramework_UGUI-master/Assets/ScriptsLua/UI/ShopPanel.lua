require "Common/define"

ShopPanel = {};
local this = ShopPanel;

local m_panel;
local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_firstCreate = false;
local m_ScrollView;

function ShopPanel.New()
    return this;
end

function ShopPanel.Awake()
    -- log("ShopPanel.Awake______________________________");
end

function ShopPanel.Start()
    -- log("ShopPanel.Start______________________________");
end

function ShopPanel.Show()
    -- panelMgr:CreatePanel('loginperfab', 'LoginPanel','Login', this.OnCreate);
end

function ShopPanel.Update()
    if m_firstCreate == false then
        m_firstCreate = true;
        this.InitView();
    end
end

function ShopPanel.OnCreate(obj)
end

function ShopPanel.InitView()
    log('ShopPanel.OnCreate____________');
    m_gameObject = GameObject.Find("Canvas/ShopPanel");
    m_transform = m_gameObject.transform;
    
    m_LuaBehaviour = m_transform:GetComponent('LuaBehaviour');
    m_LuaBehaviour:AddClick(m_gameObject, this.OnCloseClick);

    m_ScrollView = m_transform:FindChild("Scroll View");
    this.InitScrollView();
    -- for i=1,6 do
    --     log("Left"..tostring(i));
    --     local loginButton = m_transform:FindChild("Left"..tostring(i)).gameObject;
    --     m_LuaBehaviour:AddClick(loginButton, this.OnClick);
    -- end
    -- resMgr:LoadPrefab('prompt', { 'PromptItem' }, this.InitPanel);
end

--初始化面板--
function ShopPanel.InitScrollView()
    local count = 3; 
    local parent = m_ScrollView:FindChild('Grid');
    for i = 1, count do
        local prefab = Resources.Load("UI/ShopPanelItem");
        local go = GameObject.Instantiate(prefab);
        go.name = 'Item'..tostring(i);
        go.transform:SetParent(parent);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        -- print(tostring(go));
        -- print(tostring(m_LuaBehaviour));
        m_LuaBehaviour:AddClick(go, this.OnItemClick);

    --     local label = go.transform:FindChild('Text');
    --     label:GetComponent('Text').text = tostring(i);
    end
end


function ShopPanel.OnCloseClick()
    m_gameObject.SetActive(m_gameObject,false);
end

function ShopPanel.OnItemClick(obj)
    log("ShopPanel.OnItemClick____name = "..obj.name);
end

-- --单击事件--
-- function ShopPanel.OnClick(go)
--     log("________________________"..go.name);--
--     if (go.name == "Left6") then
--         sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
--     end
--     -- sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
-- end

-- function ShopPanel.SceneDone(obj)
    
-- end

-- --关闭事件--
-- function ShopPanel.Close()
--     panelMgr:ClosePanel(CtrlNames.Prompt);
-- end


