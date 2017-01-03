LoginView = 
{
	
}

function LoginView:Init()
	self:AddListener()
end

function LoginView:AddListener()
	--Add listener
	EventDelegate.Add(self.LoginButton.onClick, EventDelegate.Callback(LoginView["Login"], self))
	EventDelegate.Add(self.EnterGameButton.onClick, EventDelegate.Callback(LoginView["EnterGame"], self))
	EventDelegate.Add(self.RegisterButton.onClick, EventDelegate.Callback(LoginView["Register"], self))
	EventDelegate.Add(self.SwitchToLoginButton.onClick, EventDelegate.Callback(LoginView["SwitchToLogin"], self))
	EventDelegate.Add(self.SwitchToRegisterButton.onClick, EventDelegate.Callback(LoginView["SwitchToRegister"], self))
end

function LoginView:Login()
	self.Mediator:ConnectToServer(self.LoginAccountLabel.text, self.LoginPasswordLabel.text)
end

function LoginView:EnterGame()
	self.Mediator:EnterGame()
end

function LoginView:Register()

end

function LoginView:SwitchToLogin()
	self.SwitchToLoginButton.gameObject:SetActive(false)
	self.SwitchToRegisterButton.gameObject:SetActive(true)
	self.LoginModule:SetActive(true)
	self.RegisterModule:SetActive(false)
end

function LoginView:SwitchToRegister()
	self.SwitchToLoginButton.gameObject:SetActive(true)
	self.SwitchToRegisterButton.gameObject:SetActive(false)
	self.LoginModule:SetActive(false)
	self.RegisterModule:SetActive(true)
end
