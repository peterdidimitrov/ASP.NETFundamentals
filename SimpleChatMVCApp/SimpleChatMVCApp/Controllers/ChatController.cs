using Microsoft.AspNetCore.Mvc;
using SimpleChatMVCApp.Models.Message;

namespace SimpleChatMVCApp.Controllers
{
    public class ChatController : Controller
    {
        //Warning: the code holds the shared app data in a static field in the controller class.
        //This is just for the example, and it is generally a bad practice!
        //Use a database or other persistent storage to hold data, which should survive between the app requests and should be shared between all app users.
        private static List<KeyValuePair<string, string>> s_message = new List<KeyValuePair<string, string>>();
        public IActionResult Show()
        {
            if (!s_message.Any())
            {
                return View(new ChatViewModel());
            }

            var chatModel = new ChatViewModel()
            {
                Message = s_message
                .Select(m => new MessageViewModel()
                {
                    Sender = m.Key,
                    MessageText = m.Value
                })
                .ToList(),
            };
            return View(chatModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var newMessage = chat.CurrentMessage;

            s_message.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));

            return RedirectToAction("Show");
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
