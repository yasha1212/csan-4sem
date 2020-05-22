using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    [Serializable]
    public class MessagePackage
    {
        public string Message { get; set; }

        public bool IsToAll { get; set; }

        public bool IsForConnection { get; set; }

        public string SenderName { get; set; }

        public int ReceiverID { get; set; }

        public MessagePackage()
        {
            IsForConnection = false;
            IsToAll = false;
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
