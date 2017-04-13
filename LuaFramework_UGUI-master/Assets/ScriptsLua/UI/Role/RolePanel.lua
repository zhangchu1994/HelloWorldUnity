require "Common/define"

RolePanel = {};
local this = RolePanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;

function RolePanel.New()
    return this;
end

function RolePanel.Awake()

end

function RolePanel.Start()

end

function RolePanel.Update()

end

function RolePanel.OnCreate(obj)
    this.InitView(obj);
end

function RolePanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/RoleMainPanel/RolePanel','RoleList','RoleList',Vector3.New(0,270,0), RolePanel.OnCreateRoleList);--

    for i=1,8 do
        local equipmentItem = m_transform:FindChild("EquipmentItem"..tostring(i)).gameObject;
        m_LuaBehaviour:AddClick(equipmentItem, this.OnEquipmentItemClick);
    end

    local DressUp = m_transform:FindChild("DressUp").gameObject;
    m_LuaBehaviour:AddClick(DressUp, this.OnDressUpClick);

    local Title = m_transform:FindChild("RoleTitle").gameObject;
    m_LuaBehaviour:AddClick(Title, this.OnTitleClick);
    
    local Soul = m_transform:FindChild("Soul").gameObject;
    m_LuaBehaviour:AddClick(Soul, this.OnSoulClick);
    
    local Orange = m_transform:FindChild("Orange").gameObject;
    m_LuaBehaviour:AddClick(Orange, this.OnOrangeClick);

    local Info = m_transform:FindChild("FightVaule/Button").gameObject;
    m_LuaBehaviour:AddClick(Info, this.OnInfoClick);
end

function RolePanel.OnCreateRoleList(obj)
    obj.transform:SetSiblingIndex (0);
end

function RolePanel.OnEquipmentItemClick(go)
    panelMgr:CreatePanel('UI/Common/ItemInfoPanel', 'UICamera/SystemCanvas','ItemInfoPanel','ItemInfoPanel',Vector3.New(0,0,0), ItemInfoPanel.OnCreate);
end

function RolePanel.OnDressUpClick(go)
    panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','RoleDressMainPanel','RoleDressMainPanel',Vector3.New(0,0,0), RoleDressMainPanel.OnCreate);
end

function RolePanel.OnTitleClick(go)
    panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','RoleTitleMainPanel','RoleTitleMainPanel',Vector3.New(0,0,0), RoleTitleMainPanel.OnCreate);
end

function RolePanel.OnInfoClick(go)
    panelMgr:CreatePanel('UI/Role/RoleInfoPanel', 'UICamera/SystemCanvas','RoleInfoPanel','RoleInfoPanel',Vector3.New(0,0,0), RoleInfoPanel.OnCreate);
end

function RolePanel.OnSoulClick(go)
    log("RolePanel.OnSoulClick");
end

function RolePanel.OnOrangeClick(go)
    log("RolePanel.OnOrangeClick");
end

function RolePanel.OnCancelClick(obj)
    m_gameObject:SetActive(false);
end