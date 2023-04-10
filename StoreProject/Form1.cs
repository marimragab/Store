using System;
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
            stockInContainer.Visible = false;
            groupBox14.Visible = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadStores();
            ReloadItems();
            ReloadSuppliers();
            ReloadCustomers();
            ReloadStockIn();
            ReloadStockOut();
            ReloadTransferItems();
        }

        #region General Functions
        private void ReloadStores()
        {
            AllStoresIds.Items.Clear();
            StoresIds.Items.Clear();
            var stores = storedb.Stores;
            foreach (Store store in stores)
            {
                AllStoresIds.Items.Add(store.store_id + " - " + store.store_name);
                StoresIds.Items.Add(store.store_id + " - " + store.store_name);
            }
        }

        private void ReloadItems()
        {
            ItemsList.Items.Clear();
            var items = storedb.Items;
            foreach (Item item in items)
            {
                ItemsList.Items.Add(item.item_id + " - " + item.item_code + " - " + item.item_name);
            }
        }

        private void ReloadSuppliers()
        {
            SuppliersList.Items.Clear();
            var suppliers = storedb.Suppliers;
            foreach (Supplier supplier in suppliers)
            {
                SuppliersList.Items.Add(supplier.supplier_id + " - " + supplier.supplier_name);
            }
        }

        private void ReloadCustomers()
        {
            CustomersList.Items.Clear();
            var customers = storedb.Customers;
            foreach (Customer customer in customers)
            {
                CustomersList.Items.Add(customer.customer_id + " - " + customer.customer_name);
            }
        }

        private void ReloadStockIn()
        {
            InStockList.Items.Clear();
            var stocks_in = storedb.StockIns;
            foreach (StockIn stock in stocks_in)
            {
                InStockList.Items.Add(stock.stockin_id + " - "+stock.store_id+ " - " + stock.item_id+ " - "+ stock.supplier_id + " - " + stock.quantity);
            }
            StoreIdMenu.Items.Clear();
            var stores = storedb.Stores;
            foreach (Store store in stores)
            {
                StoreIdMenu.Items.Add(store.store_id+" - "+store.store_name+" - "+store.store_address);
            }

            ItemIdMenu.Items.Clear();
            var items = storedb.Items;
            foreach (Item item in items)
            {
                ItemIdMenu.Items.Add(item.item_id + " - " + item.item_name + " - " + item.item_code);
            }

            SupplierIdMenu.Items.Clear();
            var suppliers = storedb.Suppliers;
            foreach (Supplier supplier in suppliers)
            {
                SupplierIdMenu.Items.Add(supplier.supplier_id + " - " + supplier.supplier_name + " - " + supplier.supplier_mobile);
            }
        }

        private void ReloadStockOut()
        {
            StockOut.Items.Clear();
            var stocks_outs = storedb.StockOuts;
            foreach (StockOut stock in stocks_outs)
            {
                StockOut.Items.Add(stock.stockout_id + " - " + stock.store_id + " - " + stock.item_id + " - " + stock.customer_id + " - " + stock.quantity);
            }
            storeIdsList.Items.Clear();
            var stores = storedb.Stores;
            foreach (Store store in stores)
            {
                storeIdsList.Items.Add(store.store_id + " - " + store.store_name + " - " + store.store_address);
            }

            itemsIdsList.Items.Clear();
            var items = storedb.Items;
            foreach (Item item in items)
            {
                itemsIdsList.Items.Add(item.item_id + " - " + item.item_name + " - " + item.item_code);
            }

            customerIdsList.Items.Clear();
            var customers = storedb.Customers;
            foreach (Customer customer in customers)
            {
                customerIdsList.Items.Add(customer.customer_id+ " - " + customer.customer_name + " - " + customer.customer_mobile);
            }
        }
        private void ReloadTransferItems()
        {
            ImportedItems.Items.Clear();
            var stocks_in = storedb.StockIns;
            foreach (StockIn stock in stocks_in)
            {
                ImportedItems.Items.Add(stock.stockin_id );
            }
            StoreIdMenu.Items.Clear();
            var stores = storedb.Stores;
            foreach (Store store in stores)
            {
                ToStore.Items.Add(store.store_id + " - " + store.store_name + " - " + store.store_address);
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
                    ReloadStores();
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
                ReloadStores();
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
                    ReloadItems();
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
                ReloadItems();
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
                    ReloadSuppliers();
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
                ReloadSuppliers();
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
                    ReloadCustomers();
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
                ReloadCustomers();
                textBox27.Text = textBox28.Text = textBox29.Text = textBox30.Text =
                textBox31.Text = textBox32.Text = textBox33.Text = "";
            }
            else
            {
                MessageBox.Show("No Supplier found with provided id...");
            }
        }

        #endregion


        #region Import Permission (StockIn)
        private void button1_Click_1(object sender, EventArgs e)
        {
            ToggleGroupBox(stockInContainer);
        }

        private void InStockList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stockInId = int.Parse(InStockList.Text.Split('-')[0].Trim());
            StockIn stockIn = storedb.StockIns.Find(stockInId);
            if (stockIn != null)
            {
                StoreIdMenu.Text = stockIn.store_id.ToString();
                ItemIdMenu.Text = stockIn.item_id.ToString();
                SupplierIdMenu.Text = stockIn.supplier_id.ToString();
                quatity.Text = stockIn.quantity.ToString();
                ToStockDate.Text = stockIn.stockin_date.ToString();
                productionDate.Text = stockIn.production_date.ToString();
                expirationDate.Text = stockIn.expiry_date.ToString();
            }
            else
            {
                MessageBox.Show("Invalid Id ...");
            }

        }
        private void AddToStock_Btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StoreIdMenu.Text) || string.IsNullOrEmpty(ItemIdMenu.Text) ||
                string.IsNullOrEmpty(SupplierIdMenu.Text))
            {
                MessageBox.Show("Please select store,item,supplier from dropdown menus...");
                return;
            }
            try
            {
                using (var storedb = new StoreModel())
                {
                    int storeId = int.Parse(StoreIdMenu.Text.Split('-')[0].Trim());
                    int itemId = int.Parse(ItemIdMenu.Text.Split('-')[0].Trim());
                    int supplierId = int.Parse(SupplierIdMenu.Text.Split('-')[0].Trim());

                    MessageBox.Show(storeId.ToString() + itemId.ToString() + supplierId.ToString());
                    StockIn tostock = new StockIn();
                    tostock.stockin_date = new DateTime(ToStockDate.Value.Ticks);
                    tostock.quantity = int.Parse(quatity.Text);
                    tostock.production_date = new DateTime(productionDate.Value.Ticks);
                    tostock.expiry_date = new DateTime(expirationDate.Value.Ticks);

                    Item item = storedb.Items.Find(itemId);
                    tostock.Item = item;
                    Store store = storedb.Stores.Find(storeId);
                    tostock.Store = store;
                    Supplier supplier = storedb.Suppliers.Find(supplierId);
                    tostock.Supplier = supplier;

                    storedb.StockIns.Add(tostock);
                    item.Stores.Add(store);
                    storedb.SaveChanges();
                    MessageBox.Show("Import to stock has been completed successfully....");
                    StoreIdMenu.Text = ItemIdMenu.Text = SupplierIdMenu.Text = quatity.Text = "";

                    ReloadStockIn();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("An error occured" + ex.Message);
                MessageBox.Show("An error occurred: " + ex.Message + "\n\n" + ex.InnerException?.Message);
            }

        }

        private void UpdateToStock_Click(object sender, EventArgs e)
        {
            int stockInId = int.Parse(InStockList.Text.Split('-')[0].Trim());
            StockIn toStock = storedb.StockIns.Find(stockInId);
            if (toStock != null)
            {
                toStock.stockin_date = new DateTime(ToStockDate.Value.Ticks);
                toStock.quantity = int.Parse(quatity.Text);
                toStock.production_date = new DateTime(productionDate.Value.Ticks);
                toStock.expiry_date = new DateTime(expirationDate.Value.Ticks);

                int storeId = int.Parse(StoreIdMenu.Text.Split('-')[0].Trim());
                int itemId = int.Parse(ItemIdMenu.Text.Split('-')[0].Trim());
                int supplierId = int.Parse(SupplierIdMenu.Text.Split('-')[0].Trim());

                Item item = storedb.Items.Find(itemId);
                toStock.Item = item;
                Store store = storedb.Stores.Find(storeId);
                toStock.Store = store;
                Supplier supplier = storedb.Suppliers.Find(supplierId);
                toStock.Supplier = supplier;

                storedb.SaveChanges();
                MessageBox.Show("StockIn has been updated successfuly");
                StoreIdMenu.Text = ItemIdMenu.Text = SupplierIdMenu.Text = quatity.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid id...");
            }
            ReloadStockIn();
        }

        #endregion (StockOut)


        #region Export Permission (StockOut)
        private void StockOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stockOutId = int.Parse(StockOut.Text.Split('-')[0].Trim());
            StockOut stockOut = storedb.StockOuts.Find(stockOutId);
            if (stockOut != null)
            {
                storeIdsList.Text = stockOut.store_id.ToString();
                itemsIdsList.Text = stockOut.item_id.ToString();
                customerIdsList.Text = stockOut.customer_id.ToString();
                quantityOfItem.Text = stockOut.quantity.ToString();
                stockOutDate.Text = stockOut.stockout_date.ToString();
            }
            else
            {
                MessageBox.Show("Invalid Id ...");
            }
        }

        private void ExportItemTiggle_Click(object sender, EventArgs e)
        {
            ToggleGroupBox(groupBox14);
        }

        private void TransferFromStockBtn_Click(object sender, EventArgs e)
        {
            StockOut outStock = new StockOut();
            outStock.stockout_date = new DateTime(stockOutDate.Value.Ticks);
            outStock.quantity = int.Parse(quantityOfItem.Text);

            Item item = storedb.Items.Find(int.Parse(itemsIdsList.Text.Split('-')[0].Trim()));
            outStock.Item = item;
            Store store = storedb.Stores.Find(int.Parse(storeIdsList.Text.Split('-')[0].Trim()));
            outStock.Store = store;
            Customer customer = storedb.Customers.Find(int.Parse(customerIdsList.Text.Split('-')[0].Trim()));
            outStock.Customer = customer;

            storedb.StockOuts.Add(outStock);
            MessageBox.Show("Stockout has been completed successfully...");
            storedb.SaveChanges();
            stockOutDate.Text = quantityOfItem.Text = itemsIdsList.Text = storeIdsList.Text = customerIdsList.Text = "";
            ReloadStockOut();
        }

        private void UpdateTransferItemsFromStockBtn_Click(object sender, EventArgs e)
        {
            int stockOutId = int.Parse(StockOut.Text.Split('-')[0].Trim());
            StockOut outStock = storedb.StockOuts.Find(stockOutId);
            if (outStock != null)
            {
                outStock.stockout_date = new DateTime(stockOutDate.Value.Ticks);
                outStock.quantity = int.Parse(quantityOfItem.Text);


                int storeId = int.Parse(storeIdsList.Text.Split('-')[0].Trim());
                int itemId = int.Parse(itemsIdsList.Text.Split('-')[0].Trim());
                int customerId = int.Parse(customerIdsList.Text.Split('-')[0].Trim());

                Item item = storedb.Items.Find(itemId);
                outStock.Item = item;
                Store store = storedb.Stores.Find(storeId);
                outStock.Store = store;
                Customer customer = storedb.Customers.Find(customerId);
                outStock.Customer = customer;

                storedb.SaveChanges();
                MessageBox.Show("StockOut has been updated successfuly");
                storeIdsList.Text = itemsIdsList.Text = customerIdsList.Text = quantityOfItem.Text = stockOutDate.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid id...");
            }
            ReloadStockOut();
        }

        #endregion


        #region Transfer Item From Store To Another
        private void ImportedItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(ImportedItems.Text);
            StockIn stockItem = storedb.StockIns.Find(ID);
            if (stockItem != null)
            {
                ItemId.Text = stockItem.item_id.ToString();
                FromStore.Text = stockItem.store_id.ToString();
                ItemQuantity.Text = stockItem.quantity.ToString();
                itemToStockDate.Text = stockItem.stockin_date.ToString();
            }
            else
            {
                MessageBox.Show("Invalid ID");
            }
        }

        private void TransferItemBtn_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(ImportedItems.Text);
            StockIn stockItem = storedb.StockIns.Find(ID);
            if (stockItem != null)
            {
                int quantity = (int)stockItem.quantity - int.Parse(ItemQuantity.Text);
                if (quantity > 0)
                {
                    stockItem.quantity = quantity;

                    StockOut outOfStock = new StockOut();
                    outOfStock.stockout_date = new DateTime(itemToStockDate.Value.Ticks);
                    outOfStock.quantity = int.Parse(ItemQuantity.Text);
                    outOfStock.Item = stockItem.Item;

                    Store store = storedb.Stores.Find(int.Parse(FromStore.Text.Split('-')[0].Trim()));
                    outOfStock.Store = stockItem.Store;
                    Customer customer = new Customer() { customer_name = "Amer Ahmed" };
                    outOfStock.Customer = customer;

                    storedb.StockOuts.Add(outOfStock);
                    storedb.SaveChanges();
                    ReloadStockIn();
                    MessageBox.Show("Transfere has been Completed successfuly");
                    ReloadStockOut();
                }
                else
                {
                    MessageBox.Show($"Please enter valid quantity..");
                }

            }
            else
            {
                MessageBox.Show("invalid id");
            }
            ReloadStockOut();

        }
        #endregion


        #region Store Report


        #endregion


    }
}
