using System;
using System.Collections.Generic;

namespace Easy.Domain.Base
{
    [Serializable]
    public abstract class BrokenRuleMessage
    {
        private Dictionary<String, String> _messages;

        protected Dictionary<String, String> Messages
        {
            get { return this._messages; }
        }

        protected BrokenRuleMessage()
        {
            this._messages = new Dictionary<String, String>();
            this.PopulateMessage();
        }

        protected abstract void PopulateMessage();

        public String GetRuleDescription(String messageKey)
        {
            String description = String.Empty;
            if (this._messages.ContainsKey(messageKey))
            {
                description = this._messages[messageKey];
            }
            return description;
        }
    }
}
