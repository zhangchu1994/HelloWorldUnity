require "Common/define"

RoleList = {};
local this = RoleList;

local m_LuaBehaviour;
local m_transform;
local m_gameObject;

function RoleList.New()
    return this;
end

function RoleList.Awake()

end

function RoleList.Start()

end

function RoleList.Update()

end

function RoleList.OnCreate(obj)
    this.InitView(obj);
end

function RoleList.InitView(obj)
    m_gameObject = obj;
    m_transform =  obj.transform;
    m_LuaBehaviour = obj:GetComponent('LuaBehaviour');
end

