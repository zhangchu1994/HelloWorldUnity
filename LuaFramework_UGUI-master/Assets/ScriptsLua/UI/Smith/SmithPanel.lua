require "Common/define"

SmithPanel = {};
local this = SmithPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function SmithPanel.New()
    return this;
end

function SmithPanel.Awake()
    -- log("SmithPanel.Awake______________________________");
end

function SmithPanel.Start()
    -- log("SmithPanel.Start______________________________");
end

function SmithPanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("SmithPanel.Update______________________________");
end

function SmithPanel.OnCreate(obj)
    -- log('SmithPanel.OnCreate____________');
    this.InitView(obj);
end

function SmithPanel.InitView(obj)
    -- log('SmithPanel.InitView____________');
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


    -- LuaHelper.GetWebManager():AddCmdHandler("LC_RegisterUserMsg","SmithPanel,RegisterSuccess");
end

function SmithPanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end

--单击事件--
function SmithPanel.OnRegisterClick(go)
    -- log("SmithPanel.OnRegisterClick_________________________________________________________");
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

function SmithPanel.RegisterSuccess(message)
    -- log("RegisterSuccess = "..tostring(message));
end

function SmithPanel.SceneDone(obj)
    
end

--关闭事件--
function SmithPanel.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end