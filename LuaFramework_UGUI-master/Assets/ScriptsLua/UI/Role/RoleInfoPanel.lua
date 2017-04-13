require "Common/define"

RoleInfoPanel = {};
local this = RoleInfoPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_ScrollViewObj;
local m_ScrollView;
local m_Index;
local m_Count;

function RoleInfoPanel.New()
    return this;
end

function RoleInfoPanel.Awake()

end

function RoleInfoPanel.Start()

end

function RoleInfoPanel.Update()
    -- log("RoleInfoPanel.Update = "..m_ScrollView.horizontalNormalizedPosition);
end

function RoleInfoPanel.OnCreate(obj)
    this.InitView(obj);
end

function RoleInfoPanel.InitView(obj)
    m_Index = 1;
    m_Count = 8;
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    local bgButton = m_transform:FindChild("BgCover").gameObject;
    m_LuaBehaviour:AddClick(bgButton, this.OnCancelClick);

    local cancelButton = m_transform:FindChild("Bg/Back").gameObject;
    m_LuaBehaviour:AddClick(cancelButton, this.OnCancelClick);

    local PreviousButton = m_transform:FindChild("Bg/Previous").gameObject;
    m_LuaBehaviour:AddClick(PreviousButton, this.OnPreviousClick);

    local NextButton = m_transform:FindChild("Bg/Next").gameObject;
    m_LuaBehaviour:AddClick(NextButton, this.OnNextClick);

    m_ScrollViewObj = m_transform:FindChild("Bg/Scroll View").gameObject;
    m_ScrollView = m_ScrollViewObj:GetComponent("ScrollRect");
    this.InitScrollView();
end

--初始化面板--
function RoleInfoPanel.InitScrollView()
    local parent = m_ScrollViewObj.transform:FindChild('Grid');
    for i = 1, m_Count do
        local prefab = Resources.Load("UI/Role/RoleInfoPagePanel");
        local go = GameObject.Instantiate(prefab);
        go.name = 'Item'..tostring(i);
        go.transform:SetParent(parent);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;

        local Text = go.transform:FindChild("Item1/Text"):GetComponent("Text");
        Text.text = tostring(i);
        -- print(tostring(go));
        -- print(tostring(m_LuaBehaviour));
        -- m_LuaBehaviour:AddClick(go, this.OnItemClick);

    --     local label = go.transform:FindChild('Text');
    --     label:GetComponent('Text').text = tostring(i);
    end
end

function RoleInfoPanel.OnPreviousClick(obj)
    if m_Index > 1 then
        m_Index = m_Index - 1;
    end
    this.MoveScrollView();
end

function RoleInfoPanel.OnNextClick(obj)
    if m_Index < m_Count then
        m_Index = m_Index + 1;
    end
    this.MoveScrollView();
end

function RoleInfoPanel.MoveScrollView()
    local x1 = (m_Count - 1);
    local x0 = (m_Index-1);
    local ratio =  1 / x1;
    -- log("m_Index = "..m_Index.." ratio = "..ratio.." x1 = "..x1.." x0 = "..x0);
    DGTween46.DOHorizontalNormalizedPos (m_ScrollView,x0*ratio,0.5,false);
end

function RoleInfoPanel.OnCancelClick(obj)
    destroy(m_gameObject);
end