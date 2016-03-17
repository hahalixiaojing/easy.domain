using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace Easy.Domain.Base
{
    [Serializable]
    public class BrokenRule
    {
       
        public BrokenRule(String name, String description)
            : this(name, description, String.Empty)
        {

        }
        public BrokenRule(String name, String description,String propertyName)
        {
            this.Name = name;
            this.Description = description;
            this.PropertyName = propertyName;
        }

        public String Name
        {
            get;
            private set;
        }

        public String Description
        {
            get;
            private set;
        }
        public String PropertyName
        {
            get;
            private set;
        }
    }
}
