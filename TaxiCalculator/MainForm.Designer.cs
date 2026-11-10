namespace TaxiCalculator;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    #region Код конструктора форм Windows

    private void InitializeComponent()
    {
        tabs = new TabControl();
        calculatorPage = new TabPage();
        distanceLabel = new Label();
        distanceInput = new TextBox();
        waitLabel = new Label();
        waitInput = new TextBox();
        tariffLabel = new Label();
        tariffInput = new ComboBox();
        rates = new Label();
        calculateButton = new Button();
        result = new Label();
        historyPage = new TabPage();
        historyGrid = new DataGridView();
        historyTime = new DataGridViewTextBoxColumn();
        historyTariff = new DataGridViewTextBoxColumn();
        historyDistance = new DataGridViewTextBoxColumn();
        historyWait = new DataGridViewTextBoxColumn();
        historyTotal = new DataGridViewTextBoxColumn();
        historyStatus = new Label();
        experimentPage = new TabPage();
        experimentGrid = new DataGridView();
        experimentDistance = new DataGridViewTextBoxColumn();
        experimentControls = new FlowLayoutPanel();
        experimentWaitLabel = new Label();
        experimentWait = new NumericUpDown();
        experimentDescription = new Label();
        tabs.SuspendLayout();
        calculatorPage.SuspendLayout();
        historyPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)historyGrid).BeginInit();
        experimentPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)experimentGrid).BeginInit();
        experimentControls.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)experimentWait).BeginInit();
        SuspendLayout();
        //
        // tabs
        //
        tabs.Controls.Add(calculatorPage);
        tabs.Controls.Add(historyPage);
        tabs.Controls.Add(experimentPage);
        tabs.Dock = DockStyle.Fill;
        tabs.Location = new Point(0, 0);
        tabs.Name = "tabs";
        tabs.SelectedIndex = 0;
        tabs.Size = new Size(680, 380);
        tabs.TabIndex = 0;
        //
        // calculatorPage
        //
        calculatorPage.Controls.Add(distanceLabel);
        calculatorPage.Controls.Add(distanceInput);
        calculatorPage.Controls.Add(waitLabel);
        calculatorPage.Controls.Add(waitInput);
        calculatorPage.Controls.Add(tariffLabel);
        calculatorPage.Controls.Add(tariffInput);
        calculatorPage.Controls.Add(rates);
        calculatorPage.Controls.Add(calculateButton);
        calculatorPage.Controls.Add(result);
        calculatorPage.Location = new Point(4, 24);
        calculatorPage.Name = "calculatorPage";
        calculatorPage.Padding = new Padding(16);
        calculatorPage.Size = new Size(672, 352);
        calculatorPage.TabIndex = 0;
        calculatorPage.Text = "Расчёт";
        calculatorPage.UseVisualStyleBackColor = true;
        //
        // distanceLabel
        //
        distanceLabel.AutoSize = true;
        distanceLabel.Location = new Point(20, 26);
        distanceLabel.Name = "distanceLabel";
        distanceLabel.Size = new Size(99, 15);
        distanceLabel.TabIndex = 0;
        distanceLabel.Text = "Расстояние, км";
        //
        // distanceInput
        //
        distanceInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        distanceInput.Location = new Point(200, 23);
        distanceInput.Name = "distanceInput";
        distanceInput.Size = new Size(445, 23);
        distanceInput.TabIndex = 1;
        //
        // waitLabel
        //
        waitLabel.AutoSize = true;
        waitLabel.Location = new Point(20, 67);
        waitLabel.Name = "waitLabel";
        waitLabel.Size = new Size(103, 15);
        waitLabel.TabIndex = 2;
        waitLabel.Text = "Ожидание, мин";
        //
        // waitInput
        //
        waitInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        waitInput.Location = new Point(200, 64);
        waitInput.Name = "waitInput";
        waitInput.Size = new Size(445, 23);
        waitInput.TabIndex = 3;
        waitInput.Text = "0";
        //
        // tariffLabel
        //
        tariffLabel.AutoSize = true;
        tariffLabel.Location = new Point(20, 108);
        tariffLabel.Name = "tariffLabel";
        tariffLabel.Size = new Size(44, 15);
        tariffLabel.TabIndex = 4;
        tariffLabel.Text = "Тариф";
        //
        // tariffInput
        //
        tariffInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tariffInput.DropDownStyle = ComboBoxStyle.DropDownList;
        tariffInput.FormattingEnabled = true;
        tariffInput.Location = new Point(200, 105);
        tariffInput.Name = "tariffInput";
        tariffInput.Size = new Size(445, 23);
        tariffInput.TabIndex = 5;
        //
        // rates
        //
        rates.AutoSize = true;
        rates.Location = new Point(20, 153);
        rates.Name = "rates";
        rates.Size = new Size(140, 15);
        rates.TabIndex = 6;
        rates.Text = "Условия тарифа";
        //
        // calculateButton
        //
        calculateButton.AutoSize = true;
        calculateButton.Location = new Point(200, 187);
        calculateButton.Name = "calculateButton";
        calculateButton.Size = new Size(100, 25);
        calculateButton.TabIndex = 7;
        calculateButton.Text = "Рассчитать";
        calculateButton.UseVisualStyleBackColor = true;
        //
        // result
        //
        result.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        result.Location = new Point(20, 227);
        result.Name = "result";
        result.Size = new Size(625, 45);
        result.TabIndex = 8;
        result.Text = "Введите данные поездки.";
        //
        // historyPage
        //
        historyPage.Controls.Add(historyGrid);
        historyPage.Controls.Add(historyStatus);
        historyPage.Location = new Point(4, 24);
        historyPage.Name = "historyPage";
        historyPage.Padding = new Padding(3);
        historyPage.Size = new Size(672, 352);
        historyPage.TabIndex = 1;
        historyPage.Text = "История";
        historyPage.UseVisualStyleBackColor = true;
        //
        // historyGrid
        //
        historyGrid.AllowUserToAddRows = false;
        historyGrid.AllowUserToDeleteRows = false;
        historyGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        historyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        historyGrid.Columns.AddRange(new DataGridViewColumn[] { historyTime, historyTariff, historyDistance, historyWait, historyTotal });
        historyGrid.Dock = DockStyle.Fill;
        historyGrid.Location = new Point(3, 31);
        historyGrid.Name = "historyGrid";
        historyGrid.ReadOnly = true;
        historyGrid.RowHeadersVisible = false;
        historyGrid.Size = new Size(666, 318);
        historyGrid.TabIndex = 1;
        //
        // historyTime
        //
        historyTime.FillWeight = 150F;
        historyTime.HeaderText = "Дата и время";
        historyTime.Name = "historyTime";
        historyTime.ReadOnly = true;
        //
        // historyTariff
        //
        historyTariff.HeaderText = "Тариф";
        historyTariff.Name = "historyTariff";
        historyTariff.ReadOnly = true;
        //
        // historyDistance
        //
        historyDistance.HeaderText = "Расстояние, км";
        historyDistance.Name = "historyDistance";
        historyDistance.ReadOnly = true;
        //
        // historyWait
        //
        historyWait.HeaderText = "Ожидание, мин";
        historyWait.Name = "historyWait";
        historyWait.ReadOnly = true;
        //
        // historyTotal
        //
        historyTotal.HeaderText = "Стоимость";
        historyTotal.Name = "historyTotal";
        historyTotal.ReadOnly = true;
        //
        // historyStatus
        //
        historyStatus.Dock = DockStyle.Top;
        historyStatus.Location = new Point(3, 3);
        historyStatus.Name = "historyStatus";
        historyStatus.Size = new Size(666, 28);
        historyStatus.TabIndex = 0;
        historyStatus.Text = "Последние расчёты (до 100 записей)";
        historyStatus.TextAlign = ContentAlignment.MiddleLeft;
        //
        // experimentPage
        //
        experimentPage.Controls.Add(experimentGrid);
        experimentPage.Controls.Add(experimentControls);
        experimentPage.Controls.Add(experimentDescription);
        experimentPage.Location = new Point(4, 24);
        experimentPage.Name = "experimentPage";
        experimentPage.Padding = new Padding(3);
        experimentPage.Size = new Size(672, 352);
        experimentPage.TabIndex = 2;
        experimentPage.Text = "Эксперимент";
        experimentPage.UseVisualStyleBackColor = true;
        //
        // experimentGrid
        //
        experimentGrid.AllowUserToAddRows = false;
        experimentGrid.AllowUserToDeleteRows = false;
        experimentGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        experimentGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        experimentGrid.Columns.AddRange(new DataGridViewColumn[] { experimentDistance });
        experimentGrid.Dock = DockStyle.Fill;
        experimentGrid.Location = new Point(3, 41);
        experimentGrid.Name = "experimentGrid";
        experimentGrid.ReadOnly = true;
        experimentGrid.RowHeadersVisible = false;
        experimentGrid.Size = new Size(666, 280);
        experimentGrid.TabIndex = 1;
        //
        // experimentDistance
        //
        experimentDistance.HeaderText = "Расстояние, км";
        experimentDistance.Name = "experimentDistance";
        experimentDistance.ReadOnly = true;
        //
        // experimentControls
        //
        experimentControls.Controls.Add(experimentWaitLabel);
        experimentControls.Controls.Add(experimentWait);
        experimentControls.Dock = DockStyle.Top;
        experimentControls.Location = new Point(3, 3);
        experimentControls.Name = "experimentControls";
        experimentControls.Padding = new Padding(4);
        experimentControls.Size = new Size(666, 38);
        experimentControls.TabIndex = 0;
        //
        // experimentWaitLabel
        //
        experimentWaitLabel.AutoSize = true;
        experimentWaitLabel.Location = new Point(7, 10);
        experimentWaitLabel.Margin = new Padding(3, 6, 3, 0);
        experimentWaitLabel.Name = "experimentWaitLabel";
        experimentWaitLabel.Size = new Size(205, 15);
        experimentWaitLabel.TabIndex = 0;
        experimentWaitLabel.Text = "Ожидание для всех поездок, мин";
        //
        // experimentWait
        //
        experimentWait.Location = new Point(218, 7);
        experimentWait.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
        experimentWait.Name = "experimentWait";
        experimentWait.Size = new Size(64, 23);
        experimentWait.TabIndex = 1;
        //
        // experimentDescription
        //
        experimentDescription.Dock = DockStyle.Bottom;
        experimentDescription.Location = new Point(3, 321);
        experimentDescription.Name = "experimentDescription";
        experimentDescription.Size = new Size(666, 28);
        experimentDescription.TabIndex = 2;
        experimentDescription.Text = "Для сравнения меняем расстояние и тариф при одинаковом времени ожидания.";
        experimentDescription.TextAlign = ContentAlignment.MiddleLeft;
        //
        // MainForm
        //
        AcceptButton = calculateButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(680, 380);
        Controls.Add(tabs);
        MinimumSize = new Size(620, 360);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Калькулятор такси — версия 2";
        tabs.ResumeLayout(false);
        calculatorPage.ResumeLayout(false);
        calculatorPage.PerformLayout();
        historyPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)historyGrid).EndInit();
        experimentPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)experimentGrid).EndInit();
        experimentControls.ResumeLayout(false);
        experimentControls.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)experimentWait).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TabControl tabs;
    private TabPage calculatorPage;
    private Label distanceLabel;
    private TextBox distanceInput;
    private Label waitLabel;
    private TextBox waitInput;
    private Label tariffLabel;
    private ComboBox tariffInput;
    private Label rates;
    private Button calculateButton;
    private Label result;
    private TabPage historyPage;
    private DataGridView historyGrid;
    private DataGridViewTextBoxColumn historyTime;
    private DataGridViewTextBoxColumn historyTariff;
    private DataGridViewTextBoxColumn historyDistance;
    private DataGridViewTextBoxColumn historyWait;
    private DataGridViewTextBoxColumn historyTotal;
    private Label historyStatus;
    private TabPage experimentPage;
    private DataGridView experimentGrid;
    private DataGridViewTextBoxColumn experimentDistance;
    private FlowLayoutPanel experimentControls;
    private Label experimentWaitLabel;
    private NumericUpDown experimentWait;
    private Label experimentDescription;
}
