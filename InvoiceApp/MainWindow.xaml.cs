using InvoiceApp.Models.Invoices;
using InvoiceApp.Models.Orders;
using InvoiceApp.Models.Pricings;
using InvoiceApp.Providers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Xml;

namespace InvoiceApp;
public partial class MainWindow : Window
{
    private readonly IInvoiceService _invoiceService;

    public MainWindow(IInvoiceService invoiceService)
    {
        InitializeComponent();

        grpInvoice.Visibility= Visibility.Hidden;
        _invoiceService = invoiceService;
    }

    private void btnOrder_Click(object sender, RoutedEventArgs e)
    {
        var pathToFile = OpenFile("Wybierz zamówienie", "Pliki XML (*.xml) | *.xml*");
        txtOrder.Text = pathToFile;
    }

    private void btnPricing_Click(object sender, RoutedEventArgs e)
    {
        var pathToFile = OpenFile("Wybierz cennik", "Pliki XML (*.xml) | *.xml*");
        txtPricing.Text = pathToFile;
    }

    private void btnInvoicePath_Click(object sender, RoutedEventArgs e)
    {
        var selectedPath = OpenFolder();
        txtInvoicePath.Text = selectedPath;
    }

    private void btnGenerateInvoice_Click(object sender, RoutedEventArgs e)
    {
        CheckRequiredFields(out bool checkResult, out string message);
        if (!checkResult)
        {
            System.Windows.Forms.MessageBox.Show(message);
            return;
        }

        var order = Order.CreateByXml(txtOrder.Text);
        if (order is null)
        {
            System.Windows.Forms.MessageBox.Show("Błąd przy przetwarzaniu zamówienia.");
            return;
        }

        var pricing = Pricing.CreateByXml(txtPricing.Text);
        if (pricing is null)
        {
            System.Windows.Forms.MessageBox.Show("Błąd przy przetwarzaniu cennika.");
            return;
        }

        var invoice = _invoiceService.GenerateInvoice(order, pricing);

        _invoiceService.SaveToFile(invoice, txtInvoicePath.Text);
        ShowInvoice(invoice);
    }

    private string OpenFile(string Titile, string Filter)
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Title = Titile;
            openFileDialog.Filter = Filter;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return openFileDialog.FileName;
            else
                return String.Empty;
        }
    }

    private string OpenFolder()
    {
        using (var folderBrowserDialog = new FolderBrowserDialog())
        { 

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return folderBrowserDialog.SelectedPath;
            else
                return String.Empty;
        }
    }

    private void CheckRequiredFields(out bool Result, out string Message)
    {
        Result = true;
        Message = String.Empty;

        if (string.IsNullOrEmpty(txtOrder.Text) || txtOrder.Text == "wybierz plik")
        {
            Result = false;
            Message += "Brak wybranego zamówienia.";
        }

        if (string.IsNullOrEmpty(txtPricing.Text) || txtPricing.Text == "wybierz plik")
        {
            Result = false;
            Message += "\nBrak wybranego cennika.";
        }

        if (string.IsNullOrEmpty(txtInvoicePath.Text) || txtInvoicePath.Text == "wskaż folder do zapisania faktury")
        {
            Result = false;
            Message += "\nBrak wybranej ścieżki do zapisania faktury.";
        }
    }

    private void ShowInvoice(Invoice invoice)
    {
        txtbInvoiceDateValue.Text = invoice.Date.ToString();

        txtbInvoiceIssuerName.Text = invoice.Issuer.Name;
        txtbInvoiceIssuerAddress.Text = invoice.Issuer.Address;

        txtbInvoicePurchaserName.Text = invoice.Purchaser.Name;
        txtbInvoicePurchaserAddress.Text = invoice.Purchaser.Address;

        grdInvoiceItems.ItemsSource = invoice.Items.OrderBy(x => x.OrdinalNo);

        txtbSummaryValue.Text = invoice.Summary.Sum.ToString();

        grpInvoice.Visibility = Visibility.Visible;
    }
}
