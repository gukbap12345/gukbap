using System;

public class NumberBaseballGame
{
    // 델리게이트 정의
    public delegate List<int> CreateQuestionDelegate(int b_num);
    // 입력값을 처리하는 델리게이트
    public Action<List<int>, List<int>>? PrintResultAction;

    // Comparison 델리게이트 정의
    public Comparison<List<int>>? CompareQuestionAndAnswerDelegate;

    public void StartGame(int Ball_num)
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Number Baseball Game!\n");

        Console.WriteLine("게임 규칙: 0~9 사이의 서로 다른 세 자리 숫자를 맞추는 게임입니다.\n" +
                          "각 숫자는 스트라이크와 볼로 피드백을 제공합니다.\n" +
                          "스트라이크는 숫자와 위치가 모두 일치하는 경우, 볼은 숫자는 일치하지만 위치가 다른 경우입니다.\n" +
                          "세 자리 숫자를 맞추면 게임이 종료됩니다.\n");

        // 델리게이트 인스턴스 생성 및 메서드 할당
        InputDelegate inputDelegate = new InputDelegate();
        OutputDelegate outputDelegate = new OutputDelegate();

        // CreatQuestionDelegateMethod를 CreateQuestionDelegate로 변환
        CreateQuestionDelegate? CreateQuestion = null;
        CreateQuestion = inputDelegate.CreateQuestionDelegateMethod;

        // SubmitAnswerActionMethod를 Func<string, List<int>>로 변환
        inputDelegate.SubmitAnswerActionMethod = (string answ, int b_num) => SubmitAnswerFunc(answ, b_num);

        // PrintResultAction 필드에 메서드 할당
        PrintResultAction = PrintResult; 

        // CompareQuestionAndAnswerDelegate를 Comparison<List<int>>로 변환
        // ?? CompareQuestionAndAnswer가 null인 경우 기본 델리게이트를 사용
        CompareQuestionAndAnswerDelegate = outputDelegate.CompareQuestionAndAnswer ?? ((question, answer) => 0);

        // CreateQuestion 델리게이트를 사용하여 질문 생성
        // ?? CreateQuestion이 null인 경우 빈 배열을 반환
        List<int> question = CreateQuestion?.Invoke(Ball_num) ?? new List<int>();
        if (question.Count == 0)
        {
            Console.WriteLine("Failed to generate a question. Exiting game.");
            return;
        }

        Console.WriteLine($"Generated question: {string.Join(", ", question)}\n");

        while (true)
        {
            Console.Write(string.Format("Enter your answer ({0} digits): ", Ball_num));
            // 사용자 입력을 받음. ?? 연산자를 사용하여 null 체크
            string userInput = Console.ReadLine() ?? string.Empty;

            // SubmitAnswerActionMethod를 사용하여 사용자 입력을 처리
            List<int> answer = inputDelegate.SubmitAnswerActionMethod?.Invoke(userInput, Ball_num) ?? new List<int>();
            if (answer.Count == 0)
            {
                continue;
            }

            // PrintResultAction을 사용하여 결과 출력
            PrintResultAction?.Invoke(question, answer);

            // CompareQuestionAndAnswerDelegate를 사용하여 질문과 답변 비교
            int result = CompareQuestionAndAnswerDelegate.Invoke(question, answer);

            if (result == 1)
            {
                Console.WriteLine("Congratulations! You've guessed the number correctly!");
                break;
            }
        }

        CreateQuestion = null; // 델리게이트를 null로 설정하여 메모리 해제
        inputDelegate.SubmitAnswerActionMethod = null; // 델리게이트를 null로 설정하여 메모리 해제
        PrintResultAction = null; // 메서드 필드를 null로 설정하여 메모리 해제
        CompareQuestionAndAnswerDelegate = null; // 델리게이트를 null로 설정하여 메모리 해제

        // 게임 종료 후 재시작 여부 확인
        Console.WriteLine("\nDo you want to play again? (yes/no)");
        string playAgain = Console.ReadLine()?.Trim().ToLower() ?? "no";
        if (playAgain == "yes" || playAgain == "y")
        {
            StartGame(Ball_num);
        }
        else
        {
            Console.WriteLine("\nThank you for playing! Goodbye!");
        }
    }

    public Func<string, int, List<int>> SubmitAnswerFunc = (inputAnswer, Ball_num) =>
    {
        // 입력값이 null이거나 길이가 3이 아닌 경우 처리
        if (string.IsNullOrEmpty(inputAnswer) || inputAnswer.Length != Ball_num)
        {
            Console.WriteLine(string.Format("\n{0} 자리 숫자를 입력해야 합니다.", Ball_num));
            return new List<int>();
        }
        // 입력값이 숫자가 아니거나 범위를 벗어난 경우 처리
        if (!int.TryParse(inputAnswer, out int userInput) || userInput < 0 || userInput > GetMaximum(Ball_num))
        {
            Console.WriteLine(string.Format("\n유효한 {0} 자리 숫자를 입력하세요.", Ball_num));
            return new List<int>();
        }
        return Get_Each_Numbers(userInput); //new List<int>()
        //{
        //    userInput / 10000,
        //    (userInput / 1000) % 10,
        //    (userInput / 100) % 10, // 백의 자리
        //    (userInput / 10) % 10, // 십의 자리
        //    userInput % 10 // 일의 자리
        //};
    };

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
                EachNums.Add(userinput / (int)(Math.Pow(10, i-1)));
            }
            else
            {
                EachNums.Add((userinput / (int)(Math.Pow(10, i-1))) % 10);
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

    public void PrintResult(List<int> question, List<int> answer)
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
        Console.WriteLine($"\n{strikeCount} 스트라이크, {ballCount} 볼\n");
    }
}