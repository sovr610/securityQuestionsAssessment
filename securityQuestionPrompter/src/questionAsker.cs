using securityQuestionPrompter.src.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace securityQuestionPrompter.src
{
    public class questionAsker
    {
        public string greet()
        {
            string name = null;
            Console.WriteLine("Hi, what is your name?");
            Console.Write("Name: ");
            return Console.ReadLine();
        }

        public string askQuestion(string question)
        {
            string answer = null;
            Console.WriteLine(question);
            Console.Write(": ");
            answer = Console.ReadLine();
            return answer;
        }

        public void response(string message)
        {
            Console.WriteLine(message);
        }

        public List<int> selectQuestion(List<Questions> questions)
        {
            Console.WriteLine("Please select three questions");
            for(int index = 0; index <= questions.Count() -1;  index++)
            {
                Console.WriteLine(questions[index].Id + " - " + questions[index].Question);
            }

            List<int> selection = new List<int>();
            for(int index = 1; index <= 3; index++)
            {
                Console.Write("Selection " + index + " : ");
                string select = Console.ReadLine();
                int numberOut = 0;
                bool canConvert = int.TryParse(select, out numberOut);
                if (canConvert)
                {
                    selection.Add(numberOut);
                    
                }
                else
                {
                    Console.WriteLine("please make sure you type in a number that is associated to a question.");
                    index--;
                }
            }

            return selection;
        }

        public string setAnswer(string question)
        {
            Console.WriteLine("Please set your answer for question:");
            Console.WriteLine(question);

            string answer = Console.ReadLine();
            return answer;
        }


    }
}
