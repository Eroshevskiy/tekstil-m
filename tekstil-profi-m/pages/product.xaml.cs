﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using tekstil_profi_m.models;

namespace tekstil_profi_m.pages
{
    /// <summary>
    /// Логика взаимодействия для product.xaml
    /// </summary>
    public partial class product : Window
    {
        

        private ObservableCollection<Merch> merchCollection;
        public product()
        {
            InitializeComponent();

            merchCollection = new ObservableCollection<Merch>(dipEntitie.GetContext().Merch.ToList());
            BDWorkers.ItemsSource = merchCollection;
        }

        private void EticPech(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Получаем выбранный объект из DataGrid
                var selectedMerch = BDWorkers.SelectedItem as dynamic;
                if (selectedMerch != null)
                {
                    // Получаем путь к файлу из выбранного объекта
                    string filePath = selectedMerch.filePath;

                    // Проверяем, существует ли файл по указанному пути
                    if (File.Exists(filePath))
                    {
                        // Открываем файл
                        Process.Start(filePath);
                    }
                    else
                    {
                        MessageBox.Show("Файл не найден.");
                    }
                }
            }
        }



        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditMerch merch = new EditMerch((sender as Button).DataContext as Merch);
            Visibility = Visibility.Hidden;
            merch.Show();

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var merchDell = BDWorkers.SelectedItems.Cast<Merch>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {merchDell.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    IEnumerable<Merch> enumerable = dipEntitie.GetContext().Merch.RemoveRange((IEnumerable<Merch>)merchDell);
                    dipEntitie.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    BDWorkers.ItemsSource = dipEntitie.GetContext().Merch.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void nazClick(object sender, RoutedEventArgs e)
        {
            admin adm = new admin();
            Visibility = Visibility.Hidden;
            adm.Show();
        }
    }
}
