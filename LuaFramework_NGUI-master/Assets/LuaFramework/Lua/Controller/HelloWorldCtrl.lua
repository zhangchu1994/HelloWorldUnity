require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

HelloWorldCtrl = {};
local this = HelloWorldCtrl;

local panel;
local prompt;
local transform;

--构建函数--
function HelloWorldCtrl.New()
	logWarn("HelloWorldCtrl.New--->>");
	return this;
end

function HelloWorldCtrl.Awake()
	logWarn("HelloWorldCtrl.Awake--->>!!!!!!!!!!!!!!!!!!!!!!");
	-- panelMgr:CreatePanel('Prompt', this.OnCreate);
    -- print("11111111111111111111111111111111111111111");
    panelMgr:CreatePanel("LoginUITag",'HelloWorld', this.OnCreate);
end

--启动事件--
function HelloWorldCtrl.OnCreate(obj)
	this.gameObject = obj;
	transform = obj.transform;

    soundMgr:PlayBacksound('Sound/shijie',true);
	
    panel = transform:GetComponent('UIPanel');
	prompt = transform:GetComponent('LuaBehaviour');
	logWarn("Start lua--->>"..this.gameObject.name);

	this.InitPanel();	--初始化面板--
	prompt:AddClick(HelloWorldPanel.btnOpen, this.OnClick);
end

--初始化面板--
function HelloWorldCtrl.InitPanel()
	panel.depth = 1;	--设置纵深--
	local parent = HelloWorldPanel.gridParent;
	local itemPrefab = prompt:LoadAsset('HelloWorldItem');
	for i = 1, 8 do
		local go = newObject(itemPrefab);
		go.name = tostring(i);
		go.transform.parent = parent;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = Vector3.zero;
		prompt:AddClick(go, this.OnItemClick);

		local goo = go.transform:FindChild('Label');
		goo:GetComponent('UILabel').text = i;
	end
	local grid = parent:GetComponent('UIGrid');
	grid:Reposition();
	grid.repositionNow = true;
	parent:GetComponent('WrapGrid'):InitGrid();
end

--滚动项单击事件--
function HelloWorldCtrl.OnItemClick(go)
	log(go.name);
    destroy(this.gameObject);
end

--单击事件--
function HelloWorldCtrl.OnClick(go)

end

--关闭事件--
function HelloWorldCtrl.Close()
	panelMgr:ClosePanel(CtrlNames.HelloWorld);
end