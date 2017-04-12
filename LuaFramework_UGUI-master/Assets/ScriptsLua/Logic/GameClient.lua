require "Common/define"

GameClient = {};
local this = GameClient;
local m_userName;

function GameClient.New()
    return this;
end

function GameClient.SetUserInfo()
	m_userName = "用户1557";
end

function GameClient.GetUserInfo()
	return m_userName;
end