using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorProject
{
    public partial class Form1 : Form
    {
        private double storedNum = 0;
        private string storedOp;
        private bool displayingResult = false;
        private bool DisplayBinary = false;
        public Form1()
        {
            InitializeComponent();
        }
        private string ConvertToBinary(double num)
        {
            string returnString = "";

            int integer = (int)num;
            decimal decim = (decimal)(num - integer);
            if(decim != 0)
            {
                returnString += ".";
                for(int i = 1; i <= 5; i++)
                {
                    decimal presentDec = decim * 2;

                    if(presentDec >= 1)
                    {
                        returnString += "1";
                        decim = presentDec - 1;
                    } else
                    {
                        returnString += "0";
                        decim = presentDec;
                    }
                }
            }
            while (integer > 0)
            {
                returnString = (integer % 2).ToString() + returnString;

                integer = (int)Math.Floor((decimal)(integer / 2));
            }                   

            return returnString;
        }
        private double ConvertToDouble(string BinaryNum)
        {
            double returnNum = 0;
            if(BinaryNum.Contains("."))
            {
                string[] BinaryParts = BinaryNum.Split('.');
                string wholeNumBinary = BinaryParts[0];
                string decim = BinaryParts[1];

                for(int e = 0; e < BinaryParts.Length; e++)
                {
                    returnNum += (Math.Pow(2, e) * (int)wholeNumBinary[wholeNumBinary.Length - e - 1]);
                }
                for(int e = 1; e < decim.Length; e++)
                {
                    returnNum += (Math.Pow(2, (-e)) * (int)decim[e]);
                }
            } else
            {
                for (int e = 0; e < BinaryNum.Length; e++)
                {
                    returnNum += (Math.Pow(2, e) * int.Parse(BinaryNum[BinaryNum.Length - e - 1].ToString()));
                }
            }

            return returnNum;
        }
        private void DisplayNum()
        {
            if(!DisplayBinary)
            {
                textBox1.Text = storedNum.ToString();
                displayingResult = true;
            } else
            {
                textBox1.Text = ConvertToBinary(storedNum);
                displayingResult = true;
            }
        }
        private double calculateStoredNum(double storedNum, double newNum)
        {
            if(storedOp == null)
            {
                return newNum;
            }
            var bin = ConvertToBinary(storedNum);
            ConvertToDouble(bin);
            if (storedOp == "+")
            {
                return storedNum + newNum;
            }
            else if (storedOp == "-")
            {
                return storedNum - newNum;
            }
            else if (storedOp == "*")
            {
                return storedNum * newNum;
            }
            else if (storedOp == "/")
            {
                return storedNum / newNum;
            }
            else if (storedOp == "^")
            {
                return Math.Pow(storedNum, newNum);
            }
            else if (storedOp == "And")
            {
                return ConvertToDouble(CalculateAndOr(storedNum, newNum, "And"));
            }
            else if (storedOp == "Or")
            {
                return ConvertToDouble(CalculateAndOr(storedNum, newNum, "Or"));
            }
            else
            {
                return newNum;
            }
        }

        private string CalculateAndOr(double num1, double num2, string op)
        {
            string returnBinary = "";

            string num1Bin = ConvertToBinary(num1);
            string num2Bin = ConvertToBinary(num2);
            if (num1Bin.Contains(".")) { num1Bin = num1Bin.Split('.')[0]; }
            if (num2Bin.Contains(".")) { num2Bin = num2Bin.Split('.')[0]; }
            int length = num1Bin.Length > num2Bin.Length ? num1Bin.Length : num2Bin.Length;

            if (op == "And")
            {
                for(int i = length - 1; i >= 0; i--)
                {
                    char num1Bit = (num1Bin.Length - i) < 0 ? '0' : num1Bin[num1Bin.Length - i];
                    char num2Bit = (num2Bin.Length - i) < 0 ? '0' : num2Bin[num1Bin.Length - i];

                    if(num1Bit == '1' && num2Bit == '1')
                    {
                        returnBinary = "1" + returnBinary;
                    } else
                    {
                        returnBinary = "0" + returnBinary;
                    }
                }
            } else if (op == "Or")
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    char num1Bit = (num1Bin.Length - i) < 0 ? '0' : num1Bin[num1Bin.Length - i];
                    char num2Bit = (num2Bin.Length - i) < 0 ? '0' : num2Bin[num1Bin.Length - i];

                    if (num1Bit == '1' || num2Bit == '1')
                    {
                        returnBinary = "1" + returnBinary;
                    }
                    else
                    {
                        returnBinary = "0" + returnBinary;
                    }
                }
            }

            return returnBinary;
        }

        private string CalculateNot(double num)
        {
            string numBin = ConvertToBinary(num);
            string returnString = "";
            foreach(char bit in numBin)
            {
                if(bit == '1')
                {
                    returnString += "0";
                } else if (bit == '0')
                {
                    returnString += "1";
                } else if (bit == '.')
                {
                    returnString += ".";
                }
            }

            return returnString; 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "1";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "1";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "2";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "2";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "3";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "3";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "4";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "4";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "5";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "5";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "6";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "6";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "7";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "7";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "8";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "8";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "9";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "9";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                textBox1.Text = "0";
                displayingResult = false;
            }
            else
            {
                textBox1.Text = textBox1.Text + "0";
            }
        }

        private void button11_Click(object sender, EventArgs e) // + button
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }
            storedNum = calculateStoredNum(storedNum, TextBoxNum);
            DisplayNum();

            storedOp = "+";
        }

        private void button12_Click(object sender, EventArgs e) // - button
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }
            storedNum = calculateStoredNum(storedNum, TextBoxNum);
            DisplayNum();

            storedOp = "-";
        }

        private void button13_Click(object sender, EventArgs e) // * button
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }
            storedNum = calculateStoredNum(storedNum, TextBoxNum);
            DisplayNum();

            storedOp = "*";
        }

        private void button14_Click(object sender, EventArgs e) // / button
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }
            storedNum = calculateStoredNum(storedNum, TextBoxNum);
            DisplayNum();

            storedOp = "/";
        }

        private void button15_Click(object sender, EventArgs e) // = button
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            } else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }
            
            storedNum = calculateStoredNum(storedNum, TextBoxNum);
            DisplayNum();

            storedOp = null;
        }

        private void button16_Click(object sender, EventArgs e) // Clear Box
        {
            textBox1.Text = "0";
        }

        private void button11_Click_1(object sender, EventArgs e) // Clear All
        {
            textBox1.Text = "0";
            storedNum = 0;
            storedOp = null;
        }

        private void button12_Click_1(object sender, EventArgs e) // add .
        {
            if (textBox1.Text == "0" || textBox1.Text == null || displayingResult == true)
            {
                if (textBox1.Text.Contains(".") && displayingResult != true)
                {
                    return;
                }
                textBox1.Text = "0.";
                displayingResult = false;
            }
            else
            {
                if (textBox1.Text.Contains("."))
                {
                    return;
                }

                textBox1.Text = textBox1.Text + ".";
            }
        }

        private void button13_Click_1(object sender, EventArgs e) // button square
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }

            double squaredNum = Math.Pow(TextBoxNum, 2);
            storedNum = calculateStoredNum(storedNum, squaredNum);
            DisplayNum();

            storedOp = null;
        }

        private void button14_Click_1(object sender, EventArgs e) // button Square Root
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }


            double rootNum = Math.Sqrt(TextBoxNum);
            storedNum = calculateStoredNum(storedNum, rootNum);
            DisplayNum();

            storedOp = null;
        }

        private void button17_Click(object sender, EventArgs e) // button To-The-Power
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }
            storedNum = calculateStoredNum(storedNum, TextBoxNum);
            DisplayNum();

            storedOp = "^";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                DisplayBinary = true;
            } else
            {
                DisplayBinary = false;
            }
        }

        private void button18_Click(object sender, EventArgs e) // And button
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }
            storedNum = calculateStoredNum(storedNum, TextBoxNum);
            DisplayNum();

            storedOp = "And";
        }

        private void button19_Click(object sender, EventArgs e) // Or Button
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }
            storedNum = calculateStoredNum(storedNum, TextBoxNum);
            DisplayNum();

            storedOp = "Or";
        }

        private void button20_Click(object sender, EventArgs e) // Not Button
        {
            double TextBoxNum;
            if (displayingResult)
            {
                TextBoxNum = DisplayBinary ? ConvertToDouble(textBox1.Text) : double.Parse(textBox1.Text);
            }
            else
            {
                TextBoxNum = double.Parse(textBox1.Text);
            }

            double NotNum = ConvertToDouble(CalculateNot(TextBoxNum));
            storedNum = calculateStoredNum(storedNum, NotNum);
            DisplayNum();

            storedOp = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
