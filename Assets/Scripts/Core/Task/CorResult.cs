using System;
using System.Collections;
using System.Collections.Generic;

public class CorResult
{
	private List<object> data = new List<object>();
	public List<object> Data
	{
		get { return data; }
	}

	public void Merge(CorResult another)
	{
		if (null != another)
		{
			data.AddRange(another.data);
		}
	}
}