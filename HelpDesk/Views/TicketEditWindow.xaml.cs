using HelpDesk.Models;
using System.Windows;

namespace HelpDesk.Views
{
    public partial class TicketEditWindow : Window
    {
        private Ticket _currentTicket;

        public TicketEditWindow(Ticket ticket)
        {
            InitializeComponent();

            _currentTicket = ticket;

            TicketNumberTextBlock.Text = $"Заявка №: {_currentTicket.Id}";
            CategoryComboBox.Text = _currentTicket.Category;
            DescriptionTextBox.Text = _currentTicket.Description;
            PriorityComboBox.Text = _currentTicket.Priority;
            StatusComboBox.Text = _currentTicket.Status;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show(
                    "Введите описание проблемы.",
                    "Проверка данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            _currentTicket.Category = CategoryComboBox.Text;
            _currentTicket.Description = DescriptionTextBox.Text.Trim();
            _currentTicket.Priority = PriorityComboBox.Text;
            _currentTicket.Status = StatusComboBox.Text;

            MessageBox.Show(
                "Изменения сохранены (временно в памяти приложения).",
                "HelpDesk",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
