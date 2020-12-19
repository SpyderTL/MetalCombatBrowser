using System;

namespace Snes
{
	internal static class SnesApu
	{
		internal static void Load(int source, int destination, int length)
		{
			Array.Copy(Snes.Memory, source, Apu.Memory, destination, length);
		}
	}
}