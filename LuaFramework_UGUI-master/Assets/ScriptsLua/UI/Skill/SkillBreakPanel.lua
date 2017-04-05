require "Common/define"

SkillBreakPanel = {};
local this = SkillBreakPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_CurrentObj;
local m_ViewIndex;--1角色 2转生 3羽翼 4经脉
local m_ViewList = {};
-- local m_firstCreate = false;

function SkillBreakPanel.New()
    return this;
end

function SkillBreakPanel.Awake()
    -- log("SkillBreakPanel.Awake______________________________");
end

function SkillBreakPanel.Start()
    -- log("SkillBreakPanel.Start______________________________");
end

function SkillBreakPanel.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
    -- log("SkillBreakPanel.Update______________________________");
end

function SkillBreakPanel.OnCreate(obj)
    -- log('SkillBreakPanel.OnCreate____________');
    -- this.InitView(obj);
end

function SkillBreakPanel.InitView(obj)
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
        -- log("SkillBreakPanel.InitView = "..tostring(i));
        local button = m_transform:FindChild("DownButton"..i).gameObject;
        m_LuaBehaviour:AddClick(button, this.OnTapClick);
        if i == 1 then
            this.OnTapClick(button);
        end
    end
end



function SkillBreakPanel.OnCreate1(obj)
    -- if m_CurrentObj ~= nil then
    --     log("SkillBreakPanel.OnCreate1 = "..m_CurrentObj.name.." isActive = "..tostring(m_CurrentObj.activeInHierarchy));
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

    -- log("SkillBreakPanel.OnCreate2 = "..m_CurrentObj.name.." isActive = "..tostring(m_CurrentObj.activeInHierarchy));
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

function SkillBreakPanel.OnCancelClick(obj)
    destroy(m_gameObject);
    m_ViewList = {};
    -- m_gameObject:SetActive(false);
end