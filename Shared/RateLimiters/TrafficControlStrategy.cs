

namespace Microshaoft
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    /// <summary>
    /// 流控策略
    /// </summary>
    public class TrafficControlStrategy
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 流控策略类型
        /// </summary>
        public TrafficControlStrategyType StrategyType { get; set; }

        /// <summary>
        /// 流控阈值-Int
        /// </summary>
        public long IntThreshold { get; set; }

        /// <summary>
        /// 流控阈值-Double
        /// </summary>
        public decimal DoubleThreshold { get; set; }

        /// <summary>
        /// 时间区间跨度
        /// </summary>
        public TrafficControlTimespan TimeSpan { get; set; }

        private Dictionary<string, string> flowControlConfigs;

        /// <summary>
        /// 流控维度信息
        /// </summary>
        public Dictionary<string, string> FlowControlConfigs
        {
            get
            {
                if (flowControlConfigs == null)
                {
                    flowControlConfigs = new Dictionary<string, string>();
                }

                return flowControlConfigs;
            }
            set
            {
                flowControlConfigs = value;
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Descriptions { get; set; }

        /// <summary>
        /// 触发流控后是否直接拒绝请求
        /// </summary>        
        public bool IsRefusedRequest { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastModifier { get; set; }
    }
}
