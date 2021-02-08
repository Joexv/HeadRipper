using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace HeadRipper
{
    using ps = Properties.Settings;
    public partial class Form1 : Form
    {
        HeadspaceAPI hsAPI = new HeadspaceAPI();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("Bearer.txt"))
                textBox1.Text = File.ReadAllText("Bearer.txt");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText("Bearer.txt", textBox1.Text);
        }

        public string BearerBox { get; set; }

        private void button4_Click(object sender, EventArgs e)
        {
            string Category = meditate.Checked ? "MEDITATE" : "SLEEP";
           comboBox1.DataSource = hsAPI.ParseCategories(Category);
           if(comboBox1.Items.Count > 0)
           {
                button1.Enabled = true;
                button5.Enabled = sleep.Checked;
           }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParseNotes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = hsAPI.ParseMedia(comboBox1.Text);
            ClearMedia();
            //Hide useless columns
            Hide("id");
            Hide("i18nSrcTitle");
            Hide("contentType");
            Hide("contentTypeDisplayValue");
            Hide("labelColorTheme");
            Hide("location");
            Hide("trackingName");
            Hide("freeToTry");
            Hide("primaryColor");
            Hide("secondaryColor");
            Hide("tertiaryColor");
            Hide("patternMediaId");
            Hide("contentInfoScreenTheme");
            Hide("name");
            Hide("categoryType");
            Hide("topicMenuId");
            Hide("subtextSecondary");
            Hide("dailySession");
        }

        private void Hide(string Column)
        {
            try
            {
                dataGridView1.Columns[Column].Visible = false;
            }
            catch(Exception ex) { Console.WriteLine("Column failed to hide, most likely this just means its trying to hide a column from a category that's not open " + ex.ToString()); }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearMedia();
            //this.custTableView.Rows[this.custTableView.CurrentRow.Index].Cells["ID"].Value
            if (sleep.Checked)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        ParseDefinitive();
                        break;
                    case 2:
                        ParseSOS();
                        break;
                    default:
                        ParseVarious();
                        break;
                }
            }
            else
            {
                switch (comboBox1.SelectedIndex)
                {
                    default:
                        ParseVarious();
                        break;
                    case 1:
                    case 3:
                        ParseOddities();
                        break;
                }
                
            }
            

            Application.DoEvents();

            string variation = $"{title.Text.Replace(" ", "_")}_{variations.Text}.aac";
            string media = $"{title.Text.Replace(" ", "_")}_{mediaId.Text}.aac";
            string mixed = $"{title.Text.Replace(" ", "_")}_mixed_{sessionId.Text}.mp3";
            bool Exists = false;

            Exists = (File.Exists(variation) && variations.Text != "NA" && variations.Text != string.Empty);
            Exists = (File.Exists(media) || File.Exists(mixed));

            //Will hopefully default to the first real variation rather than NA
            if(variations.Items.Count > 2)
                variations.SelectedIndex = 1;
        }

        private void ParseNotes()
        {
            if (sleep.Checked)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        notes.Text = "Sleepcasts normally run about 45 minutes. Each consists of two audio files, the voice over, and some background noise. When downloaded using Headripper they are merged on the fly into a single mp3 in order to reduce size, and allow you to use any audio player you would like. Sleepcasts are normally changed on a 24 hour cycle.";
                        break;
                    case 1:
                        notes.Text = "Wind Downs come in many different sizes and voice overs. These are all available in the variations drop down. Sadly at the moment they aren't differited and can currently only be identified as their ID. Wind Downs are downloaded as aac files.";
                        break;
                    case 2:
                        notes.Text = "Nighttime SOS are short audio clips normally with a single voice over. They are downloaded as aac files.";
                        break;
                    case 3:
                        notes.Text = "Sleep music can be upwards of an hour. Be warned when downloading that RAM usage will spike heavily! The built in garbage collector is a pain and will only release the RAM either, when the program is restarted or if you download another audio track. Sleepmusic is downloaded as aac files.";
                        break;
                    case 4:
                        notes.Text = "Sound scapes can range from 45 minutes to several hours. Be warned when downloading that RAM usage will spike heavily! The built in garbage collector is a pain and will only release the RAM either, when the program is restarted or if you download another audio track. Sound scapes are downloaded as aac files. Some sound scapes the primary media is the audio that is recommended to download. Some variations appear to have voice overs which is unusual for sound scapes.";
                        break;
                    case 5:
                        notes.Text = "Sleep radio is 500 minutes long. Roughly 1GB each! Be warned when downloading that RAM usage will spike heavily! The built in garbage collector is a pain and will only release the RAM either, when the program is restarted or if you download another audio track. Sleep radios are download as aac files.";
                        break;
                    default:
                        notes.Text = "No notes";
                        break;
                }
            }
            else
            {
                switch (comboBox1.SelectedIndex)
                {
                    default:
                        notes.Text = "No Notes";
                        break;
                    case 0:
                        notes.Text = "Courses and singles  range from 10-20 minutes. They have variations that can be selected and are downloaded as aac files.";
                        break;
                    case 1:
                        notes.Text = "SOS are short 3 minute clips with no variations. Downloaded as aac files.";
                        break;
                    case 2:
                        notes.Text = "Timers range from 5 minutes all the way up to 2 hours. Be warned that this can take up heavy amounts of RAM when downloading! Downloaded as aac files.";
                        break;
                    case 3:
                        notes.Text = "Techniques and support are very short clips ranging from under a minute to 6 minutes. There are no variations. Downloaded as aac files.";
                        break;
                }
            }
            
        }

        private void ClearMedia()
        {
            entityID.Text = "";
            title.Text = "";
            body.Text = "";
            description.Text = "";
            subtext.Text = "";
            contentID.Text = "";
            ordinalNumber.Text = "";
            subtextSecondary.Text = "";
            imageMediaId.Text = "";
            headerImageMedia.Text = "";
            paidContent.Checked = false;
            secondaryMediaId.Text = "";
            mediaId.Text = "";
            sessionId.Text = "";
            variations.DataSource = null;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void ParseDefinitive(string Category = "sleepcasts")
        {
            entityID.Text = Read("entityId").ToString();
            Application.DoEvents();
            if(entityID.Text != "0")
            {
                SleepcastContent.Attributes SA = hsAPI.ParseContent(Category, Read("entityId").ToString());
                title.Text = (String)Read("title");
                body.Text = SA.subtitle;
                description.Text = SA.description;
                subtext.Text = (String)Read("subtext");
                contentID.Text = Read("contentId").ToString();
                ordinalNumber.Text = Read("ordinalNumber").ToString();
                subtextSecondary.Text = (String)Read("subtextSecondary");
                imageMediaId.Text = Read("imageMediaId").ToString();
                headerImageMedia.Text = Read("headerImageMediaId").ToString();
                paidContent.Checked = (bool)Read("SubscriberContent");
                secondaryMediaId.Text = SA.dailySession.secondaryMediaId.ToString();
                mediaId.Text = SA.dailySession.primaryMediaId.ToString();
                sessionId.Text = SA.dailySession.episodeId.ToString();
            }
        }

        private void ParseOddities()
        {
            try
            {
                entityID.Text = Read("entityId").ToString();
                Application.DoEvents();
                if (entityID.Text != "0")
                {
                    title.Text = (String)Read("title");
                    subtext.Text = (String)Read("subtext");
                    contentID.Text = Read("contentId").ToString();
                    ordinalNumber.Text = Read("ordinalNumber").ToString();
                    subtextSecondary.Text = (String)Read("subtextSecondary");
                    imageMediaId.Text = Read("imageMediaId").ToString();
                    headerImageMedia.Text = Read("headerImageMediaId").ToString();
                    paidContent.Checked = (bool)Read("SubscriberContent");
                    mediaId.Text = hsAPI.ParseAnimationId(contentID.Text);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ParseVarious()
        {
            entityID.Text = Read("entityId").ToString();
            Application.DoEvents();
            if (entityID.Text != "0")
            {
                title.Text = (String)Read("title");
                body.Text = (String)Read("bodyText");
                subtext.Text = (String)Read("subtext");
                contentID.Text = Read("contentId").ToString();
                ordinalNumber.Text = Read("ordinalNumber").ToString();
                subtextSecondary.Text = (String)Read("subtextSecondary");
                imageMediaId.Text = Read("imageMediaId").ToString();
                headerImageMedia.Text = Read("headerImageMediaId").ToString();
                paidContent.Checked = (bool)Read("SubscriberContent");

                List<String> IDs = hsAPI.ParseWindDown(Read("entityId").ToString());
                mediaId.Text = IDs[0];
                IDs.RemoveAt(0);
                IDs.Insert(0, "NA");

                variations.DataSource = IDs;
            }
        }

        private void ParseSOS()
        {
            entityID.Text = Read("entityId").ToString();
            Application.DoEvents();
            if (entityID.Text != "0")
            {
                title.Text = (String)Read("title");
                body.Text = (String)Read("bodyText");
                subtext.Text = (String)Read("subtext");
                contentID.Text = Read("contentId").ToString();
                ordinalNumber.Text = Read("ordinalNumber").ToString();
                subtextSecondary.Text = (String)Read("subtextSecondary");
                imageMediaId.Text = Read("imageMediaId").ToString();
                headerImageMedia.Text = Read("headerImageMediaId").ToString();
                paidContent.Checked = (bool)Read("SubscriberContent");
                mediaId.Text = hsAPI.ParseSOS(Read("entityId").ToString());
            }
        }

        private Object Read(string Cell)
        {
            try
            {
                return dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[Cell].Value;
            }
            catch { return 0; }
        }

        //Play
        private void button2_Click(object sender, EventArgs e)
        {
            string variation = $"{title.Text.Replace(" ", "_")}_{variations.Text}.aac";
            string media = $"{title.Text.Replace(" ", "_")}_{mediaId.Text}.aac";
            string mixed = $"{title.Text.Replace(" ", "_")}_mixed_{sessionId.Text}.mp3";
            //Play audio based on Variations ID
            if (File.Exists(variation) && variations.Text != "NA" && variations.Text != string.Empty)
                hsAPI.play(variation);

            //Play audio based on Media ID
            else if (File.Exists(media) && !File.Exists(mixed))
                hsAPI.play(media);

            //Play audio based on Mixed Session ID
            else if (File.Exists(mixed))
                hsAPI.play(mixed);
            else
                MessageBox.Show("Error, files missing");
        }
        //Stop
        private void button3_Click(object sender, EventArgs e)
        {
            hsAPI.stop();
        }

        //Sleep casts and SOS share similar enough download checks that its slightly cleaner to bundle them together.
        //Sorta......
        //Not really...........
        private string Download_Definitive()
        {
            if (File.Exists($"{title.Text.Replace(" ", "_")}_mixed_{sessionId.Text}.mp3"))
            {
                Console.WriteLine("Mixed file already exists");
                return $"{title.Text.Replace(" ", "_")}_mixed_{sessionId.Text}.mp3";
            }
            //Download file using Media ID, with Secondary ID used to download background noise
            else if (secondaryMediaId.Text != "0" && mediaId.Text != string.Empty && secondaryMediaId.Text != string.Empty)
            {
                return hsAPI.Download(mediaId.Text, secondaryMediaId.Text, title.Text.Replace(" ", "_"), sessionId.Text);
            }
            //Download File using Media ID, no Secondary Media
            else if (mediaId.Text != string.Empty && secondaryMediaId.Text == string.Empty)
            {
                return hsAPI.Download(mediaId.Text, title.Text.Replace(" ", "_"));
            }
            //Download File using Content ID
            else
            {
                return hsAPI.Download(contentID.Text, title.Text.Replace(" ", "_"));
            }
        }

        private string Download_Various()
        {
            //Download using Variations ID
            if(variations.Text != "NA" && variations.Text != string.Empty)
            {
                return hsAPI.Download(variations.Text, title.Text.Replace(" ", "_"));
            }
            //Download using Media ID
            else
            {
                return hsAPI.Download(mediaId.Text, title.Text.Replace(" ", "_"));
            }
        }
        
        //Download Button
        private void button6_Click(object sender, EventArgs e)
        {
            if (title.Text != string.Empty)
            {
                string DownloadName = "";
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                    case 2:
                        //Sleepcast and SOS
                        DownloadName = Download_Definitive();
                        break;
                    default:
                        //Wind Down, Sleepmusic, Soundscape, Sleep Radio
                        DownloadName = Download_Various();
                        break;
                }


                if (File.Exists(DownloadName))
                {
                    MessageBox.Show("Done");
                    button2.Enabled = true;
                    button3.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error", "Missing required audio file. File seems to have failed to download.");
                }
            }
        }

        //Download All Visible
        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Media.Attributes Matt in hsAPI.ParseMedia(comboBox1.Text))
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        //Sleepcast
                        try
                        {
                            SleepcastContent.Attributes SA = hsAPI.ParseContent("sleepcasts", Matt.entityId.ToString());
                            if (!File.Exists($"{Matt.title.Replace(" ", "_")}_mixed_{SA.dailySession.episodeId}.mp3") && Matt.title != string.Empty)
                                hsAPI.Download(SA.dailySession.primaryMediaId.ToString(), SA.dailySession.secondaryMediaId.ToString(),
                                    Matt.title.Replace(" ", "_"), SA.dailySession.episodeId.ToString());
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                        break;
                    case 1:
                    case 3:
                        //Wind Down & Sleep Music
                        try
                        {
                            List<String> IDs = hsAPI.ParseWindDown(Matt.entityId.ToString());
                            foreach (string ID in IDs)
                            {
                                if (!File.Exists($"{Matt.title}_{ID}.aac") && ID != "NA" && ID != string.Empty)
                                    hsAPI.Download(ID, Matt.title.Replace(" ", "_"));
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                        break;
                    case 2:
                        //SOS
                        try
                        {
                            if (!File.Exists($"{Matt.title}_{Matt.contentId}.aac"))
                                    hsAPI.Download(Matt.contentId.ToString(), Matt.title.Replace(" ", "_"));
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                        break;
                    default:
                        break;
                }
            }  
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void sleep_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
