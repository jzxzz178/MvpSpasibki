using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MvpSpasibki.BotApi;

public class BotClient
{
    private const long BinaryTeamChat = -962985837;
    private const long Fiit2021Chat = -1560514257;
    private const long TestChat = -4028109595;
    private const long TestChat2 = -4018048128;
    private const long CurrGroup = TestChat;

    private readonly ITelegramBotClient bot;

    // <id, <название этапа (from, to, text), ответ на этапе>>
    private static readonly Dictionary<long, Dictionary<string, string>> Answers = new();


    public BotClient(string token)
    {
        bot = new TelegramBotClient(token);
    }

    public void Run()
    {
        Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        // Некоторые действия
        // Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

        switch (update.Type)
        {
            case UpdateType.Message:

                var message = update.Message;

                if (message?.Text == null)
                    break;

                if (message.Chat.Id == CurrGroup) // CHAT ID
                {
                    break;
                }

                try
                {
                    var user = await botClient.GetChatMemberAsync(CurrGroup, message.From!.Id,
                        cancellationToken: cancellationToken);

                    if (user.Status is ChatMemberStatus.Left or ChatMemberStatus.Kicked)
                    {
                        await botClient.SendTextMessageAsync(message.Chat,
                            "Вас нет в нужном чате!",
                            cancellationToken: cancellationToken);
                        
                        break;
                    }

                    // if (user.Status is ChatMemberStatus.Administrator or ChatMemberStatus.Creator)
                    // {
                    //     await botClient.SendTextMessageAsync(message.Chat,
                    //         "Вы админ!",
                    //         cancellationToken: cancellationToken,
                    //         replyMarkup: Buttons.GetMainButton());
                    // }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                switch (message.Text.ToLower())
                {
                    case "/start":
                        // var testChatId = new ChatId(TestChatId);
                        // Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(message));

                        await botClient.SendTextMessageAsync(message.Chat,
                            "Привет! Этот бот демонстрирует функционал новой фичи в фиитоботе",
                            cancellationToken: cancellationToken,
                            replyMarkup: Buttons.GetMainButton());
                        break;

                    case "изменить спасибку":
                        Answers[message.Chat.Id] = new Dictionary<string, string> { { "from", "" } };

                        await botClient.SendTextMessageAsync(message.Chat,
                            "Напишите свои имя, фамилию и тег в телеграме (по желанию)",
                            cancellationToken: cancellationToken,
                            replyMarkup: null);
                        break;

                    case "написать спасибку":
                        Answers[message.Chat.Id] = new Dictionary<string, string> { { "from", "" } };

                        await botClient.SendTextMessageAsync(message.Chat,
                            "Напишите свои имя и фамилию",
                            cancellationToken: cancellationToken,
                            replyMarkup: null);
                        break;

                    case "отправить спасибку":

                        if (!Answers.ContainsKey(message.Chat.Id))
                        {
                            break;
                        }

                        var spasibka = Answers[message.Chat.Id];
                        if (spasibka.Keys.Count != 3)
                        {
                            break;
                        }

                        Writer.WriteSpasibka(spasibka["from"], spasibka["to"], spasibka["text"]);

                        await botClient.SendTextMessageAsync(new ChatId(CurrGroup), // CHAT ID
                            "Нам прилетела новая спасибка!\n\n" +
                            $"От: {spasibka["from"]} (@{message.From?.Username})\n\n" +
                            $"Кому: {spasibka["to"]}\n\n" +
                            $"{spasibka["text"]}",
                            cancellationToken: cancellationToken,
                            replyMarkup: null
                        );

                        await botClient.SendTextMessageAsync(message.Chat,
                            "Спасибка отправлена!",
                            cancellationToken: cancellationToken,
                            replyMarkup: Buttons.GetMainButton());

                        break;


                    default:
                        if (Answers.TryGetValue(message.Chat.Id, out var stage))
                        {
                            switch (stage.Keys.Count)
                            {
                                case 0:
                                    await botClient.SendTextMessageAsync(message.Chat,
                                        "Нажмите на кнопу, чтобы попробовать новую фичу",
                                        cancellationToken: cancellationToken,
                                        replyMarkup: Buttons.GetMainButton());
                                    break;

                                case 1:
                                    stage["from"] = message.Text;
                                    stage.Add("to", "");
                                    await botClient.SendTextMessageAsync(message.Chat,
                                        "Напишите имя и фамилию человека, которого хотите отблагодарить",
                                        cancellationToken: cancellationToken,
                                        replyMarkup: null);
                                    break;

                                case 2:
                                    stage["to"] = message.Text;
                                    stage.Add("text", "");
                                    await botClient.SendTextMessageAsync(message.Chat,
                                        "Напишите, за что хотите отблагодарить этого человека",
                                        cancellationToken: cancellationToken,
                                        replyMarkup: null);
                                    break;

                                case 3:
                                    if (stage["text"].Length == 0)
                                    {
                                        stage["text"] = message.Text;

                                        await botClient.SendTextMessageAsync(message.Chat,
                                            $"Вот, что у нас получилось:\n\n" +
                                            $"От: {stage["from"]} (@{message.From?.Username})\n\n" +
                                            $"Кому: {stage["to"]}\n\n" +
                                            $"{stage["text"]}",
                                            cancellationToken: cancellationToken,
                                            replyMarkup: Buttons.GetEditButton());
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(message.Chat,
                                "Нажми на кнопу, чтобы попробовать новую фичу",
                                cancellationToken: cancellationToken,
                                replyMarkup: Buttons.GetMainButton());
                        }

                        break;
                }

                break;
        }
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        return Task.CompletedTask;
    }
}