using System;
using System.Diagnostics;
using System.Linq;
using MetalCombat;

namespace Midi
{
	public static class MidiPlayer
	{
		public static int Song;

		public static int MasterVolume;

		public static int[] Notes;
		public static int[] Volume;
		public static int[] Pan;
		public static int[] Instruments;
		public static int[] Drums;
		public static int[] NoteOffsets;
		public static int[] Transpose;
		public static int[] Tuning;
		public static int[] Portamento;

		public static void Start()
		{
			MasterVolume = 0xff;
			Notes = new int[8];
			Pan = Enumerable.Repeat(10, 8).ToArray();
			Volume = Enumerable.Repeat(0xFF, 8).ToArray();
			Instruments = Enumerable.Repeat(48, 8).ToArray();
			Drums = new int[8];
			NoteOffsets = new int[8];
			Transpose = new int[8];
			Tuning = new int[8];
			Portamento = new int[8];

			Midi.Enable();

			for (var channel = 0; channel < 8; channel++)
			{
				Midi.ControlChange(channel, 123, 0);
				Midi.ProgramChange(channel, Instruments[channel]);
				Midi.ControlChange(channel, 0x5b, 127);
				Midi.ControlChange(channel, 0x5c, 127);
				Midi.ControlChange(channel, 0x5d, 127);
				Midi.ControlChange(channel, 0x5e, 127);
				Midi.ControlChange(channel, 0x5f, 127);

				Midi.ControlChange(channel, Midi.Controls.Portamento, 1);
			}
		}

		public static void Stop()
		{
			for (var channel = 0; channel < 8; channel++)
				Midi.ControlChange(channel, 123, 0);

			Midi.Disable();
		}

		public static void Update()
		{
			UpdateVolume();

			for (var channel = 0; channel < 8; channel++)
			{
				UpdateInstruments(channel);
				UpdateVolume(channel);
				UpdatePan(channel);
				UpdateTuning(channel);
				UpdateNotes(channel);
			}
		}

		private static void UpdateTuning(int channel)
		{
			if (Tuning[channel] != SongPlayer.ChannelTuning[channel])
			{
				int value = 0x2000 + (int)((SongPlayer.ChannelTuning[channel] / 255.0f) * 0x1000);
				Midi.PitchBendChange(channel, value);
				Tuning[channel] = SongPlayer.ChannelTuning[channel];
			}
		}

		private static void UpdateNotes(int channel)
		{
			if (SongPlayer.ChannelNotes[channel] == 0 && Notes[channel] != 0)
			{
				if (Drums[channel] == 0)
					Midi.NoteOff(channel, Notes[channel] + NoteOffsets[channel] + SongPlayer.ChannelTranspose[channel], 0);
				else
					Midi.NoteOff(9, Drums[channel], 0);

				Notes[channel] = SongPlayer.ChannelNotes[channel];
			}
			
			if (SongPlayer.ChannelNoteStart[channel])
			{
				if (SongPlayer.ChannelNotes[channel] != 0)
				{
					if (SongPlayer.ChannelPortamento[channel] != Portamento[channel])
					{
						Midi.ControlChange(channel, Midi.Controls.PortamentoEnable, SongPlayer.ChannelPortamento[channel] == 0 ? 0 : 127);
						Portamento[channel] = SongPlayer.ChannelPortamento[channel];
					}

					if (Notes[channel] != 0)
						Midi.NoteOff(channel, Notes[channel] + NoteOffsets[channel] + SongPlayer.ChannelTranspose[channel], 0);

					if (SongPlayer.ChannelNotes[channel] != 0)
						//Midi.NoteOn(channel, SongPlayer.ChannelNotes[channel] + NoteOffsets[channel] + SongPlayer.ChannelTranspose[channel], (int)((SongPlayer.ChannelVelocities[channel] / 15.0f) * 127.0f));
						Midi.NoteOn(channel, SongPlayer.ChannelNotes[channel] + NoteOffsets[channel] + SongPlayer.ChannelTranspose[channel], (int)(((SongPlayer.ChannelVelocities[channel] + 1) / 64.0f) * 127.0f));
						//Midi.NoteOn(channel, SongPlayer.ChannelNotes[channel] + NoteOffsets[channel] + SongPlayer.ChannelTranspose[channel], 127);
				}
				else
				{
					//if (Notes[channel] != 0)
					//	Midi.NoteOff(9, Drums[channel], 0);

					var note = SongPlayer.ChannelInstruments[channel] == 0 ?
						36 :
						38;

					//Midi.NoteOn(9, 38, (int)((SongPlayer.ChannelVelocities[channel] / 15.0f) * 127.0f));
					Midi.NoteOn(9, note, (int)(((SongPlayer.ChannelVelocities[channel] + 1) / 64.0f) * 127.0f));
					//Midi.NoteOn(9, 38, 127);

					Midi.NoteOff(9, note, 0);
				}

				Notes[channel] = SongPlayer.ChannelNotes[channel];
			}
		}

