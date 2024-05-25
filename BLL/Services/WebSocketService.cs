using BLL.Models;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Threading.Timer;

namespace BLL.Services
{
    public class WebSocketService
    {
        private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        private Timer _timer;

        public WebSocketService()
        {

        }

        public string AddSocket(WebSocket socket)
        {
            var connId = Guid.NewGuid().ToString();
            _sockets.TryAdd(connId, socket);
            return connId;
        }

        public async Task ListenToSocket(string connId, WebSocket socket)
        {
            var buffer = new byte[1024 * 4];
            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        if (string.IsNullOrEmpty(message) || message.Equals("null"))
                        {
                            await SendAsync(socket, null); // send pong
                        }
                        else
                        {
                            var assignment = JsonConvert.DeserializeObject<AssignmentVm>(message);
                            await SendMessageToAllAsync(connId,assignment);
                        }
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await RemoveSocketAsync(connId);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (consider using a logging library)
                Console.WriteLine($"Error in ListenToSocket: {ex.Message}");
            }
        }

        public async Task RemoveSocketAsync(string id)
        {
            if (_sockets.TryRemove(id, out WebSocket socket))
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by WebSocketService", CancellationToken.None);
                socket.Dispose();
            }
        }

        public async Task SendMessageToAllAsync(string connId, AssignmentVm assignment)
        {
            foreach (var pair in _sockets)
            {
                if (pair.Key == connId) continue;

                if (pair.Value.State == WebSocketState.Open)
                {
                    await SendAsync(pair.Value, assignment);
                }
            }
        }
        public async Task SendAsync(WebSocket socket, AssignmentVm? assignment)
        {
            string message = JsonConvert.SerializeObject(assignment);
            var buffer = Encoding.UTF8.GetBytes(message ?? "Pong");
            await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }

    }

    public class SocketMessage
    {
        public string Type { get; set; }
        public AssignmentVm Payload { get; set; }
    }
}
