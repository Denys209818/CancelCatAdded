using ApplicationSettings.Models;
using ApplicationSettings.Services;
using EFData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CancelAddedCats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EFDataContext _context { get; set; }
        public ObservableCollection<CatModel> _cats { get; set; }
        public WindowModel _window { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _context = new EFDataContext();
            _window = new WindowModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DbSeeder.SeedAll(_context);

            ReloadProject();
        }

        private void ReloadProject() 
        {
            _cats = new ObservableCollection<CatModel>(_context.Cats.Select(x => new CatModel
            {
                Id = x.Id,
                Name = x.Name,
                Details = x.Details,
                ImgUrl = x.ImgUrl,
                Birthday = x.Birthday,
                Price = 200
            }).ToList());

            dgCats.ItemsSource = _cats;
            this.DataContext = _window;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int res;
            if(int.TryParse(this.txtCount.Text, out res)) 
            {
                AddWindow dlg = new AddWindow(res, _context);
                dlg.Show();
                ReloadProject();
            }
        }
    }
}
