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
using MasterMind;

namespace Uppgift5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MasterMindModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new MasterMindModel();
        }

        private void buttKlar_Click(object sender, RoutedEventArgs e)
        {
            string testKey = tstKey.Text;
            tstKey.Text = "";            

            MatchResult ms = model.TestIfMatch(testKey);
            
        }

        private void tstKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tstKey.Text.Length == 4)
            {
                buttKlar.IsEnabled = true;
            }
            else
            {
                buttKlar.IsEnabled = false;
            }
        }
    }
}
