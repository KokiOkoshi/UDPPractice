using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using Prism.Commands;
using System.Net;
using System.Net.Sockets;
using System.Windows.Input;

namespace UDPPractice.ViewModels
{
    sealed class MainWindowViewModel : BindableBase
    {
        #region バッキングフィールド

        string _sendIpAddress;
        string _sendPortNo;
        string _sendData;
        string _receiveIpAddress;
        string _receivePortNo;
        string _receivedData;

        #endregion

        public string SendIpAddress
        {
            get => _sendIpAddress;
            set => SetProperty(ref _sendIpAddress, value);
        }

        public string SendPortNo
        {
            get => _sendPortNo;
            set => SetProperty(ref _sendPortNo, value);
        }

        public string SendData
        {
            get => _sendData;
            set => SetProperty(ref _sendData, value);
        }

        public string ReceiveIpAddress
        {
            get => _receiveIpAddress;
            set => SetProperty(ref _receiveIpAddress, value);
        }

        public string ReceivePortNo
        {
            get => _receivePortNo;
            set => SetProperty(ref _receivePortNo, value);
        }

        public string ReceivedData
        {
            get => _receivedData;
            set => SetProperty(ref _receivedData, value);
        }

        public ICommand SendCommand { get; }

        public ICommand StartReceiveCommand { get; }

        public MainWindowViewModel()
        {
            SendCommand = new DelegateCommand(
                () =>
                {
                    if (!IPAddress.TryParse(SendIpAddress, out var ipAddress)
                    || !uint.TryParse(SendPortNo, out var port))
                    {
                        return;
                    }

                    var sendBytes = Encoding.UTF8.GetBytes(SendData ?? string.Empty);
                    using var client = new UdpClient(SendIpAddress, (int)port);
                    client.Send(sendBytes, sendBytes.Length);
                    client.Close();
                },
                () => IPAddress.TryParse(SendIpAddress, out var tmpIp)
                        && uint.TryParse(SendPortNo, out var tmpPort))
                .ObservesProperty(() => SendIpAddress)
                .ObservesProperty(() => SendPortNo);

            StartReceiveCommand = new DelegateCommand(
                async () =>
                {
                    if (!IPAddress.TryParse(ReceiveIpAddress, out var ipAddress)
                    || !uint.TryParse(ReceivePortNo, out var port))
                    {
                        return;
                    }

                    var endPoint = new IPEndPoint(ipAddress, (int)port);
                    using var client = new UdpClient(endPoint);

                    while (true)
                    {
                        var result = await client.ReceiveAsync();
                        ReceivedData += $"{Encoding.UTF8.GetString(result.Buffer)}\r\n";
                    }
                }
                );
        }

    }
}
