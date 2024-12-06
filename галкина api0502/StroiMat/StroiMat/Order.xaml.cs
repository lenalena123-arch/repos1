using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace StroiMat
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {

        private AppDbContext _context;
        private ICollectionView _productView;
        public Order()
        {

            InitializeComponent();
            _context = new AppDbContext();
            LoadData();
        }
        private void LoadData()
        {
            var products = _context.Products.ToList();

            // Проверяем наличие фотографии, если она отсутствует - ставим заглушку
            foreach (var product in products)
            {
                if (string.IsNullOrEmpty(product.ProductPhoto))
                {
                    product.ProductPhoto = "/Resources/picture.png"; // путь к картинке-заглушке
                }
            }

            _productView = CollectionViewSource.GetDefaultView(products);
            ProductListView.ItemsSource = _productView;

            // Add dynamic filter options to the ComboBox
            var manufacturers = products.Select(p => p.ProductManufacturer).Distinct().ToList();
            foreach (var manufacturer in manufacturers)
            {
                ManufacturerFilter.Items.Add(manufacturer);
            }
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }
        private void ManufacturerFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }
        private void ApplyFilter()
        {
            string searchQuery = SearchBox.Text.ToLower();
            string selectedManufacturer = ManufacturerFilter.SelectedItem?.ToString();

            _productView.Filter = productObj =>
            {
                var product = productObj as Product;
                bool matchesSearch = product.ProductName.ToLower().Contains(searchQuery) || product.ProductDescription.ToLower().Contains(searchQuery);
                bool matchesManufacturer = selectedManufacturer == "All Manufacturers" || product.ProductManufacturer == selectedManufacturer;
                return matchesSearch && matchesManufacturer;
            };
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addOrder = new addOrder(); // Create new window for adding products
            addOrder.ShowDialog();
            LoadData(); // Refresh the data after adding a product
        }

    }
}
