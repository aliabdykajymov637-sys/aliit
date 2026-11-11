using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HelpDesk.Views
{
    public partial class MainWindow : Window
    {
        private List<Ticket> tickets;

        public MainWindow()
        {
            InitializeComponent();

            tickets = new List<Ticket>
            {
                new Ticket
                {
                    Id = 1,
                    Category = "Сеть",
                    Description = "Нет доступа в Интернет",
                    Priority = "Высокий",
                    Status = "Новая",
                    CreatedAt = DateTime.Now
                },
                new Ticket
                {
                    Id = 2,
                    Category = "ПО",
                    Description = "Не запускается Microsoft Word",
                    Priority = "Средний",
                    Status = "В работе",
                    CreatedAt = DateTime.Now
                },
                new Ticket
                {
                    Id = 3,
                    Category = "Оборудование",
                    Description = "Не работает принтер",
                    Priority = "Низкий",
                    Status = "Выполнена",
                    CreatedAt = DateTime.Now
                }
            };

            TicketsDataGrid.ItemsSource = tickets;
        }

        // Единый метод фильтрации и поиска (Задания 23–25)
        private void ApplyFilters()
        {
            if (tickets == null || SearchTextBox == null || StatusComboBox == null)
            {
                return;
            }

            string searchText = SearchTextBox.Text.Trim().ToLower();
            string selectedStatus = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            IEnumerable<Ticket> filteredTickets = tickets;

            // Фильтрация по тексту (по описанию и категории)
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredTickets = filteredTickets.Where(ticket =>
                    ticket.Description.ToLower().Contains(searchText) ||
                    ticket.Category.ToLower().Contains(searchText));
            }

            // Фильтрация по статусу
            if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "Все")
            {
                filteredTickets = filteredTickets.Where(ticket =>
                    ticket.Status == selectedStatus);
            }

            TicketsDataGrid.ItemsSource = filteredTickets.ToList();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            StatusComboBox.SelectedIndex = 0;
            TicketsDataGrid.ItemsSource = tickets;
        }

        private void AddTicketButton_Click(object sender, RoutedEventArgs e)
        {
            TicketCreateWindow window = new TicketCreateWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void EditTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsDataGrid.SelectedItem == null)
            {
                MessageBox.Show(
                    "Сначала выберите заявку.",
                    "Редактирование",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            Ticket selectedTicket = TicketsDataGrid.SelectedItem as Ticket;

            if (selectedTicket == null)
            {
                MessageBox.Show(
                    "Не удалось получить данные выбранной заявки.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            TicketEditWindow window = new TicketEditWindow(selectedTicket);
            window.Owner = this;

            bool? result = window.ShowDialog();

            if (result == true)
            {
                ApplyFilters();
                TicketsDataGrid.Items.Refresh();
            }
        }

        // Двойной клик по строке таблицы для быстрого редактирования (Задание 26)
        private void TicketsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TicketsDataGrid.SelectedItem != null)
            {
                EditTicketButton_Click(sender, e);
            }
        }

        private void DeleteTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsDataGrid.SelectedItem == null)
            {
                MessageBox.Show(
                    "Выберите заявку для удаления.",
                    "Удаление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить выбранную заявку?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show(
                    "На следующих занятиях здесь будет выполняться " +
                    "удаление записи из MS SQL Server.",
                    "HelpDesk",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Выйти из учётной записи?",
                "HelpDesk",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
