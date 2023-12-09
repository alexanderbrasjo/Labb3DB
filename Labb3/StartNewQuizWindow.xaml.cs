using Labb3.Models;
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

namespace Labb3
{
    /// <summary>
    /// Interaction logic for StartNewQuizWindow.xaml
    /// </summary>
    public partial class StartNewQuizWindow : Window
    {
        public StartNewQuizWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cbbQuizChoose.ItemsSource = Game.GetAllQuizes();
            lbxCategoryChoose.ItemsSource = Game.categories;
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
        private void cbbQuizChoose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbxCategoryChoose.SelectedItem = null;
            if (cbbQuizChoose.SelectedItem != null)
            {
                btnStartQuiz.IsEnabled = true;
            }
            
        }

        private void btnStartQuiz_Click(object sender, RoutedEventArgs e)
        {
            if(cbbQuizChoose.SelectedItem != null)
            {
                Game.activeQuiz = (Quiz)cbbQuizChoose.SelectedItem;
            }
            else
            {
                HashSet<string> chosenCategories = new HashSet<string>();

                foreach(var selectedItem in lbxCategoryChoose.SelectedItems)
                {
                    chosenCategories.Add((string)selectedItem);
                }
                Game.activeQuiz = Quiz.CreateRandomQuizByQuestionCategory(chosenCategories);
            }

            Game.activeQuiz.ShuffleQuestions();
            PlayWindow playWindow = new PlayWindow();
            playWindow.Show();
        }

        private void btnPlayRandom_Click(object sender, RoutedEventArgs e)
        {
            //Game.ClearDatabase();
            //Game.FillDatabase();
            PlayWindow playWindow = new PlayWindow();
            playWindow.Show();
        }

        private void lbxCategoryChoose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbbQuizChoose.SelectedItem = null;

            if (lbxCategoryChoose.SelectedItems.Count > 0)
            {
                btnStartQuiz.IsEnabled = true;
            }
            else
            {
                btnStartQuiz.IsEnabled = false;
            }
        }
    }
}
