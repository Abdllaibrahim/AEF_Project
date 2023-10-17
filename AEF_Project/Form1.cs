using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace AEF_Project
{
    //welcom
    public partial class Form1 : Form
    {
        EModel Ent = new EModel();


        #region Initalization
        public Form1()
        {
            InitializeComponent();

            #region Store
            var store = from st in Ent.Stores select st;
            foreach (var st in store)
            {
                if (st != null)
                {
                    dataGridView1.Rows.Add(st.store_name, st.address, st.manger_name);
                    StorenamecomboBox.Items.Add(st.store_name);
                    storecombobox.Items.Add(st.store_name);
                }
            }
            #endregion

            #region Item
            var Item = from it in Ent.Items select it;

            foreach (var i in Item)
            {
                if (i != null)
                {
                    dataGridView2.Rows.Add(i.id_item, i.name, i.unit, i.name_store);
                    itemcompmbox.Items.Add(i.name);
                    unitcompobox.Items.Add(i.unit);
                }
            }
            #endregion

            #region Supplier
            var supss = from sp in Ent.suppliers select sp;
            foreach (var st in supss)
            {
                if (st != null)
                {
                    dataGridView3.Rows.Add(st.name_sup, st.tel_sup, st.mobile, st.e_mail);
                    suppliercompobox.Items.Add(st.name_sup);
                }
            }
            #endregion

            #region Client
            var custss = from ct in Ent.customers select ct;
            foreach (var st in custss)
            {
                if (st != null)
                {
                    dataGridView4.Rows.Add(st.name_customer, st.tel_customer, st.mobile, st.e_mail);

                }
            }
            #endregion

            
        }
        #endregion


        #region Store Tab

        #region ADD
        private void Addbutton_Click(object sender, EventArgs e)
        {
            if (textstore.Text == "" || textaddress.Text== "" || textmanger.Text=="" )
            {
                MessageBox.Show("Empty Data");
            }
            else
            {

            
                Store str = new Store();
                str.store_name = textstore.Text;
                str.address = textaddress.Text;
                str.manger_name = textmanger.Text;
                Store stor = Ent.Stores.Find(str.store_name);
                if (stor == null)
                {
                    if (str.store_name != "" && str.address != "" && str.manger_name != "")
                    {
                        Ent.Stores.Add(str);
                        Ent.SaveChanges();
                        textstore.Text = textaddress.Text = textmanger.Text = "";
                        MessageBox.Show("Store Added");
                        dataGridView1.Rows.Clear();
                        StorenamecomboBox.Items.Clear();
                        var storess = from st in Ent.Stores select st;
                        foreach (var st in storess)
                        {
                            if (st != null)
                            {
                                dataGridView1.Rows.Add(st.store_name, st.address, st.manger_name);
                                StorenamecomboBox.Items.Add(st.store_name);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Empty Data");
                    }

                }
                else
                {
                    MessageBox.Show("Store Already found");
                }
                //var store = from st in Ent.Stores select st;
                //foreach (var st in store)
                //{
                //    if (st != null)
                //    {
                    
                //        StorenamecomboBox.Items.Add(st.store_name);
                  
                //    }
                //}

            }
        }
        #endregion

        #region Edit
        private void Editbutton_Click(object sender, EventArgs e)
        {

            if (textstore.Text == "" || textaddress.Text == "" || textmanger.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                    Store str = new Store();
                str.store_name = textstore.Text;

                Store stor = Ent.Stores.Find(str.store_name);
                if (stor != null && str.store_name != "" && str.address != "" && str.manger_name != "")
                {
                    foreach (Item ir in stor.Items.ToList())
                    {
                        ir.name_store = textstore.Text;

                        Ent.SaveChanges();
                    }
                    foreach (supplier_item_store il in stor.supplier_item_store.ToList())
                    {
                        il.store_name= textstore.Text;
                        Ent.SaveChanges();
                    }
                    foreach (Customer_item_store il in stor.Customer_item_store.ToList())
                    {
                        il.store_name= textstore.Text;
                        Ent.SaveChanges();
                    }
                    foreach (Transaction il in stor.Transactions.ToList())
                    {
                        il.Store_name= textstore.Text;
                        il.Store_Filled = textstore.Text;
                        Ent.SaveChanges();
                    }
                    stor.address = textaddress.Text;
                    stor.manger_name = textmanger.Text;
                    Ent.SaveChanges();
                    textstore.Text = textaddress.Text = textmanger.Text = "";
                    dataGridView1.Rows.Clear();
                    var storess = from st in Ent.Stores select st;
                    foreach (var st in storess)
                    {
                        if (st != null)
                        {
                            dataGridView1.Rows.Add(st.store_name, st.address, st.manger_name);
                        }
                    }
                    MessageBox.Show("Store Updated");
                }
                else
                {
                    MessageBox.Show("Store is not found");
                }
               
                var storn = from s in Ent.Stores select s;

                
                foreach (var s in storn)
                {
                    StorenamecomboBox.Items.Add(s.store_name);
                }
            }
        }
        #endregion

        #region Delete
        private void Deletebutton_Click_1(object sender, EventArgs e)
        {
            if (textstore.Text == "")
            {
                MessageBox.Show("Enter Store Name");
            }
            else
            {
        
                Store str = new Store();
                str.store_name = textstore.Text;
                Store stor = Ent.Stores.Find(str.store_name);
                if (stor != null && str.store_name != "" && str.address != "" && str.manger_name != "")
                {
                    
                    foreach (Item ir in stor.Items.ToList())
                    {
                        Ent.Items.Remove(ir);
                        Ent.SaveChanges();
                    }
                    foreach (supplier_item_store il in stor.supplier_item_store.ToList())
                    {
                        Ent.supplier_item_store.Remove(il);
                        Ent.SaveChanges();
                    }
                    foreach (Customer_item_store il in stor.Customer_item_store.ToList())
                    {
                        Ent.Customer_item_store.Remove(il);
                        Ent.SaveChanges();
                    }
                    foreach (Transaction il in stor.Transactions.ToList())
                    {
                        Ent.Transactions.Remove(il);
                        il.Store_Filled= null;
                        Ent.SaveChanges();
                    }

                    Ent.Stores.Remove(stor);
                    Ent.SaveChanges();
                    textstore.Text = textaddress.Text = textmanger.Text = "";
                    dataGridView1.Rows.Clear();
                    var storess = from st in Ent.Stores select st;
                    foreach (var st in storess)
                    {
                        if (st != null)
                        {
                            dataGridView1.Rows.Add(st.store_name, st.address, st.manger_name);
                        }
                    }
                    var Item = from it in Ent.Items select it;
                    dataGridView2.Rows.Clear();
                    StorenamecomboBox.Items.Clear();
                    StorenamecomboBox.Text = "";
                    foreach (var i in Item)
                    {
                        if (i != null)
                        {
                            dataGridView2.Rows.Add(i.id_item, i.name, i.unit, i.name_store);
                        }
                    }
                    var store = from st in Ent.Stores select st;
                    foreach (var st in store)
                    {
                        StorenamecomboBox.Items.Add(st.store_name);
                    }
                    MessageBox.Show("Store Deleted");
                }
                else
                {
                    MessageBox.Show("Store is not found");
                }
            }
        }
        #endregion
        #endregion


        #region Item Tab

        #region ADD
        private void Addibtn_Click_1(object sender, EventArgs e)
        {
            if (IDtext.Text == "" || Inametext.Text == "" || unittext.Text == "" || StorenamecomboBox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(IDtext.Text, out int n);
                if (success)
                {
                     Item it = new Item();
                    it.id_item = int.Parse(IDtext.Text);
                    it.name = Inametext.Text;
                    it.unit = unittext.Text;
                    it.name_store = StorenamecomboBox.Text;
                    it.Item_Quantity = 0;
                    Item itm = Ent.Items.Find(it.id_item);
                    if (itm == null && it.name != "" && it.unit != "")
                    {
                        Ent.Items.Add(it);
                        Ent.SaveChanges();
                        IDtext.Text = Inametext.Text = unittext.Text = "";
                        MessageBox.Show("Item Added");
                        dataGridView2.Rows.Clear();
                        StorenamecomboBox.Items.Clear();
                        var itemss = from itt in Ent.Items select itt;

                        foreach (var i in itemss)
                        {
                            if (i != null)
                            {
                                dataGridView2.Rows.Add(i.id_item, i.name, i.unit, i.name_store);

                            }

                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("Item Already found");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Data");
                }
            }
        }


        #endregion

        #region Edit
        private void editibtn_Click(object sender, EventArgs e)
        {
            if (IDtext.Text == "" || Inametext.Text == "" || unittext.Text == "" || StorenamecomboBox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(IDtext.Text, out int n);
                if (success)
                {
                    Item it = new Item();
                    it.id_item = int.Parse(IDtext.Text);

                    Item itm = Ent.Items.Find(it.id_item);
                    if (itm != null && it.name != "" && it.unit != "")
                    {
                        foreach (supplier_item_store ir in itm.supplier_item_store.ToList())
                        {
                            ir.item_name = Inametext.Text;
                            ir.Item_unit = unittext.Text;
                            
                        }
                        foreach (Customer_item_store ir in itm.Customer_item_store.ToList())
                        {
                            ir.item_name = Inametext.Text;
                            ir.Item_unit = unittext.Text;
                           
                        }
                        foreach (Transaction ir in itm.Transactions.ToList())
                        {
                            ir.Item_name = Inametext.Text;
                           
                        }
                        itm.name = Inametext.Text;
                        itm.unit = unittext.Text;
                        itm.name_store = StorenamecomboBox.Text;
                        itm.Item_Quantity = 0;
                        Ent.SaveChanges();
                        IDtext.Text = Inametext.Text = unittext.Text = "";
                        MessageBox.Show("Item Updated");
                        dataGridView2.Rows.Clear();
                        StorenamecomboBox.Items.Clear();
                        var itemss = from itt in Ent.Items select itt;
                        var storn = from s in Ent.Stores select s;
                        foreach (var i in itemss)
                        {
                            if (i != null)
                            {
                                dataGridView2.Rows.Add(i.id_item, i.name, i.unit, i.name_store);

                            }

                        }
                        foreach (var s in storn)
                        {
                            StorenamecomboBox.Items.Add(s.store_name);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Item is not found");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Data");
                }
            }
        }



        #endregion

        #region Delete
        private void deleteibtn_Click(object sender, EventArgs e)
        {
            if (IDtext.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(IDtext.Text, out int n);
                if (success)
                {
                    Item it = new Item();
                    it.id_item = int.Parse(IDtext.Text);

                    Item itm = Ent.Items.Find(it.id_item);
                    if (itm != null && it.name != "" && it.unit != "")
                    {
                        foreach (supplier_item_store ir in itm.supplier_item_store.ToList())
                        {
                            Ent.supplier_item_store.Remove(ir);
                            Ent.SaveChanges();
                        }
                        foreach (Customer_item_store il in itm.Customer_item_store.ToList())
                        {
                            Ent.Customer_item_store.Remove(il);
                            Ent.SaveChanges();
                        }
                        foreach (Transaction il in itm.Transactions.ToList())
                        {
                            Ent.Transactions.Remove(il);
                            Ent.SaveChanges();
                        }
                        itm.name_store = null;
                        Ent.Items.Remove(itm);
                        Ent.SaveChanges();
                        IDtext.Text = Inametext.Text = unittext.Text = "";
                        MessageBox.Show("Item Deleted");
                        dataGridView2.Rows.Clear();
                        StorenamecomboBox.Items.Clear();
                        var itemss = from itt in Ent.Items select itt;
                        var storn = from s in Ent.Stores select s;
                        foreach (var i in itemss)
                        {
                            if (i != null)
                            {
                                dataGridView2.Rows.Add(i.id_item, i.name, i.unit, i.name_store);

                            }

                        }
                        foreach (var s in storn)
                        {
                            StorenamecomboBox.Items.Add(s.store_name);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Item is not found");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Data");
                }
            }
        }
        #endregion

        #endregion


        #region Supplier Tab

        #region ADD

        private void saddbtn_Click_1(object sender, EventArgs e)
        {
            if (snametext.Text == "" || steltext.Text == "" || smobiletext.Text == "" || semailtext.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(steltext.Text, out int n);
                bool success1 = int.TryParse(smobiletext.Text, out int nn);
                if (success ==false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {

 
                supplier sup = new supplier();
                sup.name_sup = snametext.Text;
                sup.tel_sup = int.Parse(steltext.Text);
                sup.mobile = int.Parse(smobiletext.Text);
                sup.e_mail = semailtext.Text;
                supplier supp = Ent.suppliers.Find(sup.tel_sup);
                if (supp == null)
                {
                    if (sup.name_sup != "" && sup.tel_sup != 0 && sup.mobile != 0 && sup.e_mail != "")
                    {
                        Ent.suppliers.Add(sup);
                        Ent.SaveChanges();
                        textstore.Text = textaddress.Text = textmanger.Text = "";
                        MessageBox.Show("Supplier Added");
                        dataGridView3.Rows.Clear();
                        var supss = from sp in Ent.suppliers select sp;
                        foreach (var st in supss)
                        {
                            if (st != null)
                            {
                                dataGridView3.Rows.Add(st.name_sup, st.tel_sup, st.mobile, st.e_mail);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Empty Data");
                    }

                }
                else
                {
                    MessageBox.Show("Supplier Already found");
                }
                   }


             }
        }




        #endregion

        #region Update
        private void seditbtn_Click_1(object sender, EventArgs e)
        {
            if (snametext.Text == "" || steltext.Text == "" || smobiletext.Text == "" || semailtext.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(steltext.Text, out int n);
                bool success1 = int.TryParse(smobiletext.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    supplier sup = new supplier();
                    sup.tel_sup = int.Parse(steltext.Text);
                    supplier supp = Ent.suppliers.Find(sup.tel_sup);
                    if (supp != null)
                    {
                        if (sup.name_sup != "" && sup.tel_sup != 0 && sup.mobile != 0 && sup.e_mail != "")
                        {
                            foreach (supplier_item_store ir in supp.supplier_item_store.ToList())
                            {
                                ir.tel_sup = sup.tel_sup;
                                ir.supplier_name = snametext.Text;
                            }
                            supp.name_sup = snametext.Text;
                            supp.mobile = int.Parse(smobiletext.Text);
                            supp.e_mail = semailtext.Text;

                            Ent.SaveChanges();
                            textstore.Text = textaddress.Text = textmanger.Text = "";
                            MessageBox.Show("Supplier Updated");
                            dataGridView3.Rows.Clear();
                            var supss = from sp in Ent.suppliers select sp;
                            foreach (var st in supss)
                            {
                                if (st != null)
                                {
                                    dataGridView3.Rows.Add(st.name_sup, st.tel_sup, st.mobile, st.e_mail);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Empty Data");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Supplier is not found");
                    }
                }
            }
        }
        #endregion

        #region Delete
        private void sdeletebtn_Click_1(object sender, EventArgs e)
        {
            if (snametext.Text == "" || steltext.Text == "")
            {
                MessageBox.Show("Enter Name and telephone");
            }
            else
            {
                bool success = int.TryParse(steltext.Text, out int n);
                bool success1 = int.TryParse(smobiletext.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    supplier sup = new supplier();
                    sup.tel_sup = int.Parse(steltext.Text);
                    supplier supp = Ent.suppliers.Find(sup.tel_sup);
                    if (supp != null)
                    {
                        if (sup.name_sup != "" && sup.tel_sup != 0 && sup.mobile != 0 && sup.e_mail != "")
                        {
                            foreach (supplier_item_store ir in supp.supplier_item_store.ToList())
                            {
                                Ent.supplier_item_store.Remove(ir);
                                Ent.SaveChanges();
                            }
                            foreach (Transaction il in supp.Transactions.ToList())
                            {
                                Ent.Transactions.Remove(il);
                                Ent.SaveChanges();
                            }
                            Ent.suppliers.Remove(supp);
                            Ent.SaveChanges();
                            textstore.Text = textaddress.Text = textmanger.Text = "";
                            MessageBox.Show("Supplier Deleted");
                            dataGridView3.Rows.Clear();
                            var supss = from sp in Ent.suppliers select sp;
                            foreach (var st in supss)
                            {
                                if (st != null)
                                {
                                    dataGridView3.Rows.Add(st.name_sup, st.tel_sup, st.mobile, st.e_mail);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Empty Data");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Supplier is not found");
                    }
                }
            }
        }

        #endregion

        #endregion


        #region Client Tab

        #region ADD
        private void caddbtn_Click(object sender, EventArgs e)
        {
            if (cnametext.Text == "" || cteltext.Text == "" || cmobtext.Text == "" || cemailtext.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(cteltext.Text, out int n);
                bool success1 = int.TryParse(cmobtext.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    customer cust = new customer();
                    cust.name_customer = cnametext.Text;
                    cust.tel_customer = int.Parse(cteltext.Text);
                    cust.mobile = int.Parse(cmobtext.Text);
                    cust.e_mail = cemailtext.Text;
                    customer custt = Ent.customers.Find(cust.tel_customer);
                    if (custt == null)
                    {
                        if (cust.name_customer != "" && cust.tel_customer != 0 && cust.mobile != 0 && cust.e_mail != "")
                        {
                            Ent.customers.Add(cust);
                            Ent.SaveChanges();

                            MessageBox.Show("Client Added");
                            dataGridView4.Rows.Clear();
                            var custss = from ct in Ent.customers select ct;
                            foreach (var st in custss)
                            {
                                if (st != null)
                                {
                                    dataGridView4.Rows.Add(st.name_customer, st.tel_customer, st.mobile, st.e_mail);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Empty Data");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Client Already found");
                    }
                }
            }
        }
        #endregion

        #region Edit
        private void ceditbtn_Click(object sender, EventArgs e)
        {
            if (cnametext.Text == "" || cteltext.Text == "" || cmobtext.Text == "" || cemailtext.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(cteltext.Text, out int n);
                bool success1 = int.TryParse(cmobtext.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    customer cust = new customer();

                    cust.tel_customer = int.Parse(cteltext.Text);

                    customer custt = Ent.customers.Find(cust.tel_customer);
                    if (custt != null)
                    {
                        if (cust.name_customer != "" && cust.tel_customer != 0 && cust.mobile != 0 && cust.e_mail != "")
                        {
                            foreach (Customer_item_store ir in custt.Customer_item_store.ToList())
                            {
                                ir.tel_customer = cust.tel_customer;
                                ir.customer_name = cnametext.Text;
                            }
                            
                            custt.name_customer = cnametext.Text;
                            custt.mobile = int.Parse(cmobtext.Text);
                            custt.e_mail = cemailtext.Text;

                            Ent.SaveChanges();

                            MessageBox.Show("Client Updated");
                            dataGridView4.Rows.Clear();
                            var custss = from ct in Ent.customers select ct;
                            foreach (var st in custss)
                            {
                                if (st != null)
                                {
                                    dataGridView4.Rows.Add(st.name_customer, st.tel_customer, st.mobile, st.e_mail);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Empty Data");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Client is not found");
                    }
                }
            }
        }
        #endregion

        #region Delete
        private void cdeletebtn_Click(object sender, EventArgs e)
        {
            if (cnametext.Text == "" || cteltext.Text == "")
            {
                MessageBox.Show("Enter Name and telephone");
            }
            else
            {
                bool success = int.TryParse(cteltext.Text, out int n);
                bool success1 = int.TryParse(cmobtext.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    customer cust = new customer();

                    cust.tel_customer = int.Parse(cteltext.Text);

                    customer custt = Ent.customers.Find(cust.tel_customer);
                    if (custt != null)
                    {
                        if (cust.name_customer != "" && cust.tel_customer != 0 && cust.mobile != 0 && cust.e_mail != "")
                        {
                            foreach (Customer_item_store ir in custt.Customer_item_store.ToList())
                            {
                                Ent.Customer_item_store.Remove(ir);
                                Ent.SaveChanges();
                            }
                            
                            Ent.customers.Remove(custt);
                            Ent.SaveChanges();

                            MessageBox.Show("Client Deleted");
                            dataGridView4.Rows.Clear();
                            var custss = from ct in Ent.customers select ct;
                            foreach (var st in custss)
                            {
                                if (st != null)
                                {
                                    dataGridView4.Rows.Add(st.name_customer, st.tel_customer, st.mobile, st.e_mail);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Empty Data");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Client is not found");
                    }
                }
            }
        }





        #endregion

        #endregion

       
        #region Supplier Permission Tab 
        
        #region Display supply permission data
        private void button4_Click(object sender, EventArgs e)
        {

            #region Store
            dataGridView1.Rows.Clear();
            StorenamecomboBox.Items.Clear();
            storecombobox.Items.Clear();
            var store = from st in Ent.Stores select st;
            foreach (var st in store)
            {
                if (st != null)
                {
                    dataGridView1.Rows.Add(st.store_name, st.address, st.manger_name);
                    StorenamecomboBox.Items.Add(st.store_name);
                    storecombobox.Items.Add(st.store_name);
                }
            }
            #endregion

            #region Item
            var Item = from it in Ent.Items select it;
            dataGridView2.Rows.Clear();
            itemcompmbox.Items.Clear();
            unitcompobox.Items.Clear();
            foreach (var i in Item)
            {
                if (i != null)
                {
                    dataGridView2.Rows.Add(i.id_item, i.name, i.unit, i.name_store);
                    itemcompmbox.Items.Add(i.name);
                    unitcompobox.Items.Add(i.unit);
                }
            }
            #endregion

            #region Supplier
            dataGridView3.Rows.Clear();
            suppliercompobox.Items.Clear();
            var supss = from sp in Ent.suppliers select sp;
            foreach (var st in supss)
            {
                if (st != null)
                {
                    dataGridView3.Rows.Add(st.name_sup, st.tel_sup, st.mobile, st.e_mail);
                    suppliercompobox.Items.Add(st.name_sup);
                }
            }
            #endregion

            #region Supply Permission
            dataGridView5.Rows.Clear();
            var persup = from ps in Ent.supplier_item_store select ps;
            foreach (var pus in persup)
            {
                if (pus != null)
                {
                    dataGridView5.Rows.Add(pus.store_name, pus.supplier_name,
                       pus.perm_date, pus.item_name, pus.Item_unit,
                       pus.pro_date, pus.exp_date, pus.perm_num, pus.Last_quantity,
                       pus.quantity, pus.Total_quantity);
                }
            }
            #endregion

        }
        #endregion

        #region ADD
        int last_qauntity = 0;
        private void paddbtn_Click_1(object sender, EventArgs e)
        {
            if (storecombobox.Text == "" || itemcompmbox.Text == ""
                || perntextBox.Text == "" || quantitytextbox.Text == ""
                || suppliercompobox.Text == "" || unitcompobox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(perntextBox.Text, out int n);
                bool success1 = int.TryParse(quantitytextbox.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    if (ProTimePicker.Text == ExpdateTimePicker.Text)
                    {
                        MessageBox.Show("Production Date Can't be Same the Expiration Date");
                    }
                    else
                    {
                        supplier_item_store sis = new supplier_item_store();
                        sis.store_name = storecombobox.Text;
                        string item_name = itemcompmbox.Text;
                        sis.id_item = (from ps in Ent.Items where ps.name == item_name select ps.id_item).FirstOrDefault();
                        sis.perm_num = int.Parse(perntextBox.Text);
                        string sup_name = suppliercompobox.Text;
                        sis.tel_sup = (from ps in Ent.suppliers where ps.name_sup == sup_name select ps.tel_sup).FirstOrDefault();
                        
                        sis.item_name = item_name;
                        sis.supplier_name = sup_name;
                        sis.Item_unit = (from ps in Ent.Items where ps.name == item_name select ps.unit).FirstOrDefault(); 
                        
                        string item_unit = unitcompobox.Text;
                        sis.Last_quantity = (from p in Ent.supplier_item_store where p.item_name == item_name && 
                                             p.store_name == sis.store_name select p.Final_Quantity).FirstOrDefault();
                        if(sis.Last_quantity == null) { sis.Last_quantity=0; }
                        sis.quantity = int.Parse(quantitytextbox.Text);
                        sis.Total_quantity = sis.quantity + sis.Last_quantity;
                        sis.Final_Quantity = sis.Total_quantity;
                        
                          
                        sis.pro_date = DateTime.Parse(ProTimePicker.Text);
                        sis.exp_date = DateTime.Parse(ExpdateTimePicker.Text);
                        sis.perm_date = DateTime.Parse(PerdateTimePicker.Text);
                        var percsup = from ps in Ent.supplier_item_store select ps;
                        foreach (var pus in percsup)
                        {
                            if (pus != null)
                            {
                                if (pus.perm_num == sis.perm_num)
                                {
                                    MessageBox.Show("Error: Permission Number has to be Unique");
                                    return;
                                }
                            }
                        }


                        supplier_item_store stor = Ent.supplier_item_store.Find(sis.store_name, sis.id_item, sis.tel_sup,sis.perm_num);
                        if (sis.store_name != "" && item_name != "" && sup_name != "" && item_unit != "" && sis.quantity != 0)
                        {
                            if (stor == null)
                            {

                                Ent.supplier_item_store.Add(sis);
                                Ent.SaveChanges();
                                MessageBox.Show("Supply Permission Added");
                                dataGridView5.Rows.Clear();
                                var persup = from ps in Ent.supplier_item_store select ps;
                                foreach (var pus in persup)
                                {
                                    if (pus != null)
                                    {
                                        dataGridView5.Rows.Add(pus.store_name, pus.supplier_name,
                                           pus.perm_date, pus.item_name, pus.Item_unit,
                                           pus.pro_date, pus.exp_date, pus.perm_num, pus.Last_quantity,
                                           pus.quantity,pus.Total_quantity);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Supplier permission is already found");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Empty Data");
                        }
                    }
                
                }
            }
        }




        #endregion

        #region Update
        private void peditptn_Click_1(object sender, EventArgs e)
        {
            if (storecombobox.Text == "" || itemcompmbox.Text == ""
                || perntextBox.Text == "" || quantitytextbox.Text == ""
                || suppliercompobox.Text == "" || unitcompobox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(perntextBox.Text, out int n);
                bool success1 = int.TryParse(quantitytextbox.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    if (ProTimePicker.Text == ExpdateTimePicker.Text)
                    {
                        MessageBox.Show("Production Date Can't be Same the Expiration Date");
                    }
                    else
                    {
                        supplier_item_store sis = new supplier_item_store();
                        sis.store_name = storecombobox.Text;
                        string item_name = itemcompmbox.Text;
                        string sup_name = suppliercompobox.Text;
                        sis.id_item = (from ps in Ent.Items where ps.name == item_name select ps.id_item).FirstOrDefault();
                        sis.tel_sup = (from ps in Ent.suppliers where ps.name_sup == sup_name select ps.tel_sup).FirstOrDefault();
                        sis.perm_num = int.Parse(perntextBox.Text);

                        sis.item_name = item_name;
                        sis.supplier_name = sup_name;
                        if (sis.store_name != "" && item_name != "" && sup_name != "" && sis.quantity != 0)
                        {
                            supplier_item_store stor = Ent.supplier_item_store.Find(sis.store_name, sis.id_item, sis.tel_sup, sis.perm_num);
                            if (stor != null)
                            {
                                stor.Item_unit = (from ps in Ent.Items where ps.name == item_name select ps.unit).FirstOrDefault();

                                string item_unit = unitcompobox.Text;
                                stor.Last_quantity = (from p in Ent.supplier_item_store
                                                     where p.item_name == item_name &&
                                                     p.store_name == stor.store_name
                                                     select p.Final_Quantity).FirstOrDefault();
                                if (stor.Last_quantity == null) { stor.Last_quantity = 0; }
                                stor.quantity = int.Parse(quantitytextbox.Text);
                                stor.Total_quantity = stor.quantity + stor.Last_quantity;
                                stor.Final_Quantity = stor.Total_quantity;


                                stor.pro_date = DateTime.Parse(ProTimePicker.Text);
                                stor.exp_date = DateTime.Parse(ExpdateTimePicker.Text);
                                stor.perm_date = DateTime.Parse(PerdateTimePicker.Text);
                               
                                Ent.SaveChanges();
                                MessageBox.Show("Supply Permission Updated");
                                dataGridView5.Rows.Clear();
                                var persup = from ps in Ent.supplier_item_store select ps;
                                foreach (var pus in persup)
                                {
                                    if (pus != null)
                                    {
                                        dataGridView5.Rows.Add(pus.store_name, pus.supplier_name,
                                           pus.perm_date, pus.item_name, pus.Item_unit,
                                           pus.pro_date, pus.exp_date, pus.perm_num, pus.Last_quantity,
                                           pus.quantity, pus.Total_quantity);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Supplier permission is not found");

                            }
                        }
                        else
                        {
                            MessageBox.Show("Empty Data");
                        }
                    }
                }
            }
        }
        #endregion

        #region Delete
        private void pdeletebtn_Click_1(object sender, EventArgs e)
        {
            if (storecombobox.Text == "" || itemcompmbox.Text == ""
                || perntextBox.Text == "" 
                || suppliercompobox.Text == "" )
            {
                MessageBox.Show("Enter Store name ,Item ,Supplier and Permission no.");
            }
            else
            {
                bool success = int.TryParse(perntextBox.Text, out int n);
                bool success1 = int.TryParse(quantitytextbox.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    supplier_item_store sis = new supplier_item_store();
                    sis.store_name = storecombobox.Text;
                    string item_name = itemcompmbox.Text;
                    string sup_name = suppliercompobox.Text;
                    sis.id_item = (from ps in Ent.Items where ps.name == item_name select ps.id_item).FirstOrDefault();
                    sis.tel_sup = (from ps in Ent.suppliers where ps.name_sup == sup_name select ps.tel_sup).FirstOrDefault();
                    string item_unit = unitcompobox.Text;
                    sis.item_name = item_name;
                    sis.supplier_name = sup_name;
                    sis.perm_num = int.Parse(perntextBox.Text);

                    if (sis.store_name != "" && item_name != "" && sup_name != "")
                    {
                        supplier_item_store stor = Ent.supplier_item_store.Find(sis.store_name, sis.id_item, sis.tel_sup, sis.perm_num);

                        if (stor != null)
                        {

                            Ent.supplier_item_store.Remove(stor);
                            stor.Last_quantity = (from p in Ent.supplier_item_store
                                                 where p.item_name == item_name &&
                                             p.store_name == sis.store_name
                                                 select p.Last_quantity).FirstOrDefault();
                            stor.quantity = int.Parse(quantitytextbox.Text);
                            stor.Total_quantity = sis.Last_quantity - sis.quantity;
                            Ent.SaveChanges();
                            MessageBox.Show("Supply Permission Deleted");
                            dataGridView5.Rows.Clear();
                            var persup = from ps in Ent.supplier_item_store select ps;
                            foreach (var pus in persup)
                            {
                                if (pus != null)
                                {
                                    dataGridView5.Rows.Add(pus.store_name, pus.supplier_name,
                                       pus.perm_date, pus.item_name, pus.Item_unit,
                                       pus.pro_date, pus.exp_date, pus.perm_num, pus.Last_quantity,
                                       pus.quantity, pus.Total_quantity);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Supplier permission is not found");
                        }

                    }
                    else
                    {

                        MessageBox.Show("Empty Data");
                    }
                }
            }
            
        }

        #endregion

        #endregion


        0.# region Exchanger Tab

        #region Exchanger Permission tab

        #region Display Client Permission
        private void cldisplaybtn_Click(object sender, EventArgs e)
        {
            #region Store

            cstorecomboBox.Items.Clear();

            var store = from st in Ent.Stores select st;
            foreach (var st in store)
            {
                if (st != null)
                {
                    cstorecomboBox.Items.Add(st.store_name);

                }
            }
            #endregion

            #region Item
            var Item = from it in Ent.Items select it;
            citemnamecompobox.Items.Clear();
            cunitcomboBox.Items.Clear();
            foreach (var i in Item)
            {
                if (i != null)
                {
                    citemnamecompobox.Items.Add(i.name);
                    cunitcomboBox.Items.Add(i.unit);
                }
            }
            #endregion

            #region Client

            cexchangetextBox.Items.Clear();
            var supss = from sp in Ent.customers select sp;
            foreach (var st in supss)
            {
                if (st != null)
                {
                    cexchangetextBox.Items.Add(st.name_customer);
                }
            }
            #endregion


            #endregion

            #region Extchange Permission

            dataGridView6.Rows.Clear();
            var persup = from ps in Ent.Customer_item_store select ps;
            foreach (var pus in persup)
            {
                if (pus != null)
                {
                    dataGridView6.Rows.Add(pus.store_name, pus.customer_name,
                       pus.cperm_date, pus.item_name, pus.Item_unit,
                       pus.pro_date, pus.exp_date, pus.perm_num, pus.Last_quantity, pus.tquantity, pus.Total_quantity);
                }
            }
            #endregion
        }


        #endregion

        #region Add Exchanger permission
        private void claddbtn_Click_1(object sender, EventArgs e)
        {
            int X =0;
            if (cstorecomboBox.Text == "" || citemnamecompobox.Text == ""
                || cunitcomboBox.Text == "" || cexchangetextBox.Text == ""
                || cperntextBox.Text == "" || cquantitytextBox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(cperntextBox.Text, out int n);
                bool success1 = int.TryParse(cquantitytextBox.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    if (cprodateTimePicker.Text == cexpdateTimePicker.Text)
                    {
                        MessageBox.Show("Production Date Can't be Same the Expiration Date");
                    }
                    else
                    {
                        Customer_item_store cis = new Customer_item_store();
                        cis.store_name = cstorecomboBox.Text;
                        var citem_name = citemnamecompobox.Text;
                        cis.id_item = (from cs in Ent.Items where cs.name == citem_name select cs.id_item).First();
                        string item_unit = cunitcomboBox.Text;
                        string customer_name = cexchangetextBox.Text;
                        cis.item_name = citem_name;
                        cis.customer_name = customer_name;
                        cis.Item_unit = item_unit;
                        cis.perm_num = int.Parse(cperntextBox.Text);
                        cis.tel_customer = (from ps in Ent.customers where ps.name_customer == customer_name select ps.tel_customer).FirstOrDefault();
                        cis.tquantity = int.Parse(cquantitytextBox.Text);
                        cis.pro_date = DateTime.Parse(cprodateTimePicker.Text);
                        cis.exp_date = DateTime.Parse(cexpdateTimePicker.Text);
                        cis.cperm_date = DateTime.Parse(cperdateTimePicker.Text);
                        cis.tquantity = int.Parse(cquantitytextBox.Text);
                        if (cis.Last_quantity == null) { cis.Last_quantity = 0; }
                        cis.Last_quantity = (from p in Ent.supplier_item_store
                                             where p.item_name == cis.item_name &&
                                             p.store_name == cis.store_name
                                             select p.Final_Quantity).FirstOrDefault();
                        if (cis.Last_quantity == null) { cis.Last_quantity = 0; }
                        cis.Total_quantity = cis.Last_quantity - cis.tquantity;
                        if (cis.Total_quantity < 0) { MessageBox.Show("The Quantity Has been Demanded if Higher than Store Capcity"); }
                        else
                        {


                            (from p in Ent.supplier_item_store where p.item_name == cis.item_name && p.store_name == cis.store_name select p)
                                .ToList().ForEach(x => x.Final_Quantity = cis.Total_quantity);


                            var percsup = from ps in Ent.Customer_item_store select ps;
                            foreach (var pus in percsup)
                            {
                                if (pus != null)
                                {
                                    if (pus.perm_num == cis.perm_num)
                                    {
                                        MessageBox.Show("Error: Permission Number has to be Unique");
                                        return;
                                    }
                                }
                            }
                            Customer_item_store stor = Ent.Customer_item_store.Find(cis.store_name, cis.id_item, cis.tel_customer, cis.perm_num);
                            if (X == 0)
                            {
                                if (stor == null)
                                {
                                    if (cis.store_name != "" && citem_name != "" && customer_name != "" && item_unit != "" && cis.tquantity != 0)
                                    {
                                        Ent.Customer_item_store.Add(cis);
                                        Ent.SaveChanges();
                                        MessageBox.Show("Exchange Permission Added");
                                        dataGridView6.Rows.Clear();
                                        var persup = from ps in Ent.Customer_item_store select ps;
                                        foreach (var pus in persup)
                                        {
                                            if (pus != null)
                                            {
                                                dataGridView6.Rows.Add(pus.store_name, pus.customer_name,
                                                   pus.cperm_date, pus.item_name, pus.Item_unit,
                                                   pus.pro_date, pus.exp_date, pus.perm_num, pus.Last_quantity, pus.tquantity, pus.Total_quantity);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Empty Data");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Client permission is already found");
                                }

                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Update Exchanger Permission 
        private void cleditbtn_Click(object sender, EventArgs e)
        {
            int X = 0;
            if (cstorecomboBox.Text == "" || citemnamecompobox.Text == ""
                || cunitcomboBox.Text == "" || cexchangetextBox.Text == ""
                || cperntextBox.Text == "" || cquantitytextBox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(cperntextBox.Text, out int n);
                bool success1 = int.TryParse(cquantitytextBox.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    if (cprodateTimePicker.Text == cexpdateTimePicker.Text)
                    {
                        MessageBox.Show("Production Date Can't be Same the Expiration Date");
                    }
                    else
                    {
                        Customer_item_store cis = new Customer_item_store();
                        cis.store_name = cstorecomboBox.Text;
                        var citem_name = citemnamecompobox.Text;
                        cis.id_item = (from cs in Ent.Items where cs.name == citem_name select cs.id_item).First();
                        cis.perm_num = int.Parse(cperntextBox.Text);
                        string customer_name = cexchangetextBox.Text;
                        cis.item_name = citem_name;
                        cis.customer_name = customer_name;
                        string item_unit = cunitcomboBox.Text;
                        cis.tel_customer = (from ps in Ent.customers where ps.name_customer == customer_name select ps.tel_customer).FirstOrDefault();



                        Customer_item_store stor = Ent.Customer_item_store.Find(cis.store_name, cis.id_item, cis.tel_customer, cis.perm_num);
                           
                        if (stor != null)
                        {
                            if (cis.store_name != "" && citem_name != "" && customer_name != "" && item_unit != "" && cis.tquantity != 0)
                            {


                                stor.Item_unit = item_unit;
                                stor.tquantity = int.Parse(cquantitytextBox.Text);
                                stor.pro_date = DateTime.Parse(cprodateTimePicker.Text);
                                stor.exp_date = DateTime.Parse(cexpdateTimePicker.Text);
                                stor.cperm_date = DateTime.Parse(cperdateTimePicker.Text);
                                stor.tquantity = int.Parse(cquantitytextBox.Text);
                                if (stor.Last_quantity == null) { stor.Last_quantity = 0; }
                                stor.Last_quantity = (from p in Ent.supplier_item_store
                                                        where p.item_name == stor.item_name &&
                                                        p.store_name == stor.store_name
                                                        select p.Final_Quantity).FirstOrDefault();

                                if (stor.Last_quantity == null) { stor.Last_quantity = 0; }
                                stor.Total_quantity = stor.Last_quantity - stor.tquantity;
                                if (cis.Total_quantity < 0) { MessageBox.Show("The Quantity Has been Demanded if Higher than Store Capcity"); }
                                else
                                {
                                    (from p in Ent.supplier_item_store where p.item_name == stor.item_name && p.store_name == stor.store_name select p)
                                        .ToList().ForEach(x => x.Final_Quantity = stor.Total_quantity);
                                    Ent.SaveChanges();
                                    MessageBox.Show("Exchange Permission Edited");
                                    dataGridView6.Rows.Clear();
                                    var persup = from ps in Ent.Customer_item_store select ps;
                                    foreach (var pus in persup)
                                    {
                                        if (pus != null)
                                        {
                                            dataGridView6.Rows.Add(pus.store_name, pus.customer_name,
                                                pus.cperm_date, pus.item_name, pus.Item_unit,
                                                pus.pro_date, pus.exp_date, pus.perm_num, pus.Last_quantity, pus.tquantity, pus.Total_quantity);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Empty Data");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Client permission is not found");
                        }

                            
                        
                    }
                }
            }
        }
        #endregion

        #region Delete Exchanger Permission

        private void cldeletebtn_Click(object sender, EventArgs e)
        {
            if (cstorecomboBox.Text == "" || citemnamecompobox.Text == ""
                || cexchangetextBox.Text == "" || cperntextBox.Text == "")
            {
                MessageBox.Show("Enter Store name ,Item ,Supplier and Permission no.");
            }
            else
            {
                bool success = int.TryParse(cperntextBox.Text, out int n);
                bool success1 = int.TryParse(cquantitytextBox.Text, out int nn);
                if (success == false && success1 == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    Customer_item_store cis = new Customer_item_store();
                    cis.store_name = cstorecomboBox.Text;
                    var citem_name = citemnamecompobox.Text;
                    cis.id_item = (from cs in Ent.Items where cs.name == citem_name select cs.id_item).First();
                    string item_unit = cunitcomboBox.Text;
                    string customer_name = cexchangetextBox.Text;
                    cis.perm_num = int.Parse(cperntextBox.Text);
                    cis.item_name = citem_name;
                    cis.tel_customer = (from ps in Ent.customers where ps.name_customer == customer_name select ps.tel_customer).FirstOrDefault();
                    if (cis.store_name != "" && citem_name != "" && customer_name != "" && cis.perm_num != 0)
                    {
                        Customer_item_store stor = Ent.Customer_item_store.Find(cis.store_name, cis.id_item, cis.tel_customer, cis.perm_num);

                        if (stor != null)
                        {

                            Ent.Customer_item_store.Remove(stor);
                            Ent.SaveChanges();
                            MessageBox.Show("Exchange Permission Deleted");
                            dataGridView6.Rows.Clear();
                            var persup = from ps in Ent.Customer_item_store select ps;
                            foreach (var pus in persup)
                            {
                                if (pus != null)
                                {
                                    dataGridView6.Rows.Add(pus.store_name, pus.customer_name,
                                       pus.cperm_date, pus.item_name, pus.Item_unit,
                                       pus.pro_date, pus.exp_date, pus.perm_num, pus.Last_quantity, pus.tquantity, pus.Total_quantity);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Empty Data");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Client permission is not found");
                    }
                }
            }
        }



        #endregion

        #endregion


        #region Transaction Tab

        #region Display Transaction
        private void tdisplaybtn_Click(object sender, EventArgs e)
        {

            #region Store
            dataGridView1.Rows.Clear();
            StorenamecomboBox.Items.Clear();
            storecombobox.Items.Clear();
            tstorecomboBox.Items.Clear();
            tstorefcomboBox.Items.Clear();
            var store = from st in Ent.Stores select st;
            foreach (var st in store)
            {
                if (st != null)
                {
                    dataGridView1.Rows.Add(st.store_name, st.address, st.manger_name);
                    StorenamecomboBox.Items.Add(st.store_name);
                    storecombobox.Items.Add(st.store_name);
                    tstorecomboBox.Items.Add(st.store_name);
                    tstorefcomboBox.Items.Add(st.store_name);
                }
            }
            #endregion

            #region Item
            var Item = from it in Ent.Items select it;
            dataGridView2.Rows.Clear();
            itemcompmbox.Items.Clear();
            unitcompobox.Items.Clear();
            titemcomboBox.Items.Clear();
            foreach (var i in Item)
            {
                if (i != null)
                {
                    dataGridView2.Rows.Add(i.id_item, i.name, i.unit, i.name_store);
                    itemcompmbox.Items.Add(i.name);
                    unitcompobox.Items.Add(i.unit);
                    titemcomboBox.Items.Add(i.name);

                }
            }
            #endregion

            #region Supplier
            dataGridView3.Rows.Clear();
            suppliercompobox.Items.Clear();
            tsuppliercomboBox.Items.Clear();
            var supss = from sp in Ent.suppliers select sp;
            foreach (var st in supss)
            {
                if (st != null)
                {
                    dataGridView3.Rows.Add(st.name_sup, st.tel_sup, st.mobile, st.e_mail);
                    suppliercompobox.Items.Add(st.name_sup);
                    tsuppliercomboBox.Items.Add(st.name_sup);
                }
            }
            #endregion

            #region Supply Permission
            var persup = from ps in Ent.supplier_item_store select ps;
            foreach (var pus in persup)
            {
                if (pus != null)
                {
                    dataGridView5.Rows.Add(pus.store_name, pus.supplier_name,
                       pus.perm_date, pus.item_name, pus.Item_unit, pus.quantity,
                       pus.pro_date, pus.exp_date, pus.perm_num);
                }
            }
            #endregion

            #region Transaction
            dataGridView7.Rows.Clear();
            var pertsup = from ps in Ent.Transactions select ps;
            foreach (var pus in pertsup)
            {
                if (pus != null)
                {
                    dataGridView7.Rows.Add(pus.Store_name, pus.Last_quantity, pus.Quantitiy, pus.Total_quantity
                        , pus.Store_Filled, pus.Last_quantity1, pus.Quantitiy, pus.Total_quantity1
                        , pus.Supplier_name, pus.Date, pus.Item_name,
                        pus.Unit, pus.Pro_date, pus.Exp_date);
                }
            }
            #endregion
        }

        #endregion

        #region Add Transaction
        private void taddbtn_Click(object sender, EventArgs e)
        {
            if (tstorecomboBox.Text == "" || tstorefcomboBox.Text == ""
                || titemcomboBox.Text == "" || tsuppliercomboBox.Text == ""
                || tquantitiytextBox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(tquantitiytextBox.Text, out int n);

                if (success == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    if (texpdateTimePicker.Text == tprodateTimePicker.Text)
                    {
                        MessageBox.Show("Production Date Can't be Same the Expiration Date");
                    }
                    else
                    {
                        if (tstorecomboBox.Text == tstorefcomboBox.Text)
                        {
                            MessageBox.Show("The Reposited Store can't be the same of Filled Store");
                        }
                        else
                        {
                            Transaction tran = new Transaction();
                            tran.Store_name = tstorecomboBox.Text;
                            tran.Store_Filled = tstorefcomboBox.Text;
                            string item_name = titemcomboBox.Text;
                            tran.Item_id = (from ps in Ent.Items where ps.name == item_name select ps.id_item).FirstOrDefault();
                            tran.Unit = (from ps in Ent.Items where ps.name == item_name select ps.unit).FirstOrDefault();
                            string sup_name = tsuppliercomboBox.Text;
                            tran.Item_name = item_name;
                            tran.Supplier_name = sup_name;
                            tran.Supplier_tel = (from ps in Ent.suppliers where ps.name_sup == sup_name select ps.tel_sup).FirstOrDefault();
                            tran.Quantitiy = int.Parse(tquantitiytextBox.Text);
                            tran.Pro_date = DateTime.Parse(tprodateTimePicker.Text);
                            tran.Exp_date = DateTime.Parse(texpdateTimePicker.Text);
                            tran.Date = DateTime.Parse(tperdateTimePicker.Text);

                            #region Quantity Caculation
                            if (tran.Last_quantity == null) { tran.Last_quantity = 0; }
                            tran.Last_quantity = (from p in Ent.supplier_item_store
                                                  where p.item_name == item_name &&
                                                 p.store_name == tran.Store_name
                                                  select p.Final_Quantity).FirstOrDefault();
                            if (tran.Last_quantity == null) { tran.Last_quantity = 0; }
                            tran.Quantitiy = int.Parse(tquantitiytextBox.Text);
                            tran.Total_quantity = tran.Last_quantity - tran.Quantitiy;
                            if (tran.Total_quantity < 0) { MessageBox.Show("The Quantity Has been Demanded if Higher than Store Capcity"); }
                            else
                            {


                                (from p in Ent.supplier_item_store where p.item_name == tran.Item_name && p.store_name == tran.Store_name select p)
                                    .ToList().ForEach(x => x.Final_Quantity = tran.Total_quantity);
                                tran.Last_quantity1 = (from p in Ent.supplier_item_store
                                                       where p.item_name == item_name &&
                                                      p.store_name == tran.Store_Filled
                                                       select p.Final_Quantity).FirstOrDefault();
                                if (tran.Last_quantity1 == null) { tran.Last_quantity1 = 0; }
                                tran.Total_quantity1 = tran.Last_quantity1 + tran.Quantitiy;

                                (from p in Ent.supplier_item_store where p.item_name == tran.Item_name && p.store_name == tran.Store_Filled select p)
                                    .ToList().ForEach(x => x.Final_Quantity = tran.Total_quantity1);
                                #endregion
                                
                               
                                    Transaction stor = Ent.Transactions.Find(tran.Store_name, tran.Item_id, tran.Supplier_tel);
                                    if (tran.Store_name != "" && item_name != "" && sup_name != "" && tran.Quantitiy != 0)
                                    {
                                        if (stor == null)
                                        {

                                            Ent.Transactions.Add(tran);
                                            Ent.SaveChanges();
                                            MessageBox.Show("Transaction Permission Added");
                                            dataGridView7.Rows.Clear();
                                            var persup = from ps in Ent.Transactions select ps;
                                            foreach (var pus in persup)
                                            {
                                                if (pus != null)
                                                {
                                                    dataGridView7.Rows.Add(pus.Store_name, pus.Last_quantity, pus.Quantitiy, pus.Total_quantity
                                                        , pus.Store_Filled, pus.Last_quantity1, pus.Quantitiy, pus.Total_quantity1
                                                        , pus.Supplier_name, pus.Date, pus.Item_name,
                                                        pus.Unit, pus.Pro_date, pus.Exp_date);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Transction permission is already found");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Empty Data");
                                    }
                                }
                            
                        }
                    }
                }
            }
        }
        #endregion

        #region Edit Transaction
        private void teditbtn_Click(object sender, EventArgs e)
        {
            if (tstorecomboBox.Text == "" || tstorefcomboBox.Text == ""
                || titemcomboBox.Text == "" || tsuppliercomboBox.Text == ""
                || tquantitiytextBox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                bool success = int.TryParse(tquantitiytextBox.Text, out int n);

                if (success == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    if (texpdateTimePicker.Text == tprodateTimePicker.Text)
                    {
                        MessageBox.Show("Production Date Can't be Same the Expiration Date");
                    }
                    else
                    {
                        Transaction tran = new Transaction();
                        tran.Store_name = tstorecomboBox.Text;
                        tran.Store_Filled = tstorefcomboBox.Text;
                        string sup_name = tsuppliercomboBox.Text;
                        tran.Supplier_name = sup_name;
                        string item_name = titemcomboBox.Text;
                        tran.Item_id = (from ps in Ent.Items where ps.name == item_name select ps.id_item).FirstOrDefault();
                        tran.Supplier_tel = (from ps in Ent.suppliers where ps.name_sup == sup_name select ps.tel_sup).FirstOrDefault();
                        tran.Unit = (from ps in Ent.Items where ps.name == item_name select ps.unit).FirstOrDefault();


                        Transaction stor = Ent.Transactions.Find(tran.Store_name, tran.Item_id, tran.Supplier_tel);
                        if (tran.Store_name != "" && item_name != "" && sup_name != "" && tran.Quantitiy != 0)
                        {
                            if (stor != null)
                            {
                                stor.Item_name = item_name;
                                stor.Quantitiy = int.Parse(tquantitiytextBox.Text);
                                stor.Pro_date = DateTime.Parse(tprodateTimePicker.Text);
                                stor.Exp_date = DateTime.Parse(texpdateTimePicker.Text);
                                stor.Date = DateTime.Parse(tperdateTimePicker.Text);
                                Ent.SaveChanges();
                                MessageBox.Show("Transaction Permission Updated");
                                dataGridView7.Rows.Clear();
                                var persup = from ps in Ent.Transactions select ps;
                                foreach (var pus in persup)
                                {
                                    if (pus != null)
                                    {
                                        dataGridView7.Rows.Add(pus.Store_name, pus.Store_Filled, pus.Supplier_name, pus.Date, pus.Item_name,
                                            pus.Unit, pus.Quantitiy,
                                           pus.Pro_date, pus.Exp_date);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Transction permission is not found");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Empty Data");
                        }
                    }
                }
            }
        }
        #endregion

        #region Delete Transaction
        private void tdeletebtn_Click(object sender, EventArgs e)
        {
            if (tstorecomboBox.Text == "" || titemcomboBox.Text == "" 
                || tsuppliercomboBox.Text == "")
            {
                MessageBox.Show("Please Enter The Reposited Store, Item, Supplier");
            }
            else
            {
                bool success = int.TryParse(tquantitiytextBox.Text, out int n);

                if (success == false)
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    Transaction tran = new Transaction();
                    tran.Store_name = tstorecomboBox.Text;
                    tran.Store_Filled = tstorefcomboBox.Text;
                    string sup_name = tsuppliercomboBox.Text;
                    tran.Supplier_name = sup_name;
                    string item_name = titemcomboBox.Text;
                    tran.Item_id = (from ps in Ent.Items where ps.name == item_name select ps.id_item).FirstOrDefault();
                    tran.Supplier_tel = (from ps in Ent.suppliers where ps.name_sup == sup_name select ps.tel_sup).FirstOrDefault();
                    tran.Unit = (from ps in Ent.Items where ps.name == item_name select ps.unit).FirstOrDefault();
                    Transaction stor = Ent.Transactions.Find(tran.Store_name, tran.Item_id, tran.Supplier_tel);
                    if (tran.Store_name != "" && item_name != "" && sup_name != "" && tran.Quantitiy != 0)
                    {
                        if (stor != null)
                        {
                            Ent.Transactions.Remove(stor);
                            Ent.SaveChanges();
                            MessageBox.Show("Transaction Permission Deleted");
                            dataGridView7.Rows.Clear();
                            var persup = from ps in Ent.Transactions select ps;
                            foreach (var pus in persup)
                            {
                                if (pus != null)
                                {
                                    dataGridView7.Rows.Add(pus.Store_name, pus.Store_Filled, pus.Supplier_name, pus.Date, pus.Item_name,
                                        pus.Unit, pus.Quantitiy,
                                       pus.Pro_date, pus.Exp_date);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Transction permission is not found");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Empty Data");
                    }
                }
            }
        }



        #endregion

        #endregion


        #region Store Reports

        #region Enter Store name
        private void enterbtn_Click(object sender, EventArgs e)
        {
            if (sstorecombobox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                if (fromdateTimePicker.Text == TodateTimePicker.Text)
                {
                    MessageBox.Show("Start Date Can't be Same the End Date");
                }
                else
                {
                    DateTime From_date = DateTime.Parse(fromdateTimePicker.Text);
                    DateTime To_date = DateTime.Parse(TodateTimePicker.Text);
                    string store_name = sstorecombobox.Text;

                    var data = (from d in Ent.supplier_item_store
                                join c in Ent.Stores on d.store_name equals c.store_name
                                where d.perm_date >= From_date
                                && d.perm_date <= To_date && d.store_name.Contains(store_name)
                                select new { d.Final_Quantity, d.store_name, d.item_name, d.Item_unit, d.perm_date, d.supplier_name, c.manger_name, c.address }).ToList();
                    dataGridView8.Rows.Clear();
                    foreach (var item in data)
                    {
                        dataGridView8.Rows.Add(item.item_name, item.manger_name, item.address, item.perm_date, item.Final_Quantity, item.Item_unit, item.supplier_name);
                    }
                    sstorecombobox.Text = "";
                    
                }
            }

        }
        #endregion



        #region Display Stores
        private void ssdisplybtn_Click(object sender, EventArgs e)
        {
            sstorecombobox.Items.Clear();
            var store = from st in Ent.Stores select st;
            foreach (var st in store)
            {
                if (st != null)
                {


                    sstorecombobox.Items.Add(st.store_name);
                }
            }
        }
        #endregion

        #endregion


        #region Report For Each Item

        #region Display Stores
        private void adisplaybtn_Click(object sender, EventArgs e)
        {
            astorecombobox.Items.Clear();
            var store = from st in Ent.Stores select st;
            foreach (var st in store)
            {
                if (st != null)
                {
                    astorecombobox.Items.Add(st.store_name);
                }
            }
        }
        #endregion

        #region Add Store
        public List<string> stores_selected = new List<string>();

 

        public void astroebtn_Click(object sender, EventArgs e)
        {
            if (astorecombobox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                stores_selected.Add(astorecombobox.Text);
                dataGridView9.Rows.Add(astorecombobox.Text);
            }
        }
        #endregion

        #region Clear
        public void clearbtn_Click(object sender, EventArgs e)
        {
            stores_selected.Clear();
            dataGridView9.Rows.Clear();
            astorecombobox.Text = "";
        }
        #endregion

        #region Show Results
        private void showbtn_Click(object sender, EventArgs e)
        {
            if (frommdateTimePicker.Text == ToodateTimePicker.Text)
            {
                MessageBox.Show("Start Date Can't be Same the End Date");
            }
            else
            {
                DateTime From_date = DateTime.Parse(frommdateTimePicker.Text);
                DateTime To_date = DateTime.Parse(ToodateTimePicker.Text);
                DialogResult respond = MessageBox.Show("Do You Want To Set Period Of Time", "Confirm", MessageBoxButtons.YesNoCancel);
                if (respond == DialogResult.Yes)
                {
                    dataGridView10.Rows.Clear();
                    foreach (var st in stores_selected)
                    {
                        var data = (from d in Ent.supplier_item_store
                                    join c in Ent.Stores on d.store_name equals c.store_name
                                    where d.perm_date >= From_date
                                    && d.perm_date <= To_date && d.store_name.Contains(st)
                                    select new
                                    {
                                        d.item_name,
                                        d.Final_Quantity,
                                        d.Item_unit,
                                        d.store_name,
                                        d.perm_date,
                                        d.supplier_name,
                                        d.id_item
                                    }).ToList();

                        foreach (var item in data)
                        {
                            dataGridView10.Rows.Add(item.id_item, item.item_name, item.store_name,
                                item.perm_date, item.Final_Quantity,
                                item.Item_unit, item.supplier_name);
                        }

                    }
                    stores_selected.Clear();
                    dataGridView9.Rows.Clear();
                    astorecombobox.Text = "";

                }


                else if (respond == DialogResult.No)
                {
                    dataGridView10.Rows.Clear();
                    foreach (var st in stores_selected)
                    {
                        var data = (from d in Ent.supplier_item_store
                                    join c in Ent.Stores on d.store_name equals c.store_name
                                    where d.store_name.Contains(st)
                                    select new
                                    {
                                        d.item_name,
                                        d.Final_Quantity,
                                        d.Item_unit,
                                        d.store_name,
                                        d.perm_date,
                                        d.supplier_name,
                                        d.id_item
                                    }).ToList();

                        foreach (var item in data)
                        {
                            dataGridView10.Rows.Add(item.id_item, item.item_name, item.store_name,
                                item.perm_date, item.Final_Quantity,
                                item.Item_unit, item.supplier_name);
                        }
                    }
                }
                stores_selected.Clear();
                dataGridView9.Rows.Clear();
                astorecombobox.Text = "";
            }



        }

        #endregion

        #endregion


        #region Report For Log Item

        #region Display Stores
        private void displaylogbtn_Click(object sender, EventArgs e)
        {
            Addstorelcombobox.Items.Clear();
            var store = from st in Ent.Stores select st;
            foreach (var st in store)
            {
                if (st != null)
                {
                    Addstorelcombobox.Items.Add(st.store_name);
                }
            }
        }
        #endregion

        #region Add Store
        public List<string> stores_selected2 = new List<string>();

      

        public void addlogbtn_Click(object sender, EventArgs e)
        {
            if (Addstorelcombobox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                stores_selected2.Add(Addstorelcombobox.Text);
                dataGridView12.Rows.Add(Addstorelcombobox.Text);
            }
        }
        #endregion

        #region Clear
        public void clearlogbtn_Click(object sender, EventArgs e)
        {
            stores_selected2.Clear();
            dataGridView12.Rows.Clear();
            Addstorelcombobox.Text = "";
        }
        #endregion

        #region Show Results
        private void showlogbtn_Click(object sender, EventArgs e)
        {
            if (fromlogdateTimePicker.Text == tologdateTimePicker.Text)
            {
                MessageBox.Show("Start Date Can't be Same the End Date");
            }
            else
            {

                DateTime From_date = DateTime.Parse(fromlogdateTimePicker.Text);
                DateTime To_date = DateTime.Parse(tologdateTimePicker.Text);
                DialogResult respond = MessageBox.Show("Do You Want To Set Period Of Time", "Confirm", MessageBoxButtons.YesNoCancel);
                if (respond == DialogResult.Yes)
                {
                    dataGridView11.Rows.Clear();
                    foreach (var st in stores_selected2)
                    {
                        var data = (from d in Ent.supplier_item_store
                                    join c in Ent.Customer_item_store on d.store_name equals c.store_name
                                    join p in Ent.Transactions on c.store_name equals p.Store_name
                                   
                                    where d.perm_date >= From_date 
                                    && d.perm_date <= To_date && d.store_name.Contains(st)
                                    select new
                                    {                                       
                                        d.item_name,
                                        d.store_name,
                                        d.Final_Quantity,
                                        d.Item_unit,
                                        p.Store_name,
                                        p.Store_Filled,
                                        p.Quantitiy,
                                        p.Date,
                                        c.tquantity,
                                        c.cperm_date,
                                        c.customer_name,
                                        d.quantity,
                                        d.perm_date,
                                        d.supplier_name
                                    }).ToList();

                        foreach (var item in data)
                        {
                            dataGridView11.Rows.Add( item.item_name,
                                item.store_name, item.Final_Quantity, item.Item_unit,
                                item.Store_name, item.Store_Filled, item.Quantitiy,
                                item.Date,item.tquantity,item.cperm_date,item.customer_name,
                                item.quantity,item.perm_date,item.supplier_name);
                        }

                    }
                    stores_selected2.Clear();
                    dataGridView12.Rows.Clear();
                    Addstorelcombobox.Text = "";
                }


                else if (respond == DialogResult.No)
                {
                    dataGridView11.Rows.Clear();
                    foreach (var st in stores_selected)
                    {
                        var data = (from d in Ent.supplier_item_store
                                    join c in Ent.Customer_item_store on d.store_name equals c.store_name
                                    join p in Ent.Transactions on c.store_name equals p.Store_name

                                    where d.perm_date >= From_date
                                    && d.perm_date <= To_date && d.store_name.Contains(st)
                                    select new
                                    {
                                        d.item_name,
                                        d.store_name,
                                        d.Final_Quantity,
                                        d.Item_unit,
                                        p.Store_name,
                                        p.Store_Filled,
                                        p.Quantitiy,
                                        p.Date,
                                        c.tquantity,
                                        c.cperm_date,
                                        c.customer_name,
                                        d.quantity,
                                        d.perm_date,
                                        d.supplier_name
                                    }).ToList();

                        foreach (var item in data)
                        {
                            dataGridView11.Rows.Add(item.item_name,
                                item.store_name, item.Final_Quantity, item.Item_unit,
                                item.Store_name, item.Store_Filled, item.Quantitiy,
                                item.Date, item.tquantity, item.cperm_date, item.customer_name,
                                item.quantity, item.perm_date, item.supplier_name);
                        }
                    }
                }
                stores_selected2.Clear();
                dataGridView12.Rows.Clear();
                Addstorelcombobox.Text = "";
            }



        }








        #endregion

        #endregion



        #region Items With Period Of Time


        #region Display Stores
        private void tildisplaybtn_Click_1(object sender, EventArgs e)
        {
            tilcomboBox.Items.Clear();
            var store = from st in Ent.Stores select st;
            foreach (var st in store)
            {
                if (st != null)
                {
                    tilcomboBox.Items.Add(st.store_name);
                }
            }
        }
        #endregion

        #region Add Store
        public List<string> stores_selected3 = new List<string>();



        public void tiladdbtn_Click(object sender, EventArgs e)
        {
            if (tilcomboBox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                stores_selected3.Add(tilcomboBox.Text);
                dataGridView14.Rows.Add(tilcomboBox.Text);
            }
        }
        #endregion

        #region Clear
        public void tilclearbtn_Click(object sender, EventArgs e)
        {
            stores_selected3.Clear();
            dataGridView14.Rows.Clear();
            tilcomboBox.Text = "";
        }
        #endregion

        #region Show Results
        private void tilshowbtn_Click_1(object sender, EventArgs e)
        {
            if (tilfromdateTimePicker.Text == tiltodateTimePicker.Text)
            {
                MessageBox.Show("Start Date Can't be Same the End Date");
            }
            else
            {
                DateTime From_date = DateTime.Parse(tilfromdateTimePicker.Text);
                DateTime To_date = DateTime.Parse(tiltodateTimePicker.Text);
                DialogResult respond = MessageBox.Show("Do You Want To Set Period Of Time", 
                    "Confirm",MessageBoxButtons.YesNoCancel);
                if (respond == DialogResult.Yes)
                {
                    dataGridView13.Rows.Clear();
                    foreach (var st in stores_selected3)
                    {
                        var data = (from d in Ent.supplier_item_store
                                    join c in Ent.Stores on d.store_name equals c.store_name
                                    where d.perm_date >= From_date
                                    && d.perm_date <= To_date && d.store_name.Contains(st)
                                    select new
                                    {
                                        d.item_name,
                                        d.quantity,
                                        d.Item_unit,
                                        d.store_name,
                                        d.perm_date,
                                        d.supplier_name,
                                        d.id_item
                                    }).ToList();

                        foreach (var item in data)
                        {
                            int month, year,day;
                            string period;
                            DateTime n = DateTime.Now;
                            DateTime mo = (DateTime)item.perm_date;
                            month = DateTime.Now.Month - mo.Month;
                            day = DateTime.Now.Day - mo.Day;
                            year = (DateTime.Now.Year - mo.Year);
                            if (day < 0)
                            {
                                month = month - 1;
                                day = day + 31;
                                period = $"{day} Days - {month} Month-{year} Year";
                            }
                            if (month < 0)
                            {
                                month = month + 12;
                                year = year - 1;
                                period = $"{day} Days - {month} Month-{year} Year";
                            }

                            
                            period = $"{day} Days - {month} Month-{year} Year";
                            

                            dataGridView13.Rows.Add(item.id_item, item.item_name, item.store_name,
                               period, item.quantity,
                                item.Item_unit, item.supplier_name);
                        }

                    }
                    stores_selected3.Clear();
                    dataGridView14.Rows.Clear();
                    tilcomboBox.Text = "";

                }


                else if (respond == DialogResult.No)
                {
                    dataGridView13.Rows.Clear();
                    foreach (var st in stores_selected3)
                    {
                        var data = (from d in Ent.supplier_item_store
                                    join c in Ent.Stores on d.store_name equals c.store_name
                                    where d.store_name.Contains(st)
                                    select new
                                    {
                                        d.item_name,
                                        d.quantity,
                                        d.Item_unit,
                                        d.store_name,
                                        d.perm_date,
                                        d.supplier_name,
                                        d.id_item
                                    }).ToList();

                        foreach (var item in data)
                        {
                            dataGridView13.Rows.Add(item.id_item, item.item_name, item.store_name,
                                item.perm_date, item.quantity,
                                item.Item_unit, item.supplier_name);
                        }
                    }
                }
                stores_selected3.Clear();
                dataGridView14.Rows.Clear();
                tilcomboBox.Text = "";
            }



        }



        #endregion

        #endregion



        #region Expiration Items



        #region Display Stores
        private void expdiplaybtn_Click(object sender, EventArgs e)
        {
            expitemcombobox.Items.Clear();
            var store = from st in Ent.Stores select st;
            foreach (var st in store)
            {
                if (st != null)
                {
                    expitemcombobox.Items.Add(st.store_name);
                }
            }
        }
        #endregion

        #region Add Store
        public List<string> stores_selected5 = new List<string>();



        public void Expaddbtn_Click(object sender, EventArgs e)
        {
            if (expitemcombobox.Text == "")
            {
                MessageBox.Show("Empty Data");
            }
            else
            {
                stores_selected5.Add(expitemcombobox.Text);
                dataGridView16.Rows.Add(expitemcombobox.Text);
            }
        }
        #endregion

        #region Clear
        public void expclearbtn_Click(object sender, EventArgs e)
        {
            stores_selected5.Clear();
            dataGridView16.Rows.Clear();
            expitemcombobox.Text = "";
        }
        #endregion

        #region Show Results
        public void expshowbtn_Click(object sender, EventArgs e)
        {
            if (expmonthtextbox.Text == null || expmonthtextbox.Text == "")
            {
                MessageBox.Show("Please Enter number of months");
            }
            else
            {
                Boolean success = int.TryParse(expmonthtextbox.Text, out int nomonth);
                if (success == false) { MessageBox.Show("Invalid Data"); }
                else
                {


                    DateTime dateTime = DateTime.Now;
                    DateTime From_date =  DateTime.Now;
                    DateTime To_date = dateTime.AddMonths(nomonth);

                    dataGridView15.Rows.Clear();
                        foreach (var sttt in stores_selected5.ToList())
                        {
                            var data = (from d in Ent.supplier_item_store
                                        join c in Ent.Stores on d.store_name equals c.store_name
                                        where d.exp_date >= From_date
                                        && d.exp_date <= To_date && d.store_name.Contains(sttt)
                                        select new
                                        {
                                            d.item_name,
                                            d.quantity,
                                            d.Item_unit,
                                            d.store_name,
                                            d.exp_date,
                                            d.supplier_name,
                                            d.id_item
                                        }).ToList();

                            foreach (var item in data)
                            {

                                dataGridView15.Rows.Add(item.id_item, item.item_name, item.store_name,
                                   item.exp_date, item.quantity,
                                    item.Item_unit, item.supplier_name);
                            }


                            stores_selected5.Clear();
                            dataGridView14.Rows.Clear();
                            expitemcombobox.Text = "";

                        }
                    



                    
                }
            }



        }



        #endregion

        #endregion

      
    }
}
