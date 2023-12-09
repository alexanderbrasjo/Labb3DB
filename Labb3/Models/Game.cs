using Labb3.Data;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Labb3.Models
{
    public class Game
    {
        static MongoCRUD db = new MongoCRUD("QuizGameDB");
        public static List<Question>? listOfAllQuestions { get; set; } = new List<Question>();
        public static List<Quiz>? listOfMyQuizes { get; set; } = new List<Quiz>();
        public static List<Quiz>? listOfDefaultQuizes { get; set; } = new List<Quiz>();
        public static Quiz? activeQuiz { get; set; } = new Quiz();
        public static HashSet<string> categories { get; set; } = new HashSet<string>();
        public static List<string> selectedCategories { get; set; } = new List<string>();

        public static string filePath = Assembly.GetExecutingAssembly().Location;
        public static string projectDir = Path.GetDirectoryName(filePath);
        public static string dataDir = Path.Combine(projectDir, "Data");
        public static string imageDir = Path.Combine(projectDir, "Images");
        public static string defaultQuizesPath = Path.Combine(dataDir, "defaultQuizes.json");
        public static string localData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"QuizGame");
        public static string myQuizesFilePath = Path.Combine(localData, "myQuizes.json");



        public static List<Quiz> GetAllQuizes()
        {
            List<Quiz> copyOfList1 = listOfMyQuizes.ToList();
            List<Quiz> copyOfList2 = listOfDefaultQuizes.ToList();

            List<Quiz> combinedList = copyOfList1.Concat(copyOfList2).ToList();

            return combinedList;
        }
       
        public static bool CheckIfQuizExists(string title)
        {
            foreach(var quiz in listOfMyQuizes)
            {
                if(quiz.Title == title)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckIfQuestionExist(string statement)
        {
            foreach(var question in listOfAllQuestions)
            {
                if(question.Statement == statement)
                {
                    return true;
                }
            }
            return false;
        }
        public static async void DeleteQuiz(Quiz quiz)
        {
            listOfMyQuizes.Remove(quiz);

            string json = JsonConvert.SerializeObject(listOfMyQuizes, Newtonsoft.Json.Formatting.Indented);

            try
            {
                await File.WriteAllTextAsync(Game.myQuizesFilePath, json);
            }
            catch (IOException e)
            {
                MessageBox.Show("Quiz deleted, Could not save to file!");
            }

        }
        public static void DeleteQuestion(Question question)
        {
            db.DeleteQuestion("Questions", question);
        }
        public static void UpdateQuestion(Question question, Question updatedQuestion)
        {
            db.UpdateQuestion("Questions", question, updatedQuestion);
        }
        public static async void AddQuiz(Quiz quiz)
        {
            listOfMyQuizes.Add(quiz);

            string json = JsonConvert.SerializeObject(listOfMyQuizes, Newtonsoft.Json.Formatting.Indented);

            try
            {
                await File.WriteAllTextAsync(Game.myQuizesFilePath, json);
            }
            catch (IOException e)
            {
                MessageBox.Show("Quiz added, Could not save to file!");
            }

        }
        public async static void Init()
        {
            if (!File.Exists(localData))
            {
                Directory.CreateDirectory(localData);
            }

            if (!File.Exists(myQuizesFilePath))
            {
                await File.WriteAllTextAsync(myQuizesFilePath, "[]");
            }
        }
        public static void Load()
        {
             
            LoadAllQuestions();
            LoadCategories();
            LoadDefaultQuizes();
            LoadMyQuizes();
        }
        public static void FillDatabase()
        {

            foreach (var question in listOfAllQuestions)
            {
                question.Id = ObjectId.GenerateNewId();
                db.FillDatabaseWithQuestions("Questions", question);
            }
            foreach (var quiz in listOfDefaultQuizes)
            {
                db.FillDatabaseWithQuizes("DefaultQuizzes", quiz);
            }
            foreach (var quiz in listOfMyQuizes)
            {
                db.FillDatabaseWithQuizes("MyQuizzes", quiz);
            }
            

        }
        public static void ClearDatabase()
        {
            db.ClearDataBase("Questions");
            db.ClearDataBase("DefaultQuizzes");
            db.ClearDataBase("MyQuizzes");
        }
        public static void LoadDefaultQuizes()
        {
            listOfDefaultQuizes = db.GetAllDefaultQuizes("DefaultQuizzes");
        }
        public static void LoadMyQuizes()
        {
            listOfMyQuizes = db.GetMyQuizes("MyQuizzes");
        }
        public static async void LoadAllQuestions()
        {
            listOfAllQuestions = db.GetAllQuestions("Questions");
            //string filepath = Path.Combine(dataDir, "questions.json");

            //try
            //{
            //    if (File.Exists(filepath))
            //    {
            //        string json = await File.ReadAllTextAsync(filepath);
            //        listOfAllQuestions = JsonConvert.DeserializeObject<List<Question>>(json);
            //    }
            //    else
            //    {
            //        MessageBox.Show("File does not exist");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error occured" + ex.Message);
            //}

        }
        public static async void LoadCategories()
        {
            foreach (Question question in listOfAllQuestions)
            {
                categories.Add(question.Category);
            }


            //string filepath = Path.Combine(dataDir, "questions.json");

            //try
            //{
            //    if (File.Exists(filepath))
            //    {
            //        string json = await File.ReadAllTextAsync(filepath);
            //        List<Question> listOfAllQuestions = JsonConvert.DeserializeObject<List<Question>>(json);

            //        foreach (Question question in listOfAllQuestions)
            //        {
            //            categories.Add(question.Category);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("File does not exist");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error occured" + ex.Message);
            //}
           
        }
        //public static async void LoadDefaultQuizes()
        //{
        //    string json = await File.ReadAllTextAsync(defaultQuizesPath);
        //    listOfDefaultQuizes = JsonConvert.DeserializeObject<List<Quiz>>(json);
        //}
        //public static async void LoadMyQuizes()
        //{
        //    string json = await File.ReadAllTextAsync(myQuizesFilePath);
        //    listOfMyQuizes = JsonConvert.DeserializeObject<List<Quiz>>(json);
        //}


    }
}
