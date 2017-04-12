require "Common/define"

ShopPanel = {};
local this = ShopPanel;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;

function ShopPanel.New()
    return this;
end

function ShopPanel.Awake()

end

function ShopPanel.Start()

end

function ShopPanel.Update()

end

function ShopPanel.OnCreate(obj)
    -- log('ShopPanel.OnCreate____________');
    this.InitView(obj);
end

function ShopPanel.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');

    m_ScrollView = m_transform:FindChild("Scroll View").gameObject;
    this.InitScrollView();
end

--初始化面板--
function ShopPanel.InitScrollView()
    local count = 20; 
    local parent = m_ScrollView.transform:FindChild('Background/Grid');
    for i = 1, count do
        local prefab = Resources.Load("UI/Shop/ShopPanelItem");
        local go = GameObject.Instantiate(prefab);
        go.name = 'Item'..tostring(i);
        go.transform:SetParent(parent);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        -- -- print(tostring(go));
        -- -- print(tostring(m_LuaBehaviour));
        -- m_LuaBehaviour:AddClick(go, this.OnItemClick);

    --     local label = go.transform:FindChild('Text');
    --     label:GetComponent('Text').text = tostring(i);
    end
end