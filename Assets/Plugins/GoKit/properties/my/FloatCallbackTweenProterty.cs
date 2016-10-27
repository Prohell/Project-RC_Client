public class FloatCallbackTweenProterty : AbstractTweenProperty {

	protected float _startValue;
	protected float _endValue;
	protected float _diffValue;
	protected System.Action<float> _callback;
	
	public FloatCallbackTweenProterty(float startValue, float endValue, System.Action<float> callback, bool isRelative = false ) : base( isRelative )
	{	
		_startValue = startValue;
		_endValue = endValue;
		_callback = callback;
	}
	
	public override bool validateTarget( object target )
	{
		return true;
	}
	
	public override void prepareForUse()
	{
		// if this is a from tween we need to swap the start and end values
		if( _ownerTween.isFrom )
		{
			float tmp = _startValue;
			_startValue = _endValue;
			_endValue = tmp;
		}
		
		// setup the diff value
		if( _isRelative && !_ownerTween.isFrom )
			_diffValue = _endValue;
		else
			_diffValue = _endValue - _startValue;
	}
	
	
	public override void tick( float totalElapsedTime )
	{
		var easedValue = _easeFunction( totalElapsedTime, _startValue, _diffValue, _ownerTween.duration );
		_callback.Invoke(easedValue);
	}
}
