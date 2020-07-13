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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 13);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(151, 49);
            this.button4.TabIndex = 3;
            this.button4.Text = "Load Catagories";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(171, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1136, 372);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 634);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1295, 138);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(170, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(182, 21);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 49);
            this.button1.TabIndex = 9;
            this.button1.Text = "Load Medias";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(638, 442);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Play";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(638, 469);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(12, 363);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(151, 49);
            this.button5.TabIndex = 12;
            this.button5.Text = "Rip Current Category";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(638, 495);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(101, 23);
            this.button6.TabIndex = 13;
            this.button6.Text = "Download";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 610);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Bearer ID: Required. Used for authentication on Headspace servers";
            // 
            // title
            // 
            this.title.Location = new System.Drawing.Point(170, 445);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(237, 20);
            this.title.TabIndex = 15;
            // 
            // contentID
            // 
            this.contentID.Location = new System.Drawing.Point(413, 445);
            this.contentID.Name = "contentID";
            this.contentID.Size = new System.Drawing.Size(65, 20);
            this.contentID.TabIndex = 16;
            // 
            // entityID
            // 
            this.entityID.Location = new System.Drawing.Point(484, 445);
            this.entityID.Name = "entityID";
            this.entityID.Size = new System.Drawing.Size(65, 20);
            this.entityID.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Title";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 429);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Content ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(481, 429);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Entity ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(552, 429);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Ordinal Number";
            // 
            // ordinalNumber
            // 
            this.ordinalNumber.Location = new System.Drawing.Point(555, 445);
            this.ordinalNumber.Name = "ordinalNumber";
            this.ordinalNumber.Size = new System.Drawing.Size(77, 20);
            this.ordinalNumber.TabIndex = 21;
            // 
            // body
            // 
            this.body.Location = new System.Drawing.Point(170, 470);
            this.body.Multiline = true;
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(237, 48);
            this.body.TabIndex = 23;
            // 
            // subtext
            // 
            this.subtext.Location = new System.Drawing.Point(484, 472);
            this.subtext.Name = "subtext";
            this.subtext.Size = new System.Drawing.Size(148, 20);
            this.subtext.TabIndex = 24;
            // 
            // subtextSecondary
            // 
            this.subtextSecondary.Location = new System.Drawing.Point(484, 498);
            this.subtextSecondary.Name = "subtextSecondary";
            this.subtextSecondary.Size = new System.Drawing.Size(148, 20);
            this.subtextSecondary.TabIndex = 25;
            // 
            // imageMediaId
            // 
            this.imageMediaId.Location = new System.Drawing.Point(416, 549);
            this.imageMediaId.Name = "imageMediaId";
            this.imageMediaId.Size = new System.Drawing.Size(101, 20);
            this.imageMediaId.TabIndex = 26;
            // 
            // headerImageMedia
            // 
            this.headerImageMedia.Location = new System.Drawing.Point(523, 549);
            this.headerImageMedia.Name = "headerImageMedia";
            this.headerImageMedia.Size = new System.Drawing.Size(109, 20);
            this.headerImageMedia.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(413, 530);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Image Media ID";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(520, 530);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Header Image Media";
            // 
            // paidContent
            // 
            this.paidContent.AutoSize = true;
            this.paidContent.Location = new System.Drawing.Point(170, 575);
            this.paidContent.Name = "paidContent";
            this.paidContent.Size = new System.Drawing.Size(98, 17);
            this.paidContent.TabIndex = 30;
            this.paidContent.Text = "Is Paid Content";
            this.paidContent.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(830, 592);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(101, 23);
            this.button7.TabIndex = 31;
            this.button7.Text = "Open Folder";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(520, 579);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Secondary Media ID";
            // 
            // secondaryMediaId
            // 
            this.secondaryMediaId.Location = new System.Drawing.Point(523, 595);
            this.secondaryMediaId.Name = "secondaryMediaId";
            this.secondaryMediaId.Size = new System.Drawing.Size(109, 20);
            this.secondaryMediaId.TabIndex = 32;
            // 
            // description
            // 
            this.description.Location = new System.Drawing.Point(170, 521);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(237, 48);
            this.description.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(413, 579);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Media ID";
            // 
            // mediaId
            // 
            this.mediaId.Location = new System.Drawing.Point(416, 595);
            this.mediaId.Name = "mediaId";
            this.mediaId.Size = new System.Drawing.Size(101, 20);
            this.mediaId.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(635, 530);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Session ID";
            // 
            // sessionId
            // 
            this.sessionId.Location = new System.Drawing.Point(638, 548);
            this.sessionId.Name = "sessionId";
            this.sessionId.Size = new System.Drawing.Size(101, 20);
            this.sessionId.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(413, 475);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Duration";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(413, 501);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 40;
            this.label12.Text = "Voice";
            // 
            // variations
            // 
            this.variations.FormattingEnabled = true;
            this.variations.Location = new System.Drawing.Point(638, 594);
            this.variations.Name = "variations";
            this.variations.Size = new System.Drawing.Size(101, 21);
            this.variations.TabIndex = 41;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(635, 579);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "Variations";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(827, 426);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 43;
            this.label14.Text = "Notes:";
            // 
            // notes
            // 
            this.notes.Location = new System.Drawing.Point(830, 445);
            this.notes.Multiline = true;
            this.notes.Name = "notes";
            this.notes.Size = new System.Drawing.Size(477, 69);
            this.notes.TabIndex = 44;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 784);
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
            this.Name = "Form1";
            this.Text = "HeadRipper";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
    }
}

