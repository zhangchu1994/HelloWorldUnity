require "Common/define"

require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

BigWorld = {};
local this = BigWorld;

local m_panel;
local m_LuaBehaviour;
local m_transform;
local m_gameObject;
local m_firstCreate = false;
local m_ScrollView;

function BigWorld.New()
    return this;
end

function BigWorld.Awake()
    -- log("BigWorld.Awake______________________________");
end

function BigWorld.Start()
    -- log("BigWorld.Start______________________________");
    this.InitView();
end

function BigWorld.Show()
    -- panelMgr:CreatePanel('loginperfab', 'LoginPanel','Login', this.OnCreate);
end

function BigWorld.Update()
    -- if m_firstCreate == false then
    --     m_firstCreate = true;
    --     this.InitView();
    -- end
end

function BigWorld.InitView()
    log('BigWorld.OnCreate____________');
    m_gameObject = GameObject.Find("UICamera/Canvas");
    m_transform = m_gameObject.transform;
    
    m_LuaBehaviour = m_transform:GetComponent('LuaBehaviour');
    local button = m_gameObject.Find("Button");
    m_LuaBehaviour:AddClick(button, this.OnCloseClick);
end

function BigWorld.OnCloseClick()
    -- MainCityUIManager.m_firstCreate = false;
    sceneMgr:GoToScene("maincity","MainCity","MainCityScene",this.OnLoadScene);  
end

function BigWorld.OnLoadScene(obj)
    
end




