using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MvpSpasibki.BotApi;

public class BotClient
{
    private static readonly ITelegramBotClient Bot =
        new TelegramBotClient("5628215183:AAFeTuAoxldhloxF8yFzgUaC3GK04w28hOk");

    public BotClient()
    {
    }

    public static void Run()
    {
        Console.WriteLine("Запущен бот " + Bot.GetMeAsync().Result.FirstName);

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        Bot.StartReceiving(
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
                    return;

                switch (message.Text.ToLower())
                {
                    case "/start":
                        var testChatId = new ChatId(-4028109595); //4056715262
                        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(testChatId));


                        await botClient.SendTextMessageAsync(testChatId,
                            "Привет! Этот бот демонстрирует функционал новой фичи в фиитоботе",
                            cancellationToken: cancellationToken,
                            replyMarkup: Buttons.GetMainButton());
                        return;

                    default:
                        await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!",
                            cancellationToken: cancellationToken);
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