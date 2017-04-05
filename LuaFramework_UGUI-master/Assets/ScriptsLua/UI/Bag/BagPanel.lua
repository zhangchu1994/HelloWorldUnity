require "Common/define"

BagPanel = {};
local this = BagPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function BagPanel.New()
    return this;
end

function BagPanel.Awake()
    -- log("BagPanel.Awake______________________________");
end

function BagPanel.Start()
    -- log("BagPanel.Start______________________________");
end

function BagPanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("BagPanel.Update______________________________");
end

function BagPanel.OnCreate(obj,argIndex)
    -- log('BagPanel.OnCreate____________');
    this.InitView(obj);
end

function BagPanel.InitView(obj)
    -- log('BagPanel.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    -- log(m_gameObject.name);

    m_ScrollView = m_transform:FindChild("Scroll View").gameObject;
    -- log("ShopPanel.InitView"..m_ScrollView.name);
    this.InitScrollView();


    -- local bgButton = m_transform:FindChild("Bg").gameObject;
    -- m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);

    -- local cancelButton = m_transform:FindChild("Cancel").gameObject;
    -- m_LuaBehaviour:AddClick(cancelButton, this.OnCancelClick);

    -- local registerButton = m_transform:FindChild("Confirm").gameObject;
    -- m_LuaBehaviour:AddClick(registerButton, this.OnRegisterClick);

    -- m_userName = m_transform:FindChild("UserName/InputField"):GetComponent('InputField');
    -- m_passWord = m_transform:FindChild("PassWord/InputField"):GetComponent('InputField');
    -- m_passWor1 = m_transform:FindChild("PassWordConfirm/InputField"):GetComponent('InputField');


    -- LuaHelper.GetWebManager():AddCmdHandler("LC_RegisterUserMsg","BagPanel,RegisterSuccess");
end

--初始化面板--
function BagPanel.InitScrollView()
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

function BagPanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end

--单击事件--
function BagPanel.OnRegisterClick(go)
    -- log("BagPanel.OnRegisterClick_________________________________________________________");
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

function BagPanel.RegisterSuccess(message)
    -- log("RegisterSuccess = "..tostring(message));
end

function BagPanel.SceneDone(obj)
    
end

--关闭事件--
function BagPanel.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end