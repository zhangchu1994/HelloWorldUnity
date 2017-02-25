require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

MainCityUIManager = {};
local this = MainCityUIManager;

-- local m_panel;
local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_Canvas;
local m_CanvasTransform;
local m_firstCreate = false;

function MainCityUIManager.New()
    return this;
end

function MainCityUIManager.Awake()
    -- log("MainCityUIManager.Awake______________________________");
end

function MainCityUIManager.Start()
    -- log("MainCityUIManager.Start______________________________");
end

function MainCityUIManager.Show()
    -- panelMgr:CreatePanel('loginperfab', 'LoginPanel','Login', this.OnCreate);
end

function MainCityUIManager.Update()
    if m_firstCreate == false then
        m_firstCreate = true;
        this.InitView();
    end
end

function MainCityUIManager.OnCreate(obj)
end

function MainCityUIManager.InitView()
    log('MainCityUIManager.OnCreate____________');

    m_gameObject = GameObject.Find("Canvas/MainCityPanel");
    m_transform = m_gameObject.transform;

    m_Canvas = GameObject.Find("Canvas");
    m_CanvasTransform = m_Canvas.transform;

    -- m_panel = m_transform:GetComponent('loginPanel');
    m_LuaBehaviour = m_transform:GetComponent('LuaBehaviour');

    for i=1,7 do
        log("Left"..tostring(i));
        local loginButton = m_transform:FindChild("Left"..tostring(i)).gameObject;
        m_LuaBehaviour:AddClick(loginButton, this.OnClick);
    end
    -- resMgr:LoadPrefab('prompt', { 'PromptItem' }, this.InitPanel);
end

--单击事件--
function MainCityUIManager.OnClick(go)
    log("________________________"..go.name);--
    if (go.name == "Left6") then
        sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
    elseif (go.name == "Left7") then
        local BossPanel = m_CanvasTransform:FindChild("BossPanel").gameObject;
        BossPanel.SetActive(BossPanel,true);
    elseif (go.name == "Left2") then
        this.InitGetItemAlert();
    elseif (go.name == "Left3") then

    elseif (go.name == "Left4") then

    elseif (go.name == "Left5") then
        local ShopPanel = m_CanvasTransform:FindChild("ShopPanel").gameObject;
        ShopPanel.SetActive(ShopPanel,true);
    end
    -- sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
end

function MainCityUIManager.InitGetItemAlert()
    local prefab = Resources.Load("UI/GetItemAlert");
    local go = GameObject.Instantiate(prefab);
    -- go.name = 'Item'..tostring(i);
    go.transform:SetParent(m_CanvasTransform);
    go.transform.localScale = Vector3.one;
    go.transform.localPosition = Vector3.zero;
    local LuaBehaviour = go.transform:GetComponent('LuaBehaviour');
    LuaBehaviour.luaName = "MainCityUIManager";
    LuaBehaviour:AddiTween(go,"iTweenDone")


-- -- //  iTween.MoveBy(t,new Vector3(0,10,0),0.3f);
--     iTween.ScaleBy(t,new Vector3(2,2,2),0.5f);
    -- iTween.MoveBy(go,Vector3.New(0,100,0),0.5);
end

function MainCityUIManager.iTweenDone(obj)
    GameObject.Destroy(obj);
end

function MainCityUIManager.SceneDone(obj)
    
end

--关闭事件--
function MainCityUIManager.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end


