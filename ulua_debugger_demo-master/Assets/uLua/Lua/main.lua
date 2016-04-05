ZBS = "E:/Program Files/ZeroBraneStudio";
LuaPath = "E:/WorkSpace_Unity/ulua_debugger_demo-master/Assets/uLua/Lua"
package.path = package.path..";./?.lua;"..ZBS.."lualibs/?/?.lua;"..ZBS.."lualibs/?.lua;"..LuaPath.."?.lua;"

--require("mobdebug").start()

GameManager = {};
local this = GameManager;

local game; 
local transform;
local gameObject;

require "Logic/LuaClass"
require "Common/functions"

function GameManager.Awake()
  log('Awake--->>>');
end

--启动事件--
function GameManager.Start()
	warn('Start--->>>');
end

--初始化完成，发送链接服务器信息--
function GameManager.OnInitOK()
  warn('SimpleFramework InitOK--->>>');
end



GameManager.Awake();
