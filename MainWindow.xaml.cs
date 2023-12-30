using Microsoft.Win32;
using System;
using System.IO;
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

namespace _27._09._2023_Команды
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isChanged = false;

        private string userType = "none";
        string a;

        public MainWindow() {
            InitializeComponent();

            CommandBinding binding;
            binding = new CommandBinding(ApplicationCommands.Close);
            binding.Executed += CloseCommand;
            CommandBindings.Add(binding);
        }

        private void NewCommand(object sender, ExecutedRoutedEventArgs e) { 
            textBox.Clear();
            isChanged = false;
        }

        private void OpenCommand(object sender, ExecutedRoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files|*.txt|All Files|*.*";

            if (openFileDialog.ShowDialog() == true) {
                string selectedFilePath = openFileDialog.FileName;
                try {
                    string fileContent = File.ReadAllText(selectedFilePath);
                    textBox.Text = fileContent;
                    isChanged = false;
                }
                catch (Exception ex) {
                    MessageBox.Show("Ошибка при открытии файла: " + ex.Message);
                }
            }
        }

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files|*.txt|All Files|*.*";

            if (saveFileDialog.ShowDialog() == true) {
                string filePath = saveFileDialog.FileName;
                string contentToSave = textBox.Text;
                try {
                    File.WriteAllText(filePath, contentToSave);
                    isChanged = false;
                }
                catch (Exception ex) {
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                }
            }
        }

        private void CloseCommand(object sender, ExecutedRoutedEventArgs e) { 
            this.Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e) {
            isChanged = true;
        }

        private void Command_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = isChanged;
        }

        private void UpdateMenu() {
            switch (userType) {
                case "none":
                    DisableAllMenuItems();
                    break;

                case "trial":
                    EnableFileItems();
                    DisableEditItems();
                    break;

                case "pro":
                    EnableAllMenuItems();
                    break;

                case null:
                    DisableAllMenuItems();
                    break;
            }
        }

        private void CheckKey() {
            UpdateMenu();
        }

        private void EnableFileItems() {
            newMenuItem.IsEnabled = true;
            openMenuItem.IsEnabled = true;
            saveMenuItem.IsEnabled = true;

            CommandBinding binding;
            binding = new CommandBinding(ApplicationCommands.New);
            binding.Executed += NewCommand;
            binding.CanExecute += Command_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Open);
            binding.Executed += OpenCommand;
            binding.CanExecute += Command_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Save);
            binding.Executed += SaveCommand;
            binding.CanExecute += Command_CanExecute;
            CommandBindings.Add(binding);
        }
        private void DisableFileItems() {
            newMenuItem.IsEnabled = false;
            openMenuItem.IsEnabled = false;
            saveMenuItem.IsEnabled = false;

            CommandBindings.Clear();
        }

        private void EnableEditItems() {
            cutMenuItem.IsEnabled = true;
            copyMenuItem.IsEnabled = true;
            pasteMenuItem.IsEnabled = true;
        }
        private void DisableEditItems() {
            cutMenuItem.IsEnabled = false;
            copyMenuItem.IsEnabled = false;
            pasteMenuItem.IsEnabled = false;
        }

        private void EnableAllMenuItems() {
            EnableFileItems();
            EnableEditItems();
        }
        private void DisableAllMenuItems(){
            DisableFileItems();
            DisableEditItems();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            NewWindow newWindow = new NewWindow();
            newWindow.Owner = this; 
            this.IsEnabled = false;

            newWindow.Closed += (s, eventArgs) => {
                this.IsEnabled = true;
                if (newWindow.DialogResult == false) {
                    userType = newWindow.KeyValue;
                    CheckKey();
                }
            };
            newWindow.ShowDialog();
        }
    }
}
