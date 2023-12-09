using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Labb3.Models
{
    public class Question
    {
        public ObjectId Id {  get; set; }
        public string Category { get; set; }
        public string Statement { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswer { get; set; }

        [JsonProperty("Image", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Image { get; set; } = "\\Images\\quiz.png";

        public Question(string category, string statement, string[] answers, int correctAnswer)
        {
            Id = ObjectId.GenerateNewId();
            Category = category;
            Statement = statement;
            CorrectAnswer = correctAnswer;
            Answers = answers;
        }
        [Newtonsoft.Json.JsonConstructor]
        public Question(string category, string statement, string[] answers, int correctAnswer, string image)
        {
            Category = category;
            Statement = statement;
            CorrectAnswer = correctAnswer;
            Answers = answers;
            Image = string.IsNullOrEmpty(image)? "\\Images\\quiz.png": image;
        }
        public Question() { }
    }
}
