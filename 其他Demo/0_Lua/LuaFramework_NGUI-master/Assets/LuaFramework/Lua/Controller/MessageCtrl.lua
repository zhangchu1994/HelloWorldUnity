
MessageCtrl = {};
local this = MessageCtrl;

local message;
local transform;

--构建函数--
function MessageCtrl.New()
	panelMgr:CreatePanel("GuiCamera",'Message', this.OnCreate);
	return this;
end

function MessageCtrl.Awake()

end

--启动事件--
function MessageCtrl.OnCreate(obj)
	this.gameObject = obj;

	local panel = this.gameObject:GetComponent('UIPanel');
	panel.depth = 10;	--设置纵深--

	message = this.gameObject:GetComponent('LuaBehaviour');
	message:AddClick(MessagePanel.btnClose, this.OnClick);

	-- logWarn("Start lua--->>"..gameObject.name);
end

--单击事件--
function MessageCtrl.OnClick(go)
	destroy(this.gameObject);
end

--关闭事件--
function MessageCtrl.Close()
	panelMgr:ClosePanel(CtrlNames.Message);
end