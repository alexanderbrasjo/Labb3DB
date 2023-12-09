using Labb3.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace Labb3
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {
        public static Question? currentQuestion;
        public int correctAnswers { get; set; } = 0;
        public int questionIndex { get; set; } = 0;
        public string quizStatus { get; set; } = string.Empty;
        public double percentage { get; set; } = 100;

        
        
        public PlayWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if(Game.activeQuiz.Questions.Count == 0)
            {
                Game.activeQuiz = Quiz.CreateRandomQuiz();
                currentQuestion = Game.activeQuiz.GetQuestion(questionIndex);
            }
            else
            {
                currentQuestion = Game.activeQuiz.GetQuestion(questionIndex);
            }

            ProgressBar.Maximum = Game.activeQuiz.Questions.Count;
            this.DataContext = currentQuestion;
            CorrectQuestions.Text = QuizStatus();
            PercentageQuestions.Text = QuizPercentage();
        }

        private void btnStopPlaying_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            int selectedAnswerIndex = int.Parse(button.Tag.ToString());

            if(questionIndex == 10)
            {
                MessageBox.Show("You have already answered all questions! Good Job");
                return;
            }

            if (currentQuestion.CorrectAnswer == selectedAnswerIndex)
            {
                correctAnswers++;
            }
           
            questionIndex++;

            if(Game.activeQuiz.GetQuestion(questionIndex) != null)
            {
                currentQuestion = Game.activeQuiz.GetQuestion(questionIndex);
            }
            else
            {
                QuestionStatement.Text = "YOU ANSWERED ALL QUESTIONS!";
                btnAnswer1.Visibility = Visibility.Hidden;
                btnAnswer1.Content = "";
                btnAnswer2.Visibility = Visibility.Hidden;
                btnAnswer2.Content = "";
                btnAnswer3.Visibility = Visibility.Hidden;
                btnAnswer3.Content = "";
            }
            ProgressBar.Value = questionIndex;
            this.DataContext = currentQuestion;
            CorrectQuestions.Text = QuizStatus();
            PercentageQuestions.Text = QuizPercentage();
        }

        private string? GetQuestionImagePath()
        {
            if (currentQuestion != null && currentQuestion.Image != null)
            {
                return System.IO.Path.Combine(Game.imageDir, currentQuestion.Image);
                
            }
            return null;
        }
        private string QuizStatus()
        {
            return $"Correct answers: {correctAnswers} / {questionIndex}";
        }
        private string QuizPercentage()
        {
            return $"Percentage: {Convert.ToInt32(CountPercentage())} %";
        }
        private double CountPercentage()
        {
            if(questionIndex != 0)
            {
                return Convert.ToDouble(correctAnswers) / questionIndex * 100;
            }
            else
            {
                return 0;
            }
        }
    }
}
