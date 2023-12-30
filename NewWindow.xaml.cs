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
using System.Windows.Shapes;

namespace _27._09._2023_Команды
{
    /// <summary>
    /// Логика взаимодействия для NewWindow.xaml
    /// </summary>
    public partial class NewWindow : Window
    {
        string[] keys;

        public NewWindow() {
            InitializeComponent();

            keys = new string[] {"none", "trial", "pro" };
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            string key = textBox.Text;
            if (key == keys[0]) {
                this.Close();
            }
            else if (key == keys[1]){
                this.Close();
            }
            else if (key == keys[2]) {
                this.Close();
            }
            else { 
                MessageBox.Show("Incorrect key!"); 
            }
        }

        private void Click_Close(object sender, RoutedEventArgs e) {
            this.Close();
        }

        public static void ShowDialogWindow() {
            NewWindow window = new NewWindow();
            window.ShowDialog();
        }

        public string KeyValue {
            get { return textBox.Text; }
        }

    }
}
