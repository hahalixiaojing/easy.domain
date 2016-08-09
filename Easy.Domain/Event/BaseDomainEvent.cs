using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Event
{
    public abstract class BaseDomainEvent : IDomainEvent
    {
        public BaseDomainEvent()
        {
            this.OccurredOn = DateTime.Now;
            this.Version = 0;
        }
        /// <summary>
        /// 事件版本
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 事件创建时间
        /// </summary>
        public DateTime OccurredOn
        {
            get;
            set;
        }
    }
}
