using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyFile : MonoBehaviour
{
    // 10^8은 연산량으로 치면 약 1초가 걸린다.
    // 이것을 이용하면 코딩 테스트가 요구하는 알고리즘을 유추할 수 있다.

    // 시간복잡도 (빅오표기법)
    // = 1부터 n까지의 모든 숫자를 더한 값을 리턴하세요.
    private int SumNum(int n)
    {
        int sum = 0;
        for (int i = 1; i <= n; i++)
            sum += i;

        return sum;
    }
    private int SumNum2(int n)
    {
        return n * (n + 1) / 2;
    }

    // for문을 이용하면 구할 수는 있으나 n의 크기가 10^8이상이라면 문제가 생긴다.
    // 따라서 수학적 공식 'n*(n+2)/2'를 통해 n의 크기와 상관없이 일정한 속도를 보장.

    // 빅오표기법
    // = 각 알고리즘이 입력값 n에 따라서 가장 많은 연산을 할 경우
    //   점근적으로 얼마나 길어지는지 추정하는 방식.
    //   위 loop의 경우 n에 따라 시간이 비례해 커지므로 O(n)이라고 부른다.

    // 시간복잡도는 loop의 개수로도 구할 수 있다.
    // 2중 for문의 경우에는 O(n²), 3중 loop는 O(n³)
    
    // 결론 
    // -> 입력 제한을 보고 n의 최악인 경우가 얼마인지를 봐야한다.
    //    문제가 요구되는 시간복잡도를 알면 'n=10,000'인데 O(n²)은 구현해봐야 의미가 없다. (효용성 통과 X)


}
