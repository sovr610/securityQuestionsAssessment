using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace securityQuestionPrompter.src.models
{
    public class Questions
    {
        public Questions(int id, string question)
        {
            Id = id;
            Question = question;
        }

        public int Id { get; set; }
        public string Question { get; set; }
    }
}
