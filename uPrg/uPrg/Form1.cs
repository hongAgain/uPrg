using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace uprg
{
	public partial class frmMain : Form
	{
		public const int InputWidth = 32;
		public const int OutputWidth = 24;
		public const int ItemWidth = 32;
		public const int ItemHeight = 32;

		public class DisplayRow
		{
			public static TextBox NumberEntry;
			protected static Label LastEdited;

			public Label Address;
			public List<Label> Inputs;
			public List<Label> Outputs;
			public Label FalseType;
			public Label FalseDwell;
			public Label FalseDest;
			public Label TrueType;
			public Label TrueDwell;
			public Label TrueDest;

			public DisplayRow()
			{
				Address = new Label();
				Address.AutoSize = false;
				Address.Width = 2 * ItemWidth;
				Address.Height = ItemHeight;
				Address.BorderStyle = BorderStyle.FixedSingle;

				Inputs = new List<Label>();
				for (int i = 0; i < InputWidth; i++)
				{
					Label l = new Label();
					l.AutoSize = false;
					l.Width = ItemWidth;
					l.Height = ItemHeight;
					l.BorderStyle = BorderStyle.FixedSingle;
					Inputs.Add(l);
				}

				Outputs = new List<Label>();
				for (int i = 0; i < OutputWidth; i++)
				{
					Label l = new Label();
					l.AutoSize = false;
					l.Width = ItemWidth;
					l.Height = ItemHeight;
					l.BorderStyle = BorderStyle.FixedSingle;
					Outputs.Add(l);
				}

				FalseType = new Label();
				FalseType.AutoSize = false;
				FalseType.Width = 2 * ItemWidth;
				FalseType.Height = ItemHeight;
				FalseType.BorderStyle = BorderStyle.FixedSingle;

				FalseDwell = new Label();
				FalseDwell.AutoSize = false;
				FalseDwell.Width = 2 * ItemWidth;
				FalseDwell.Height = ItemHeight;
				FalseDwell.BorderStyle = BorderStyle.FixedSingle;

				FalseDest = new Label();
				FalseDest.AutoSize = false;
				FalseDest.Width = 2 * ItemWidth;
				FalseDest.Height = ItemHeight;
				FalseDest.BorderStyle = BorderStyle.FixedSingle;

				TrueType = new Label();
				TrueType.AutoSize = false;
				TrueType.Width = 2 * ItemWidth;
				TrueType.Height = ItemHeight;
				TrueType.BorderStyle = BorderStyle.FixedSingle;

				TrueDwell = new Label();
				TrueDwell.AutoSize = false;
				TrueDwell.Width = 2 * ItemWidth;
				TrueDwell.Height = ItemHeight;
				TrueDwell.BorderStyle = BorderStyle.FixedSingle;

				TrueDest = new Label();
				TrueDest.AutoSize = false;
				TrueDest.Width = 2 * ItemWidth;
				TrueDest.Height = ItemHeight;
				TrueDest.BorderStyle = BorderStyle.FixedSingle;
			}

			public void SetLocation(int x, int y)
			{
				Address.Left = x;
				Address.Top = y;

				for (int i = 0; i < Inputs.Count; i++)
				{
					Inputs[i].Left = (i * ItemWidth) + (Address.Left + Address.Width);
					Inputs[i].Top = y;
				}

				for (int i = 0; i < Outputs.Count; i++)
				{
					Outputs[i].Left = (i * ItemWidth) + (Inputs[Inputs.Count - 1].Left + ItemWidth);
					Outputs[i].Top = y;
				}

				FalseType.Left = Outputs[Outputs.Count - 1].Left + ItemWidth;
				FalseType.Top = y;

				FalseDwell.Left = FalseType.Left + FalseType.Width;
				FalseDwell.Top = y;

				FalseDest.Left = FalseDwell.Left + FalseDwell.Width;
				FalseDest.Top = y;

				TrueType.Left = FalseDest.Left + FalseDest.Width;
				TrueType.Top = y;

				TrueDwell.Left = TrueType.Left + TrueType.Width;
				TrueDwell.Top = y;

				TrueDest.Left = TrueDwell.Left + TrueDwell.Width;
				TrueDest.Top = y;
			}

			public Label[] GetRowItems()
			{
				List<Label> output = new List<Label>();

				output.Add(Address);
				output.AddRange(Inputs.ToArray());
				output.AddRange(Outputs.ToArray());
				output.Add(FalseType);
				output.Add(FalseDwell);
				output.Add(FalseDest);
				output.Add(TrueType);
				output.Add(TrueDwell);
				output.Add(TrueDest);

				return output.ToArray();
			}

			protected void BitClick(object sender, EventArgs e)
			{
				((Label)sender).Focus();
				if (((Label)sender).Text == "N/A")
				{
					((Label)sender).Text = "TRUE";
				}
				else if (((Label)sender).Text == "TRUE")
				{
					((Label)sender).Text = "FALSE";
				}
				else if (((Label)sender).Text == "FALSE")
				{
					((Label)sender).Text = "N/A";
				}
				else
				{
					((Label)sender).Text = "N/A";
				}
			}

			protected void TypeClick(object sender, EventArgs e)
			{
				((Label)sender).Focus();
				if (((Label)sender).Text == "Goto")
				{
					((Label)sender).Text = "Fault";
				}
				else if (((Label)sender).Text == "Fault")
				{
					((Label)sender).Text = "Reset";
				}
				else if (((Label)sender).Text == "Reset")
				{
					((Label)sender).Text = "JSR";
				}
				else if (((Label)sender).Text == "JSR")
				{
					((Label)sender).Text = "Goto";
				}
				else
				{
					((Label)sender).Text = "Goto";
				}
			}

			protected static void NumClick(object sender, EventArgs e)
			{
				if (LastEdited != null)
				{
					ExitNumberEntry(null, null);
				}

				LastEdited = ((Label)sender);
				NumberEntry.Left = LastEdited.Left;
				NumberEntry.Top = LastEdited.Top;
				NumberEntry.Text = LastEdited.Text;
				LastEdited.Visible = false;
				NumberEntry.Visible = true;
				NumberEntry.SelectAll();
				NumberEntry.Focus();
			}

			protected static void ExitNumberEntry(object sender, EventArgs e)
			{
				NumberEntry.Visible = false;
				LastEdited.Text = NumberEntry.Text;
				LastEdited.Visible = true;
			}

			protected void NumberEntry_ReturnPressed(object sender, KeyPressEventArgs e)
			{
				if (e.KeyChar == 13)
				{
					ExitNumberEntry(sender, e);
				}
			}
		}

		public class Header : DisplayRow
		{
			public Header() : base()
			{
				Address.Text = "Address";

				for (int i = 0; i < InputWidth; i++)
				{
					Inputs[i].Text = "DI" + i.ToString();
					Inputs[i].Click += NumClick;
				}

				for (int i = 0; i < OutputWidth; i++)
				{
					Outputs[i].Text = "DO" + i.ToString();
					Outputs[i].Click += NumClick;
				}

				FalseType.Text = "False Flags";
				FalseDwell.Text = "False Dwell";
				FalseDest.Text = "False Dest";
				TrueType.Text = "True Flags";
				TrueDwell.Text = "True Dwell";
				TrueDest.Text = "True Dest";
			}
		}

		public class SeqRow : DisplayRow
		{
			public SeqRow(int step) : base()
			{
				NumberEntry = new TextBox();
				NumberEntry.Width = 32;
				NumberEntry.Height = 32;
				NumberEntry.Visible = false;
				NumberEntry.LostFocus += ExitNumberEntry;
				NumberEntry.Leave += ExitNumberEntry;
				NumberEntry.KeyPress += NumberEntry_ReturnPressed;

				Address.Text = step.ToString();
				Address.Click += NumClick;

				for (int i = 0; i < InputWidth; i++)
				{
					Inputs[i].Text = "N/A";
					Inputs[i].Click += BitClick;
				}

				for (int i = 0; i < OutputWidth; i++)
				{
					Outputs[i].Text = "N/A";
					Outputs[i].Click += BitClick;
				}

				FalseType.Text = "Goto";
				FalseType.Click += TypeClick;

				FalseDwell.Text = "0";
				FalseDwell.Click += NumClick;

				FalseDest.Text = "0";
				FalseDest.Click += NumClick;

				TrueType.Text = "Goto";
				TrueType.Click += TypeClick;

				TrueDwell.Text = "0";
				TrueDwell.Click += NumClick;

				TrueDest.Text = "0";
				TrueDest.Click += NumClick;
			}
		}

		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			Panel maindisplay = new Panel();
			maindisplay.Width = 256;
			maindisplay.Height = 256;
			maindisplay.Top = 0;
			maindisplay.Left = 0;
			maindisplay.HorizontalScroll.Enabled = true;
			maindisplay.AutoScroll = true;
			maindisplay.VerticalScroll.Enabled = true;

			Header h = new Header();
			h.SetLocation(0, 0);
			maindisplay.Controls.AddRange(h.GetRowItems());

			SeqRow[] s = new SeqRow[4];

			for (int i = 0; i < 4; i++)
			{
				s[i] = new SeqRow(i);
				s[i].SetLocation(0, 32 + (i * 32));
				maindisplay.Controls.AddRange(s[i].GetRowItems());
			}

			maindisplay.Controls.Add(SeqRow.NumberEntry);

			this.Controls.Add(maindisplay);
		}
	}
}