		private static void UpdatePan(int channel)
		{
			if (Pan[channel] != SongPlayer.ChannelPan[channel])
			{
				var value = (int)((SongPlayer.ChannelPan[channel] / 20.0) * 127.0);

				Midi.ControlChange(channel, Midi.Controls.Pan, value);
				Pan[channel] = SongPlayer.ChannelPan[channel];
			}
		}

		private static void UpdateVolume()
		{
			if (MasterVolume != SongPlayer.Volume)
			{
				for (var channel = 0; channel < 8; channel++)
				{
					var value = (int)((Volume[channel] / (double)0xff) * (SongPlayer.Volume / (double)0xff) * 0x7f);

					Midi.ControlChange(channel, 7, value);
				}

				MasterVolume = SongPlayer.Volume;
			}
		}

		private static void UpdateVolume(int channel)
		{
			if (Volume[channel] != SongPlayer.ChannelVolume[channel])
			{
				var value = (int)((SongPlayer.ChannelVolume[channel] / (double)0xff) * (SongPlayer.Volume / (double)0xff) * 0x7f);

				Midi.ControlChange(channel, 7, value);
				Volume[channel] = SongPlayer.ChannelVolume[channel];
			}
		}

		private static void UpdateInstruments(int channel)
		{
			if (Instruments[channel] != SongPlayer.ChannelInstruments[channel])
			{
				if (Notes[channel] != 0)
				{
					Midi.NoteOff(channel, Notes[channel] + NoteOffsets[channel], 0);
				}

				var instrument = 80;
				var offset = 12;

				switch (SongPlayer.ChannelInstruments[channel])
				{
					case 0x06:
						//instrument = 121;
						instrument = 122;
						offset = 0;
						break;

					case 0x10:
						instrument = 29;
						break;

					case 0x11:
						instrument = 38;
						//instrument = 80;
						//instrument = 36;
						//instrument = 87;
						break;

					case 0x12:
						instrument = 18;
						//instrument = 20;
						//instrument = 82;
						break;

					case 0x13:
						//instrument = 83;
						//instrument = 50;
						instrument = 48;
						offset = 24;
						break;

					case 0x14:
						instrument = 112;
						offset = 36;
						break;

					case 0x15:
						//instrument = 38;
						instrument = 29;
						offset = 12;
						break;

					case 0x1A:
						//instrument = 30;
						instrument = 29;
						offset = 12;
						break;

					case 0x1B:
						instrument = 38;
						//instrument = 39;
						offset = 24;
						break;

					case 0x1D:
						instrument = 49;
						//instrument = 39;
						offset = 24;
						break;

					case 0x36:
						//instrument = 56;
						instrument = 61;
						//instrument = 63;
						//instrument = 51;
						offset = 24;
						break;

					case 0x3A:
						if (Song == 0x03)
						{
							instrument = 114;
							offset = 36;
						}
						else if (Song == 0x0B)
						{
							instrument = 56;
							offset = 12;
						}
						else
						{
							instrument = 82;
							offset = 36;
						}
						break;

					case 0x3B:
						if (Song == 0x05)
						{
							instrument = 56;
							offset = 24;
						}
						else if (Song == 0x0B)
						{
							//instrument = 56;
							//instrument = 57;
							instrument = 61;
							//instrument = 58;
							offset = 24;
						}
						else
						{
							instrument = 104;
							//instrument = 106;
							//instrument = 105;
							//instrument = 84;
							//instrument = 82;
							//instrument = 83;
							offset = 36;
						}
						break;

					case 0x3C:
						instrument = 56;
						offset = 24;
						break;

					case 0x3E:
						instrument = 38;
						offset = 24;
						break;
				}

				Midi.ProgramChange(channel, instrument);

				Instruments[channel] = SongPlayer.ChannelInstruments[channel];
				NoteOffsets[channel] = offset;
			}
		}
	}
}