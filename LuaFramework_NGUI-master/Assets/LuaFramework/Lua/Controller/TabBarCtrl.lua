require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"
require "Controller/PromptCtrl"
require "Controller/HelloWorldCtrl"
require "Controller/MessageCtrl"
require "Controller/MainCityCtrl"
require "Logic/CtrlManager"



TabBarCtrl = {};
local this = TabBarCtrl;

local panel;
local prompt;
local transform;
local gameObject;

--构建函数--
function TabBarCtrl.New()
	print("TabBarCtrl.New--->>");
	return this;
end



function TabBarCtrl.Awake()
	print("TabBarCtrl.Awake--->>!!!!!!!!!!!!!!!!!!!!!!");
	-- panelMgr:CreatePanel('Prompt', this.OnCreate);
    -- print("11111111111111111111111111111111111111111");
    classname = "TabBarCtrl";
    panelMgr:CreatePanel1("TabBarCamera","Prefabs/TabBarPanel","TabBarCtrl", this.OnCreate);--Controller/
end

function TabBarCtrl.Update()
    print("TabBarCtrl.Update--->>"..312312312);
end

--启动事件--
function TabBarCtrl.OnCreate(obj)
	this.gameObject = obj;
	transform = obj.transform;



    -- soundMgr:PlayBacksound('Sound/shijie',true);
	
    panel = transform:GetComponent('UIPanel');
	prompt = transform:GetComponent('LuaBehaviour');
	print("Start lua--->>"..this.gameObject.name);

    for i=1,5 do
        local btnOpen1 = transform:FindChild("Open"..i).gameObject;
        if i == 5 then
            prompt:AddClick(btnOpen1, this.OnClick1);
        else
            prompt:AddClick(btnOpen1, this.OnClick);
        end
    end

	this.InitPanel();	--初始化面板--
	-- prompt:AddClick(TabBarCtrl.btnOpen, this.OnClick);
end

--初始化面板--
function TabBarCtrl.InitPanel()
	-- panel.depth = 1;	--设置纵深--
	-- local parent = TabBarCtrl.gridParent;
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
function TabBarCtrl.OnItemClick(go)
    print("__________________________________________");
end

--单击事件--
function TabBarCtrl.OnClick(go)
    print("__________________________________________"..go.name);
    if go.name == "Open1" then
        this.currentView = PromptCtrl.New();--CtrlManager.GetCtrl(CtrlNames.Prompt);--
    elseif go.name == "Open2" then
        this.currentView = HelloWorldCtrl.New();--CtrlManager.GetCtrl(CtrlNames.HelloWorld);--
    elseif go.name == "Open3" then
        this.currentView = MessageCtrl.New();--CtrlManager.GetCtrl(CtrlNames.Message);--
    elseif go.name == "Open4" then
        this.currentView = MainCityCtrl.New();
    end
    
    if this.currentView ~= nil and AppConst.ExampleMode then
        this.currentView:Awake();
    end
    print(this.currentView.classname);
    -- for k,v in pairs(currentView) do
    --     print(k,v)
    -- end
end

function TabBarCtrl.OnClick1(go)
    print(this.currentView.classname);
    if this.currentView ~= nil and this.currentView.gameObject ~= nil then
        -- currentView.gameObject.SetActive(false);
        GameObject.Destroy(this.currentView.gameObject);
        this.currentView = nil;
    end
end

--关闭事件--
function TabBarCtrl.Close()
	panelMgr:ClosePanel(CtrlNames.HelloWorld);
end