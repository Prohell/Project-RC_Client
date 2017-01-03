CityView = 
{
	targetHighlight = nil
}

function CityView:Start()
	self.CameraMove.gameObjectClick = self.CameraMove.gameObjectClick + System.Action_UnityEngine_GameObject(self["ClickBuilding"], self, true);
end

--选中建筑物后
function CityView:ClickBuilding(obj)
	if(obj.tag == "Building")
	then
		Logger.Log(obj.name);
		local pos = Vector3.New (obj.transform.position.x, 
			self.CameraMove.limitBounds.center.y + self.CameraMove.limitBounds.extents.y, 
			obj.transform.position.z - Mathf.Tan (self.CameraMove.angleXRange.y) * (self.CameraMove.limitBounds.center.y + self.CameraMove.limitBounds.extents.y))

		local angle = Vector3.New (self.CameraMove.angleXRange.y, 0, 0)

		self.CameraMove.transform:DOMove(pos, 0.5, false):SetEase(DG.Tweening.Ease.OutCirc)
		self.CameraMove.cam.transform:DORotate(angle, 0.5, DG.Tweening.RotateMode.Fast):SetEase(DG.Tweening.Ease.OutCirc);

		targetHighlight = obj:GetComponent(Type.GetType("HighlighterController"..", Assembly-CSharp-firstpass"))

		targetHighlight:Fire2 ()
	end
end


function CityView:Update ()
	if (targetHighlight ~= nil)
	then
		targetHighlight:MouseOver ()
	end
end


