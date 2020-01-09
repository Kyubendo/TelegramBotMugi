using System;
using Telegram.Bot;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MugiBot
{

    class Program
    {
        const string TOKEN = "820921503:AAEpGJT_t-Qn67rIMj0iK42MjSmSy8wPwSk";

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    GetUpdate().Wait();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибочка: " + e);
                }
            }
        }

        static async Task GetUpdate()
        {
            TelegramBotClient bot = new TelegramBotClient(TOKEN);
            int offset = 0;
            try
            {
                await bot.SetWebhookAsync("");

                while (true)
                {
                    var updates = await bot.GetUpdatesAsync(offset, 1);

                    foreach (var update in updates)
                    {
                        var message = update.Message;
                        if (message != null)
                        {

                            if (message.Text == "Hello!")
                            {
                                Console.WriteLine("Сообщение: " + message.Text);

                                await bot.SendTextMessageAsync(message.Chat.Id, "Hello World!");
                            }
                          

                            if (message.Text == "Муги, скинь фоточку" || message.Text == "Муги, скинь фоточку.")
                            {
                                Random rand = new Random();

                                string[] pictures = { "https://coubsecure-s.akamaihd.net/get/b173/p/coub/simple/cw_timeline_pic/708ec3a3fcf/d37c5efc2377c8301300a/big_1497962224_image.jpg",
                                    "https://i.ytimg.com/vi/-JWN1W5zSHQ/maxresdefault.jpg",
                                    "https://i.imgur.com/Lpxh2Ew.jpg",
                                    "https://static1.fjcdn.com/comments/Mugi+is+best+kon+_e62d3e424899cd5b5f93269d912cb2a8.jpg",
                                    "http://imgur.com/cdte1Sq.jpg",
                                    "https://i.ytimg.com/vi/lgSch646X6M/hqdefault.jpg",
                                    "https://i.pinimg.com/236x/a6/38/5d/a6385d1c56a793e9677d14cca594bf89--mugi-k-on-k-on-mugi.jpg?b=t"
                                    };

                                await bot.SendPhotoAsync(message.Chat.Id, pictures[rand.Next(7)]);
                            }

                            if (message.Text == "Муги, кинь кубик." || message.Text == "Муги, кинь кубик")
                            {
                                Random rand = new Random();

                                string msg = String.Format("Кинула! \nНа кубике выпало {0}.", rand.Next(20) + 1);
                                await bot.SendTextMessageAsync(message.Chat.Id, msg);
                            }

                            
                            if (message.Text != null)
                            {
                                Regex regex = new Regex(@"Муги, кинь \d+d\d+");

                                MatchCollection matches = regex.Matches(message.Text);
                                if (matches.Count > 0)
                                {
                                    Random rand = new Random();

                                    int modifier = 0;
                                    int quantity = Int32.Parse(Regex.Match(message.Text, @"\d+").Value);

                                    string sizeS = Regex.Match(message.Text, @"d(\d+)").Value;
                                    int size = Int32.Parse(Regex.Replace(sizeS, @"d", ""));

                                    Regex regexM = new Regex(@"\+(\d+)");

                                    MatchCollection matchesM = regexM.Matches(message.Text);

                                    if (matchesM.Count > 0)
                                    {
                                        string modifierS = Regex.Match(message.Text, @"\+(\d+)").Value;
                                        modifier = Int32.Parse(Regex.Replace(modifierS, @"\+", ""));
                                    }
                                                                        
                                    Console.WriteLine("Числа: " + quantity + " " + size);

                                    int number = 0;
                                    for (int i = 0; i < quantity; i++)
                                    {
                                        int rands = rand.Next(size) + 1;
                                        number += rands;
                                        //Console.WriteLine("Кубик: " + rands);
                                    }
                                    number += modifier;

                                    string msg = String.Format("Кинула! \nВыпало {0}.", number);
                                    await bot.SendTextMessageAsync(message.Chat.Id, msg);
                                }
                            }

                            if (message.Text != null)
                            {
                                Regex regex = new Regex(@"Иг.рь(\w*)");
                                MatchCollection matches = regex.Matches(message.Text);
                                if (matches.Count > 0)
                                {
                                    Console.Beep();
                                    Console.Beep();
                                    Console.Beep();
                                    Console.WriteLine("Тебя зовут: " + message.Text);
                                }
                            }

                            offset = update.Id + 1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибочка: " + e);
                //offset = update.Id + 1;
            }
        }
    }
}