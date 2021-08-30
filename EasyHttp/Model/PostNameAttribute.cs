using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHttp.Model
{
    /// <summary>
    /// post名称特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PostNameAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string PostName { get; set; }
    }
}
