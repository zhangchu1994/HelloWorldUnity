local transform;
local gameObject;

HelloWorldPanel = {};
local this = HelloWorldPanel;

--启动事件--
function HelloWorldPanel.Awake(obj)
	gameObject = obj;
	transform = obj.transform;

	this.InitPanel();
	logWarn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function HelloWorldPanel.InitPanel()
	this.btnOpen = transform:FindChild("Open").gameObject;
	this.gridParent = transform:FindChild('ScrollView/Grid');
end

--单击事件--
function HelloWorldPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end