require "Common/define"

SystemMainPanel = {};
local this = SystemMainPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject = {[0]={},[1]={}};
local m_ViewIndex;
local m_ViewList = {[0]={},[1]={}};
local m_DelegateObj= {[0]={},[1]={}};
local m_DepthIndex = 0;

function SystemMainPanel.New()
    return this;
end

function SystemMainPanel.Awake()

end

function SystemMainPanel.Start()

end

function SystemMainPanel.Update()

end

function SystemMainPanel.InitView(obj,delegate,sysIndex,Index)
    -- log('Register.InitView____________');
    m_DepthIndex = Index;

    m_gameObject[m_DepthIndex] = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    m_DelegateObj[m_DepthIndex] = delegate;
    CameraControl.isEffective = false;
    m_ViewList[m_DepthIndex] = {};

    local systeminfo = CtrlManager.GetTableSystemInfo(sysIndex);
    -- log(m_gameObject.name);
    
    local bgButton = m_transform:FindChild("BgCover").gameObject;
    if m_DepthIndex == 1 then
        bgButton:SetActive(false);
    else
        m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);
    end

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
    m_DelegateObj[m_DepthIndex].OnDownButtonClick(go,m_ViewIndex);
end

function SystemMainPanel.OnSubViewCreate(obj)
    for key,panel in pairs(m_ViewList[m_DepthIndex]) do
        if key ~= m_ViewIndex and panel.activeInHierarchy == true then
            panel:SetActive(false);
        end
    end

    if (obj == nil) then
        return;
    end

    if m_ViewList[m_DepthIndex][m_ViewIndex] == nil then
        m_ViewList[m_DepthIndex][m_ViewIndex] = obj;
    end

    m_DelegateObj[m_DepthIndex].InitView(obj,m_ViewIndex);

    -- log("SystemMainPanel.OnDownButtonClick_________ = "..obj.name);
    local rectTransform = obj:GetComponent("RectTransform")
    rectTransform.anchoredPosition =  Vector2.New(0,50);

end

function SystemMainPanel.OnCancelClick(obj)
    destroy(m_gameObject[m_DepthIndex]);
    if m_DepthIndex == 1 then
        m_ViewList[m_DepthIndex] = {};
        m_DepthIndex = 0;
    elseif m_DepthIndex == 0 then
         CameraControl.isEffective = true;
    end
    -- m_gameObject:SetActive(false);
end