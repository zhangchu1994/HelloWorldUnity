require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

MainCityUIManager = {};
local this = MainCityUIManager;

local panel;
local prompt;
local transform;
local gameObject;

function MainCityUIManager.New()
    return this;
end

function MainCityUIManager.Awake()
    -- log("MainCityUIManager.Awake______________________________");
end

function MainCityUIManager.Start()
    -- log("MainCityUIManager.Start______________________________");
end

function MainCityUIManager.Show()
    -- panelMgr:CreatePanel('loginperfab', 'LoginPanel','Login', this.OnCreate);
end

function MainCityUIManager.Update()

end

function MainCityUIManager.OnCreate(obj)
    -- log('MainCityUIManager.OnCreate____________');
    gameObject = obj;
    transform = obj.transform;

    panel = transform:GetComponent('loginPanel');
    prompt = transform:GetComponent('LuaBehaviour');

    local loginButton = transform:FindChild("MainCityUIManager").gameObject;

    prompt:AddClick(loginButton, this.OnClick);
    -- resMgr:LoadPrefab('prompt', { 'PromptItem' }, this.InitPanel);
end

-- --初始化面板--
-- function MainCityUIManager.InitPanel(objs)
--     local count = 100; 
--     local parent = PromptPanel.gridParent;
--     for i = 1, count do
--         local go = newObject(objs[0]);
--         go.name = 'Item'..tostring(i);
--         go.transform:SetParent(parent);
--         go.transform.localScale = Vector3.one;
--         go.transform.localPosition = Vector3.zero;
--         prompt:AddClick(go, this.OnItemClick);

--         local label = go.transform:FindChild('Text');
--         label:GetComponent('Text').text = tostring(i);
--     end
-- end

-- --滚动项单击--
-- function MainCityUIManager.OnItemClick(go)
--     log(go.name);
-- end

--单击事件--
function MainCityUIManager.OnClick(go)
    sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone); 
    -- sceneMgr:GoToScene('firstbattlescene',"FirstBattle","FirstBattleScene",this.SceneDone); 


end

function MainCityUIManager.SceneDone(obj)
    
end

--关闭事件--
function MainCityUIManager.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end


