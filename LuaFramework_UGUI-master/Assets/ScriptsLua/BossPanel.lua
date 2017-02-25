require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

BossPanel = {};
local this = BossPanel;

local m_panel;
local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_firstCreate = false;
local m_ScrollView;

function BossPanel.New()
    return this;
end

function BossPanel.Awake()
    -- log("BossPanel.Awake______________________________");
end

function BossPanel.Start()
    -- log("BossPanel.Start______________________________");
end

function BossPanel.Show()
    -- panelMgr:CreatePanel('loginperfab', 'LoginPanel','Login', this.OnCreate);
end

function BossPanel.Update()
    if m_firstCreate == false then
        m_firstCreate = true;
        this.InitView();
    end
end

function BossPanel.OnCreate(obj)
end

function BossPanel.InitView()
    log('BossPanel.OnCreate____________');
    m_gameObject = GameObject.Find("Canvas/BossPanel");
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
function BossPanel.InitScrollView()
    local count = 3; 
    local parent = m_ScrollView:FindChild('Grid');
    for i = 1, count do
        local prefab = Resources.Load("UI/BossPanelItem");
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


function BossPanel.OnCloseClick()
    m_gameObject.SetActive(m_gameObject,false);
end

function BossPanel.OnItemClick(obj)
    log("BossPanel.OnItemClick____name = "..obj.name);
end

-- --单击事件--
-- function BossPanel.OnClick(go)
--     log("________________________"..go.name);--
--     if (go.name == "Left6") then
--         sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
--     end
--     -- sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
-- end

-- function BossPanel.SceneDone(obj)
    
-- end

-- --关闭事件--
-- function BossPanel.Close()
--     panelMgr:ClosePanel(CtrlNames.Prompt);
-- end


