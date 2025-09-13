using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp01C
{
    public partial class MainWindow : Window
    {
        private StudentDataService dataService;
        private List<Student> allStudents;

        public MainWindow()
        {
            InitializeComponent();
            dataService = new StudentDataService();
            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                allStudents = dataService.GetAllStudents();
                dgStudents.ItemsSource = allStudents;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los estudiantes: {ex.Message}",
                               "Error",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                dgStudents.ItemsSource = allStudents;
                return;
            }

            try
            {
                List<Student> filteredStudents = dataService.SearchStudentsByName(searchTerm);
                dgStudents.ItemsSource = filteredStudents;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar estudiantes: {ex.Message}",
                               "Error",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            dgStudents.ItemsSource = allStudents;
        }

        private void TxtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Búsqueda automática mientras se escribe (opcional)
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                dgStudents.ItemsSource = allStudents;
            }
        }
    }
}