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
local gameObject;

--构建函数--
function HelloWorldCtrl.New()
	logWarn("HelloWorldCtrl.New--->>");
	return this;
end

function HelloWorldCtrl.Awake()
	logWarn("HelloWorldCtrl.Awake--->>!!!!!!!!!!!!!!!!!!!!!!");
	-- panelMgr:CreatePanel('Prompt', this.OnCreate);
    print("11111111111111111111111111111111111111111");
    panelMgr:CreatePanel('HelloWorld', this.OnCreate);
end

--启动事件--
function HelloWorldCtrl.OnCreate(obj)
	gameObject = obj;
	transform = obj.transform;

    soundMgr:PlayBacksound('Sound/shijie',true);
	
    panel = transform:GetComponent('UIPanel');
	prompt = transform:GetComponent('LuaBehaviour');
	logWarn("Start lua--->>"..gameObject.name);

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
    
end

--单击事件--
function HelloWorldCtrl.OnClick(go)
	if TestProtoType == ProtocalType.BINARY then
		this.TestSendBinary();
	end
	if TestProtoType == ProtocalType.PB_LUA then
		this.TestSendPblua();
	end
	if TestProtoType == ProtocalType.PBC then
		this.TestSendPbc();
	end
	if TestProtoType == ProtocalType.SPROTO then
		this.TestSendSproto();
	end
	logWarn("OnClick---->>>"..go.name);
end

--测试发送SPROTO--
function HelloWorldCtrl.TestSendSproto()
    local sp = sproto.parse [[
    .Person {
        name 0 : string
        id 1 : integer
        email 2 : string

        .PhoneNumber {
            number 0 : string
            type 1 : integer
        }

        phone 3 : *PhoneNumber
    }

    .AddressBook {
        person 0 : *Person(id)
        others 1 : *Person
    }
    ]]

    local ab = {
        person = {
            [10000] = {
                name = "Alice",
                id = 10000,
                phone = {
                    { number = "123456789" , type = 1 },
                    { number = "87654321" , type = 2 },
                }
            },
            [20000] = {
                name = "Bob",
                id = 20000,
                phone = {
                    { number = "01234567890" , type = 3 },
                }
            }
        },
        others = {
            {
                name = "Carol",
                id = 30000,
                phone = {
                    { number = "9876543210" },
                }
            },
        }
    }
    local code = sp:encode("AddressBook", ab)
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Protocal.Message);
    buffer:WriteByte(ProtocalType.SPROTO);
    buffer:WriteBuffer(code);
    networkMgr:SendMessage(buffer);
end

--测试发送PBC--
function HelloWorldCtrl.TestSendPbc()
    local path = Util.DataPath.."lua/3rd/pbc/addressbook.pb";

    local addr = io.open(path, "rb")
    local buffer = addr:read "*a"
    addr:close()
    protobuf.register(buffer)

    local addressbook = {
        name = "Alice",
        id = 12345,
        phone = {
            { number = "1301234567" },
            { number = "87654321", type = "WORK" },
        }
    }
    local code = protobuf.encode("tutorial.Person", addressbook)
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Protocal.Message);
    buffer:WriteByte(ProtocalType.PBC);
    buffer:WriteBuffer(code);
    networkMgr:SendMessage(buffer);
end

--测试发送PBLUA--
function HelloWorldCtrl.TestSendPblua()
    local login = login_pb.LoginRequest();
    login.id = 2000;
    login.name = 'game';
    login.email = 'jarjin@163.com';
    local msg = login:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Protocal.Message);
    buffer:WriteByte(ProtocalType.PB_LUA);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
end

--测试发送二进制--
function HelloWorldCtrl.TestSendBinary()
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Protocal.Message);
    buffer:WriteByte(ProtocalType.BINARY);
    buffer:WriteString("ffff我的ffffQ靈uuu");
    buffer:WriteInt(200);
    networkMgr:SendMessage(buffer);
end

--关闭事件--
function HelloWorldCtrl.Close()
	panelMgr:ClosePanel(CtrlNames.HelloWorld);
end