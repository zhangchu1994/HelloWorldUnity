require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

Login = {};
local this = Login;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_firstCreate = false;

function Login.New()
    return this;
end

function Login.Awake()
    -- log("Login.Awake______________________________");
end

function Login.Start()
    -- log("Login.Start______________________________");
end

-- function Login.Show()
--     panelMgr:CreatePanel('loginperfab', 'LoginPanel','Login', this.OnCreate);
-- end

function Login.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("Login.Update______________________________");
end

function Login.OnCreate(obj)
    -- log('Login.OnCreate____________');
    -- resMgr:LoadPrefab('prompt', { 'PromptItem' }, this.InitPanel);
end

function Login.InitView()
    log('Login.InitView____________');
    m_gameObject = GameObject.Find("LoginCanvas");
    m_transform =  GameObject.Find("LoginCanvas").transform;

    m_LuaBehaviour = m_transform:GetComponent('LuaBehaviour');

    local loginPanel = m_transform:FindChild("Image/LoginPanel").gameObject;
    loginPanel.SetActive(loginPanel,true);

    local loginButton = m_transform:FindChild("Image/LoginPanel/Login").gameObject;
    m_LuaBehaviour:AddClick(loginButton, this.OnClick);

    local InfoPanel = m_transform:FindChild("Image/InfoPanel").gameObject;
    InfoPanel.SetActive(InfoPanel,false);
end

--单击事件--
function Login.OnClick(go)
    print("Login.OnClick_________________________________________________________");
    sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
    -- sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
end

function Login.SceneDone(obj)
    
end

--关闭事件--
function Login.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end


