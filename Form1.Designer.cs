﻿namespace HeadRipper
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(24, 163);
            this.button4.Margin = new System.Windows.Forms.Padding(6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(302, 94);
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
            this.dataGridView1.Location = new System.Drawing.Point(342, 77);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.Size = new System.Drawing.Size(2272, 715);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 1219);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(2586, 262);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(342, 27);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(360, 33);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(24, 269);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(302, 94);
            this.button1.TabIndex = 9;
            this.button1.Text = "Load Medias";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(1276, 850);
            this.button2.Margin = new System.Windows.Forms.Padding(6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(202, 44);
            this.button2.TabIndex = 10;
            this.button2.Text = "Play";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(1276, 902);
            this.button3.Margin = new System.Windows.Forms.Padding(6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(202, 44);
            this.button3.TabIndex = 11;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(24, 819);
            this.button5.Margin = new System.Windows.Forms.Padding(6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(302, 94);
            this.button5.TabIndex = 12;
            this.button5.Text = "Rip Current Category";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1276, 952);
            this.button6.Margin = new System.Windows.Forms.Padding(6);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(202, 44);
            this.button6.TabIndex = 13;
            this.button6.Text = "Download";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 1173);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(657, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "Bearer ID: Required. Used for authentication on Headspace servers";
            // 
            // title
            // 
            this.title.Location = new System.Drawing.Point(340, 856);
            this.title.Margin = new System.Windows.Forms.Padding(6);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(470, 31);
            this.title.TabIndex = 15;
            // 
            // contentID
            // 
            this.contentID.Location = new System.Drawing.Point(826, 856);
            this.contentID.Margin = new System.Windows.Forms.Padding(6);
            this.contentID.Name = "contentID";
            this.contentID.Size = new System.Drawing.Size(126, 31);
            this.contentID.TabIndex = 16;
            // 
            // entityID
            // 
            this.entityID.Location = new System.Drawing.Point(968, 856);
            this.entityID.Margin = new System.Windows.Forms.Padding(6);
            this.entityID.Name = "entityID";
            this.entityID.Size = new System.Drawing.Size(126, 31);
            this.entityID.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 819);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "Title";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(820, 825);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 25);
            this.label3.TabIndex = 19;
            this.label3.Text = "Content ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(962, 825);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 25);
            this.label4.TabIndex = 20;
            this.label4.Text = "Entity ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1104, 825);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 25);
            this.label5.TabIndex = 22;
            this.label5.Text = "Ordinal Number";
            // 
            // ordinalNumber
            // 
            this.ordinalNumber.Location = new System.Drawing.Point(1110, 856);
            this.ordinalNumber.Margin = new System.Windows.Forms.Padding(6);
            this.ordinalNumber.Name = "ordinalNumber";
            this.ordinalNumber.Size = new System.Drawing.Size(150, 31);
            this.ordinalNumber.TabIndex = 21;
            // 
            // body
            // 
            this.body.Location = new System.Drawing.Point(340, 904);
            this.body.Margin = new System.Windows.Forms.Padding(6);
            this.body.Multiline = true;
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(470, 89);
            this.body.TabIndex = 23;
            // 
            // subtext
            // 
            this.subtext.Location = new System.Drawing.Point(968, 908);
            this.subtext.Margin = new System.Windows.Forms.Padding(6);
            this.subtext.Name = "subtext";
            this.subtext.Size = new System.Drawing.Size(292, 31);
            this.subtext.TabIndex = 24;
            // 
            // subtextSecondary
            // 
            this.subtextSecondary.Location = new System.Drawing.Point(968, 958);
            this.subtextSecondary.Margin = new System.Windows.Forms.Padding(6);
            this.subtextSecondary.Name = "subtextSecondary";
            this.subtextSecondary.Size = new System.Drawing.Size(292, 31);
            this.subtextSecondary.TabIndex = 25;
            // 
            // imageMediaId
            // 
            this.imageMediaId.Location = new System.Drawing.Point(832, 1056);
            this.imageMediaId.Margin = new System.Windows.Forms.Padding(6);
            this.imageMediaId.Name = "imageMediaId";
            this.imageMediaId.Size = new System.Drawing.Size(198, 31);
            this.imageMediaId.TabIndex = 26;
            // 
            // headerImageMedia
            // 
            this.headerImageMedia.Location = new System.Drawing.Point(1046, 1056);
            this.headerImageMedia.Margin = new System.Windows.Forms.Padding(6);
            this.headerImageMedia.Name = "headerImageMedia";
            this.headerImageMedia.Size = new System.Drawing.Size(214, 31);
            this.headerImageMedia.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(826, 1019);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 25);
            this.label6.TabIndex = 28;
            this.label6.Text = "Image Media ID";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1040, 1019);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(211, 25);
            this.label7.TabIndex = 29;
            this.label7.Text = "Header Image Media";
            // 
            // paidContent
            // 
            this.paidContent.AutoSize = true;
            this.paidContent.Location = new System.Drawing.Point(340, 1106);
            this.paidContent.Margin = new System.Windows.Forms.Padding(6);
            this.paidContent.Name = "paidContent";
            this.paidContent.Size = new System.Drawing.Size(190, 29);
            this.paidContent.TabIndex = 30;
            this.paidContent.Text = "Is Paid Content";
            this.paidContent.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1660, 1138);
            this.button7.Margin = new System.Windows.Forms.Padding(6);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(202, 44);
            this.button7.TabIndex = 31;
            this.button7.Text = "Open Folder";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1040, 1113);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(206, 25);
            this.label8.TabIndex = 33;
            this.label8.Text = "Secondary Media ID";
            // 
            // secondaryMediaId
            // 
            this.secondaryMediaId.Location = new System.Drawing.Point(1046, 1144);
            this.secondaryMediaId.Margin = new System.Windows.Forms.Padding(6);
            this.secondaryMediaId.Name = "secondaryMediaId";
            this.secondaryMediaId.Size = new System.Drawing.Size(214, 31);
            this.secondaryMediaId.TabIndex = 32;
            // 
            // description
            // 
            this.description.Location = new System.Drawing.Point(340, 1002);
            this.description.Margin = new System.Windows.Forms.Padding(6);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(470, 89);
            this.description.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(826, 1113);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 25);
            this.label9.TabIndex = 36;
            this.label9.Text = "Media ID";
            // 
            // mediaId
            // 
            this.mediaId.Location = new System.Drawing.Point(832, 1144);
            this.mediaId.Margin = new System.Windows.Forms.Padding(6);
            this.mediaId.Name = "mediaId";
            this.mediaId.Size = new System.Drawing.Size(198, 31);
            this.mediaId.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1270, 1019);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 25);
            this.label10.TabIndex = 38;
            this.label10.Text = "Session ID";
            // 
            // sessionId
            // 
            this.sessionId.Location = new System.Drawing.Point(1276, 1054);
            this.sessionId.Margin = new System.Windows.Forms.Padding(6);
            this.sessionId.Name = "sessionId";
            this.sessionId.Size = new System.Drawing.Size(198, 31);
            this.sessionId.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(826, 913);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 25);
            this.label11.TabIndex = 39;
            this.label11.Text = "Duration";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(826, 963);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 25);
            this.label12.TabIndex = 40;
            this.label12.Text = "Voice";
            // 
            // variations
            // 
            this.variations.FormattingEnabled = true;
            this.variations.Location = new System.Drawing.Point(1276, 1142);
            this.variations.Margin = new System.Windows.Forms.Padding(6);
            this.variations.Name = "variations";
            this.variations.Size = new System.Drawing.Size(198, 33);
            this.variations.TabIndex = 41;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1270, 1113);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 25);
            this.label13.TabIndex = 42;
            this.label13.Text = "Variations";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1654, 819);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 25);
            this.label14.TabIndex = 43;
            this.label14.Text = "Notes:";
            // 
            // notes
            // 
            this.notes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notes.Location = new System.Drawing.Point(1660, 856);
            this.notes.Margin = new System.Windows.Forms.Padding(6);
            this.notes.Multiline = true;
            this.notes.Name = "notes";
            this.notes.Size = new System.Drawing.Size(950, 129);
            this.notes.TabIndex = 44;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 398);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(272, 300);
            this.label15.TabIndex = 45;
            this.label15.Text = "Step 1:\r\nLoad categories from \r\nHeadspace\r\n\r\nStep2:\r\nSelect category from \r\nthe d" +
    "rop down box\r\n\r\nStep 3:\r\nLoad Medias\r\nThen, select the audio you \r\nwant to downl" +
    "oad or rip all\r\n";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sleep);
            this.groupBox1.Controls.Add(this.meditate);
            this.groupBox1.Location = new System.Drawing.Point(24, 25);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(302, 127);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Category Type";
            // 
            // sleep
            // 
            this.sleep.AutoSize = true;
            this.sleep.Location = new System.Drawing.Point(14, 83);
            this.sleep.Margin = new System.Windows.Forms.Padding(6);
            this.sleep.Name = "sleep";
            this.sleep.Size = new System.Drawing.Size(98, 29);
            this.sleep.TabIndex = 1;
            this.sleep.Text = "Sleep";
            this.sleep.UseVisualStyleBackColor = true;
            this.sleep.CheckedChanged += new System.EventHandler(this.sleep_CheckedChanged);
            // 
            // meditate
            // 
            this.meditate.AutoSize = true;
            this.meditate.Checked = true;
            this.meditate.Location = new System.Drawing.Point(14, 38);
            this.meditate.Margin = new System.Windows.Forms.Padding(6);
            this.meditate.Name = "meditate";
            this.meditate.Size = new System.Drawing.Size(126, 29);
            this.meditate.TabIndex = 0;
            this.meditate.TabStop = true;
            this.meditate.Text = "Meditate";
            this.meditate.UseVisualStyleBackColor = true;
            this.meditate.CheckedChanged += new System.EventHandler(this.meditate_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(759, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(108, 25);
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
            this.languages.Location = new System.Drawing.Point(873, 25);
            this.languages.Name = "languages";
            this.languages.Size = new System.Drawing.Size(314, 33);
            this.languages.TabIndex = 48;
            this.languages.Text = "English (en-US)";
            this.languages.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2644, 1508);
            this.Controls.Add(this.languages);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.notes);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.variations);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.sessionId);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.mediaId);
            this.Controls.Add(this.description);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.secondaryMediaId);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.paidContent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.headerImageMedia);
            this.Controls.Add(this.imageMediaId);
            this.Controls.Add(this.subtextSecondary);
            this.Controls.Add(this.subtext);
            this.Controls.Add(this.body);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ordinalNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.entityID);
            this.Controls.Add(this.contentID);
            this.Controls.Add(this.title);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "HeadRipper";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
    }
}

