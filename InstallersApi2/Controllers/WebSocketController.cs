using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task HandleWebSocket()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                string socketId = _socketService.AddSocket(webSocket);

                await _socketService.ListenToSocket(socketId, webSocket);

            }
            else
            {
                HttpContext.Response.StatusCode = 400; 
            }
        }    
    }
}
