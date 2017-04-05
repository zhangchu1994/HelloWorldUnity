require "Common/define"

SystemMainPanel = {};
local this = SystemMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_ViewIndex;
local m_ViewList = {};
local m_DelegateObj;

function SystemMainPanel.New()
    return this;
end

function SystemMainPanel.Awake()

end

function SystemMainPanel.Start()

end

function SystemMainPanel.Update()

end

function SystemMainPanel.InitView(obj,delegate,Index)
    -- log('Register.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    m_DelegateObj = delegate;
    CameraControl.isEffective = false;


    local systeminfo = CtrlManager.GetTableSystemInfo(Index);
    -- log(m_gameObject.name);

    local bgButton = m_transform:FindChild("BgCover").gameObject;
    m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);

    local cancelButton = m_transform:FindChild("Back").gameObject;
    m_LuaBehaviour:AddClick(cancelButton, this.OnCancelClick);

    -- local registerButton = m_transform:FindChild("Confirm").gameObject;
    -- m_LuaBehaviour:AddClick(registerButton, this.OnRegisterClick);

    for i=1,4 do
        local button = m_transform:FindChild("DownButton"..i).gameObject;
        if i > #systeminfo.SubSystem then
            button:SetActive(false);
        else
            button:SetActive(true);
            local text = button.transform:FindChild("Text"):GetComponent("Text");
            text.text = systeminfo.SubSystem[i];
            m_LuaBehaviour:AddClick(button, this.OnDownButtonClick);

            if i == 1 then
                this.OnDownButtonClick(button);
            end
        end
    end
end

function SystemMainPanel.OnDownButtonClick(go)
    local str = string.gsub(go.name, "DownButton", "");
    m_ViewIndex = tonumber(str); 
    m_DelegateObj.OnDownButtonClick(go,m_ViewIndex);
end

function SystemMainPanel.OnSubViewCreate(obj)
    for key,panel in pairs(m_ViewList) do
        if key ~= m_ViewIndex and panel.activeInHierarchy == true then
            panel:SetActive(false);
        end
    end

    if (obj == nil) then
        return;
    end

    if m_ViewList[m_ViewIndex] == nil then
        m_ViewList[m_ViewIndex] = obj;
    end

    m_DelegateObj.InitView(obj,m_ViewIndex);

    -- log("SystemMainPanel.OnDownButtonClick_________ = "..obj.name);
    local rectTransform = obj:GetComponent("RectTransform")
    rectTransform.anchoredPosition =  Vector2.New(0,50);

end

function SystemMainPanel.OnCancelClick(obj)
    destroy(m_gameObject);
    m_ViewList = {};
    CameraControl.isEffective = true;
    -- m_gameObject:SetActive(false);
end