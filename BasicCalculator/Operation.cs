using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCalculator
{/// <summary>
/// Содержит информацию об одной операции калькулятора
/// </summary>
    class Operation
    {
        #region Public Properties
        /// <summary>
        /// Левая сторона операции
        /// </summary>
        public string LeftSide { get; set; }
        /// <summary>
        /// Правая сторона операции
        /// </summary>
        public string RightSide { get; set; }
        /// <summary>
        /// Тип операции ,которой будет выполнятся  
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Operation InnerOperationType { get; set; }
        #endregion
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        #region Constructor
        public Operation()
        {
            this.LeftSide = string.Empty;
            this.RightSide = string.Empty;

        }
        #endregion
    }
}
