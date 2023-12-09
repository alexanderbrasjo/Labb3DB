using Labb3.Data;
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
    /// Interaction logic for EditQuizWindow.xaml
    /// </summary>
    public partial class EditQuizWindow : Window
    {
         
        public Question selectedQuestion = new Question();
        public EditQuizWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ListBoxChooseQuestion.ItemsSource = Game.listOfAllQuestions;
            btnEditQuestion.IsEnabled = false;
            btnDeleteQuestion.IsEnabled = false;
        }

        

        private void ListBoxChooseQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedQuestion = ListBoxChooseQuestion.SelectedItem as Question;
            if (selectedQuestion != null )
            {
                btnEditQuestion.IsEnabled = true;
                btnDeleteQuestion.IsEnabled = true;
            }
            else
            {
                btnEditQuestion.IsEnabled = false;
                btnDeleteQuestion.IsEnabled = false;
            }
        }

        private void btnDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            Game.DeleteQuestion(selectedQuestion);
            MessageBox.Show("Deleted Question!");
            ListBoxChooseQuestion.ItemsSource = null;
            Game.LoadAllQuestions();
            ListBoxChooseQuestion.ItemsSource = Game.listOfAllQuestions;
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void btnEditQuestion_Click(object sender, RoutedEventArgs e)
        {
            selectedQuestion = (Question)ListBoxChooseQuestion.SelectedItem;
            EditQuestionWindow editQuestionWindow = new EditQuestionWindow(selectedQuestion);
            editQuestionWindow.Show();
            this.Close();
            
        }
    }
}
