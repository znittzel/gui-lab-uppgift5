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
        MasterMindModel _model = new MasterMindModel();
        int _pos = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttKlar_Click(object sender, RoutedEventArgs e)
        {
            string testKey = tstKey.Text;
            tstKey.Text = "";

            if (MasterMindModel.IsValidKey(testKey))
            {
                MatchResult mr = _model.TestIfMatch(testKey);
                showTestKey(_pos, testKey);
                showTestResult(_pos, mr);
                _pos++;

                if (_pos == 10 || mr.NumCorrect == 4)
                {
                    tstKey.IsEnabled = false;
                    buttKlar.IsEnabled = false;

                    if (mr.NumCorrect == 4)
                        MessageBox.Show("Grattis, du vann! Spelet är slut.");
                    else
                        MessageBox.Show("Tyvärr, du förlorade. Spelet är slut.");

                    Application.Current.Shutdown();
                }
            }
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

        private MMRow getMMRowOfPosition(int pos)
        {
            int row = (theGrid.Children.Count - 2) - pos;
            return theGrid.Children[row] as MMRow;
        }

        private void showTestKey(int pos, string testKey)
        {
            getMMRowOfPosition(pos).ShowTestKey(testKey);
        }

        private void showTestResult(int pos, MatchResult mr)
        {
            getMMRowOfPosition(pos).ShowTestResult(mr, false);
        }
    }
}
