using BLL.Models;
using DAL.Entities;
using DAL.Enums;
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
        private readonly ConcurrentDictionary<CompanyNames, List<SocketUser>> _sockets = new ConcurrentDictionary<CompanyNames, List<SocketUser>>();

        private Timer _timer;

        public WebSocketService()
        {

        }

        public Guid AddSocket(WebSocket socket, List<CompanyNames> companyNames)
        {
            var connId = Guid.NewGuid();
            var user = new SocketUser
            {
                Id = connId,
                Socket = socket,
                Companies = companyNames
            };

            foreach (var company in companyNames)
            {
                if (_sockets.TryGetValue(company, out var list))
                {
                    list.Add(user);
                }
                else
                {
                    _sockets.TryAdd(company, new List<SocketUser> { user });
                }
            }

            return connId;
        }

        public async Task ListenToSocket(Guid connId, WebSocket socket, List<CompanyNames> companies)
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
                            await SendMessageToAllAsync(connId, assignment, assignment.CompanyName);
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

        public async Task RemoveSocketAsync(Guid id)
        {
            foreach (var companyId in _sockets.Keys.ToList())
            {
                var usersList = _sockets[companyId];
                var user = usersList.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    await user.Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by WebSocketService", CancellationToken.None);
                    user.Socket.Dispose();
                    usersList.Remove(user); // Remove from the specific company list
                }

                // If no users are left for the company, remove the company entry
                if (!_sockets[companyId].Any())
                {
                    _sockets.TryRemove(companyId, out _);
                }
            }
        }

        public async Task SendMessageToAllAsync(Guid connId, AssignmentVm assignment, CompanyNames companyName)
        {
            if (_sockets.TryGetValue(companyName, out var usersList))
            {
                foreach (var pair in usersList)
                {
                    // Don't send the message to the same user
                    if (pair.Id == connId) continue;

                    if (pair.Socket.State == WebSocketState.Open)
                    {
                        await SendAsync(pair.Socket, assignment);
                    }
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

    public class SocketUser
    {
        public Guid Id { get; set; }
        public WebSocket Socket { get; set; }
        public List<CompanyNames> Companies { get; set; } = new List<CompanyNames>();
    }
}
