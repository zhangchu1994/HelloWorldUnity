require "Common/define"

PartnerPanel = {};
local this = PartnerPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;

function PartnerPanel.New()
    return this;
end

function PartnerPanel.Awake()

end

function PartnerPanel.Start()

end

function PartnerPanel.Update()

end

function PartnerPanel.OnCreate(obj)
    this.InitView(obj);
end

function PartnerPanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/PartnerMainPanel/PartnerPanel','RoleList','RoleList',Vector3.New(0,270,0), PartnerPanel.OnCreateRoleList);--
end

function PartnerPanel.OnCreateRoleList(obj)
    obj.transform:SetSiblingIndex (0);
end

function PartnerPanel.OnEquipmentItemClick(go)
    panelMgr:CreatePanel('UI/Common/ItemInfoPanel', 'UICamera/SystemCanvas','ItemInfoPanel','ItemInfoPanel',Vector3.New(0,0,0), ItemInfoPanel.OnCreate);
end

function PartnerPanel.OnDressUpClick(go)
    panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','RoleDressMainPanel','RoleDressMainPanel',Vector3.New(0,0,0), RoleDressMainPanel.OnCreate);
end