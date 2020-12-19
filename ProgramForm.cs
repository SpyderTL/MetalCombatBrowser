using System;
using System.Windows.Forms;
using MetalCombat;
using Snes;

namespace MetalCombatBrowser
{
	internal class ProgramForm
	{
		internal static BrowserForm Form;

		internal static void Show()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Form = new BrowserForm();
			Form.Text = "Metal Combat: Falcon's Revenge";

			Rom.Data = Properties.Resources.Rom;

			SnesRom.Load();

			var romNode = Form.TreeView.Nodes.Add("rom", "Metal Combat: Falcon's Revenge");
			var songsNode = romNode.Nodes.Add("songs", "Songs");

			var position = MetalCombatRom.SongTableAddress;

			Form.TreeView.AfterSelect += TreeView_AfterSelect;

			Form.ListView.View = View.Details;

			Form.ListView.DoubleClick += ListView_DoubleClick;

			Application.Run(Form);
		}

		private static void ListView_DoubleClick(object sender, EventArgs e)
		{
			foreach (ListViewItem item in Form.ListView.SelectedItems)
			{
				switch (item.SubItems[1].Name)
				{
					case "song":
						var song = int.Parse(item.Name);

						var position = MetalCombatRom.SongTableAddress + (song * 8);

						var source = Snes.Snes.Memory[position + 0] | (Snes.Snes.Memory[position + 1] << 8) | (Snes.Snes.Memory[position + 2] << 16);
						var length = BitConverter.ToUInt16(Snes.Snes.Memory, position + 3);
						var destination = BitConverter.ToUInt16(Snes.Snes.Memory, position + 5);

						Array.Copy(Snes.Snes.Memory, source + 0xB90000, Apu.Memory, destination, length);
						SongReader.Position = destination;

						SongWindow.Show();
						break;
				}
			}
		}

		private static void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			switch (e.Node.Name)
			{
				case "songs":
					Form.ListView.Items.Clear();
					Form.ListView.Columns.Clear();

					Form.ListView.Columns.Add("Name");
					Form.ListView.Columns.Add("Type");

					for (var song = 0; song < MetalCombatRom.SongCount; song++)
					{
						var item = Form.ListView.Items.Add(song.ToString(), song.ToString("X2"), "song");
						var subitem = item.SubItems.Add("Song");
						subitem.Name = "song";
					}
					break;
			}
		}
	}
}