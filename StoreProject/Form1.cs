﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
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
            groupBox8.Visible = false;
            groupBox9.Visible = false;
            groupBox11.Visible = false;
            groupBox12.Visible = false;
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


        #region Item
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
                    item.item_code = int.Parse(textBox8.Text);
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

            if (string.IsNullOrWhiteSpace(ItemsList.Text))
            {
                MessageBox.Show("Please select item from dropdown to update");
                return;
            }

            if (string.IsNullOrEmpty(textBox11.Text) || string.IsNullOrEmpty(textBox12.Text))
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }
            int itemId = int.Parse(textBox40.Text);
            Item item = storedb.Items.Find(itemId);

            if (item != null)
            {
                item.item_code = int.Parse(textBox11.Text);
                item.item_name = textBox12.Text;
                item.item_measure_unit = textBox13.Text;
                storedb.SaveChanges();
                MessageBox.Show("Item has been updated successfully...");
                UpdateItemInDropDown(item);
                DisplayItems();
                textBox11.Text = textBox12.Text = textBox13.Text = textBox40.Text = "";
            }
            else
            {
                MessageBox.Show("No Item found with provided id...");
            }
        }

        #endregion


        #region Suppliers
        private void SuppliersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int supplierId = int.Parse(SuppliersList.Text.Split('-')[0].Trim());
            Supplier supplier = storedb.Suppliers.Find(supplierId);
            if (supplier != null)
            {
                textBox26.Text = supplierId.ToString();
                textBox26.Enabled = false;
                textBox20.Text = supplier.supplier_name;
                textBox21.Text = supplier.supplier_phone;
                textBox22.Text = supplier.supplier_fax;
                textBox23.Text = supplier.supplier_mobile;
                textBox24.Text = supplier.supplier_email;
                textBox25.Text = supplier.supplier_website;
            }
            else
            {
                MessageBox.Show("Invalid id...");
            }
        }

        private void AddSupplier_Toggle_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox9);
        }

        private void EditSupplier_Toggle_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox8);
        }

        private void AddSupplier_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox14.Text) || string.IsNullOrEmpty(textBox17.Text))
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }
            try
            {
                using (var storedb = new StoreModel())
                {
                    Supplier supplier = new Supplier();
                    supplier.supplier_name = textBox14.Text;
                    supplier.supplier_phone = textBox15.Text;
                    supplier.supplier_fax = textBox16.Text;
                    supplier.supplier_mobile = textBox17.Text;
                    supplier.supplier_email = textBox18.Text;
                    supplier.supplier_website = textBox19.Text;
                    storedb.Suppliers.Add(supplier);
                    storedb.SaveChanges();
                    MessageBox.Show("Supplier has been added successfully....");
                    textBox14.Text = textBox15.Text = textBox16.Text =
                    textBox17.Text = textBox18.Text = textBox19.Text = "";
                    DisplaySuppliers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured" + ex.Message);
            }

        }

        private void UpdateSupplierInDropDown(Supplier supplier)
        {
            foreach (string supp in SuppliersList.Items)
            {
                int id = int.Parse(supp.Split('-')[0].Trim());
                //MessageBox.Show(id.ToString());
                if (id == supplier.supplier_id)
                {
                    int index = SuppliersList.Items.IndexOf(supp);
                    SuppliersList.Items[index] = supplier.supplier_id.ToString() + " - " + supplier.supplier_name;
                    return;
                }
            }
        }
        private void UpdateSupplier_button_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(SuppliersList.Text))
            {
                MessageBox.Show("Please select item from dropdown to update");
                return;
            }

            if (string.IsNullOrEmpty(textBox20.Text) || string.IsNullOrEmpty(textBox23.Text))
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }
            int supplierId = int.Parse(textBox26.Text);
            Supplier supplier = storedb.Suppliers.Find(supplierId);

            if (supplier != null)
            {
                supplier.supplier_name = textBox20.Text;
                supplier.supplier_phone = textBox21.Text;
                supplier.supplier_fax = textBox22.Text;
                supplier.supplier_mobile = textBox23.Text;
                supplier.supplier_email = textBox24.Text;
                supplier.supplier_website = textBox25.Text;
                storedb.SaveChanges();
                MessageBox.Show("Supplier has been updated successfully...");
                UpdateSupplierInDropDown(supplier);
                DisplaySuppliers();
                textBox20.Text = textBox21.Text = textBox22.Text = textBox23.Text =
                textBox24.Text = textBox25.Text = textBox26.Text = "";
            }
            else
            {
                MessageBox.Show("No Supplier found with provided id...");
            }
        }
        #endregion


        #region Customers
        private void CustomersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int customerId = int.Parse(CustomersList.Text.Split('-')[0].Trim());
            Customer customer = storedb.Customers.Find(customerId);
            if (customer != null)
            {
                textBox27.Text = customerId.ToString();
                textBox27.Enabled = false;
                textBox28.Text = customer.customer_name;
                textBox29.Text = customer.customer_phone;
                textBox30.Text = customer.customer_fax;
                textBox31.Text = customer.customer_mobile;
                textBox32.Text = customer.customer_email;
                textBox33.Text = customer.customer_website;
            }
            else
            {
                MessageBox.Show("Invalid id...");
            }
        }

        private void AddCustomer_Toggle_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox12);
        }

        private void EditCustomer_Toggle_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox11);
        }

        private void AddCustomer_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox34.Text) || string.IsNullOrEmpty(textBox35.Text))
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }
            try
            {
                using (var storedb = new StoreModel())
                {
                    Customer customer = new Customer();
                    customer.customer_name = textBox34.Text;
                    customer.customer_phone = textBox35.Text;
                    customer.customer_fax = textBox36.Text;
                    customer.customer_mobile = textBox37.Text;
                    customer.customer_email = textBox38.Text;
                    customer.customer_website = textBox39.Text;
                    storedb.Customers.Add(customer);
                    storedb.SaveChanges();
                    MessageBox.Show("Customer has been added successfully....");
                    textBox34.Text = textBox35.Text = textBox36.Text =
                    textBox37.Text = textBox38.Text = textBox39.Text = "";
                    DisplayCustomers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured" + ex.Message);
            }

        }

        private void UpdateCustomerInDropDown(Customer customer)
        {
            foreach (string cust in CustomersList.Items)
            {
                int id = int.Parse(cust.Split('-')[0].Trim());
                //MessageBox.Show(id.ToString());
                if (id == customer.customer_id)
                {
                    int index = CustomersList.Items.IndexOf(cust);
                    CustomersList.Items[index] = customer.customer_id.ToString() + " - " + customer.customer_name;
                    return;
                }
            }
        }
        private void UpdateCustomer_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CustomersList.Text))
            {
                MessageBox.Show("Please select item from dropdown to update");
                return;
            }

            if (string.IsNullOrEmpty(textBox28.Text) || string.IsNullOrEmpty(textBox31.Text))
            {
                MessageBox.Show("Please fill in all the * fields");
                return;
            }
            int customerId = int.Parse(textBox27.Text);
            Customer customer = storedb.Customers.Find(customerId);

            if (customer != null)
            {
                customer.customer_name = textBox28.Text;
                customer.customer_phone = textBox29.Text;
                customer.customer_fax = textBox30.Text;
                customer.customer_mobile = textBox31.Text;
                customer.customer_email = textBox32.Text;
                customer.customer_website = textBox33.Text;
                storedb.SaveChanges();
                MessageBox.Show("Customer has been updated successfully...");
                UpdateCustomerInDropDown(customer);
                DisplayCustomers();
                textBox27.Text = textBox28.Text = textBox29.Text = textBox30.Text =
                textBox31.Text = textBox32.Text = textBox33.Text = "";
            }
            else
            {
                MessageBox.Show("No Supplier found with provided id...");
            }
        } 
        #endregion


    }
}
