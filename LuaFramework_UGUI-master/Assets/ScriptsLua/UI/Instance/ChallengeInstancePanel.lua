require "Common/define"

ChallengeInstancePanel = {};
local this = ChallengeInstancePanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;

function ChallengeInstancePanel.New()
    return this;
end

function ChallengeInstancePanel.Awake()

end

function ChallengeInstancePanel.Start()

end

function ChallengeInstancePanel.Update()

end

function ChallengeInstancePanel.OnCreate(obj)
    this.InitView(obj);
end

function ChallengeInstancePanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/InstanceMainPanel/ChallengeInstancePanel','RoleList','RoleList',Vector3.New(0,270,0), ChallengeInstancePanel.OnCreateRoleList);--
end

function ChallengeInstancePanel.OnCreateRoleList(obj)
    obj.transform:SetSiblingIndex (0);
end