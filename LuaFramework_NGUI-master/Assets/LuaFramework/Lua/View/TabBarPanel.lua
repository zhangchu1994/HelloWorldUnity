--管理器--
TabBarPanel = {};
local this = TabBarPanel;

function TabBarPanel.New()

    return this;
end

function TabBarPanel.init()
    prompt:AddClick(go, this.OnItemClick);
end

function TabBarPanel.OnItemClick(go)
   
end

function TabBarPanel.OnDestroy()
	--logWarn('OnDestroy--->>>');
end
