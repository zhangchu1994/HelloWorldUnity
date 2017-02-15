require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

FirstBattleScene = {};
local this = FirstBattleScene;

local panel;
local prompt;
local transform;
local gameObject;
local m_role;
local m_agent
local m_index;
local m_GoFlag;

--构建函数--
function FirstBattleScene.New()
    -- logWarn("MainCity.New--->>");
    return this;
end

function FirstBattleScene.Show()
    -- panelMgr:CreatePanel('login', this.OnCreate,'Login');
end

function FirstBattleScene.Awake()
    log("FirstBattleScene.Awake______________________________");
end

function FirstBattleScene.Start()
    log("FirstBattleScene.Start______________________________");
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

    local go = GameObject.New();
    go.name = "Actor";
    go:AddComponent(typeof(Actor1));
end

function FirstBattleScene.ActorDone()
    m_GoFlag = false;
    local startObj = GameObject.Find("Start");
    local endObj = GameObject.Find("End");

    m_role = GameObject.Find("Ian1970");
    print("FirstBattleScene.Start___________________"..m_role.name);
    m_role.transform.localPosition = Vector3.New(startObj.transform.position.x,startObj.transform.position.y,startObj.transform.position.z); --Vector3.New(-36,0,9.6);
    -- print(tostring( startObj.transform.position));
    -- print(tostring( startObj.transform.localPosition));
    -- print(tostring( actor.transform.localPosition));
    -- actor.transform.localPosition = Vector3.New(100,100,100);
    -- actor.transform.localPosition = Vector3.New(-39,-7,86);

    -- actor.transform.localScale = Vector3.New(1,1,1);

    -- coroutine.start(this.Go);
    this.Go();
    -- startCon


end

function FirstBattleScene.Go()
    -- coroutine.wait(1);  
    m_GoFlag = true;
    -- local endObj = GameObject.Find("End");

    -- m_agent = m_role:GetComponent('NavMeshAgent');
    -- m_agent.speed = 1;
    -- m_agent.stoppingDistance = 0.01; 
    -- m_agent.radius = 3;
    -- m_agent.acceleration = 1;

end

function FirstBattleScene.Update()
    -- if m_GoFlag == true then
    --     local endObj = GameObject.Find("End");
    --     m_role.transform.LookAt( m_role.transform,endObj.transform.localPosition); 
    --     m_agent:SetDestination(endObj.transform.localPosition);  
    --     log("FirstBattleScene.Update______________________________");
    -- end
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
function FirstBattleScene.OnCreate(obj)
    log('FirstBattleScene.OnCreate____________');
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
function FirstBattleScene.InitPanel(objs)
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
function FirstBattleScene.OnItemClick(go)
    log(go.name);
end

--单击事件--
function FirstBattleScene.OnClick(go)
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

function FirstBattleScene.SceneDone(obj)

end

--关闭事件--
function FirstBattleScene.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end
