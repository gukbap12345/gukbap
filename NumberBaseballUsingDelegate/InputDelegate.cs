using System;

public class InputDelegate
{
    // CreateQuestionDelegateMethod 델리게이트는 int 배열을 반환하는 메서드
    public List<int> CreateQuestionDelegateMethod(int Ball_num)
    {
        Random random = new Random();

        // 0~9 사이의 서로 다른 세 자리 숫자를 랜덤으로 생성
        List<int> answ = new List<int>();
        for (int i = 1; i <= Ball_num; i++)
        {
            int RandomRes = random.Next(0, 9);
            while (answ.Contains(RandomRes) == true)
            {
                RandomRes = random.Next(0, 9);
            }
            answ.Add(RandomRes);
        }
        
        return answ; // 생성된 숫자를 배열로 반환        
    }

    // SubmitAnswerActionMethod 델리게이트는 string을 입력받아 int 배열을 반환하는 메서드
    public Func<string, int, List<int>>? SubmitAnswerActionMethod;
}

