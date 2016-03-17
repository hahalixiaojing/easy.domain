using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.Base
{
    /// <summary>
    /// 可并发实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConcurrentEntityBase<T> : EntityBase<T>
    {
        /// <summary>
        /// 当前版本号
        /// </summary>
        public int Version
        {
            get
            {
                int version = this.OldVersion;
                return ++version;
            }
        }
        /// <summary>
        /// 旧版本号
        /// </summary>
        public int OldVersion
        {
            get;
            protected set;
        }

        protected override abstract BrokenRuleMessage GetBrokenRuleMessages();
        public override abstract bool Validate();
    }
}
