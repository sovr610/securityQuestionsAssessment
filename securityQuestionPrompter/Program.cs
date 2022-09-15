using securityQuestionPrompter.src;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        prompter securityQuestions = new prompter();
        while (securityQuestions.run())
        {

        }
    }
}
