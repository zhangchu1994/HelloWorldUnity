require "Common/define"

ReLiveBossPanel = {};
local this = ReLiveBossPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
-- local m_firstCreate = false;

local m_userName;
local m_passWord;
local m_passWord1;


function ReLiveBossPanel.New()
    return this;
end

function ReLiveBossPanel.Awake()

end

function ReLiveBossPanel.Start()

end

function ReLiveBossPanel.Update()

end

function ReLiveBossPanel.OnCreate(obj)
    this.InitView(obj);
end

function ReLiveBossPanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/BossMainPanel/ReLiveBossPanel','RoleList','RoleList',Vector3.New(0,270,0), ReLiveBossPanel.OnCreateRoleList);--
end

function ReLiveBossPanel.OnCreateRoleList(obj)
    obj.transform:SetSiblingIndex (0);
end