using BLL.Services;
using DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace InstallersApi2.Controllers
{
    [Route("api/ws")]
    [ApiController]
    

    public class WebSocketController : ControllerBase
    {
        private readonly WebSocketService _socketService;

        public WebSocketController(WebSocketService socketService)
        {
            _socketService = socketService;
        }

        [HttpGet("{companyName1}/{companyName2?}")]
        public async Task HandleWebSocket(string companyName1, string? companyName2)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var list = new List<CompanyNames>();
                if (Enum.TryParse<CompanyNames>(companyName1, true, out var companyName))
                {
                    list.Add(companyName);
                }
                if (!companyName2.IsNullOrEmpty() && Enum.TryParse<CompanyNames>(companyName2, true, out var compName))
                {
                    list.Add(compName);
                }
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                Guid socketId = _socketService.AddSocket(webSocket, list);

                await _socketService.ListenToSocket(socketId, webSocket, list);

            }
            else
            {
                HttpContext.Response.StatusCode = 400; 
            }
        }    
    }
}
