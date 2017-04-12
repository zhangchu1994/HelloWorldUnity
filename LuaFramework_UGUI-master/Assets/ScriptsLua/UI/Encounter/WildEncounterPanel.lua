require "Common/define"

WildEncounterPanel = {};
local this = WildEncounterPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;

function WildEncounterPanel.New()
    return this;
end

function WildEncounterPanel.Awake()

end

function WildEncounterPanel.Start()

end

function WildEncounterPanel.Update()

end

function WildEncounterPanel.OnCreate(obj)
    this.InitView(obj);
end

function WildEncounterPanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/EncounterMainPanel/WildEncounterPanel','RoleList','RoleList',Vector3.New(0,270,0), WildEncounterPanel.OnCreateRoleList);--
end

function WildEncounterPanel.OnCreateRoleList(obj)
    obj.transform:SetSiblingIndex (0);
end