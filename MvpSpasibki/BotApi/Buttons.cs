using Telegram.Bot.Types.ReplyMarkups;

namespace MvpSpasibki.BotApi;

public static class Buttons
{
    public static ReplyKeyboardMarkup GetMainButton()
    {
        var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
                new KeyboardButton("Написать спасибку"),
            }
        })
        {
            ResizeKeyboard = true
        };
        return replyKeyboardMarkup;
    }

    // public static InlineKeyboardMarkup GetInlineKeyboardWithDays()
    // {
    //     var inlineKeyboardDays = new InlineKeyboardMarkup(new[]
    //     {
    //         new[]
    //         {
    //             InlineKeyboardButton.WithCallbackData(Today.GetDescription())
    //         },
    //         new[]
    //         {
    //             InlineKeyboardButton.WithCallbackData(Monday.GetDescription()),
    //             InlineKeyboardButton.WithCallbackData(Tuesday.GetDescription()),
    //             InlineKeyboardButton.WithCallbackData(Wednesday.GetDescription()),
    //         },
    //         new[]
    //         {
    //             InlineKeyboardButton.WithCallbackData(Thursday.GetDescription()),
    //             InlineKeyboardButton.WithCallbackData(Friday.GetDescription()),
    //         },
    //     });
    //
    //     return inlineKeyboardDays;
    // }
}