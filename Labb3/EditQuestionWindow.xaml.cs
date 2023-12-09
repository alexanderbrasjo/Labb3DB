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
    /// Interaction logic for EditQuestionWindow.xaml
    /// </summary>
    public partial class EditQuestionWindow : Window
    {
        Question questionToEdit = new Question();
        Question updatedQuestion = new Question();
        public EditQuestionWindow(Question question)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            questionToEdit = question;
            this.DataContext = questionToEdit;

            if(questionToEdit.CorrectAnswer == 0)
            {
                Checkbox1.IsChecked = true;
            }
            else if(questionToEdit.CorrectAnswer == 1)
            {
                Checkbox2.IsChecked = true;
            }
            else { Checkbox3.IsChecked = true;}
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            EditQuizWindow editQuizWindow = new EditQuizWindow();
            editQuizWindow.Show();
            this.Close();
        }

        private void btnEditQuestion_Click(object sender, RoutedEventArgs e)
        {
            int correctAnswer = -1;

            if (string.IsNullOrEmpty(CategoryName.Text) || string.IsNullOrEmpty(QuestionStatement.Text) || string.IsNullOrEmpty(Answer1.Text) || string.IsNullOrEmpty(Answer2.Text) || string.IsNullOrEmpty(Answer3.Text))
            {
                MessageBox.Show("Please input category, statement, answers and check in the correct answer!");
            }
            else
            {
                if (Checkbox1.IsChecked == true && Checkbox2.IsChecked == false && Checkbox3.IsChecked == false)
                {
                    correctAnswer = 0;
                }
                else if (Checkbox2.IsChecked == true && Checkbox1.IsChecked == false && Checkbox3.IsChecked == false)
                {
                    correctAnswer = 1;
                }
                else if (Checkbox3.IsChecked == true && Checkbox1.IsChecked == false && Checkbox2.IsChecked == false)
                {
                    correctAnswer = 2;
                }
                else
                {
                    MessageBox.Show("Check in the correct answer (You can only check in one box!)");
                    return;
                }

                updatedQuestion.Id = questionToEdit.Id;
                updatedQuestion.Category = CategoryName.Text;
                updatedQuestion.Statement = QuestionStatement.Text;
                updatedQuestion.Answers = new[]
                {
                    Answer1.Text,
                    Answer2.Text,
                    Answer3.Text
                };
                updatedQuestion.CorrectAnswer = correctAnswer;

                Game.UpdateQuestion(questionToEdit, updatedQuestion);


                CategoryName.Text = string.Empty;
                QuestionStatement.Text = string.Empty;
                Answer1.Text = string.Empty;
                Answer2.Text = string.Empty;
                Answer3.Text = string.Empty;
                Checkbox1.IsChecked = false;
                Checkbox2.IsChecked = false;
                Checkbox3.IsChecked = false;
            }
        }
    }
    
}
