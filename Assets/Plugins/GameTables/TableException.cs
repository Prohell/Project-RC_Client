using System.IO;

public sealed class TableException : IOException
{

    internal TableException(string message)
        : base(message) { }


    internal static TableException ErrorReader(string fort, params object[] args)
    {
        string error = string.Format(fort, args);
        return new TableException(error);
    }
}