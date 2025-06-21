using System;

public class OutputDelegate
{
    // Comparison 델리게이트 정의: 두 int 배열을 비교하여 스트라이크와 볼을 계산하는 메서드
    // 스트라이크가 3개인 경우 1을 반환하고, 그렇지 않으면 0을 반환
    public Comparison<List<int>>? CompareQuestionAndAnswer = (question, answer) =>
    {
        // 스트라이크와 볼을 계산하는 로직
        int strikeCount = 0;
        int ballCount = 0;
        for (int i = 0; i < question.Count; i++)
        {
            if (question[i] == answer[i])
            {
                strikeCount++;
            }
            else if (answer.Contains(question[i]))
            {
                ballCount++;
            }
        }
        // 결과 출력
        //Console.WriteLine($"\n{strikeCount} 스트라이크, {ballCount} 볼\n");

        // 게임 종료 조건: 스트라이크가 3개인 경우
        return strikeCount == answer.Count ? 1 : 0;
    };
}