using Microsoft.AspNetCore.SignalR.Client;
using RealTimeChat.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.Shared
{
    public class SupplierChatClient : IAsyncDisposable
    {
        public const string HUBURL = "/SupplierChatHub";
        private readonly string _hubUrl;

        private HubConnection _hubConnection;

        public SupplierChatClient(string userName, string siteUrl)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }
            _hubUrl = siteUrl.TrimEnd('/') + HUBURL;
        }

        private readonly string _username;
        private bool _started = false;

        public async Task StartAsync()
        {
            if (!_started)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_hubUrl)
                    .Build();

                _hubConnection.On<string, Message>("ReceiveMessage", (user, message) =>
                {
                    HandleReceiveMessage(user, message);
                });
                await _hubConnection.StartAsync();
                _started = true;
                await _hubConnection.SendAsync("Register", _username);
            }
        }

        public event MessageReceivedEventHandler MessageReceived;
        private void HandleReceiveMessage(string username, Message message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, message));
        }  

        public async Task SendAsync(Message message)
        {
            if (!_started)
            {
                throw new InvalidOperationException("Chat client not started");
            }
            await _hubConnection.SendAsync("SendMessage", _username, message);
        }

        public async Task StopAsync()
        {
            if (_started)
            {
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _started = false;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }
    }

    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

    public class MessageReceivedEventArgs : EventArgs
    {
        public string Username { get; set; }
        public Message Message { get; set; }
        public MessageReceivedEventArgs(string username, Message message)
        {
            Username = username;
            Message = message;
        }
    }
}
