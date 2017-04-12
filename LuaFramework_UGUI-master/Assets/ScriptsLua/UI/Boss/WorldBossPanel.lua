require "Common/define"

WorldBossPanel = {};
local this = WorldBossPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function WorldBossPanel.New()
    return this;
end

function WorldBossPanel.Awake()

end

function WorldBossPanel.Start()

end

function WorldBossPanel.Update()

end

function WorldBossPanel.OnCreate(obj)
    this.InitView(obj);
end

function WorldBossPanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/BossMainPanel/WorldBossPanel','RoleList','RoleList',Vector3.New(0,270,0), WorldBossPanel.OnCreateRoleList);--
end

function WorldBossPanel.OnCreateRoleList(obj)
    obj.transform:SetSiblingIndex (0);
end