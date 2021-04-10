using ApplicationSettings.Models;
using Bogus;
using EFData;
using EFEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CancelAddedCats
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private EFDataContext _context { get; set; }
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private Faker<CatModel> faker;
        public int _result { get; set; }
        public AddWindow(int result, EFDataContext context)
        {
            InitializeComponent();
            _result = result;
            _context = context;
            this.faker = new Faker<CatModel>("uk")
                .RuleFor(x => x.Name, (f, p) => { return f.Name.FirstName(); })
                .RuleFor(x => x.Price, (f, p) => f.Random.Decimal(0, 1000))
                .RuleFor(x => x.Birthday, (f, p) => DateTime.Now)
                .RuleFor(X => X.Details, (f, p) => f.Lorem.Paragraph())
                .RuleFor(x => x.ImgUrl, (f, p) => 
                "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d6/Manoel.jpg/275px-Manoel.jpg");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.catProgres.Maximum = _result;
            this.catProgres.Minimum = 0;
            Thread thread = new Thread(AddCats);
            thread.Start();

        }

        private void AddCats() 
        {
            ParallelOptions parOpt = new ParallelOptions();
            parOpt.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
            parOpt.CancellationToken = tokenSource.Token;
            Parallel.Invoke(parOpt, new Action(() => {
                    for (int i = 1; i <= _result; i++)
                    {
                        try
                        {
                            parOpt.CancellationToken.ThrowIfCancellationRequested();
                            Dispatcher.Invoke(new Action(() =>
                            {
                                this.catProgres.Value = i;
                            }));
                            Thread.Sleep(300);
                            CatModel model = faker.Generate();
                            AppCat cat = new AppCat {
                                Name = model.Name,
                                ImgUrl = model.ImgUrl,
                                Details = model.Details,
                                Birthday = model.Birthday,
                            };
                            _context.Cats.Add(cat);
                            _context.SaveChanges();

                            AppCatPrice price = new AppCatPrice { 
                                DateCreate = DateTime.Now,
                                CatId = cat.Id,
                                Price = model.Price
                            };
                            _context.CatPrices.Add(price);
                            _context.SaveChanges();
                        } 
                        catch
                        {
                            return;
                        }
                    }
                }));
        }

        private void catProgres_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.catProgres.Value == _result) 
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
            this.Close();
        }
    }
}
