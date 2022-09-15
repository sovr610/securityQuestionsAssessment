using Newtonsoft.Json;
using securityQuestionPrompter.src.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace securityQuestionPrompter.src
{
    public class Database
    {
        private List<User> users = new List<User>();
        private List<Questions> questions = new List<Questions>();
        private List<Answers> answers = new List<Answers>();

        

        private string directory = Environment.CurrentDirectory + "\\Database";
        public Database()
        {

            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                File.WriteAllText(directory + "\\users.json", "");
                File.WriteAllText(directory + "\\answers.json", "");
                File.WriteAllText(directory + "\\questions.json", "");
            }
        }

        /// <summary>
        /// initialize questions for the user to select to add answers too.
        /// </summary>
        public void initializeQuestions()
        {
            this.addQuestion("In what city were you born?");
            this.addQuestion("What is the name of your favorite pet?");
            this.addQuestion("What is your mother's maiden name?");
            this.addQuestion("What high school did you attend?");
            this.addQuestion("What was the mascot of your high school?");
            this.addQuestion("What was the make of your first car?");
            this.addQuestion("What was your favorite toy as a child?");
            this.addQuestion("Where did you meet your spouse?");
            this.addQuestion("What is your favorite meal?");
            this.addQuestion("Who is your favorite actor / actress?");


            
        }

        /// <summary>
        /// Add a new user to the database
        /// </summary>
        /// <param name="name">users full name</param>
        public void addUser(string name)
        {
            int id = users.Count();
            users.Add(new User(id, name));

        }

        /// <summary>
        /// get all users
        /// </summary>
        /// <returns><List of users/returns>
        public List<User> getUsers()
        {
            return users;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<User> getUsersById(int id)
        {
            var filteredUsers = from ans in users
                                  where ans.Id == id
                                  select ans;
            if (filteredUsers.Any())
            {
                return filteredUsers.ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get a specific answer of a security question associated to a user
        /// </summary>
        /// <param name="userId">int user id</param>
        /// <param name="QuestId">int question id</param>
        /// <returns>Answer object</returns>
        public Answers getAnswerOnUserAndQuestionID(int userId, int QuestId)
        {
            var answer = from ans in answers
                         where ans.QuestionID == QuestId && ans.userId == userId
                         select ans;
            if (answer.Any())
            {
                return answer.ToList()[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get a list of questions associated to a user.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>List of Questions</returns>
        public List<Questions> getQuestionsBasedOnUser(int userId)
        {
            var questionstest = questions;
            var filteredQuestions = from ans in questions
                                 join jin in answers
                                 on ans.Id equals jin.QuestionID
                                 where jin.userId == userId
                                 select ans;

            if (filteredQuestions.Any())
            {
                return filteredQuestions.ToList();
            }
            else
            {
                return null;
            }
        } 

        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns>List of questions</returns>
        public List<Questions> getQuestions()
        {
            return questions;
        }

        /// <summary>
        /// Get answers associated to a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of  answers</returns>
        public List<Answers> GetAnswers(int userId)
        {
            var filteredAnswers = from ans in answers
                                  where ans.userId == userId
                                  select ans;
            if(filteredAnswers.Any())
            {
                return filteredAnswers.ToList();
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Checks to see if the user is in the database or not.
        /// </summary>
        /// <param name="name">Users full nname</param>
        /// <returns>boolean if  the user exists or not</returns>
        public bool hasUsername(string name)
        {
            if (users.Count() > 0)
            {
                var nameCheck = from nme in users
                                where nme.Name == name
                                select nme;
                if (nameCheck.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get  the user ID based on their  full name
        /// </summary>
        /// <param name="name">Full name of user</param>
        /// <returns>user ID</returns>
        public int getNameID(string name)
        {
            var id = from itm in users
                     where itm.Name == name
                     select itm;
            if(id.Any())
            {
                return id.ToList()[0].Id;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// checks to see if user has any answers
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>booleann</returns>
        public bool userHasAnswers(int id)
        {
            var answerCheck = from ans in answers
                              where ans.userId == id
                              select ans;
            if (answerCheck.Any())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Add an answer to a question that a specific user entered in, to the database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="questionId"></param>
        /// <param name="answer">string, the answer text</param>
        /// <returns></returns>
        public bool addAnswer(int userId, int questionId, string  answer)
        {
            var duplicateCheck = from ind in answers
                                 where ind.userId == userId && ind.answer == answer
                                 select ind;

            if (!duplicateCheck.Any())
            {
                int id = answers.Count();
                answers.Add(new Answers(id, questionId, userId, answer));
                return true;
            } 
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Add a question in the database.
        /// </summary>
        /// <param name="question">question text</param>
        /// <returns></returns>
        public bool addQuestion(string question)
        {
            var duplicateQuestion = from ind in questions
                                    where ind.Question == question
                                    select ind;
            if(!duplicateQuestion.Any())
            {
                int id = questions.Count();
                questions.Add(new Questions(id, question));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// load the data in the lists for users, questions and answers.
        /// </summary>
        /// <returns>boolean, to tell feedback on loading the data</returns>
        public bool loadChanges()
        {
            try
            {
                var answersTmp = JsonConvert.DeserializeObject<List<Answers>>(File.ReadAllText(directory + "\\answers.json"));
                var usersTmp = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(directory + "\\users.json"));
                var questionsTmp = JsonConvert.DeserializeObject<List<Questions>>(File.ReadAllText(directory + "\\questions.json"));
                
                if(answersTmp != null)
                {
                    answers = answersTmp;
                }

                if (usersTmp != null)
                {
                    users = usersTmp;
                }

                if (answersTmp != null)
                {
                    questions = questionsTmp;
                }

                return true;
            }
            catch(Exception i)
            {
                return false;
            }
        }

        public bool saveChanges()
        {
            try
            {

                if(File.Exists(directory + "\\users.json"))
                {
                    File.Delete(directory + "\\users.json");
                }

                File.WriteAllText(directory + "\\users.json", JsonConvert.SerializeObject(users));

                if(File.Exists(directory + "\\answers.json"))
                {
                    File.Delete(directory + "\\answers.json");
                }

                File.WriteAllText(directory + "\\answers.json", JsonConvert.SerializeObject(answers));

                if (File.Exists(directory + "\\questions.json"))
                {
                    File.Delete(directory + "\\questions.json");
                }

                File.WriteAllText(directory + "\\questions.json", JsonConvert.SerializeObject(questions));

                return true;
            }
            catch(Exception i)
            {
                return false;
            }
        }
    }
}
