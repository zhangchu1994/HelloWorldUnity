require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

BattleField = {};
local this = BattleField;

local panel;
local prompt;
local transform;
local gameObject;

--构建函数--
function BattleField.New()
    logWarn("Login.New--->>");
    return this;
end

function BattleField.Show()
    panelMgr:CreatePanel('login', this.OnCreate,'Login');
end

function BattleField.Awake()
    logWarn("BattleField.Awake---___________________________________________>>");
    -- panelMgr:CreatePanel('Prompt', this.OnCreate);
end

function BattleField.Start()

end

--启动事件--
function BattleField.OnCreate(obj)
    log('BattleField.OnCreate____________');
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
function BattleField.InitPanel(objs)
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
function BattleField.OnItemClick(go)
    log(go.name);
end

--单击事件--
function BattleField.OnClick(go)
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
    panelMgr:GoToScene('maincity', this.SceneDone,"MainCity");  
end

function BattleField.SceneDone(obj)

end

--关闭事件--
function BattleField.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end
