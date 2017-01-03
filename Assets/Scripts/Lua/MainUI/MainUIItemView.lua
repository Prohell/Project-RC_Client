MainUIItemView = 
{
	originalPos,
	m_MainUIView
}

--Store original position at startup
function MainUIItemView:SetPos()
	self.originalPos = self.transform.localPosition
end

function MainUIItemView:OpenComplete()
	self.m_MainUIView:EnableDrawer()
end

function MainUIItemView:CloseComplete()
	self.gameObject:SetActive(false)
	self.m_MainUIView:EnableDrawer()
end