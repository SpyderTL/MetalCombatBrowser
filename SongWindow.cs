using System;
using System.Threading;
using MetalCombat;

namespace MetalCombatBrowser
{
	internal static class SongWindow
	{
		internal static SongForm Form;

		internal static Timer Timer = new Timer(Timer_Elapsed, null, Timeout.Infinite, 10);

		internal static void Show()
		{
			Form = new SongForm();

			Form.Timer.Tick += Timer_Tick;
			Form.PlayButton.Click += PlayButton_Click;
			Form.StopButton.Click += StopButton_Click;
			Form.FormClosed += Form_FormClosed;

			Form.Show();
		}

		private static void Timer_Tick(object sender, EventArgs e)
		{
			Form.TrackLabel.Text = SongReader.Position.ToString("X4");

			//Form.Channel1Label.Text = SongPlayer.ChannelNotes[0] == 0 ? string.Empty : SongPlayer.ChannelNotes[0].ToString("X2");
			//Form.Channel2Label.Text = SongPlayer.ChannelNotes[1] == 0 ? string.Empty : SongPlayer.ChannelNotes[1].ToString("X2");
			//Form.Channel3Label.Text = SongPlayer.ChannelNotes[2] == 0 ? string.Empty : SongPlayer.ChannelNotes[2].ToString("X2");
			//Form.Channel4Label.Text = SongPlayer.ChannelNotes[3] == 0 ? string.Empty : SongPlayer.ChannelNotes[3].ToString("X2");
			//Form.Channel5Label.Text = SongPlayer.ChannelNotes[4] == 0 ? string.Empty : SongPlayer.ChannelNotes[4].ToString("X2");
			//Form.Channel6Label.Text = SongPlayer.ChannelNotes[5] == 0 ? string.Empty : SongPlayer.ChannelNotes[5].ToString("X2");
			//Form.Channel7Label.Text = SongPlayer.ChannelNotes[6] == 0 ? string.Empty : SongPlayer.ChannelNotes[6].ToString("X2");
			//Form.Channel8Label.Text = SongPlayer.ChannelNotes[7] == 0 ? string.Empty : SongPlayer.ChannelNotes[7].ToString("X2");

			Form.Channel1Label.Text = SongPlayer.ChannelNotes[0] == 0 ? string.Empty : SongPlayer.ChannelInstruments[0].ToString("X2");
			Form.Channel2Label.Text = SongPlayer.ChannelNotes[1] == 0 ? string.Empty : SongPlayer.ChannelInstruments[1].ToString("X2");
			Form.Channel3Label.Text = SongPlayer.ChannelNotes[2] == 0 ? string.Empty : SongPlayer.ChannelInstruments[2].ToString("X2");
			Form.Channel4Label.Text = SongPlayer.ChannelNotes[3] == 0 ? string.Empty : SongPlayer.ChannelInstruments[3].ToString("X2");
			Form.Channel5Label.Text = SongPlayer.ChannelNotes[4] == 0 ? string.Empty : SongPlayer.ChannelInstruments[4].ToString("X2");
			Form.Channel6Label.Text = SongPlayer.ChannelNotes[5] == 0 ? string.Empty : SongPlayer.ChannelInstruments[5].ToString("X2");
			Form.Channel7Label.Text = SongPlayer.ChannelNotes[6] == 0 ? string.Empty : SongPlayer.ChannelInstruments[6].ToString("X2");
			Form.Channel8Label.Text = SongPlayer.ChannelNotes[7] == 0 ? string.Empty : SongPlayer.ChannelInstruments[7].ToString("X2");

			//Form.Channel1Label.Text = SongPlayer.ChannelNotes[0] == 0 ? string.Empty : RomInstruments.Instruments[SongPlayer.ChannelInstruments[0]].Value1.ToString("X2");
			//Form.Channel2Label.Text = SongPlayer.ChannelNotes[1] == 0 ? string.Empty : RomInstruments.Instruments[SongPlayer.ChannelInstruments[1]].Value1.ToString("X2");
			//Form.Channel3Label.Text = SongPlayer.ChannelNotes[2] == 0 ? string.Empty : RomInstruments.Instruments[SongPlayer.ChannelInstruments[2]].Value1.ToString("X2");
			//Form.Channel4Label.Text = SongPlayer.ChannelNotes[3] == 0 ? string.Empty : RomInstruments.Instruments[SongPlayer.ChannelInstruments[3]].Value1.ToString("X2");
			//Form.Channel5Label.Text = SongPlayer.ChannelNotes[4] == 0 ? string.Empty : RomInstruments.Instruments[SongPlayer.ChannelInstruments[4]].Value1.ToString("X2");
			//Form.Channel6Label.Text = SongPlayer.ChannelNotes[5] == 0 ? string.Empty : RomInstruments.Instruments[SongPlayer.ChannelInstruments[5]].Value1.ToString("X2");
			//Form.Channel7Label.Text = SongPlayer.ChannelNotes[6] == 0 ? string.Empty : RomInstruments.Instruments[SongPlayer.ChannelInstruments[6]].Value1.ToString("X2");
			//Form.Channel8Label.Text = SongPlayer.ChannelNotes[7] == 0 ? string.Empty : RomInstruments.Instruments[SongPlayer.ChannelInstruments[7]].Value1.ToString("X2");

			//Form.Channel1Label.Text = TrackReader.Channels[0].ToString("X4");
			//Form.Channel2Label.Text = TrackReader.Channels[1].ToString("X4");
			//Form.Channel3Label.Text = TrackReader.Channels[2].ToString("X4");
			//Form.Channel4Label.Text = TrackReader.Channels[3].ToString("X4");
			//Form.Channel5Label.Text = TrackReader.Channels[4].ToString("X4");
			//Form.Channel6Label.Text = TrackReader.Channels[5].ToString("X4");
			//Form.Channel7Label.Text = TrackReader.Channels[6].ToString("X4");
			//Form.Channel8Label.Text = TrackReader.Channels[7].ToString("X4");

			Form.Channel1Label.Left = 80 + (SongPlayer.ChannelNotes[0] * 8);
			Form.Channel2Label.Left = 80 + (SongPlayer.ChannelNotes[1] * 8);
			Form.Channel3Label.Left = 80 + (SongPlayer.ChannelNotes[2] * 8);
			Form.Channel4Label.Left = 80 + (SongPlayer.ChannelNotes[3] * 8);
			Form.Channel5Label.Left = 80 + (SongPlayer.ChannelNotes[4] * 8);
			Form.Channel6Label.Left = 80 + (SongPlayer.ChannelNotes[5] * 8);
			Form.Channel7Label.Left = 80 + (SongPlayer.ChannelNotes[6] * 8);
			Form.Channel8Label.Left = 80 + (SongPlayer.ChannelNotes[7] * 8);
		}

		private static void PlayButton_Click(object sender, EventArgs e)
		{
			Form.SongLabel.Text = SongReader.Position.ToString("X4");
			Form.Timer.Start();

			SongPlayer.Play();
			Midi.MidiPlayer.Start();

			Timer.Change(0, 10);
		}

		private static void StopButton_Click(object sender, EventArgs e)
		{
			Form.Timer.Stop();
			Timer.Change(Timeout.Infinite, 10);

			SongPlayer.Stop();
			Midi.MidiPlayer.Stop();
		}

		private static void Timer_Elapsed(object state)
		{
			//Timer.Change(Timeout.Infinite, 10);

			SongPlayer.Update();
			Midi.MidiPlayer.Update();

			//Timer.Change(10, 10);
		}

		private static void Form_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			Form.PlayButton.Click -= PlayButton_Click;
			Form.StopButton.Click -= StopButton_Click;
			Form.FormClosed -= Form_FormClosed;

			Form.Timer.Stop();
			Timer.Change(Timeout.Infinite, 10);

			SongPlayer.Stop();
			Midi.MidiPlayer.Stop();

			Form = null;
		}
	}
}