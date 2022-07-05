using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using VFT.ChartSingalR.Areas.Identity.Data;
using VFT.ChartSingalR.Controllers;
using VFT.ChartSingalR.Data;
using VFT.ChartSingalR.Models;

namespace VFT.ChartSingalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly VFTChartSingalRContext _context;

        public ChatHub(VFTChartSingalRContext context)
        {
            _context = context;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendMessageToReceiver(string userRenderID, string userReceiverID, string message)
        {

            //DateTime.Now
            DateTime aDateTime = new DateTime();
            TimeSpan timeOfDay = aDateTime.TimeOfDay;
            // tạo đối tượng Message
            var message1 = new Message();
            message1.UserRenderID = userRenderID;
            message1.UserReceiverID = userReceiverID; 

            // lấy ra thông tin người gửi và người nhận
            //var userRender = _context.Users.Where(u=> u.Id == userRenderID).FirstOrDefault();
            //var userReceiver = _context.Users.Where(u => u.Id == UserReceiverID).FirstOrDefault();

            // kiểm tra người gửi và người nhận
            // => xác nhận 2 người này đã có kết nối vs nhau chưa
            var idUsers1 = _context.Messages.Where(u => u.UserRenderID == userRenderID && u.UserReceiverID == userReceiverID).FirstOrDefault();
            var idUsers2 = _context.Messages.Where(u => u.UserRenderID == userReceiverID && u.UserReceiverID == userRenderID).FirstOrDefault();
            // nếu chưa có: thêm Message mới kèm messageContext mới
            if (idUsers1 == null && idUsers2 == null)
            {
                //thêm Message mới
              int  idMS =  insertMessage(message1);
                // sau khi thêm xong lấy ra ID Message
              
                //kèm messageContext mới
                insertMessageContext(message, userRenderID, idMS, timeOfDay.ToString());
            }
            // nếu có chỉ cần thêm MessageContext mới
            else
            {
                int idMS1 = -1;
                if(idUsers1 != null)
                {
                    idMS1 = idUsers1.Id;
                }
                if(idUsers2 != null)
                {
                    idMS1 = idUsers2.Id;
                }
                //thêm messageContext mới
                insertMessageContext(message, userRenderID, idMS1, timeOfDay.ToString());
            }

            var user = new string[]{ userRenderID, userReceiverID};
            await Clients.Users(user).SendAsync("ReceiveMessageOthor", userRenderID, userReceiverID, message);
        }
        public int insertMessage(Message ms)
        {
          

            _context.Messages.Add(ms);
            _context.SaveChanges();

            var id = ms.Id;
            return id;
        }
        public void insertMessageContext(string message, string idUserRender, int idMessage, string timestamp)
        {

            var messagecontext = new MessageContext();
            messagecontext.MessageContent = message;
            messagecontext.UserId = idUserRender;
            messagecontext.Timestamp = timestamp;
            messagecontext.MessageId = idMessage;

            _context.MessageContexts.Add(messagecontext);
          _context.SaveChanges();
            
        }
    }
}
