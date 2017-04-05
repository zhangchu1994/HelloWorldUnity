require "Common/define"

SkillSecretPanel = {};
local this = SkillSecretPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_CurrentObj;
local m_ViewIndex;--1角色 2转生 3羽翼 4经脉
local m_ViewList = {};
-- local m_firstCreate = false;

function SkillSecretPanel.New()
    return this;
end

function SkillSecretPanel.Awake()
    -- log("SkillSecretPanel.Awake______________________________");
end

function SkillSecretPanel.Start()
    -- log("SkillSecretPanel.Start______________________________");
end

function SkillSecretPanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("SkillSecretPanel.Update______________________________");
end

function SkillSecretPanel.OnCreate(obj)
    -- log('SkillSecretPanel.OnCreate____________');
    -- this.InitView(obj);
end

function SkillSecretPanel.InitView(obj)
    -- log('Register.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
    -- log(m_gameObject.name);

    local bgButton = m_transform:FindChild("BgCover").gameObject;
    m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);

    local cancelButton = m_transform:FindChild("Back").gameObject;
    m_LuaBehaviour:AddClick(cancelButton, this.OnCancelClick);

    -- local registerButton = m_transform:FindChild("Confirm").gameObject;
    -- m_LuaBehaviour:AddClick(registerButton, this.OnRegisterClick);

    for i=1,4 do
        -- log("SkillSecretPanel.InitView = "..tostring(i));
        local button = m_transform:FindChild("DownButton"..i).gameObject;
        m_LuaBehaviour:AddClick(button, this.OnTapClick);
        if i == 1 then
            this.OnTapClick(button);
        end
    end
end

function SkillSecretPanel.OnTapClick(go)
    -- log("SkillSecretPanel.OnTapClick = "..go.name);
    local str = string.gsub(go.name, "DownButton", "");
    
    m_ViewIndex = tonumber(str); 
    if (m_ViewIndex == 1) then
        panelMgr:CreatePanel('UI/Role/RolePanel', 'UICamera/Canvas/SkillSecretPanel','RolePanel', this.OnCreate1);
    elseif (m_ViewIndex == 2) then
        panelMgr:CreatePanel('UI/Role/RoleReLivePanel', 'UICamera/Canvas/SkillSecretPanel','RoleReLivePanel', this.OnCreate1);
    elseif (m_ViewIndex == 3) then
        panelMgr:CreatePanel('UI/Role/RoleWingsPanel', 'UICamera/Canvas/SkillSecretPanel','RoleWingsPanel', this.OnCreate1);
    elseif (m_ViewIndex == 4) then
        panelMgr:CreatePanel('UI/Role/RoleMeridiansPanel', 'UICamera/Canvas/SkillSecretPanel','RoleMeridiansPanel', this.OnCreate1);
    end
end

function SkillSecretPanel.OnCreate1(obj)
    -- if m_CurrentObj ~= nil then
    --     log("SkillSecretPanel.OnCreate1 = "..m_CurrentObj.name.." isActive = "..tostring(m_CurrentObj.activeInHierarchy));
    -- end
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

    -- log("SkillSecretPanel.OnCreate2 = "..m_CurrentObj.name.." isActive = "..tostring(m_CurrentObj.activeInHierarchy));
    if (m_ViewIndex == 1) then
        RolePanel.OnCreate(obj);
    elseif (m_ViewIndex == 2) then
        RoleReLivePanel.OnCreate(obj);
    elseif (m_ViewIndex == 3) then
        RoleWingsPanel.OnCreate(obj);
    elseif (m_ViewIndex == 4) then
        RoleMeridiansPanel.OnCreate(obj);
    end
end

function SkillSecretPanel.OnCancelClick(obj)
    destroy(m_gameObject);
    m_ViewList = {};
    -- m_gameObject:SetActive(false);
end