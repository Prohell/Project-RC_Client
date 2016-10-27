using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit, Size=8, CharSet=CharSet.Ansi)]
public struct HexData 
{
	[FieldOffset(0)]public byte nMat; 
	[FieldOffset(1)]public byte nLev;
}