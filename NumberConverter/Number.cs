// Jake Sussner
// 9/25/24
// Number class for Number Converter program



using System;

namespace NumberConverter
{
    public class Number
    {
        private long num;
        private long currBase;
        private long nextBase;
        private long numBits;
        
        // constructor
        public Number(long num, long currBase, long nextBase, long numBits)
        {
            this.num = num;
            this.currBase = currBase;
            this.nextBase = nextBase;
            this.numBits = numBits;
        }

        // getters and setters
        public long Num
        {
            get { return num; }
            set { num = value; }
        }
        public long CurrBase
        {
            get { return currBase; }
            set { currBase = value; }
        }
        public long NextBase
        {
            get { return nextBase; }
            set { nextBase = value; }
        }
        public long NumBits
        {
            get { return numBits; }
            set { numBits = value; }
        }

        // method to check the bit length of our final conversion
        public string checkBits(string newNum)
        {
            // don't have enough bits
            if (newNum.Length > this.numBits)
            {
                return "OVERFLOW";
            }
            // need more bits
            else if (newNum.Length < this.numBits)
            {
                long temp = this.numBits - newNum.Length;
                string zeros = "";
                while (temp > 0)
                {
                    zeros += "0";
                    temp --;
                }
                newNum = zeros + newNum;
                return newNum;
            }
            // perfect bit length
            else {
                return newNum;
            }
        }

        // method to convert a number from base 10 to any other base up to 16
        public string fromBaseTen(long numToConvert)
        {
            string newNum = "";

            // check if our next base requires alphabet characters as bit placeholders
            // if its ten or less, we dont need them
            if (this.nextBase <= 10)
            {
                List<string> remainders = new List<string>();
                // continuously divide number by the base to get a list of remainders
                while (numToConvert > 0)
                {
                    long remainder = numToConvert % this.nextBase;
                    remainders.Add(remainder.ToString());
                    numToConvert /= this.nextBase;
                }
                // that list of remainders in reverse is the new number at the new base
                for (int i = remainders.Count - 1; i >= 0; i--)
                {
                    newNum += remainders[i];
                }
            }
            // new number requires alphabet characters as bit placeholders
            else
            {
                // initialize a number dictionary to refer to when converting
                Dictionary<long, string> numDict = new Dictionary<long, string>();
                numDict[10] = "A";
                numDict[11] = "B";
                numDict[12] = "C";
                numDict[13] = "D";
                numDict[14] = "E";
                numDict[15] = "F";

                List<string> remainders = new List<string>();
                // continuously divide number by the base to get a list of remainders
                while (numToConvert > 0)
                {
                    long remainder = numToConvert % this.nextBase;
                    // remainder requires alphabet character
                    if (remainder > 9)
                    {
                        // grab the associated character from the number dictionary
                        remainders.Add(numDict[remainder]);
                    }
                    else 
                    {
                        remainders.Add(remainder.ToString());
                    }

                    numToConvert /= this.nextBase;
                }
                // that list of remainders in reverse is the new number at the new base
                for (int i = remainders.Count - 1; i >= 0; i--)
                {
                    newNum += remainders[i];
                }
            }
            // check the bit length of conversion
            return this.checkBits(newNum);
        }

        // method to convert a number to base 10
        public string toBaseTen(bool flag)
        {
            long newNum = 0;
            // initalize a number dictionary to refer to when converting
            Dictionary<char, long> numDict = new Dictionary<char, long>();
            numDict['A'] = 10;
            numDict['B'] = 11;
            numDict['C'] = 12;
            numDict['D'] = 13;
            numDict['E'] = 14;
            numDict['F'] = 15;
            double power = 0;
            double currbase = this.currBase;
            string numStr = "";

            // convert our curent number to a string and reverse it
            for (int i = (this.num).ToString().Length - 1; i >= 0; i--)
            {
                numStr += (this.num).ToString()[i];
            }

            // continuously take the current bit and multiply it by the current base raised to the power of its bit position
            for (int i = 0; i < numStr.Length; i++)
            {
                char num = numStr[i];
                long temp;
                // if the current bit is a alphabet character, convert it to a number
                if(numDict.ContainsKey(num))
                {
                    temp = numDict[num];
                }
                else
                {
                    // convert the current bit to a number by subtracting the ASCII value of the character 0
                    temp = num - '0';
                }
                newNum += (long)(temp * Math.Pow(currbase, power));
                power += 1;
            }

            // convert to a string
            string newNumStr = newNum.ToString();

            // if there is no more converting, check its bit length
            if (flag)
            {
                return this.checkBits(newNumStr);
            }
            // if still need to convert, do not check bit length yet
            else 
            {
                return newNumStr;
            }
        }

        // method to convert a non-base 10 number to another non-base 10 number
        public string convertToOtherBase()
        {
            // first convert it to base 10, then convert it from base ten to new base
            string baseTenNum = this.toBaseTen(false);
            return this.fromBaseTen(long.Parse(baseTenNum));
        }

        // string method
        public string toString()
        {
            return string.Format("Number: {0}, Current Base: {1}, Next Base: {2}, Number of Bits: {3} \n", this.num, this.currBase, this.nextBase, this.numBits);
        }
    }
}