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
            groupBox5.Visible = false;
            groupBox6.Visible = false;

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayStores();
            DisplayItems();
            DisplaySuppliers();
            DisplayCustomers();
        }

        #region General Functions
        private void DisplayStores()
        {
            AllStoresIds.Items.Clear();
            var stores = storedb.Stores;
            foreach (Store store in stores)
            {
                AllStoresIds.Items.Add(store.store_id + " - " + store.store_name);
            }
        }

        private void DisplayItems()
        {
            ItemsList.Items.Clear();
            var items = storedb.Items;
            foreach (Item item in items)
            {
                ItemsList.Items.Add(item.item_id + " - " + item.item_code + " - " + item.item_name);
            }
        }

        private void DisplaySuppliers()
        {
            SuppliersList.Items.Clear();
            var suppliers = storedb.Suppliers;
            foreach (Supplier supplier in suppliers)
            {
                SuppliersList.Items.Add(supplier.supplier_id + " - " + supplier.supplier_name);
            }
        }

        private void DisplayCustomers()
        {
            CustomersList.Items.Clear();
            var customers = storedb.Customers;
            foreach (Customer customer in customers)
            {
                CustomersList.Items.Add(customer.customer_id + " - " + customer.customer_name);
            }
        }
        private void ToggleGroupBox(GroupBox groupBox)
        {
            groupBox.Visible = !groupBox.Visible;
        } 
        #endregion


        #region Store
        private void button1_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox3);
        }
        private void AllStoresIds_SelectedIndexChanged(object sender, EventArgs e)
        {
            int storeId = int.Parse(AllStoresIds.Text.Split('-')[0].Trim());
            Store store = storedb.Stores.Find(storeId);
            if (store != null)
            {
                //MessageBox.Show("You Choosed [" + store.store_name + "] " + "store");
                textBox4.Text = storeId.ToString();
                textBox4.Enabled = false;
                textBox5.Text = store.store_name;
                textBox6.Text = store.store_address;
                textBox7.Text = store.store_manager;
            }
            else
            {
                MessageBox.Show("No Store with provided id...");
            }
        }

        private void AddStore_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }
            try
            {
                using (var storedb = new StoreModel())
                {
                    Store store = new Store();
                    store.store_name = textBox1.Text;
                    store.store_address = textBox2.Text;
                    store.store_manager = textBox3.Text;
                    storedb.Stores.Add(store);
                    storedb.SaveChanges();
                    MessageBox.Show("Store has been added successfully....");
                    textBox1.Text = textBox2.Text = textBox3.Text = "";
                    DisplayStores();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured" + ex.Message);
            }
        }

        private void UpdateStoreInDropDown(Store store)
        {
            foreach (string item in AllStoresIds.Items)
            {
                int id = int.Parse(item.Split('-')[0].Trim());
                //MessageBox.Show(id.ToString());
                if (id == store.store_id)
                {
                    int index = AllStoresIds.Items.IndexOf(item);
                    AllStoresIds.Items[index] = store.store_id.ToString() + " - " + store.store_name;
                    return;
                }
            }
        }
        private void UpdateStore_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }

            if (string.IsNullOrWhiteSpace(AllStoresIds.Text))
            {
                MessageBox.Show("Please select store from dropdown to update");
                return;
            }
            int storeId = int.Parse(textBox4.Text);
            // MessageBox.Show(storeId.ToString());

            Store store = storedb.Stores.Find(storeId);
            //MessageBox.Show(store.store_id.ToString());

            if (store != null)
            {
                store.store_name = textBox5.Text;
                store.store_address = textBox6.Text;
                store.store_manager = textBox7.Text;
                storedb.SaveChanges();
                MessageBox.Show("Store has been updated successfully...");
                UpdateStoreInDropDown(store);
                DisplayStores();
                textBox4.Text=textBox5.Text = textBox6.Text = textBox7.Text = "";
            }
            else
            {
                MessageBox.Show("No Store with provided id...");
            }
        }

        #endregion


        private void ItemsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemId = int.Parse(ItemsList.Text.Split('-')[0].Trim());
            Item item = storedb.Items.Find(itemId);
            if (item != null)
            {
                textBox40.Text = itemId.ToString();
                textBox40.Enabled = false;
                textBox11.Text = item.item_code.ToString();
                textBox12.Text = item.item_name;
                textBox13.Text = item.item_measure_unit;
            }
            else
            {
                MessageBox.Show("Invalid id...");
            }
        }
        private void AddItem_Toggle_Button_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox5);
        }

        private void EditItem_Toggle_Button_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox6);
        }

        private void AddItem_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }
            try
            {
                using (var storedb = new StoreModel())
                {
                    Item item = new Item();
                    item.item_code =int.Parse(textBox8.Text);
                    item.item_name = textBox9.Text;
                    item.item_measure_unit = textBox10.Text;
                    storedb.Items.Add(item);
                    storedb.SaveChanges();
                    MessageBox.Show("Item has been added successfully....");
                    textBox8.Text = textBox9.Text = textBox10.Text = "";
                    DisplayItems();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured" + ex.Message);
            }
        }

        private void UpdateItemInDropDown(Item itm)
        {
            foreach (string item in ItemsList.Items)
            {
                int id = int.Parse(item.Split('-')[0].Trim());
                //MessageBox.Show(id.ToString());
                if (id == itm.item_id)
                {
                    int index = ItemsList.Items.IndexOf(item);
                    ItemsList.Items[index] = itm.item_id.ToString() + " - " + itm.item_code + " - " + itm.item_name;
                    return;
                }
            }
        }
        private void UpdateItem_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox11.Text) || string.IsNullOrEmpty(textBox12.Text))
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }

            if (string.IsNullOrWhiteSpace(ItemsList.Text))
            {
                MessageBox.Show("Please select item from dropdown to update");
                return;
            }
            int itemId = int.Parse(textBox40.Text);
            Item item = storedb.Items.Find(itemId);

            if (item != null)
            {
                item.item_code= int.Parse(textBox11.Text);
                item.item_name= textBox12.Text;
                item.item_measure_unit= textBox13.Text;
                storedb.SaveChanges();
                MessageBox.Show("Item has been updated successfully...");
                UpdateItemInDropDown(item);
                DisplayItems();
                textBox11.Text = textBox12.Text = textBox13.Text = textBox40.Text= "";
            }
            else
            {
                MessageBox.Show("No Item found with provided id...");
            }
        }

        
    }
}
