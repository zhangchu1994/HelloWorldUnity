require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

Login = {};
local this = Login;

local panel;
local prompt;
local transform;
local gameObject;
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

function Login.Show()
    panelMgr:CreatePanel('loginperfab', 'LoginPanel','Login', this.OnCreate);
end

function Login.Update()
    if m_firstCreate == false then
        m_firstCreate = true;
        this.InitView();
    end
    -- log("Login.Update______________________________");
end

function Login.OnCreate(obj)
    -- log('Login.OnCreate____________');
    -- resMgr:LoadPrefab('prompt', { 'PromptItem' }, this.InitPanel);

end

function Login.InitView()
    log('Login.InitView____________');
    gameObject = GameObject.Find("LoginCanvas");
    transform =  GameObject.Find("LoginCanvas").transform;

    panel = transform:GetComponent('loginPanel');
    prompt = transform:GetComponent('LuaBehaviour');

    local loginButton = transform:FindChild("Image/Login").gameObject;

    prompt:AddClick(loginButton, this.OnClick);
end

-- --初始化面板--
-- function Login.InitPanel(objs)
--     local count = 100; 
--     local parent = PromptPanel.gridParent;
--     for i = 1, count do
--         local go = newObject(objs[0]);
--         go.name = 'Item'..tostring(i);
--         go.transform:SetParent(parent);
--         go.transform.localScale = Vector3.one;
--         go.transform.localPosition = Vector3.zero;
--         prompt:AddClick(go, this.OnItemClick);

--         local label = go.transform:FindChild('Text');
--         label:GetComponent('Text').text = tostring(i);
--     end
-- end

-- --滚动项单击--
-- function Login.OnItemClick(go)
--     log(go.name);
-- end

--单击事件--
function Login.OnClick(go)
    sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
    -- sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 


end

function Login.SceneDone(obj)
    
end

--关闭事件--
function Login.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end


