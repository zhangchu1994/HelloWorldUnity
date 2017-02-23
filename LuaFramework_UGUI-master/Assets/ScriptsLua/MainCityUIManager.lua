require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

MainCityUIManager = {};
local this = MainCityUIManager;

local m_panel;
local m_LuaBehaviour;
local m_transform;
local m_gameObject;
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

    -- m_panel = m_transform:GetComponent('loginPanel');
    m_LuaBehaviour = m_transform:GetComponent('LuaBehaviour');

    for i=1,6 do
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
    end
    -- sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
end

function MainCityUIManager.SceneDone(obj)
    
end

--关闭事件--
function MainCityUIManager.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end


