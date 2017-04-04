require "Common/define"

Login = {};
local this = Login;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;
local m_userNameField;
local m_passWordField;

function Login.New()
    return this;
end

function Login.Awake()
    -- log("Login.Awake______________________________");
end

function Login.Start()
    -- log("Login.Start______________________________");
end

function Login.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("Login.Update______________________________");
end

function Login.OnCreate(obj)
    -- log('Login.OnCreate____________');
    this.InitView(obj);
end

function Login.InitView(obj)
    -- log('Login.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    log(m_gameObject.name);

    local loginButton = m_transform:FindChild("Login").gameObject;
    m_LuaBehaviour:AddClick(loginButton, this.OnLoginClick);

    local registerButton = m_transform:FindChild("Register").gameObject;
    m_LuaBehaviour:AddClick(registerButton, this.OnRegisterClick);

    m_userNameField = m_transform:FindChild("Name"):GetComponent('InputField');
    m_passWordField = m_transform:FindChild("Password"):GetComponent('InputField');

    log("Login.OnLoginClick_________________________________________________________");
    LuaHelper.GetWebManager():AddCmdHandler("LC_LoginMsg","Login,LoginSucceess");
    LuaHelper.GetWebManager():AddCmdHandler("LC_ReturnErrorMsg","Login,LoginError");
end

--单击事件--
function Login.OnLoginClick(go)
    local json = JsonObject.New();
    json:set_Item("userName", m_userNameField.text);
    json:set_Item("password", m_passWordField.text);
    json:set_Item("deviceid", "Fuck");
    json:set_Item("operatorId", "zhongqingsdk");
    json:set_Item("ptId", "-1");
    json:set_Item("playway", "android");
    json:set_Item("language", 0);
    json:set_Item("test", 0);
    log(json:ToString());
    LuaHelper.GetWebManager():CMD("CL_LoginMsg", json); 
    -- sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
    -- sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
end

function Login.LoginSucceess(message)
    log("Login.LoginSucceess_________________________"..tostring(message));
end

function Login.LoginError(message)
    log("Login.LoginError_________________________"..tostring(message));
end

function Login.OnRegisterClick(go)
    log("Login.OnRegisterClick_________________________________________________________");
    panelMgr:CreatePanel('UI/Login/RegisterPanel', 'UICamera/LoginCanvas/Bg','Register', Register.OnCreate);
end

function Login.SceneDone(obj)
    
end

--关闭事件--
function Login.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end