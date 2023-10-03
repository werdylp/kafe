using System;
using System.Collections.Generic;
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

namespace obsluhovač
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        List<string>[] recepty = new List<string>[6];
        List<string> listSurovin = new List<string>();

        int surovinVPripravne = 0;
        bool jeHrnek = false;

        Image nad;

        public MainWindow()
        {
            InitializeComponent();
            recepty[0] = new List<string> { "maly", "kafe" };
            recepty[1] = new List<string> { "stredni", "kafe", "slehacka" };
            recepty[2] = new List<string> { "stredni", "kafe", "mleko" };
            recepty[3] = new List<string> { "stredni", "kafe", "pena" };
            recepty[4] = new List<string> { "velky", "kafe", "mleko", "pena" };
            recepty[5] = new List<string> { "sklenice", "kafe", "mleko", "led" };
        }

        private void surovinaClick(object sender, MouseButtonEventArgs e)
        {
            if (surovinVPripravne < 6)
            {
                Image sur = new Image()
                {
                    Source = new BitmapImage(new Uri("img/suroviny-" + (sender as Label).Name + ".png", UriKind.Relative))
                };

                Grid.SetColumn(sur, surovinVPripravne % 3);
                Grid.SetRow(sur, surovinVPripravne / 3);

                priprava.Children.Add(sur);
                surovinVPripravne++;
                listSurovin.Add((sender as Label).Name);
            }
        }

        private void nadobiClick(object sender, MouseButtonEventArgs e)
        {
            if (!jeHrnek)
            {
                nad = new Image()
                {
                    Source = (sender as Image).Source,
                    Margin = new Thickness(110, 110, 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 60,
                    Height = 60,
                };

                kavovar.Children.Add(nad);
                listSurovin.Add((sender as Image).Name);
            }
        }

        private void uvarKafe(object sender, MouseButtonEventArgs e)
        {
            for (int recept = 0; recept < 6; recept++)
            {
                if (recepty[recept].Count() != listSurovin.Count()) 
                {
                    continue;
                }

                recepty[recept].Sort();
                listSurovin.Sort();

                bool k = listSurovin == recepty[recept];

                MessageBox.Show(k.ToString());

                bool nasel = true;

                for (int s = 0; s < listSurovin.Count; s++)
                {
                    if (recepty[recept][s] != listSurovin[s])
                    {
                        nasel = false;
                        break;
                    }
                }

                Image kafe;
                if (nasel)
                {
                    // kafe na pas
                    kafe = new Image()
                    {
                        Margin = new Thickness(0),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,   

                    };

                    switch (recept)
                    {
                        case 0:
                            kafe.Source = new BitmapImage(new Uri("img/kafe-male_espresso.png", UriKind.Relative));
                            break;
                        case 1:
                            kafe.Source = new BitmapImage(new Uri("img/kafe-espresso_s_mlekem.png", UriKind.Relative));
                            break;
                        case 2:
                            kafe.Source = new BitmapImage(new Uri("img/kafe-espresso_se_slehasckou.png", UriKind.Relative));
                            break;
                        case 3:
                            kafe.Source = new BitmapImage(new Uri("img/kafe-espresso_macchiato.png", UriKind.Relative));
                            break;
                        case 4:
                            kafe.Source = new BitmapImage(new Uri("img/kafe-capuccino.png", UriKind.Relative));
                            break;
                        case 5:
                            kafe.Source = new BitmapImage(new Uri("img/kafe-ledova_kava.png", UriKind.Relative));
                            break;


                    }

                    break;
                }

                kafe = new Image()
                {
                    Margin = new Thickness(0),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Source = new BitmapImage(new Uri("img/kafe-nezname.png", UriKind.Relative))

                };
            }


            priprava.Children.Clear();
            surovinVPripravne = 0;

            kavovar.Children.Remove(nad);

            jeHrnek = false;  
        }
    }
}
