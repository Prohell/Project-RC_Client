using System;

public class CorError
{
	public int errCode;
	public string description;
	public CorError(int errCode, string description = "")
	{
		this.errCode = errCode;
		this.description = description;
	}	
}

public class GDSCorErrorCode
{
	public const int DOWNLOAD_VERSION_CFG = 1;
	public const int PARSE_VERSION_CFG = 2;
	public const int DOWNLOAD_CSV_FILE = 3;
}

public class CorExceptionError : CorError
{
	public CorExceptionError():base(-1, "Exception occurs!")
	{
	}
}