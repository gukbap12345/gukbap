namespace NumberBaseballUsingDelegate
{
    internal class Utility
    {
        //유저가 입력한 숫자를 한개한개 쪼개줌
        public static List<int> Get_Each_Numbers(int userinput)
        {
            int len = userinput.ToString().Length; //자릿수 구함
            List<int> EachNums = new List<int>();

            for (int i = len; i > 0; i--)
            {
                if (i == 0)
                {
                    EachNums.Add(userinput % 10);
                }
                else if (i == len)
                {
                    EachNums.Add(userinput / (int)(Math.Pow(10, i - 1)));
                }
                else
                {
                    EachNums.Add((userinput / (int)(Math.Pow(10, i - 1))) % 10);
                }
            }

            return EachNums;
        }

        //숫자 범위 구하는거
        public static int GetMaximum(int num)
        {
            int res = 0;

            for (int i = 0; i < num; i++)
            {
                res += 9 * (int)(Math.Pow(10, i));
            }

            return res;
        }
    }
}
