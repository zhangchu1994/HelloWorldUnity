require "Common/define"

ItemInfoPanel = {};
local this = ItemInfoPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function ItemInfoPanel.New()
    return this;
end

function ItemInfoPanel.Awake()
    -- log("ItemInfoPanel.Awake______________________________");
end

function ItemInfoPanel.Start()
    -- log("ItemInfoPanel.Start______________________________");
end

function ItemInfoPanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("ItemInfoPanel.Update______________________________");
end

function ItemInfoPanel.OnCreate(obj)
    -- log('ItemInfoPanel.OnCreate____________');
    this.InitView(obj);
end

function ItemInfoPanel.InitView(obj)
    -- log('ItemInfoPanel.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    -- log(m_gameObject.name);

    local bgButton = m_transform:FindChild("BgCover").gameObject;
    m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);
end


function ItemInfoPanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end

--单击事件--
function ItemInfoPanel.OnRegisterClick(go)
    if (m_passWord.text ~= m_passWor1.text) then
        -- log("两次密码不一致");-- = "..m_passWord.text.." m_passWor1.text = "..m_passWor1.text..
        return;
    end

    local json =  JsonObject.New();
    json:set_Item("account",m_userName.text);
    json:set_Item("passWord",m_passWord.text);
    LuaHelper.GetWebManager():CMD("CL_RegisterUserMsg", json);
end

function ItemInfoPanel.RegisterSuccess(message)
    -- log("RegisterSuccess = "..tostring(message));
end

function ItemInfoPanel.SceneDone(obj)
    
end

--关闭事件--
function ItemInfoPanel.Close()
    -- panelMgr:ClosePanel(CtrlNames.Prompt);
end

function ItemInfoPanel.OnCancelClick(obj)
    destroy(m_gameObject);
    -- m_ViewList = {};
    -- CameraControl.isEffective = true;
    -- m_gameObject:SetActive(false);
end