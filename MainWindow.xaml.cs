using decoxmodel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;


namespace DecoX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        public decoxentitiesmodel ctx = new decoxentitiesmodel();
        CollectionViewSource produseVSource;
        CollectionViewSource clientiVSource;
        CollectionViewSource categoriiVSource;

        private void Windows_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

      
     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource produseViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("produseViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // produseViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource clientiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // clientiViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource categoriiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("categoriiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // categoriiViewSource.Source = [generic data source]
            produseVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("produseViewSource")));
            produseVSource.Source = ctx.Produses.Local;
            ctx.Produses.Load();
        }

   

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;

            

        }
        private void btnEditO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;

         

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            produseVSource.View.MoveCurrentToNext();
            clientiVSource.View.MoveCurrentToNext();
            categoriiVSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            produseVSource.View.MoveCurrentToNext();
            clientiVSource.View.MoveCurrentToNext();
            categoriiVSource.View.MoveCurrentToNext();
        }
        private void SaveProduse()
        {
            Produse produse = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    produse = new Produse()
                    {
                        Cod = int.Parse(codTextBox.Text.Trim()),
                        Culoare = culoareTextBox.Text.Trim(),
                        Denumire = denumireTextBox.Text.Trim(),
                        Material = materialTextBox.Text.Trim(),
                        Pret = int.Parse(pretTextBox.Text.Trim()),
                        ProdusId = int.Parse(produsIdTextBox.Text.Trim())
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Produses.Add(produse);
                    produseVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }

                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    produse = (Produse)produseDataGrid.SelectedItem;
                    produse.Cod = int.Parse(codTextBox.Text.Trim());
                    produse.Culoare = culoareTextBox.Text.Trim();
                    produse.Denumire = denumireTextBox.Text.Trim();
                    produse.Material = materialTextBox.Text.Trim();
                    produse.Pret = decimal.Parse(pretTextBox.Text.Trim());
                    produse.ProdusId = int.Parse(produsIdTextBox.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    produse = (Produse)produseDataGrid.SelectedItem;
                    ctx.Produses.Remove(produse);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                produseVSource.View.Refresh();
            }

        }

        private void SaveClienti()
        {
            Clienti clienti = null;
            if (action == ActionState.New)
            {
                try
                {

                    clienti = new Clienti()
                    {
                        Adresa = adresaTextBox.Text.Trim(),
                        ClientId = int.Parse(clientIdTextBox.Text.Trim()),
                        ComandaId = int.Parse(comandaIdTextBox.Text.Trim()),
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim(),

                    };
                    //adaugam entitatea nou creata in context
                    ctx.Clientis.Add(clienti);
                    clientiVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }

                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    clienti = (Clienti)clientiDataGrid.SelectedItem;
                    clienti.Adresa = adresaTextBox.Text.Trim();
                    clienti.ClientId = int.Parse(clientIdTextBox.Text.Trim());
                    clienti.ComandaId = int.Parse(comandaIdTextBox.Text.Trim());
                    clienti.Nume = numeTextBox.Text.Trim();
                    clienti.Prenume = prenumeTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    clienti = (Clienti)clientiDataGrid.SelectedItem;
                    ctx.Clientis.Remove(clienti);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientiVSource.View.Refresh();
            }

        }

        private void SaveCategorii()
        {
            Categorii categorii = null;
            if (action == ActionState.New)
            {
                try
                {

                    categorii = new Categorii()
                    {
                        
                        Categorie = int.Parse(categorieIdTextBox.Text.Trim()),
                        Denumire = numeTextBox.Text.Trim(),
                        

                    };
                    //adaugam entitatea nou creata in context
                    ctx.Categoriis.Add(categorii);
                    categoriiVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }

                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    categorii.Categorie = int.Parse(categorieIdTextBox.Text.Trim());
                    categorii.Denumire = numeTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    categorii = (Categorii)categoriiDataGrid.SelectedItem;
                    ctx.Categoriis.Remove(categorii);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                categoriiVSource.View.Refresh();
            }

        }
        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlDecox.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Produse":
                    SaveProduse();
                    break;
                case "Clienti":
                    SaveClienti();
                    break;
                case "Categorii":
                    SaveCategorii();
                    break;

                case "Comenzi":
                    break;
            }
            ReInitialize();
        }

    }
}




