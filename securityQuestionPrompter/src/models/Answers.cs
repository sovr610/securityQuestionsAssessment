using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace securityQuestionPrompter.src.models
{
    public class Answers
    {
        public Answers(int id, int questionID, int userId, string answer)
        {
            Id = id;
            QuestionID = questionID;
            this.userId = userId;
            this.answer = answer;
        }

        public int Id { get; set; }
        public int QuestionID { get; set; }
        public int userId { get; set; }
        public string answer { get; set; }
    }
}
