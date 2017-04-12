require "Common/define"

RoleTitlePanel = {};
local this = RoleTitlePanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_HightIndex = 0;
local m_Cells= {};

function RoleTitlePanel.New()
    return this;
end

function RoleTitlePanel.Awake()

end

function RoleTitlePanel.Start()

end

function RoleTitlePanel.Update()

end

function RoleTitlePanel.OnCreate(obj)
    this.InitView(obj);
end

function RoleTitlePanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    -- panelMgr:CreatePanel('UI/Common/RoleList', 'UICamera/SystemCanvas/RoleTitleMainPanel/RoleTitlePanel','RoleList','RoleList',Vector3.New(0,270,0), RoleTitlePanel.OnCreateRoleList);--

    m_ScrollView = m_transform:FindChild("Scroll View").gameObject;
    this.InitScrollView();

end

-- function RoleTitlePanel.OnCreateRoleList(obj)
--     obj.transform:SetSiblingIndex (0);
-- end

--初始化面板--
function RoleTitlePanel.InitScrollView()
    
    local titleIndex = 0;
    local parent = m_ScrollView.transform:FindChild('Background/Grid');
    local count = 15; 
    if m_HightIndex ~= 0 then
        count = count+1;
    end
   
    for j=1,count do
        local go;
        if m_HightIndex ~= 0 and j == m_HightIndex + 1 then
           
            local prefab1 = Resources.Load("UI/Role/RoleTitleContentItem");
            go = GameObject.Instantiate(prefab1);
            go.name = 'ItemContent'..tostring(m_HightIndex);
            
        else
            titleIndex = titleIndex + 1;
            local prefab1 = Resources.Load("UI/Role/RoleTitleItem");
            go = GameObject.Instantiate(prefab1);
            go.name = 'Item'..tostring(titleIndex);
            local Button = go.transform:FindChild('Buy');
            Button.name = "Buy"..tostring(titleIndex);
            m_LuaBehaviour:AddClick(Button.gameObject, this.OnTitleClick);
        end
        go.transform:SetParent(parent);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;

        m_Cells[j] = go;
    end
end

function RoleTitlePanel.OnTitleClick(go)
    local str = string.gsub(go.name, "Buy", "");
    local ViewIndex = tonumber(str);

    if m_HightIndex == ViewIndex then
        m_HightIndex = 0;
    else
         m_HightIndex = ViewIndex;
    end

    for k,v in pairs(m_Cells) do
        GameObject.Destroy(v);
    end
   this.InitScrollView(); 
end