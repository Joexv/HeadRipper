namespace HeadRipper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.button4 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.title = new System.Windows.Forms.TextBox();
			this.contentID = new System.Windows.Forms.TextBox();
			this.entityID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.ordinalNumber = new System.Windows.Forms.TextBox();
			this.body = new System.Windows.Forms.TextBox();
			this.subtext = new System.Windows.Forms.TextBox();
			this.subtextSecondary = new System.Windows.Forms.TextBox();
			this.imageMediaId = new System.Windows.Forms.TextBox();
			this.headerImageMedia = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.paidContent = new System.Windows.Forms.CheckBox();
			this.button7 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.secondaryMediaId = new System.Windows.Forms.TextBox();
			this.description = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.mediaId = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.sessionId = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.variations = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.notes = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.sleep = new System.Windows.Forms.RadioButton();
			this.meditate = new System.Windows.Forms.RadioButton();
			this.label16 = new System.Windows.Forms.Label();
			this.languages = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.beforeMerge = new System.Windows.Forms.CheckBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.mainVolume = new System.Windows.Forms.NumericUpDown();
			this.label17 = new System.Windows.Forms.Label();
			this.volumeUpDown = new System.Windows.Forms.NumericUpDown();
			this.autoMerge = new System.Windows.Forms.CheckBox();
			this.keepBackground = new System.Windows.Forms.CheckBox();
			this.keepMain = new System.Windows.Forms.CheckBox();
			this.button8 = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button9 = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button10 = new System.Windows.Forms.Button();
			this.focus = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mainVolume)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.volumeUpDown)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(16, 164);
			this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(201, 60);
			this.button4.TabIndex = 3;
			this.button4.Text = "Load Categories";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(228, 49);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersWidth = 82;
			this.dataGridView1.Size = new System.Drawing.Size(1981, 458);
			this.dataGridView1.TabIndex = 4;
			this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
			this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 806);
			this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(1725, 144);
			this.textBox1.TabIndex = 7;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(228, 17);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(241, 24);
			this.comboBox1.TabIndex = 8;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(16, 232);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(201, 60);
			this.button1.TabIndex = 9;
			this.button1.Text = "Load Medias";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(629, 37);
			this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(135, 28);
			this.button2.TabIndex = 10;
			this.button2.Text = "Play";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(629, 70);
			this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(135, 28);
			this.button3.TabIndex = 11;
			this.button3.Text = "Stop";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button5
			// 
			this.button5.Enabled = false;
			this.button5.Location = new System.Drawing.Point(19, 534);
			this.button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(201, 60);
			this.button5.TabIndex = 12;
			this.button5.Text = "Rip Current Category";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(629, 102);
			this.button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(135, 28);
			this.button6.TabIndex = 13;
			this.button6.Text = "Download";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 786);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(1021, 16);
			this.label1.TabIndex = 14;
			this.label1.Text = "Bearer ID: Required. Used for authentication on Headspace servers. Being actievly" +
    " logged into your browser has been reported to make HeadRipper perform more reli" +
    "ably";
			// 
			// title
			// 
			this.title.Location = new System.Drawing.Point(5, 41);
			this.title.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.title.Name = "title";
			this.title.Size = new System.Drawing.Size(315, 22);
			this.title.TabIndex = 15;
			// 
			// contentID
			// 
			this.contentID.Location = new System.Drawing.Point(329, 41);
			this.contentID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.contentID.Name = "contentID";
			this.contentID.Size = new System.Drawing.Size(85, 22);
			this.contentID.TabIndex = 16;
			// 
			// entityID
			// 
			this.entityID.Location = new System.Drawing.Point(423, 41);
			this.entityID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.entityID.Name = "entityID";
			this.entityID.Size = new System.Drawing.Size(85, 22);
			this.entityID.TabIndex = 17;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 17);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 16);
			this.label2.TabIndex = 18;
			this.label2.Text = "Title";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(325, 21);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 16);
			this.label3.TabIndex = 19;
			this.label3.Text = "Content ID";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(419, 21);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 16);
			this.label4.TabIndex = 20;
			this.label4.Text = "Entity ID";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(514, 21);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 16);
			this.label5.TabIndex = 22;
			this.label5.Text = "Ordinal Number";
			// 
			// ordinalNumber
			// 
			this.ordinalNumber.Location = new System.Drawing.Point(518, 41);
			this.ordinalNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ordinalNumber.Name = "ordinalNumber";
			this.ordinalNumber.Size = new System.Drawing.Size(101, 22);
			this.ordinalNumber.TabIndex = 21;
			// 
			// body
			// 
			this.body.Location = new System.Drawing.Point(5, 72);
			this.body.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.body.Multiline = true;
			this.body.Name = "body";
			this.body.Size = new System.Drawing.Size(315, 58);
			this.body.TabIndex = 23;
			// 
			// subtext
			// 
			this.subtext.Location = new System.Drawing.Point(423, 74);
			this.subtext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.subtext.Name = "subtext";
			this.subtext.Size = new System.Drawing.Size(196, 22);
			this.subtext.TabIndex = 24;
			// 
			// subtextSecondary
			// 
			this.subtextSecondary.Location = new System.Drawing.Point(423, 106);
			this.subtextSecondary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.subtextSecondary.Name = "subtextSecondary";
			this.subtextSecondary.Size = new System.Drawing.Size(196, 22);
			this.subtextSecondary.TabIndex = 25;
			// 
			// imageMediaId
			// 
			this.imageMediaId.Location = new System.Drawing.Point(333, 169);
			this.imageMediaId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.imageMediaId.Name = "imageMediaId";
			this.imageMediaId.Size = new System.Drawing.Size(133, 22);
			this.imageMediaId.TabIndex = 26;
			// 
			// headerImageMedia
			// 
			this.headerImageMedia.Location = new System.Drawing.Point(475, 169);
			this.headerImageMedia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.headerImageMedia.Name = "headerImageMedia";
			this.headerImageMedia.Size = new System.Drawing.Size(144, 22);
			this.headerImageMedia.TabIndex = 27;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(329, 145);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(102, 16);
			this.label6.TabIndex = 28;
			this.label6.Text = "Image Media ID";
			this.label6.Click += new System.EventHandler(this.label6_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(471, 145);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(135, 16);
			this.label7.TabIndex = 29;
			this.label7.Text = "Header Image Media";
			// 
			// paidContent
			// 
			this.paidContent.AutoSize = true;
			this.paidContent.Location = new System.Drawing.Point(5, 201);
			this.paidContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.paidContent.Name = "paidContent";
			this.paidContent.Size = new System.Drawing.Size(118, 20);
			this.paidContent.TabIndex = 30;
			this.paidContent.Text = "Is Paid Content";
			this.paidContent.UseVisualStyleBackColor = true;
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(772, 37);
			this.button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(135, 28);
			this.button7.TabIndex = 31;
			this.button7.Text = "Open Folder";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(471, 205);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(130, 16);
			this.label8.TabIndex = 33;
			this.label8.Text = "Secondary Media ID";
			// 
			// secondaryMediaId
			// 
			this.secondaryMediaId.Location = new System.Drawing.Point(475, 225);
			this.secondaryMediaId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.secondaryMediaId.Name = "secondaryMediaId";
			this.secondaryMediaId.Size = new System.Drawing.Size(144, 22);
			this.secondaryMediaId.TabIndex = 32;
			// 
			// description
			// 
			this.description.Location = new System.Drawing.Point(5, 134);
			this.description.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.description.Multiline = true;
			this.description.Name = "description";
			this.description.Size = new System.Drawing.Size(315, 58);
			this.description.TabIndex = 34;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(329, 205);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(61, 16);
			this.label9.TabIndex = 36;
			this.label9.Text = "Media ID";
			// 
			// mediaId
			// 
			this.mediaId.Location = new System.Drawing.Point(333, 225);
			this.mediaId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.mediaId.Name = "mediaId";
			this.mediaId.Size = new System.Drawing.Size(133, 22);
			this.mediaId.TabIndex = 35;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(625, 145);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 16);
			this.label10.TabIndex = 38;
			this.label10.Text = "Session ID";
			// 
			// sessionId
			// 
			this.sessionId.Location = new System.Drawing.Point(629, 168);
			this.sessionId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.sessionId.Name = "sessionId";
			this.sessionId.Size = new System.Drawing.Size(133, 22);
			this.sessionId.TabIndex = 37;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(329, 77);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(57, 16);
			this.label11.TabIndex = 39;
			this.label11.Text = "Duration";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(329, 109);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(42, 16);
			this.label12.TabIndex = 40;
			this.label12.Text = "Voice";
			// 
			// variations
			// 
			this.variations.FormattingEnabled = true;
			this.variations.Location = new System.Drawing.Point(629, 224);
			this.variations.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.variations.Name = "variations";
			this.variations.Size = new System.Drawing.Size(133, 24);
			this.variations.TabIndex = 41;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(625, 205);
			this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(67, 16);
			this.label13.TabIndex = 42;
			this.label13.Text = "Variations";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(1176, 511);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(46, 16);
			this.label14.TabIndex = 43;
			this.label14.Text = "Notes:";
			// 
			// notes
			// 
			this.notes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.notes.Location = new System.Drawing.Point(1179, 531);
			this.notes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.notes.Multiline = true;
			this.notes.Name = "notes";
			this.notes.Size = new System.Drawing.Size(562, 70);
			this.notes.TabIndex = 44;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(13, 315);
			this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(166, 192);
			this.label15.TabIndex = 45;
			this.label15.Text = "Step 1:\r\nLoad categories from \r\nHeadspace\r\n\r\nStep2:\r\nSelect category from \r\nthe d" +
    "rop down box\r\n\r\nStep 3:\r\nLoad Medias\r\nThen, select the audio you \r\nwant to downl" +
    "oad or rip all\r\n";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.focus);
			this.groupBox1.Controls.Add(this.sleep);
			this.groupBox1.Controls.Add(this.meditate);
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox1.Size = new System.Drawing.Size(201, 116);
			this.groupBox1.TabIndex = 46;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Category Type";
			// 
			// sleep
			// 
			this.sleep.AutoSize = true;
			this.sleep.Location = new System.Drawing.Point(9, 53);
			this.sleep.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.sleep.Name = "sleep";
			this.sleep.Size = new System.Drawing.Size(64, 20);
			this.sleep.TabIndex = 1;
			this.sleep.Text = "Sleep";
			this.sleep.UseVisualStyleBackColor = true;
			this.sleep.CheckedChanged += new System.EventHandler(this.sleep_CheckedChanged);
			// 
			// meditate
			// 
			this.meditate.AutoSize = true;
			this.meditate.Checked = true;
			this.meditate.Location = new System.Drawing.Point(9, 24);
			this.meditate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.meditate.Name = "meditate";
			this.meditate.Size = new System.Drawing.Size(80, 20);
			this.meditate.TabIndex = 0;
			this.meditate.TabStop = true;
			this.meditate.Text = "Meditate";
			this.meditate.UseVisualStyleBackColor = true;
			this.meditate.CheckedChanged += new System.EventHandler(this.meditate_CheckedChanged);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(506, 19);
			this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(68, 16);
			this.label16.TabIndex = 47;
			this.label16.Text = "Language";
			// 
			// languages
			// 
			this.languages.FormattingEnabled = true;
			this.languages.Items.AddRange(new object[] {
            "English (en-US)",
            "Dutch (de-DE)",
            "French (fr-FR)",
            "Spanish (es-ES)",
            "Portugese (pt-BR)"});
			this.languages.Location = new System.Drawing.Point(582, 16);
			this.languages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.languages.Name = "languages";
			this.languages.Size = new System.Drawing.Size(211, 24);
			this.languages.TabIndex = 48;
			this.languages.Text = "English (en-US)";
			this.languages.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.beforeMerge);
			this.groupBox2.Controls.Add(this.label19);
			this.groupBox2.Controls.Add(this.label18);
			this.groupBox2.Controls.Add(this.mainVolume);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.volumeUpDown);
			this.groupBox2.Controls.Add(this.autoMerge);
			this.groupBox2.Controls.Add(this.keepBackground);
			this.groupBox2.Controls.Add(this.keepMain);
			this.groupBox2.Location = new System.Drawing.Point(1178, 607);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(563, 168);
			this.groupBox2.TabIndex = 49;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Audio Settings";
			// 
			// beforeMerge
			// 
			this.beforeMerge.AutoSize = true;
			this.beforeMerge.Location = new System.Drawing.Point(291, 106);
			this.beforeMerge.Name = "beforeMerge";
			this.beforeMerge.Size = new System.Drawing.Size(224, 20);
			this.beforeMerge.TabIndex = 8;
			this.beforeMerge.Text = "Adjust audio before merging files";
			this.toolTip1.SetToolTip(this.beforeMerge, "Adjusting volume before the merge will allow you to have the \r\ntwo seperate files" +
        " with audio already changed. \r\nThis takes significantly longer to process though" +
        ".");
			this.beforeMerge.UseVisualStyleBackColor = true;
			this.beforeMerge.CheckedChanged += new System.EventHandler(this.beforeMerge_CheckedChanged);
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(5, 127);
			this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(422, 16);
			this.label19.TabIndex = 7;
			this.label19.Text = "Sometimes the main and background are swapped. Adjust as needed";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(287, 79);
			this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(85, 16);
			this.label18.TabIndex = 6;
			this.label18.Text = "Main Volume";
			// 
			// mainVolume
			// 
			this.mainVolume.DecimalPlaces = 2;
			this.mainVolume.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.mainVolume.Location = new System.Drawing.Point(435, 77);
			this.mainVolume.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.mainVolume.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.mainVolume.Name = "mainVolume";
			this.mainVolume.Size = new System.Drawing.Size(80, 22);
			this.mainVolume.TabIndex = 5;
			this.mainVolume.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.mainVolume.ValueChanged += new System.EventHandler(this.mainVolume_ValueChanged);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(287, 51);
			this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(129, 16);
			this.label17.TabIndex = 4;
			this.label17.Text = "Background Volume";
			// 
			// volumeUpDown
			// 
			this.volumeUpDown.DecimalPlaces = 2;
			this.volumeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.volumeUpDown.Location = new System.Drawing.Point(435, 49);
			this.volumeUpDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.volumeUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.volumeUpDown.Name = "volumeUpDown";
			this.volumeUpDown.Size = new System.Drawing.Size(80, 22);
			this.volumeUpDown.TabIndex = 3;
			this.volumeUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			// 
			// autoMerge
			// 
			this.autoMerge.AutoSize = true;
			this.autoMerge.Checked = true;
			this.autoMerge.CheckState = System.Windows.Forms.CheckState.Checked;
			this.autoMerge.Location = new System.Drawing.Point(291, 22);
			this.autoMerge.Name = "autoMerge";
			this.autoMerge.Size = new System.Drawing.Size(221, 20);
			this.autoMerge.TabIndex = 2;
			this.autoMerge.Text = "Merge Audio Files Automatically";
			this.autoMerge.UseVisualStyleBackColor = true;
			this.autoMerge.CheckedChanged += new System.EventHandler(this.mergeAuto_CheckedChanged);
			// 
			// keepBackground
			// 
			this.keepBackground.AutoSize = true;
			this.keepBackground.Checked = true;
			this.keepBackground.CheckState = System.Windows.Forms.CheckState.Checked;
			this.keepBackground.Location = new System.Drawing.Point(7, 49);
			this.keepBackground.Name = "keepBackground";
			this.keepBackground.Size = new System.Drawing.Size(237, 20);
			this.keepBackground.TabIndex = 1;
			this.keepBackground.Text = "Keep Background Audio On Merge";
			this.keepBackground.UseVisualStyleBackColor = true;
			this.keepBackground.CheckedChanged += new System.EventHandler(this.keepBackground_CheckedChanged);
			// 
			// keepMain
			// 
			this.keepMain.AutoSize = true;
			this.keepMain.Checked = true;
			this.keepMain.CheckState = System.Windows.Forms.CheckState.Checked;
			this.keepMain.Location = new System.Drawing.Point(7, 22);
			this.keepMain.Name = "keepMain";
			this.keepMain.Size = new System.Drawing.Size(193, 20);
			this.keepMain.TabIndex = 0;
			this.keepMain.Text = "Keep Main Audio On Merge";
			this.keepMain.UseVisualStyleBackColor = true;
			this.keepMain.CheckedChanged += new System.EventHandler(this.keepMain_CheckedChanged);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(19, 641);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(198, 58);
			this.button8.TabIndex = 50;
			this.button8.Text = "Edit Category Options";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(772, 70);
			this.button9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(135, 61);
			this.button9.TabIndex = 51;
			this.button9.Text = "Copy Info To Clipboard";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.button9);
			this.groupBox3.Controls.Add(this.button2);
			this.groupBox3.Controls.Add(this.button3);
			this.groupBox3.Controls.Add(this.button6);
			this.groupBox3.Controls.Add(this.title);
			this.groupBox3.Controls.Add(this.contentID);
			this.groupBox3.Controls.Add(this.entityID);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.ordinalNumber);
			this.groupBox3.Controls.Add(this.label13);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.variations);
			this.groupBox3.Controls.Add(this.body);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.subtext);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.subtextSecondary);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.imageMediaId);
			this.groupBox3.Controls.Add(this.sessionId);
			this.groupBox3.Controls.Add(this.headerImageMedia);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.mediaId);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.description);
			this.groupBox3.Controls.Add(this.paidContent);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.button7);
			this.groupBox3.Controls.Add(this.secondaryMediaId);
			this.groupBox3.Location = new System.Drawing.Point(228, 517);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox3.Size = new System.Drawing.Size(945, 257);
			this.groupBox3.TabIndex = 52;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Info";
			// 
			// button10
			// 
			this.button10.Location = new System.Drawing.Point(1613, 12);
			this.button10.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(130, 27);
			this.button10.TabIndex = 53;
			this.button10.Text = "Get BearerID";
			this.button10.UseVisualStyleBackColor = true;
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// focus
			// 
			this.focus.AutoSize = true;
			this.focus.Location = new System.Drawing.Point(9, 81);
			this.focus.Margin = new System.Windows.Forms.Padding(4);
			this.focus.Name = "focus";
			this.focus.Size = new System.Drawing.Size(65, 20);
			this.focus.TabIndex = 2;
			this.focus.Text = "Focus";
			this.focus.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1283, 675);
			this.Controls.Add(this.button10);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.languages);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.notes);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button4);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HeadRipper";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.mainVolume)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.volumeUpDown)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox title;
        private System.Windows.Forms.TextBox contentID;
        private System.Windows.Forms.TextBox entityID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ordinalNumber;
        private System.Windows.Forms.TextBox body;
        private System.Windows.Forms.TextBox subtext;
        private System.Windows.Forms.TextBox subtextSecondary;
        private System.Windows.Forms.TextBox imageMediaId;
        private System.Windows.Forms.TextBox headerImageMedia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox paidContent;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox secondaryMediaId;
        private System.Windows.Forms.TextBox description;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox mediaId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox sessionId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox variations;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox notes;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton sleep;
        private System.Windows.Forms.RadioButton meditate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox languages;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox autoMerge;
        private System.Windows.Forms.CheckBox keepBackground;
        private System.Windows.Forms.CheckBox keepMain;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown volumeUpDown;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown mainVolume;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox beforeMerge;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button10;
		private System.Windows.Forms.RadioButton focus;
	}
}

