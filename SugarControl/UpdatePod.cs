using System;
using Gtk;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SugarControl
{
	public partial class UpdatePod : Gtk.Window
	{
		public UpdatePod() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}

		protected void OnButtonShowClicked(object sender, EventArgs e)
		{
			new TreeViewEvidencija();
		}

		protected void OnButtonBackClicked(object sender, EventArgs e)
		{
			new MainWindow();
			Visible = false;
		}

		protected void OnButtonSaveClicked(object sender, EventArgs e)
		{
			SQLiteConnection m_dbConection;
			m_dbConection = new SQLiteConnection("Data source = Database.sqlite; Version =3;");

			string sql2 = "UPDATE EvidencijaSecera SET ID='" + this.entryId2.Text + "',razina='" + this.entryRazina2.Text + "',vrijeme='" + this.comboboxVrijeme2.ActiveText + "',napomena='" + this.entryNapomena2.Text + "',datum='" + this.calendarDatum2.Date +"' WHERE ID='" + this.entryId2.Text + "';";
			SQLiteCommand Command2 = new SQLiteCommand(sql2, m_dbConection);

			try
			{
				m_dbConection.Open();
				SQLiteDataReader reader = Command2.ExecuteReader();
				System.Windows.Forms.MessageBox.Show("Data updated!");

				while (reader.Read())
				{
					
				}
			}

			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
		}

		protected void OnButtonDeleteClicked(object sender, EventArgs e)
		{
			SQLiteConnection m_dbConection;
			m_dbConection = new SQLiteConnection("Data source = Database.sqlite; Version =3;");

			string sql2 = "DELETE FROM EvidencijaSecera WHERE ID='" + this.entryId2.Text + "';";
			SQLiteCommand Command2 = new SQLiteCommand(sql2, m_dbConection);

			try
			{
				m_dbConection.Open();
				SQLiteDataReader reader = Command2.ExecuteReader();
				System.Windows.Forms.MessageBox.Show("Data deleted!");

				while (reader.Read())
				{
					
				}
			}

			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
		}

		protected void OnDeleteEvent(object o, DeleteEventArgs args)
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

		protected void OnEntryId2TextInserted(object o, TextInsertedArgs args)
		{
			var id = entryId2.Text;

			int idResult;

			if (!int.TryParse(id, out idResult))
			{
				System.Windows.Forms.MessageBox.Show("ID mora biti broj!");
			}
		}

		protected void OnEntryRazina2TextInserted(object o, TextInsertedArgs args)
		{
			var razina = entryRazina2.Text;

			float razinaResult;

			if (!float.TryParse(razina, out razinaResult))
			{
				System.Windows.Forms.MessageBox.Show("Razina šećera mora biti broj!");
			}

			if (razinaResult > 50)
			{
				System.Windows.Forms.MessageBox.Show("Nemoguća razina šećera!");
			}
			else if (razinaResult< 0)
			{
				System.Windows.Forms.MessageBox.Show("Nemoguća razina šećera!");
			}
		}
	}
}
