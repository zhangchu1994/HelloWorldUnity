require "Common/define"

RoleWingsPanel = {};
local this = RoleWingsPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function RoleWingsPanel.New()
    return this;
end

function RoleWingsPanel.Awake()
    -- log("RoleWingsPanel.Awake______________________________");
end

function RoleWingsPanel.Start()
    -- log("RoleWingsPanel.Start______________________________");
end

function RoleWingsPanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("RoleWingsPanel.Update______________________________");
end

function RoleWingsPanel.OnCreate(obj)
    log('RoleWingsPanel.OnCreate____________');
    this.InitView(obj);
end

function RoleWingsPanel.InitView(obj)
    -- log('RoleWingsPanel.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    -- log(m_gameObject.name);

    -- local bgButton = m_transform:FindChild("Bg").gameObject;
    -- m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);

    -- local cancelButton = m_transform:FindChild("Cancel").gameObject;
    -- m_LuaBehaviour:AddClick(cancelButton, this.OnCancelClick);

    -- local RoleWingsPanelButton = m_transform:FindChild("Confirm").gameObject;
    -- m_LuaBehaviour:AddClick(RoleWingsPanelButton, this.OnRoleWingsPanelClick);

    -- m_userName = m_transform:FindChild("UserName/InputField"):GetComponent('InputField');
    -- m_passWord = m_transform:FindChild("PassWord/InputField"):GetComponent('InputField');
    -- m_passWor1 = m_transform:FindChild("PassWordConfirm/InputField"):GetComponent('InputField');


    -- LuaHelper.GetWebManager():AddCmdHandler("LC_RoleWingsPanelUserMsg","RoleWingsPanel,RoleWingsPanelSuccess");
end

function RoleWingsPanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end

--单击事件--
function RoleWingsPanel.OnRoleWingsPanelClick(go)
    log("RoleWingsPanel.OnRoleWingsPanelClick_________________________________________________________");
    -- sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
    -- sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
    if (m_passWord.text ~= m_passWor1.text) then
        log("两次密码不一致");-- = "..m_passWord.text.." m_passWor1.text = "..m_passWor1.text..
        return;
    end

    local json =  JsonObject.New();
    json:set_Item("account",m_userName.text);
    json:set_Item("passWord",m_passWord.text);
    LuaHelper.GetWebManager():CMD("CL_RoleWingsPanelUserMsg", json);
end

function RoleWingsPanel.RoleWingsPanelSuccess(message)
    log("RoleWingsPanelSuccess = "..tostring(message));
end

function RoleWingsPanel.SceneDone(obj)
    
end

--关闭事件--
function RoleWingsPanel.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end