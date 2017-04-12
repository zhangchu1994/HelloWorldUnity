require "Common/define"



MainCityUIManager = {};
local this = MainCityUIManager;

-- local m_panel;
local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_Canvas;
local m_CanvasTransform;
local m_firstCreate = false;


function MainCityUIManager.New()
    return this;
end

function MainCityUIManager.Awake()

end

function MainCityUIManager.Start()
    this.InitView();
end

function MainCityUIManager.Update()
    print("Update m_firstCreate = "..tostring(m_firstCreate))
    if m_firstCreate == false then
        m_firstCreate = true;
        this.InitView();
    end
end

function MainCityUIManager.InitView()
    m_gameObject = GameObject.Find("UICamera/MainCanvas/MainCityPanel");
    m_transform = m_gameObject.transform;

    m_Canvas = GameObject.Find("MainCanvas");
    m_CanvasTransform = m_Canvas.transform;

    m_LuaBehaviour = m_transform:GetComponent('LuaBehaviour');
    

    local UserName = GameClient.GetUserInfo();
    local nameObj = m_transform:FindChild("HeadBar/Name"):GetComponent('Text');
    nameObj.text = UserName;

    local levelObj = m_transform:FindChild("HeadBar/Level"):GetComponent('Text');
    levelObj.text = StringTable.string24;

    for i=1,7 do
        local loginButton = m_transform:FindChild("Left"..tostring(i)).gameObject;
        m_LuaBehaviour:AddClick(loginButton, this.OnLeftClick);
    end

    for i=1,5 do
        local loginButton = m_transform:FindChild("Down"..tostring(i)).gameObject;
        m_LuaBehaviour:AddClick(loginButton, this.OnDownClick);
    end

    for i=1,7 do
        local loginButton = m_transform:FindChild("Below"..tostring(i)).gameObject;
        m_LuaBehaviour:AddClick(loginButton, this.OnBelowClick);
    end
end

--单击事件--
function MainCityUIManager.OnLeftClick(go)
    -- log("________________________"..go.name);--
    if (go.name == "Left6") then
        sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 
    elseif (go.name == "Left7") then
        local BossPanel = m_CanvasTransform:FindChild("BossPanel").gameObject;
        BossPanel.SetActive(BossPanel,true);
    elseif (go.name == "Left2") then
        this.InitGetItemAlert();
    elseif (go.name == "Left3") then
        this.InitGetItemAlert();
    elseif (go.name == "Left4") then
        sceneMgr:GoToScene('bigworld',"BigWorld","BigWorldScene",this.SceneDone); 
    elseif (go.name == "Left5") then
        local ShopPanel = m_CanvasTransform:FindChild("ShopPanel").gameObject;
        ShopPanel.SetActive(ShopPanel,true);
    end
end

function MainCityUIManager.OnBelowClick(go)
    if (go.name == "Below1") then
        panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','PartnerMainPanel','PartnerMainPanel',Vector3.New(0,0,0), PartnerMainPanel.OnCreate);
    elseif (go.name == "Below2") then
        -- panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','SkillMainPanel','SkillMainPanel',Vector3.New(0,0,0), SkillMainPanel.OnCreate);
    elseif (go.name == "Below3") then
        -- panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','SmithMainPanel','SmithMainPanel',Vector3.New(0,0,0), SmithMainPanel.OnCreate);
    elseif (go.name == "Below4") then
        panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','BossMainPanel','BossMainPanel',Vector3.New(0,0,0), BossMainPanel.OnCreate);
    elseif (go.name == "Below5") then
        panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','EncounterMainPanel','EncounterMainPanel',Vector3.New(0,0,0), EncounterMainPanel.OnCreate);
    elseif (go.name == "Below6") then
        panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','InstanceMainPanel','InstanceMainPanel',Vector3.New(0,0,0), InstanceMainPanel.OnCreate);
    elseif (go.name == "Below7") then
        -- panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','ShopMainPanel','ShopMainPanel',Vector3.New(0,0,0), ShopMainPanel.OnCreate);
    end
end

function MainCityUIManager.OnDownClick(go)
    -- log("MainCityUIManager.OnDownClick______________"..go.name)
    if (go.name == "Down1") then
        panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','RoleMainPanel','RoleMainPanel',Vector3.New(0,0,0), RoleMainPanel.OnCreate);
    elseif (go.name == "Down2") then
        panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','SkillMainPanel','SkillMainPanel',Vector3.New(0,0,0), SkillMainPanel.OnCreate);
    elseif (go.name == "Down3") then
        panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','SmithMainPanel','SmithMainPanel',Vector3.New(0,0,0), SmithMainPanel.OnCreate);
    elseif (go.name == "Down4") then
        panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','BagMainPanel','BagMainPanel',Vector3.New(0,0,0), BagMainPanel.OnCreate);
    elseif (go.name == "Down5") then
        panelMgr:CreatePanel('UI/Common/SystemMainPanel', 'UICamera/SystemCanvas','ShopMainPanel','ShopMainPanel',Vector3.New(0,0,0), ShopMainPanel.OnCreate);
    end
end

function MainCityUIManager.InitGetItemAlert()
    local prefab = Resources.Load("UI/GetItemAlert");
    local go = GameObject.Instantiate(prefab);
    -- go.name = 'Item'..tostring(i);
    go.transform:SetParent(m_CanvasTransform);
    go.transform.localScale = Vector3.one;
    go.transform.localPosition = Vector3.zero;
    local LuaBehaviour = go.transform:GetComponent('LuaBehaviour');
    -- LuaBehaviour.luaName = "MainCityUIManager";
    LuaBehaviour:AddiTween(go,"iTweenDone")


-- -- //  iTween.MoveBy(t,new Vector3(0,10,0),0.3f);
--     iTween.ScaleBy(t,new Vector3(2,2,2),0.5f);
    -- iTween.MoveBy(go,Vector3.New(0,100,0),0.5);
end

function MainCityUIManager.iTweenDone(obj)
    GameObject.Destroy(obj);
end

function MainCityUIManager.SceneDone(obj)
    
end

--关闭事件--
function MainCityUIManager.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end


