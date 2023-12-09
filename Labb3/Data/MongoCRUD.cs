using Labb3.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Labb3.Data
{
    class MongoCRUD
    {
        private IMongoDatabase db;
        
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public List<Question> GetAllQuestions(string table)
        {
            var collection = db.GetCollection<Question>(table);
            return collection.Find(_ => true).ToList();
        }
        public List<Quiz> GetAllDefaultQuizes(string table)
        {
            var collection = db.GetCollection<Quiz>(table);
            return collection.Find(_ => true).ToList();
        }
        public List<Quiz> GetMyQuizes(string table)
        {
            var collection = db.GetCollection<Quiz>(table);
            return collection.Find(_ => true).ToList();
        }
        public void FillDatabaseWithQuestions(string table,Question question)
        {
            var collection = db.GetCollection<Question>(table);
            collection.InsertOne(question);
        }
        public void FillDatabaseWithQuizes(string table, Quiz quiz)
        {
            var collection = db.GetCollection<Quiz>(table);
            collection.InsertOne(quiz);
        }
        public void ClearDataBase(string table)
        {
            var collection = db.GetCollection<Question>(table);
            collection.DeleteMany(_ => true);
        }
        public void DeleteQuestion(string table, Question question)
        {
            var collection = db.GetCollection<Question>(table);
            collection.DeleteOne(x => x.Id == question.Id);
        }
        public void UpdateQuestion(string table, Question question, Question updatedQuestion)
        {
            var collection = db.GetCollection<Question>(table);

            var filter = Builders<Question>.Filter.Eq(x => x.Id, question.Id);

            var result = collection.ReplaceOne(filter, updatedQuestion, new ReplaceOptions { IsUpsert = true });

            if (result.IsAcknowledged)
            {
                MessageBox.Show("Question Edited");
            }
            else
            {
                MessageBox.Show("Question failed to edit");
            }
        }
    }
}
