require "Common/define"

RolePanel = {};
local this = RolePanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function RolePanel.New()
    return this;
end

function RolePanel.Awake()
    -- log("RolePanel.Awake______________________________");
end

function RolePanel.Start()
    -- log("RolePanel.Start______________________________");
end

function RolePanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("RolePanel.Update______________________________");
end

function RolePanel.OnCreate(obj)
    -- log('RolePanel.OnCreate____________');
    this.InitView(obj);
end

function RolePanel.InitView(obj)
    -- log('RolePanel.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    -- log(m_gameObject.name);

    -- local bgButton = m_transform:FindChild("Bg").gameObject;
    -- m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);

    -- local cancelButton = m_transform:FindChild("Cancel").gameObject;
    -- m_LuaBehaviour:AddClick(cancelButton, this.OnCancelClick);

    -- local registerButton = m_transform:FindChild("Confirm").gameObject;
    -- m_LuaBehaviour:AddClick(registerButton, this.OnRegisterClick);

    -- m_userName = m_transform:FindChild("UserName/InputField"):GetComponent('InputField');
    -- m_passWord = m_transform:FindChild("PassWord/InputField"):GetComponent('InputField');
    -- m_passWor1 = m_transform:FindChild("PassWordConfirm/InputField"):GetComponent('InputField');


    -- LuaHelper.GetWebManager():AddCmdHandler("LC_RegisterUserMsg","RolePanel,RegisterSuccess");
end

function RolePanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end

--单击事件--
function RolePanel.OnRegisterClick(go)
    -- log("RolePanel.OnRegisterClick_________________________________________________________");
    -- sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
    -- sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
    if (m_passWord.text ~= m_passWor1.text) then
        -- log("两次密码不一致");-- = "..m_passWord.text.." m_passWor1.text = "..m_passWor1.text..
        return;
    end

    local json =  JsonObject.New();
    json:set_Item("account",m_userName.text);
    json:set_Item("passWord",m_passWord.text);
    LuaHelper.GetWebManager():CMD("CL_RegisterUserMsg", json);
end

function RolePanel.RegisterSuccess(message)
    -- log("RegisterSuccess = "..tostring(message));
end

function RolePanel.SceneDone(obj)
    
end

--关闭事件--
function RolePanel.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end