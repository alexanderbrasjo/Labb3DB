using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Labb3.Models
{
    public class Quiz
    {
        public ObjectId Id { get; set; }
        public List<Question>? Questions { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;

        
        public Quiz(string title, string category)
        {
            Title = title;
            Category = category;
            Questions = new List<Question>();
        }
        [Newtonsoft.Json.JsonConstructor]
        public Quiz(string title, string category, List<Question> questions)
        {
            Title = title;
            Category = category;
            Questions = questions;
        }
        public Quiz()
        {
            Questions = new List<Question>();
        }
        public Question? GetQuestion(int questionIndex)
        {
            if(questionIndex < 0)
            {
                throw new Exception("You suck!");
            }
            if(questionIndex < Questions.Count)
            {
                return Questions[questionIndex];
            }
            else
            {
                return null;
            }
        }
        public Question? GetRandomQuestion()
        {
            List<Question> availableQuestions = Game.listOfAllQuestions.ToList();
            Question randomQuestion;

            if (availableQuestions.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, availableQuestions.Count);
                randomQuestion = availableQuestions[randomIndex];
                return randomQuestion;
            }
            else
            {
                return null;
            }
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public void RemoveQuestion(Question question)
        {
            if(Questions.Count > 0)
            {
                Questions.Remove(question);
            }
            else
            {
                MessageBox.Show("There is no questions to remove");
            }
        }
        public static List<Question> GetRandomQuestions(int number)
        {
            List<Question> availableQuestions = Game.listOfAllQuestions.ToList();
            List<Question> randomQuestions = new List<Question>();

            Random rand = new Random();
            for (int i = 0; i < number; i++)
            {
                if(availableQuestions.Count >= 1)
                {
                    int randomIndex;
                    randomIndex = rand.Next(0, availableQuestions.Count);
                    randomQuestions.Add(availableQuestions[randomIndex]);
                    availableQuestions.RemoveAt(randomIndex);
                }
                else
                {
                    MessageBox.Show("Not enough questions avaialable, using: " + randomQuestions.Count + " Questions");
                    break;
                }
            }
            return randomQuestions;
        }
        
        public static Quiz CreateRandomQuizByQuestionCategory(HashSet<string> categories)
        {
            List<Question> questions = GetRandomQuestionsByCategories(categories, 10);
            return new Quiz("random", "random", questions);
            
        }
        public static List<Question> GetRandomQuestionsByCategories(HashSet<string> categories, int number)
        {

            List<Question> availableQuestions = Game.listOfAllQuestions.Where(question => categories.Contains(question.Category)).ToList();
            List<Question> randomQuestions = new List<Question>();
            

            Random rand = new Random();
            for (int i = 0; i < number; i++)
            {
                if (availableQuestions.Count >= 1)
                {
                    int randomIndex;
                    randomIndex = rand.Next(0, availableQuestions.Count);
                    randomQuestions.Add(availableQuestions[randomIndex]);
                    availableQuestions.RemoveAt(randomIndex);
                }
                else
                {
                    MessageBox.Show("Not enough questions avaialable, using: " + randomQuestions.Count + " Questions");
                    break;
                }
            }
            return randomQuestions;
        }
        public static Quiz CreateRandomQuiz()
        {
            List<Question> questions = GetRandomQuestions(10);

            Quiz tempQuiz = new Quiz("Random","Random",questions);

            return tempQuiz;
        }
        public void ShuffleQuestions()
        {
            Random random = new Random();

            List<Question> shuffledQuestions = new List<Question>();
            List<Question> questions = this.Questions.ToList();

            for(int i = 0; i < this.Questions.Count; i++)
            {
                int rand = random.Next(0, questions.Count);
                shuffledQuestions.Add(questions[rand]);
                questions.RemoveAt(rand);
            }

            this.Questions = shuffledQuestions;
        }

    }
}
