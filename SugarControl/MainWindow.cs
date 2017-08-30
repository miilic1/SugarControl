using System;
using Gtk;
using System.Data;
using System.Data.SQLite;
using System.IO;


	public partial class MainWindow : Gtk.Window
	{
		public MainWindow() : base(Gtk.WindowType.Toplevel)
		{
			Build();

			SQLiteConnection m_dbConnection;
			m_dbConnection = new SQLiteConnection("Data source=Database.sqlite; Version=3;");

			string sql1 = "CREATE TABLE IF NOT EXIST EvidencijaSecera (razina FLOAT, vrijeme varchar (50),napomena VARCHAR(50),datum DATETIME)";
			SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);

		}

		protected void OnDeleteEvent(object sender, DeleteEventArgs a)
		{
			System.Windows.Forms.DialogResult dialog = System.Windows.Forms.MessageBox.Show("Želite li zaista zatvoriti program?",
																		"Exit", System.Windows.Forms.MessageBoxButtons.YesNo);

			if (dialog == System.Windows.Forms.DialogResult.Yes)
			{
				Application.Quit();
			}
			else if (dialog == System.Windows.Forms.DialogResult.No)
			{
				Application.Run();
			}
		}

		protected void OnExit(object sender, EventArgs e)
		{
			System.Windows.Forms.DialogResult dialog = System.Windows.Forms.MessageBox.Show("Želite li zaista zatvoriti program?",
																					"Exit", System.Windows.Forms.MessageBoxButtons.YesNo);

			if (dialog == System.Windows.Forms.DialogResult.Yes)
			{
				Application.Quit();
			}
			else if (dialog == System.Windows.Forms.DialogResult.No)
			{
				Application.Run();
			}
		}

		protected void OnAbout(object sender, EventArgs e)
		{
			AboutDialog about = new AboutDialog();
			about.Name = "Sugar Control - Diabetes control software";
			about.Version = "1.0.0.0";

			about.Run();

			about.Destroy();
		}


		protected void OnButtonExitClicked(object sender, EventArgs e)
		{
			System.Windows.Forms.DialogResult dialog = System.Windows.Forms.MessageBox.Show("Želite li zaista zatvoriti program?",
																					"Exit", System.Windows.Forms.MessageBoxButtons.YesNo);

			if (dialog == System.Windows.Forms.DialogResult.Yes)
			{
				Application.Quit();
			}
			else if (dialog == System.Windows.Forms.DialogResult.No)
			{
				Application.Run();
			}

		}

		protected void OnButtonSaveClicked(object sender, EventArgs e)
		{

			SQLiteConnection m_dbConnection;
			m_dbConnection = new SQLiteConnection("Data source=Database.sqlite; Version=3;");

			string sql1 = "INSERT INTO EvidencijaSecera (razina,vrijeme,napomena,datum) VALUES ('" + this.entryRazina.Text + "','" + this.comboboxVrijeme.ActiveText + "','" + this.entryNapomena.Text + "','" + this.calendarDatum.Date + "')";
			SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);

			try
			{
				m_dbConnection.Open();
				SQLiteDataReader reader = command1.ExecuteReader();
				System.Windows.Forms.MessageBox.Show("Data saved!");

				while (reader.Read())
				{
				}
			}

			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
		}

		protected void OnButtonPrikaziClicked(object sender, EventArgs e)
		{
			new TreeViewEvidencija();

		}

		protected void OnEntryRazinaTextInserted(object o, TextInsertedArgs args)
		{
			var razina = entryRazina.Text;

			float razinaResult;

			if (!float.TryParse(razina, out razinaResult))
			{
				System.Windows.Forms.MessageBox.Show("Razina šećera mora biti broj!");
			}

			if (razinaResult > 50)
			{
				System.Windows.Forms.MessageBox.Show("Nemoguća razina šećera!");
			}
			else if (razinaResult < 0)
			{
				System.Windows.Forms.MessageBox.Show("Nemoguća razina šećera!");
			}
		}

	protected void OnButtonUpdateClicked(object sender, EventArgs e)
		{
			new UpdatePod();
			Visible = false;
		}
}

public partial class UpdatePod
{
	public UpdatePod()
	{
		SugarControl.UpdatePod upd = new SugarControl.UpdatePod();
		upd.Show();
	}
}


	public class TreeViewEvidencija
	{
		public TreeViewEvidencija()
		{
			Gtk.Window window = new Gtk.Window("SugarControl - Evidencija šećera");
			window.SetSizeRequest(500, 500);
			
			Gtk.TreeView evidencija = new Gtk.TreeView();
			window.Add(evidencija);

			Gtk.TreeViewColumn idColumn = new Gtk.TreeViewColumn();
			idColumn.Title = "ID";
			Gtk.CellRendererText idCell = new Gtk.CellRendererText();
			idColumn.PackStart(idCell, true);
			//idColumn.CellIsVisible (idCell, true);

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
					string sID = Convert.ToString(ID);

					float razina = reader2.GetFloat(1);
					string srazina = Convert.ToString(razina);

					string vrijeme = reader2.GetString(2);
					string napomena = reader2.GetString(3);
					string datum = reader2.GetString(4);

					evidencijaListStore.AppendValues(sID, srazina, vrijeme, napomena, datum);
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			evidencija.Model = evidencijaListStore;

			window.ShowAll();

			command2.Dispose();
		}
	}