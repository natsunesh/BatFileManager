using System;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class SettingsForm : Form
{
    private TextBox textBoxBatName;

    private BatFileManager _manager;
    private List<IAction> _actions = new List<IAction>();

    public SettingsForm()
    {
        InitializeComponent();
        comboBox1.SelectedIndex = 0;
        
    }

    public void SetManager(BatFileManager manager) => _manager = manager;

    private void AddAction_Click(object sender, EventArgs e)
    {
        var param = textBox1.Text.Trim();
        if (string.IsNullOrEmpty(param))
        {
            MessageBox.Show("Введите параметр!");
            return;
        }

        IAction action = null;
        switch (comboBox1.SelectedItem.ToString())
        {
            case "Открыть сайт": action = new OpenSiteAction(param); break;
            case "Закрыть процесс": action = new CloseProcessAction(param); break;
            case "Запустить программу": action = new RunProgramAction(param); break;
            case "Открыть папку": action = new OpenFolderAction(param); break;
            case "Поставить задержку":
                if (int.TryParse(param, out int sec)) action = new DelayAction(sec);
                else MessageBox.Show("Некорректное время!");
                break;
        }

        if (action != null)
        {
            _actions.Add(action);
            listBox1.Items.Add(action.Description);
            textBox1.Clear();
        }
    }

    private void RemoveAction_Click(object sender, EventArgs e)
    {
        if (listBox1.SelectedIndex >= 0)
        {
            _actions.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }
    }

    private void CreateBat_Click(object sender, EventArgs e)
    {
        if (_actions.Count == 0)
        {
            MessageBox.Show("Добавьте действия!");
            return;
        }

        string batName = textBoxBatName.Text.Trim();
        if (string.IsNullOrEmpty(batName))
        {
            MessageBox.Show("Введите имя для батника!");
            return;
        }
        try
        {
            _manager.CreateBatFile(_actions, batName); // Передаём имя!
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}");
        }
        _manager.CreateBatFile(_actions, batName);
    }
}

// Designer-generated code (не изменять!)
partial class SettingsForm
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.listBox1 = new System.Windows.Forms.ListBox();
        this.button3 = new System.Windows.Forms.Button();
        this.SuspendLayout();
        ///
        /// name 
        /// 
        this.textBoxBatName = new System.Windows.Forms.TextBox();
        this.textBoxBatName.Left = 120;
        this.textBoxBatName.Top = 340;
        this.textBoxBatName.Width = 200;
        this.Controls.Add(this.textBoxBatName);
        //
        // name the bat
        //
        Label labelBatName = new Label();
        labelBatName.Text = "Имя батника:";
        labelBatName.Left = 10;
        labelBatName.Top = 340;
        labelBatName.Width = 100;

        TextBox textBoxBatName = new TextBox();
        textBoxBatName.Left = 120;
        textBoxBatName.Top = 340;
        textBoxBatName.Width = 200;

        this.Controls.Add(labelBatName);
        this.Controls.Add(textBoxBatName);
        // 
        // comboBox1
        // 
        this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox1.Items.AddRange(new object[] { "Открыть сайт", "Закрыть процесс", "Запустить программу", "Открыть папку", "Поставить задержку" });
        this.comboBox1.Location = new System.Drawing.Point(12, 12);
        this.comboBox1.Size = new System.Drawing.Size(150, 21);
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(168, 12);
        this.textBox1.Size = new System.Drawing.Size(200, 20);
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(12, 50);
        this.button1.Size = new System.Drawing.Size(150, 23);
        this.button1.Text = "Добавить действие";
        this.button1.Click += new System.EventHandler(this.AddAction_Click);
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(168, 50);
        this.button2.Size = new System.Drawing.Size(150, 23);
        this.button2.Text = "Удалить выбранное";
        this.button2.Click += new System.EventHandler(this.RemoveAction_Click);
        // 
        // listBox1
        // 
        this.listBox1.Location = new System.Drawing.Point(12, 90);
        this.listBox1.Size = new System.Drawing.Size(356, 200);
        // 
        // button3
        // 
        this.button3.Location = new System.Drawing.Point(12, 300);
        this.button3.Size = new System.Drawing.Size(150, 23);
        this.button3.Text = "Создать батник";
        this.button3.Click += new System.EventHandler(this.CreateBat_Click);
        
        // 
        // SettingsForm
        // 
        this.ClientSize = new System.Drawing.Size(380, 335);
        this.Controls.Add(this.button3);
        this.Controls.Add(this.listBox1);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.comboBox1);
        this.Name = "SettingsForm";
        this.Text = "Настройки";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.Button button3;
}
