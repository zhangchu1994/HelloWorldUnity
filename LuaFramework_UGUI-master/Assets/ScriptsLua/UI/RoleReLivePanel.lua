require "Common/define"

RoleReLivePanel = {};
local this = RoleReLivePanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function RoleReLivePanel.New()
    return this;
end

function RoleReLivePanel.Awake()
    -- log("RoleReLivePanel.Awake______________________________");
end

function RoleReLivePanel.Start()
    -- log("RoleReLivePanel.Start______________________________");
end

function RoleReLivePanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("RoleReLivePanel.Update______________________________");
end

function RoleReLivePanel.OnCreate(obj)
    -- log('RoleReLivePanel.OnCreate____________');
    this.InitView(obj);
end

function RoleReLivePanel.InitView(obj)
    -- log('RoleReLivePanel.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    -- log(m_gameObject.name);

    -- local bgButton = m_transform:FindChild("Bg").gameObject;
    -- m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);

    -- local cancelButton = m_transform:FindChild("Cancel").gameObject;
    -- m_LuaBehaviour:AddClick(cancelButton, this.OnCancelClick);

    -- local RoleReLivePanelButton = m_transform:FindChild("Confirm").gameObject;
    -- m_LuaBehaviour:AddClick(RoleReLivePanelButton, this.OnRoleReLivePanelClick);

    -- m_userName = m_transform:FindChild("UserName/InputField"):GetComponent('InputField');
    -- m_passWord = m_transform:FindChild("PassWord/InputField"):GetComponent('InputField');
    -- m_passWor1 = m_transform:FindChild("PassWordConfirm/InputField"):GetComponent('InputField');


    -- LuaHelper.GetWebManager():AddCmdHandler("LC_RoleReLivePanelUserMsg","RoleReLivePanel,RoleReLivePanelSuccess");
end

function RoleReLivePanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end

--单击事件--
function RoleReLivePanel.OnRoleReLivePanelClick(go)
    log("RoleReLivePanel.OnRoleReLivePanelClick_________________________________________________________");
    -- sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
    -- sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
    if (m_passWord.text ~= m_passWor1.text) then
        log("两次密码不一致");-- = "..m_passWord.text.." m_passWor1.text = "..m_passWor1.text..
        return;
    end

    local json =  JsonObject.New();
    json:set_Item("account",m_userName.text);
    json:set_Item("passWord",m_passWord.text);
    LuaHelper.GetWebManager():CMD("CL_RoleReLivePanelUserMsg", json);
end

function RoleReLivePanel.RoleReLivePanelSuccess(message)
    log("RoleReLivePanelSuccess = "..tostring(message));
end

function RoleReLivePanel.SceneDone(obj)
    
end

--关闭事件--
function RoleReLivePanel.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end