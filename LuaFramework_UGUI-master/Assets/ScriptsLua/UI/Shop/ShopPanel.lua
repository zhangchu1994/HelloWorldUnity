require "Common/define"

ShopPanel = {};
local this = ShopPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function ShopPanel.New()
    return this;
end

function ShopPanel.Awake()
    -- log("ShopPanel.Awake______________________________");
end

function ShopPanel.Start()
    -- log("ShopPanel.Start______________________________");
end

function ShopPanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("ShopPanel.Update______________________________");
end

function ShopPanel.OnCreate(obj)
    -- log('ShopPanel.OnCreate____________');
    this.InitView(obj);
end

function ShopPanel.InitView(obj)
    -- log('ShopPanel.InitView____________');
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


    -- LuaHelper.GetWebManager():AddCmdHandler("LC_RegisterUserMsg","ShopPanel,RegisterSuccess");
end

--初始化面板--
function ShopPanel.InitScrollView()
    local count = 20; 
    local parent = m_ScrollView.transform:FindChild('Background/Grid');
    for i = 1, count do
        local prefab = Resources.Load("UI/Shop/ShopPanelItem");
        local go = GameObject.Instantiate(prefab);
        go.name = 'Item'..tostring(i);
        go.transform:SetParent(parent);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        -- -- print(tostring(go));
        -- -- print(tostring(m_LuaBehaviour));
        -- m_LuaBehaviour:AddClick(go, this.OnItemClick);

    --     local label = go.transform:FindChild('Text');
    --     label:GetComponent('Text').text = tostring(i);
    end
end

function ShopPanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end

--单击事件--
function ShopPanel.OnRegisterClick(go)
    -- log("ShopPanel.OnRegisterClick_________________________________________________________");
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

function ShopPanel.RegisterSuccess(message)
    -- log("RegisterSuccess = "..tostring(message));
end

function ShopPanel.SceneDone(obj)
    
end

--关闭事件--
function ShopPanel.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end