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
    /// Interaction logic for CreateNewQuizWindow.xaml
    /// </summary>
    public partial class CreateNewQuizWindow : Window
    {
        
        List<Question> questions = new List<Question>();
        public CreateNewQuizWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnCreateQuestion_Click(object sender, RoutedEventArgs e)
        {
            
            int correctAnswer = -1;

            if (string.IsNullOrEmpty(QuizName.Text))
            {
                MessageBox.Show("Please input a Quizname!");
                return;
            }
            else if(Game.CheckIfQuizExists(QuizName.Text))
            {
                MessageBox.Show("That Quiz name is already used!, please choose another title");
                return;
            }
            
            

            if(string.IsNullOrEmpty(CategoryName.Text) || string.IsNullOrEmpty(QuestionStatement.Text) || string.IsNullOrEmpty(Answer1.Text) || string.IsNullOrEmpty(Answer2.Text) || string.IsNullOrEmpty(Answer3.Text))
            {
                MessageBox.Show("Please input category, statement, answers and check in the correct answer!");
            }
            else
            {
                if(Checkbox1.IsChecked == true && Checkbox2.IsChecked == false && Checkbox3.IsChecked == false)
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

                questions.Add( new Question(CategoryName.Text, QuestionStatement.Text, new string[3] { Answer1.Text, Answer2.Text, Answer3.Text }, correctAnswer));
               
                MessageBox.Show("Question was created!");

                TextBlockQuizName.Text = "QUIZ NAME:";
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

        private void btnCreateQuiz_Click(object sender, RoutedEventArgs e)
        {
            Quiz quiz = new Quiz(QuizName.Text, CategoryName.Text);
            quiz.Questions = questions;

            if (quiz.Questions.Count > 0)
            {
                Game.AddQuiz(quiz);
                MessageBox.Show($"Quiz {quiz.Title} Created!");
                QuizName.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Questions needed, not saved!");
            }
            
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
