using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace EZRX_2
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string modelName = "gpt-4";

            OpenAIAPI api = new OpenAIAPI("YOUR_API_KEY");

            ChatRequest chatRequest = new ChatRequest
            {
                Model = modelName // Replace with the model version you want to use, e.g., "davinci", "curie", etc.
            };
            var chat = api.Chat.CreateConversation(chatRequest);

            /// give instruction as System
            chat.AppendSystemMessage("You have only one purpose: to provide me with C# code that will run directly within a Grasshopper C# Script Node. Do not provide code for creating Grasshopper components. I don't want any other comments. Do not say \"here is your code\" or similar remarks. Just answer with the code itself off the bait");

            while (true)
            {
                // now let's ask it a question
                string input = Console.ReadLine();
                if (input == "exit") break;
                chat.AppendUserInput(input);
                // and get the response
                string response = await chat.GetResponseFromChatbotAsync();
                SaveStringToTextFile(response);
                // store the response in the chat
                chat.AppendExampleChatbotOutput(response);
                // print the response to the screen
                Console.WriteLine(response);
            }
        }

        public static void SaveStringToTextFile(string myString)
        {
            string fileName = "lastmessage.txt";
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", fileName);
            File.WriteAllText(path, myString);
        }

        public static string ReadStringFromTextFile(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\", fileName);
            string str = File.ReadAllText(filePath);
            return str;
        }
    }
}

