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

namespace HelpDesk.Views
{
    /// <summary>
    /// Логика взаимодействия для TicketCreateWindow.xaml
    /// </summary>
    public partial class TicketCreateWindow : Window
    {
        public TicketCreateWindow()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка категории
            if (CategoryComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show(
                    "Выберите категорию заявки.",
                    "Проверка данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // Проверка описания
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show(
                    "Введите описание проблемы.",
                    "Проверка данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // Проверка приоритета
            if (PriorityComboBox.SelectedItem == null)
            {
                MessageBox.Show(
                    "Выберите приоритет заявки.",
                    "Проверка данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // Пока данные в БД не сохраняем
            MessageBox.Show(
                "Данные заявки заполнены корректно.\n" +
                "На следующих занятиях заявка будет сохраняться в MS SQL Server.",
                "HelpDesk",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
