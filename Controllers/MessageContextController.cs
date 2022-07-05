using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VFT.ChartSingalR.Data;
using VFT.ChartSingalR.Models;

namespace VFT.ChartSingalR.Controllers
{
    public class MessageContextController : Controller
    {
        private readonly VFTChartSingalRContext _context;

        public MessageContextController(VFTChartSingalRContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListMessageContexts(string userRenderID, string userReceiverID)
        {

            // tạo đối tượng Message
            var message1 = new Message();
            message1.UserRenderID = userRenderID;
            message1.UserReceiverID = userReceiverID;

            // lấy id người đăng nhập
            var idUsers = getIdUserLoggin();
            // kiểm tra id cuộc trò chuyện của 2 người gửi lên có trong db không
            // => xác nhận 2 người này đã có kết nối vs nhau chưa
            var idUsers1 = _context.Messages.Where(u => u.UserRenderID == idUsers && u.UserReceiverID == userReceiverID).FirstOrDefault();
            var idUsers2 = _context.Messages.Where(u => u.UserRenderID == userReceiverID && u.UserReceiverID == idUsers).FirstOrDefault();

            // nếu có trả về danh sách tin nhắn của 2 người đó
            if (idUsers1 != null || idUsers2 != null)
            {
                // lấy ra ID message
                int messageId = -1;
             

                if (idUsers1 != null)
                {
                    messageId = idUsers1.Id;
                    // lấy ra toàn bộ cuộc trò chuyện của 2 người theo thứ tự
                  
                }
                else
                {
                    messageId = idUsers2.Id;
                   
                }

                // lấy ra toàn bộ cuộc trò chuyện của 2 người theo thứ tự
                var listMessageContexts = _context.MessageContexts
                                         .Where(u => u.MessageId == messageId)
                                         .Select(x => new MessageContext()
                                         {
                                             Id = x.Id,
                                             MessageContent = x.MessageContent,
                                             UserId = x.UserId,
                                             Timestamp = x.Timestamp,
                                             MessageId = x.MessageId,

                                         }).ToList();

                // var a = listMessageContexts;
                return Json(new {data = listMessageContexts});
            }
            // nếu không return null
            return NotFound();
        }
        public string getIdUserLoggin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
   
}
