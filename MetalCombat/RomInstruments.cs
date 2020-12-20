using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snes;

namespace MetalCombat
{
	public static class RomInstruments
	{
		public static Instrument[] Instruments;

		public static void Load()
		{
			var position = MetalCombatRom.InstrumentDataAddress;

			Instruments = new Instrument[MetalCombatRom.InstrumentCount];

			for (var instrument = 0; instrument < Instruments.Length; instrument++)
			{
				Instruments[instrument].Value1 = Apu.Memory[position++];
				Instruments[instrument].Value2 = Apu.Memory[position++];
				Instruments[instrument].Value3 = Apu.Memory[position++];
				Instruments[instrument].Value4 = Apu.Memory[position++];
				Instruments[instrument].Value5 = Apu.Memory[position++];
				Instruments[instrument].Value6 = Apu.Memory[position++];
			}
		}

		public struct Instrument
		{
			public int Value1;
			public int Value2;
			public int Value3;
			public int Value4;
			public int Value5;
			public int Value6;
		}
	}
}
