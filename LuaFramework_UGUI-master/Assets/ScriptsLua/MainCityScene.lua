require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

MainCityScene = {};
local this = MainCityScene;

local panel;
local prompt;
local transform;
local gameObject;
local m_role;
local m_agent
local m_index;

--构建函数--
function MainCityScene.New()
    -- logWarn("MainCity.New--->>");
    return this;
end

function MainCityScene.Show()
    -- panelMgr:CreatePanel('login', this.OnCreate,'Login');
end

function MainCityScene.Awake()
    log("MainCityScene.Awake______________________________");
end

function MainCityScene.Start()
    log("MainCityScene.Start______________________________");
    -- m_role = GameObject.Find("Role");
    -- m_agent = m_role:GetComponent('NavMeshAgent');
    -- m_index = 1;
    -- m_agent.speed = 100;
    -- m_agent.stoppingDistance = 0.01; 
    -- m_agent.radius = 3;
    -- m_agent.acceleration = 100;

    -- local tree = GameObject.Find("Tree"..tostring(m_index));
    -- m_agent:SetDestination(tree.transform.position);  

    -- local ctrl = CtrlManager.GetCtrl(CtrlNames.Actor);
    -- if ctrl ~= nil and AppConst.ExampleMode == 1 then
    --     log("Game.OnInitOK_____________ctrl:Show");
    --    ctrl:Show();
    -- end

    -- local go = GameObject.New();
    -- go.name = "Actor";
    -- local actor = go:AddComponent(typeof(Actor));
end

function MainCityScene.Update()
    -- log("MainCityScene.Update______________________________");
    -- local role = GameObject.Find("Role");

    -- log(role.transform.position.x);--+" "+role.transform.position.y+" "+role.transform.position.z

    -- role.transform.position = Vector3.New(role.transform.position.x,role.transform.position.y+1,role.transform.position.z);
    -- role.transform.Translate(role.transform,Vector3.forward * Time.deltaTime);--, Space.World

    -- if m_agent.remainingDistance <= 0 then--m_agent.pathStatus == NavMeshPathStatus.PathComplete and 
    --     log("到了  = "..tostring(m_index));
    --     if m_index >= 4 then
    --         m_index = 0;
    --     end
    --     m_index = m_index+1;
    -- else
    --     log(m_agent.remainingDistance);
    -- end
    -- m_agent.enabled = true;
    -- local tree = GameObject.Find("Tree"..tostring(m_index));
    -- m_agent:SetDestination(tree.transform.position);  

end

--启动事件--
function MainCityScene.OnCreate(obj)
    log('MainCityScene.OnCreate____________');
    gameObject = obj;
    transform = obj.transform;

    panel = transform:GetComponent('loginPanel');
    prompt = transform:GetComponent('LuaBehaviour');
    logWarn("Start lua--->>"..gameObject.name);

    local loginButton = transform:FindChild("Login").gameObject;

    prompt:AddClick(loginButton, this.OnClick);
    -- resMgr:LoadPrefab('prompt', { 'PromptItem' }, this.InitPanel);
end

--初始化面板--
function MainCityScene.InitPanel(objs)
    local count = 100; 
    local parent = PromptPanel.gridParent;
    for i = 1, count do
        local go = newObject(objs[0]);
        go.name = 'Item'..tostring(i);
        go.transform:SetParent(parent);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        prompt:AddClick(go, this.OnItemClick);

        local label = go.transform:FindChild('Text');
        label:GetComponent('Text').text = tostring(i);
    end
end

--滚动项单击--
function MainCityScene.OnItemClick(go)
    log(go.name);
end

--单击事件--
function MainCityScene.OnClick(go)
    -- if TestProtoType == ProtocalType.BINARY then
    --     this.TestSendBinary();
    -- end
    -- if TestProtoType == ProtocalType.PB_LUA then
    --     this.TestSendPblua();
    -- end
    -- if TestProtoType == ProtocalType.PBC then
    --     this.TestSendPbc();
    -- end
    -- if TestProtoType == ProtocalType.SPROTO then
    --     this.TestSendSproto();
    -- end
    logWarn("OnClick---->>>123"..go.name);
    -- SceneManager.LoadScene('MainCity');
    sceneMgr:GoToScene('maincity', this.SceneDone,"MainCity");  
end

function MainCityScene.SceneDone(obj)

end

--关闭事件--
function MainCityScene.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end
