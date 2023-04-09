using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreProject
{
    public partial class Form1 : Form
    {
        StoreModel storedb = new StoreModel();
        public Form1()
        {
            InitializeComponent();
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Store store in storedb.Stores)
            {
                AllStoresIds.Items.Add(store.store_id);
            }
        }

        private void ToggleGroupBox(GroupBox groupBox)
        {
            groupBox.Visible = !groupBox.Visible;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox3);
        }

        private void AddStore_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)|| string.IsNullOrEmpty(textBox2.Text)) 
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }
            try
            {
                using(var storedb=new StoreModel())
                {
                    Store store = new Store();
                    store.store_name = textBox1.Text;
                    store.store_address = textBox2.Text;
                    store.store_manager = textBox3.Text;
                    storedb.Stores.Add(store);
                    storedb.SaveChanges();
                    MessageBox.Show("Store has been added successfully....");
                    textBox1.Text = textBox2.Text = textBox3.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occured" + ex.Message);
            }
        }

        private void UpdateStore_button_Click(object sender, EventArgs e)
        {
            //Store store = storedb.Stores.Find(int.Parse());
            //MessageBox.Show(store.store_id.ToString());

            //if (store != null)
            //{
            //    store.store_name = textBox5.Text;
            //    store.store_address = textBox6.Text;
            //    store.store_manager = textBox7.Text;
            //    storedb.SaveChanges();
            //    MessageBox.Show("Store has been updated successfully...");
            //}
        }

        private void AllStoresIds_SelectedIndexChanged(object sender, EventArgs e)
        {
            int storeId = int.Parse(AllStoresIds.Text);
            Store store = storedb.Stores.Find(storeId);
            MessageBox.Show("You Choosed ["+store.store_name +"] "+ "store");
            textBox5.Text = store.store_name;
            textBox6.Text = store.store_address;
            textBox7.Text = store.store_manager;
           // AllStoresIds.Items.Clear();
            AllStoresIds.Text = " ";

        }
    }
}
