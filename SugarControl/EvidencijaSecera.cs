using System;
namespace SugarControl
{
	public class TreeViewEvidencija
	{
		public TreeViewEvidencija()
		{
			//Gtk.Window window = new Gtk.Window("Evidencija šećera");
			//window.SetSizeRequest(500, 200);
			//window.Sc();

			Gtk.TreeView evidencija = new Gtk.TreeView();
			window.Add(evidencija);

			Gtk.TreeViewColumn idColumn = new Gtk.TreeViewColumn();
			idColumn.Title = "ID";
			Gtk.CellRendererText idCell = new Gtk.CellRendererText();
			idColumn.PackStart(idCell, true);

			Gtk.TreeViewColumn razinaColumn = new Gtk.TreeViewColumn();
			razinaColumn.Title = "Razina šećera";
			Gtk.CellRendererText razinaCell = new Gtk.CellRendererText();
			razinaColumn.PackStart(razinaCell, true);

			Gtk.TreeViewColumn vrijemeColumn = new Gtk.TreeViewColumn();
			vrijemeColumn.Title = "Vrijeme";
			Gtk.CellRendererText vrijemeCell = new Gtk.CellRendererText();
			vrijemeColumn.PackStart(vrijemeCell, true);

			Gtk.TreeViewColumn napomenaColumn = new Gtk.TreeViewColumn();
			napomenaColumn.Title = "Napomena";
			Gtk.CellRendererText napomenaCell = new Gtk.CellRendererText();
			napomenaColumn.PackStart(napomenaCell, true);

			Gtk.TreeViewColumn datumColumn = new Gtk.TreeViewColumn();
			datumColumn.Title = "Datum";
			Gtk.CellRendererText datumCell = new Gtk.CellRendererText();
			datumColumn.PackStart(datumCell, true);

			evidencija.AppendColumn(idColumn);
			evidencija.AppendColumn(razinaColumn);
			evidencija.AppendColumn(vrijemeColumn);
			evidencija.AppendColumn(napomenaColumn);
			evidencija.AppendColumn(datumColumn);

			idColumn.AddAttribute(idCell, "text", 0);
			razinaColumn.AddAttribute(razinaCell, "text", 1);
			vrijemeColumn.AddAttribute(vrijemeCell, "text", 2);
			napomenaColumn.AddAttribute(napomenaCell, "text", 3);
			datumColumn.AddAttribute(datumCell, "text", 4);

			Gtk.ListStore evidencijaListStore = new Gtk.ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

			//konekcija na bazu
			SQLiteConnection m_dbConnection;
			m_dbConnection = new SQLiteConnection("Data source=Database.sqlite; Version=3;");

			string sql2 = "SELECT * FROM EvidencijaSecera";
			SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);

			m_dbConnection.Open();
			SQLiteDataReader reader2 = command2.ExecuteReader();

			try
			{
				while (reader2.Read())
				{

					int ID = reader2.GetInt32(0);
					float razina = reader2.GetFloat(1);
					string vrijeme = reader2.GetString(2);
					string napomena = reader2.GetString(3);
					string datum = reader2.GetString(4);

					evidencijaListStore.AppendValues(ID, razina, vrijeme, napomena, datum);

					evidencija.Model = evidencijaListStore;

				}

			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			window.ShowAll();

			command2.Dispose();
		}



	}

}
