require "Common/define"

RoleDressPanel = {};
local this = RoleDressPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;
local m_ScrollView;


function RoleDressPanel.New()
    return this;
end

function RoleDressPanel.Awake()
    -- log("RoleDressPanel.Awake______________________________");
end

function RoleDressPanel.Start()
    -- log("RoleDressPanel.Start______________________________");
end

function RoleDressPanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("RoleDressPanel.Update______________________________");
end

function RoleDressPanel.OnCreate(obj)
    -- log('RoleDressPanel.OnCreate____________');
    this.InitView(obj);
end

function RoleDressPanel.InitView(obj)
    -- log('RoleDressPanel.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    m_ScrollView = m_transform:FindChild("Scroll View").gameObject;
    this.InitScrollView();

end

--初始化面板--
function RoleDressPanel.InitScrollView()
    local count = 30; 
    local parent = m_ScrollView.transform:FindChild('Grid');
    for i = 1, count do
        local prefab = Resources.Load("UI/Common/Item");
        local go = GameObject.Instantiate(prefab);
        go.name = 'Item'..tostring(i);
        go.transform:SetParent(parent);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        -- print(tostring(go));
        -- print(tostring(m_LuaBehaviour));
        m_LuaBehaviour:AddClick(go, this.OnItemClick);

    --     local label = go.transform:FindChild('Text');
    --     label:GetComponent('Text').text = tostring(i);
    end
end

function RoleDressPanel.OnEquipmentItemClick(go)
    panelMgr:CreatePanel('UI/Common/ItemInfoPanel', 'UICamera/Canvas','ItemInfoPanel','ItemInfoPanel',Vector3.New(0,0,0), ItemInfoPanel.OnCreate);
end

function RoleDressPanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end

--单击事件--
function RoleDressPanel.OnRegisterClick(go)
    -- log("RoleDressPanel.OnRegisterClick_________________________________________________________");
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

function RoleDressPanel.RegisterSuccess(message)
    -- log("RegisterSuccess = "..tostring(message));
end

function RoleDressPanel.SceneDone(obj)
    
end

--关闭事件--
function RoleDressPanel.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end