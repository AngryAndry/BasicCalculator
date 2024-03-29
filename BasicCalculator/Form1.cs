﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCalculator
{
    /// <summary>
    /// Базовый калькулятор
    /// </summary>
    public partial class Form1 : Form
    {
        #region Constructor
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region Clearing Methods
        /// <summary>
        /// Чистит введеный пользователем текст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CEButton_Click(object sender, EventArgs e)
        {
            if (this.UserInputText.Text!=null)
            this.UserInputText.Text = string.Empty;

            FocusInputText();
        }
        private void DelButton_Click(object sender, EventArgs e)
        {

            DeleteTextValue();
            FocusInputText();
        }
        #endregion
        #region Operator Methods
        private void EqulsButton_Click(object sender, EventArgs e)
        {
            CalculateEquation();
            FocusInputText();
        }
        private void PlusButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("+");
            FocusInputText();
        }
        private void MinusButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("-");
            FocusInputText();
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("/");
            FocusInputText();
        }
        private void TimesButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("*");
            FocusInputText();
        }
        #endregion
        #region Number Funcion
        private void OneButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("1");
            FocusInputText();
        }
        private void TwoButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("2");
            FocusInputText();
        }
        private void ThreeButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("3");
            FocusInputText();
        }
        private void FourButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("4");
            FocusInputText();
        }
        private void FiveButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("5");
            FocusInputText();
        }
        private void SixButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("6");
            FocusInputText();
        }
        private void SevenButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("7");
            FocusInputText();
        }
        private void EightButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("8");
            FocusInputText();
        }
        private void NineButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("9");
            FocusInputText();
        }
        private void ZeroButton_Click(object sender, EventArgs e)
        {
            InsertTextValue("0");
            FocusInputText();
        }
        private void DotButton_Click(object sender, EventArgs e)
        {
            InsertTextValue(".");
            FocusInputText();

        }
        #endregion
        /// <summary>
        /// Calculates the given equation and outputs the answer to the user label
        /// </summary>
        private void CalculateEquation()
        {

            this.CalculationResultText.Text = ParseOperation();

            FocusInputText();
        }
        /// <summary>
        /// Передает ввод пользователя и вычисляет результат
        /// </summary>
        /// <returns></returns>
        private string ParseOperation()
        {
            try
            {
                //Получаем введенную пользователем текст
            var input = this.UserInputText.Text;
                //Удаляем все пробелы
                input = input.Replace(" ", "");
                var operation = new Operation();
                var leftSide = true;

                for (int i = 0; i < input.Length; i++)
                {

                    if ("0123456789.".Any(c => input[i] == c))
                    {
                        if (leftSide)
                        {
                            operation.LeftSide = AddNumberPart(operation.LeftSide, input[i]);
                        }
                        else
                        {
                            operation.RightSide = AddNumberPart(operation.RightSide, input[i]);

                        }
                    }
                    else if ("+-*/".Any(c => input[i] == c))
                    {
                        if (!leftSide)
                        {
                            var operatorType = GetOperationType(input[i]);
                            if (operation.RightSide.Length == 0)
                            {
                                if (operatorType != OperationType.Minus)
                                {
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an right side number ");
                                }
                                operation.RightSide += input[i];
                            }
                            else
                            {
                                operation.LeftSide = CalculateOperation(operation);
                                operation.OperationType = operatorType;
                                operation.RightSide = string.Empty;
                            }
                        }
                        else
                        {
                            var operatorType = GetOperationType(input[i]);
                            if (operation.LeftSide.Length==0)
                            {
                                if (operatorType!=OperationType.Minus)
                                {
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an left side number ");
                                }
                                operation.LeftSide += input[i];
                                }
                            else
                                {
                                operation.OperationType = operatorType;
                                leftSide = false;
                                    
                            }
                        }
                    }
                }
                return CalculateOperation(operation);
               
            }
            catch (Exception ex)
            {
                return $"Invalid equation.{ex.Message}";
                
            }
        }
        /// <summary>
        /// Считает <see cref="Operation"/>  и возвращает результат
        /// </summary>
        /// <param name="operation">операция в канукуляторе</param>
        /// <returns></returns>
        private string CalculateOperation(Operation operation)
        {
            decimal left = 0;
            decimal right = 0;
            if(string.IsNullOrEmpty(operation.LeftSide)||!decimal.TryParse(operation.LeftSide,out left))
            {
                throw new InvalidOperationException($"Left side of the operation was not a number. {operation.LeftSide}");
            }     
            if(string.IsNullOrEmpty(operation.RightSide)||!decimal.TryParse(operation.RightSide,out right))
            {
                throw new InvalidOperationException($"Left side of the operation was not a number. {operation.RightSide}");
            }
            try
            {
                switch (operation.OperationType)
                {
                    case OperationType.Add:
                        return (left + right).ToString();
                    case OperationType.Minus:
                        return (left - right).ToString();
                    case OperationType.Divide:
                        return ((decimal)left / (decimal)right).ToString();
                    case OperationType.Multiply:
                        return (left * right).ToString();
                    default:
                        throw new InvalidOperationException($"Unknown operator type when calculating operation. {operation.OperationType}");
                }
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Failed to calculate operation {operation.LeftSide} {operation.OperationType} {operation.RightSide}. {ex.Message}");
            }


        }

        /// <summary>
        /// Принимает символ и возвращает тип операции через  <see cref="OperationType"/> 
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        private OperationType GetOperationType(char character)
        {
            switch (character) {
                case '+':
                    return OperationType.Add;
                case '-':
                    return OperationType.Minus;
                case '/':
                    return OperationType.Divide;
                case '*':
                    return OperationType.Multiply;
                default:
                    throw new InvalidOperationException($"Unknow operator type {character}");


                }
        }
        private string AddNumberPart(string currentNumber, char newCharacter)
        {//Проверка на вторую точку
            if (newCharacter=='.'&& currentNumber.Contains('.'))
            {
                throw new InvalidOperationException($"Number {currentNumber} already contains a . and another cannot be aded");
            }
            return currentNumber + newCharacter;
        }

        #region Private Helpers
        /// <summary>
        /// Фокусс на введенное пользователем поле
        /// </summary>
        private void FocusInputText()
        {
            this.UserInputText.Focus();
        }
        /// <summary>
        /// Вставить значение 
        /// </summary>
        /// <param name="v"></param>
        private void InsertTextValue(string v)
        {
            var selectionStart = this.UserInputText.SelectionStart;
            this.UserInputText.Text = this.UserInputText.Text.Insert(this.UserInputText.SelectionStart, v);
            this.UserInputText.SelectionStart = selectionStart+v.Length;
            this.UserInputText.SelectionLength = 0;
        }
        /// <summary>
        /// Удалить значение
        /// </summary>
        private void DeleteTextValue()
        {
            if (this.UserInputText.Text.Length < this.UserInputText.SelectionStart+1 )
                return;
            var selectionStart = this.UserInputText.SelectionStart;
            this.UserInputText.Text = this.UserInputText.Text.Remove(this.UserInputText.SelectionStart, 1);
            this.UserInputText.SelectionStart = selectionStart;
            this.UserInputText.SelectionLength = 0;

        }
        #endregion

    }
}
