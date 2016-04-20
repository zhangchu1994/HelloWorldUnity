require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

MainCityCtrl = {};
local this = MainCityCtrl;

local panel;
local prompt;
local transform;
-- local gameObject;
-- local classname;

--构建函数--
function MainCityCtrl.New()
	logWarn("MainCityCtrl.New--->>");
	return this;
end

function MainCityCtrl.Awake()
	logWarn("MainCityCtrl.Awake--->>!!!!!!!!!!!!!!!!!!!!!!");
	-- panelMgr:CreatePanel('Prompt', this.OnCreate);
    -- print("11111111111111111111111111111111111111111");
    classname = "MainCityCtrl";
    panelMgr:CreatePanel1("MainCity3DCamera","Prefabs/MainCityPlane(3D)", this.OnCreate);
end

--启动事件--
function MainCityCtrl.OnCreate(obj)
	this.gameObject = obj;
	transform = obj.transform;

    -- soundMgr:PlayBacksound('Sound/shijie',true);
	
    panel = transform:GetComponent('UIPanel');
	prompt = transform:GetComponent('LuaBehaviour');
	logWarn("Start lua--->>"..this.gameObject.name);

	this.InitPanel();	--初始化面板--
	-- prompt:AddClick(HelloWorldPanel.btnOpen, this.OnClick);
end

--初始化面板--
function MainCityCtrl.InitPanel()
	-- panel.depth = 1;	--设置纵深--
	-- local parent = HelloWorldPanel.gridParent;
	-- local itemPrefab = prompt:LoadAsset('HelloWorldItem');
	-- for i = 1, 8 do
	-- 	local go = newObject(itemPrefab);
	-- 	go.name = tostring(i);
	-- 	go.transform.parent = parent;
	-- 	go.transform.localScale = Vector3.one;
	-- 	go.transform.localPosition = Vector3.zero;
	-- 	prompt:AddClick(go, this.OnItemClick);

	-- 	local goo = go.transform:FindChild('Label');
	-- 	goo:GetComponent('UILabel').text = i;
	-- end
	-- local grid = parent:GetComponent('UIGrid');
	-- grid:Reposition();
	-- grid.repositionNow = true;
	-- parent:GetComponent('WrapGrid'):InitGrid();
end

--滚动项单击事件--
function MainCityCtrl.OnItemClick(go)
	log(go.name);
    destroy(this.gameObject);
end

--单击事件--
function MainCityCtrl.OnClick(go)

end

--关闭事件--
function MainCityCtrl.Close()
	panelMgr:ClosePanel(CtrlNames.HelloWorld);
end