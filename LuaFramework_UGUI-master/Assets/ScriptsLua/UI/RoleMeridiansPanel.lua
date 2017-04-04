require "Common/define"

RoleMeridiansPanel = {};
local this = RoleMeridiansPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function RoleMeridiansPanel.New()
    return this;
end

function RoleMeridiansPanel.Awake()
    -- log("RoleMeridiansPanel.Awake______________________________");
end

function RoleMeridiansPanel.Start()
    -- log("RoleMeridiansPanel.Start______________________________");
end

function RoleMeridiansPanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("RoleMeridiansPanel.Update______________________________");
end

function RoleMeridiansPanel.OnCreate(obj)
    -- log('RoleMeridiansPanel.OnCreate____________');
    this.InitView(obj);
end

function RoleMeridiansPanel.InitView(obj)
    -- log('RoleMeridiansPanel.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    -- log(m_gameObject.name);

    -- local bgButton = m_transform:FindChild("Bg").gameObject;
    -- m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);

    -- local cancelButton = m_transform:FindChild("Cancel").gameObject;
    -- m_LuaBehaviour:AddClick(cancelButton, this.OnCancelClick);

    -- local RoleMeridiansPanelButton = m_transform:FindChild("Confirm").gameObject;
    -- m_LuaBehaviour:AddClick(RoleMeridiansPanelButton, this.OnRoleMeridiansPanelClick);

    -- m_userName = m_transform:FindChild("UserName/InputField"):GetComponent('InputField');
    -- m_passWord = m_transform:FindChild("PassWord/InputField"):GetComponent('InputField');
    -- m_passWor1 = m_transform:FindChild("PassWordConfirm/InputField"):GetComponent('InputField');


    -- LuaHelper.GetWebManager():AddCmdHandler("LC_RoleMeridiansPanelUserMsg","RoleMeridiansPanel,RoleMeridiansPanelSuccess");
end

function RoleMeridiansPanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end

--单击事件--
function RoleMeridiansPanel.OnRoleMeridiansPanelClick(go)
    log("RoleMeridiansPanel.OnRoleMeridiansPanelClick_________________________________________________________");
    -- sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
    -- sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
    if (m_passWord.text ~= m_passWor1.text) then
        log("两次密码不一致");-- = "..m_passWord.text.." m_passWor1.text = "..m_passWor1.text..
        return;
    end

    local json =  JsonObject.New();
    json:set_Item("account",m_userName.text);
    json:set_Item("passWord",m_passWord.text);
    LuaHelper.GetWebManager():CMD("CL_RoleMeridiansPanelUserMsg", json);
end

function RoleMeridiansPanel.RoleMeridiansPanelSuccess(message)
    log("RoleMeridiansPanelSuccess = "..tostring(message));
end

function RoleMeridiansPanel.SceneDone(obj)
    
end

--关闭事件--
function RoleMeridiansPanel.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end