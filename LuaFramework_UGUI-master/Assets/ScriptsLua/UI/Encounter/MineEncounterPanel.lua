require "Common/define"

MineEncounterPanel = {};
local this = MineEncounterPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;

function MineEncounterPanel.New()
    return this;
end

function MineEncounterPanel.Awake()

end

function MineEncounterPanel.Start()

end

function MineEncounterPanel.Update()

end

function MineEncounterPanel.OnCreate(obj)
    this.InitView(obj);
end

function MineEncounterPanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/EncounterMainPanel/MineEncounterPanel','RoleList','RoleList',Vector3.New(0,270,0), MineEncounterPanel.OnCreateRoleList);--
end

function MineEncounterPanel.OnCreateRoleList(obj)
    obj.transform:SetSiblingIndex (0);
end