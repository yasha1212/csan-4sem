using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    [Serializable]
    public class Message
    {
        public string MessageContent { get; set; }
        public List<int> AttachedFiles { get; set; }

        public Message(string content, List<int> files)
        {
            AttachedFiles = files;
            MessageContent = content;
        }
    }
}
