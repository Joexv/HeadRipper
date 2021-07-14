using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HeadRipper.Alt_Forms
{
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
        }

        private void Category_Load(object sender, EventArgs e)
        {
            foreach (string Item in File.ReadAllLines("Definitive.txt"))
                definitive.Items.Add(Item);

            foreach (string Item in File.ReadAllLines("Variable.txt"))
                variable.Items.Add(Item);
        }

        private void def2var_Click(object sender, EventArgs e)
        {
            string item = definitive.SelectedItem.ToString();
            definitive.Items.Remove(item);
            variable.Items.Add(item);
        }

        private void var2def_Click(object sender, EventArgs e)
        {
            string item = variable.SelectedItem.ToString();
            definitive.Items.Add(item);
            variable.Items.Remove(item);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete("Variable.txt");
            File.WriteAllLines("Variable.txt", variable.Items.OfType<string>().ToArray());

            File.Delete("Definitive.txt");
            File.WriteAllLines("Definitive.txt", definitive.Items.OfType<string>().ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            definitive.Items.Remove(definitive.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            variable.Items.Remove(variable.SelectedItem);
        }
    }
}
