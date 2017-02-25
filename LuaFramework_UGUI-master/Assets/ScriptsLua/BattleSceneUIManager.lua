require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

BattleSceneUIManager = {};
local this = BattleSceneUIManager;

local m_panel;
local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_firstCreate = false;

function BattleSceneUIManager.New()
    return this;
end

function BattleSceneUIManager.Awake()
    -- log("BattleSceneUIManager.Awake______________________________");
end

function BattleSceneUIManager.Start()
    -- log("BattleSceneUIManager.Start______________________________");
end

function BattleSceneUIManager.Show()
    -- panelMgr:CreatePanel('loginperfab', 'LoginPanel','Login', this.OnCreate);
end

function BattleSceneUIManager.Update()
    if m_firstCreate == false then
        m_firstCreate = true;
        this.InitView();
    end
end

function BattleSceneUIManager.OnCreate(obj)
end

function BattleSceneUIManager.InitView()
    log('BattleSceneUIManager.OnCreate____________');
    m_gameObject = GameObject.Find("Canvas/Panel");
    m_transform = m_gameObject.transform;

    -- m_panel = m_transform:GetComponent('loginPanel');
    m_LuaBehaviour = m_transform:GetComponent('LuaBehaviour');

    local loginButton = m_transform:FindChild("Back").gameObject;
    m_LuaBehaviour:AddClick(loginButton, this.OnClick);
    -- end
    -- resMgr:LoadPrefab('prompt', { 'PromptItem' }, this.InitPanel);
end

--单击事件--
function BattleSceneUIManager.OnClick(go)
    log("________________________"..go.name);--
    sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone);  
end

function BattleSceneUIManager.SceneDone(obj)
    
end

--关闭事件--
function BattleSceneUIManager.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end


