using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// 任务实体（正在进行中）
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// 子模块类型
        /// </summary>
        public int ModuleType { get; set; }
        /// <summary>
        /// 子模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 任务id
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public string TaskState { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType { get; set; }
        /// <summary>
        /// 任务内容
        /// </summary>
        public string TaskContent { get; set; }
    }
}
