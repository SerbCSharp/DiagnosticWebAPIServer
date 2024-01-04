using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace DiagnosticWebAPIServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DiagnosticController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Update update)
        {
            var message = update.Message;
            if (message != null)
            {
                var resultToHtml = await _telegramBotService.DiagnosticAsync(message);
                var client = new TelegramBotClient(_telegramConfiguration.BotToken);
                await client.SendTextMessageAsync(message.Chat.Id, resultToHtml, parseMode: ParseMode.Html, null, true);
            }
            return Ok();
        }
    }
}
