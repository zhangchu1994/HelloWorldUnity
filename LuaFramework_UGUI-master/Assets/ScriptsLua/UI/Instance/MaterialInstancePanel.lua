require "Common/define"

MaterialInstancePanel = {};
local this = MaterialInstancePanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;

function MaterialInstancePanel.New()
    return this;
end

function MaterialInstancePanel.Awake()

end

function MaterialInstancePanel.Start()

end

function MaterialInstancePanel.Update()

end

function MaterialInstancePanel.OnCreate(obj)
    this.InitView(obj);
end

function MaterialInstancePanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/InstanceMainPanel/MaterialInstancePanel','RoleList','RoleList',Vector3.New(0,270,0), MaterialInstancePanel.OnCreateRoleList);--
end

function MaterialInstancePanel.OnCreateRoleList(obj)
    obj.transform:SetSiblingIndex (0);
end