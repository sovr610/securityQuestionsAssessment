using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace securityQuestionPrompter.src
{
    public class prompter
    {
        private questionAsker asker;
        private Database database;
        public prompter()
        {
            asker = new questionAsker();
            database = new Database();
            database.loadChanges();
            if (database.getQuestions().Count() == 0)
            {
                database.initializeQuestions();
                database.saveChanges();
            }
            
        }

        public bool run()
        {
            string name = asker.greet();
            if (database.hasUsername(name))
            {
                int nameId = database.getNameID(name);

                var questions = database.getQuestionsBasedOnUser(nameId);
                asker.response("Please answer the following questions:");
                bool answered = false;
                foreach (var question in questions)
                {
                    string result = asker.askQuestion(question.Question);
                    var ans = database.getAnswerOnUserAndQuestionID(nameId, question.Id);
                    if (ans.answer.Trim() == result.Trim())
                    {
                        answered = true;
                    }

                }

                if (answered)
                {
                    asker.response("congradulations on answering the questions  correctly!");
                }
                else
                {
                    asker.response("You did not answer all the questions correctly");
                }
                return true;

            }
            else
            {
                database.addUser(name);
                int id = database.getNameID(name);
                var questions = database.getQuestions();
                var questionIds = asker.selectQuestion(questions);

                foreach (var questionId in questionIds)
                {
                    var selectQuestion = from q in questions
                                         where q.Id == questionId
                                         select q;
                    var str = asker.setAnswer(selectQuestion.ToList()[0].Question);
                    database.addAnswer(id, questionId, str);
                }

                Console.Write("Do you want to  save your answers to your security questions? (y/n): ");
                string check = Console.ReadLine();
                if (check.Trim().ToLower() == "y")
                {
                    database.saveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            
        }
    }
}
