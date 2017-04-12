require "Common/define"

PersonalBossPanel = {};
local this = PersonalBossPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function PersonalBossPanel.New()
    return this;
end

function PersonalBossPanel.Awake()

end

function PersonalBossPanel.Start()

end

function PersonalBossPanel.Update()

end

function PersonalBossPanel.OnCreate(obj)
    this.InitView(obj);
end

function PersonalBossPanel.InitView(obj)
    -- log('PersonalBossPanel.InitView____________');
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/BossMainPanel/PersonalBossPanel','RoleList','RoleList',Vector3.New(0,270,0), PersonalBossPanel.OnCreateRoleList);--
end

function PersonalBossPanel.OnCreateRoleList(obj)
    obj.transform:SetSiblingIndex (0);
end
