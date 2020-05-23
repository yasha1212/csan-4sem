﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    [Serializable]
    public class MessagePackage
    {
        public string Message { get; set; }

        public bool IsForAll { get; set; }

        public bool IsForConnection { get; set; }

        public bool IsForDisconnection { get; set; }

        public string SenderName { get; set; }

        public int ReceiverID { get; set; }

        public MessagePackage()
        {
            IsForConnection = false;
            IsForAll = false;
            IsForDisconnection = false;
            Message = "";
            SenderName = "";
            ReceiverID = 0;
        }

        public MessagePackage(string senderName) : base()
        {
            SenderName = senderName;
        }

        public MessagePackage(string message, string senderName) : base()
        {
            Message = message;
            SenderName = senderName;
        }

        public MessagePackage(string message, string senderName, int receiverID) : base()
        {
            Message = message;
            SenderName = senderName;
            ReceiverID = receiverID;
        }
    }
}
