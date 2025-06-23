using System;
using System.Drawing;
using System.Windows.Forms;


namespace manage_bat
{
    public partial class MainForm : Form
    {
        private readonly BatFileManager _manager = new BatFileManager();

        public MainForm()
        {
            InitializeComponent();
            programm_or_cite.SelectedIndexChanged += (s, e) => RunSelectedBat();
            button1.Click += (s, e) => RunSelectedBat();
            Settings.Click += OpenSettings;
            RefreshBatList();
            this.Icon = new Icon("L:\\программирование\\BatFileManager\\icon_main.ico");
        }

        private void RunSelectedBat()
        {
            if (programm_or_cite.SelectedItem is BatFile batFile)
            {
                try
                {
                    _manager.RunBatFile(batFile);
                    Log($"Запущен: {batFile.FileName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                    Log($"Ошибка: {ex.Message}");
                }
            }
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            using (var form = new SettingsForm())
            {
                form.SetManager(_manager);
                if (form.ShowDialog() == DialogResult.OK)
                    RefreshBatList();
            }
        }

        private void RefreshBatList()
        {
            programm_or_cite.Items.Clear();
            foreach (var batFile in _manager.GetBatFiles())
            {
                if (!programm_or_cite.Items.Contains(batFile))
                {
                    programm_or_cite.Items.Add(batFile);
                }
            }
        }

        private void Log(string message)
            => listBox1.Items.Add($"{DateTime.Now:HH:mm:ss} - {message}");
    }

    // Designer-generated code (не изменять!)
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.programm_or_cite = new System.Windows.Forms.ComboBox();
            this.Settings = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // programm_or_cite
            // 
            this.programm_or_cite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.programm_or_cite.Location = new System.Drawing.Point(12, 12);
            this.programm_or_cite.Size = new System.Drawing.Size(300, 21);
            // 
            // Settings
            // 
            this.Settings.Location = new System.Drawing.Point(318, 10);
            this.Settings.Size = new System.Drawing.Size(100, 23);
            this.Settings.Text = "Настройки";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(424, 10);
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.Text = "Запустить";
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(12, 50);
            this.listBox1.Size = new System.Drawing.Size(512, 290);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(536, 352);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.programm_or_cite);
            this.Name = "MainForm";
            this.Text = "Менеджер батников";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox programm_or_cite;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
    }

}
