using System;
using System.Collections.Generic;
using System.Diagnostics; //Stopwatchクラスの利用
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Lv2ゲームに対応したCPのクラス
namespace MathGame.ConsoleApp
{
  class CP_Lv2 : CP
  {
    // CPを親クラスとする子クラスのコンストラクタは、新たな処理が必要でない限り記述する必要なし
    // インスタンスの生成時、自動的に、親クラスであるCPの引数なしのコントラクタが呼ばれる

    public void Skip(int n)
    {
      point += n;
    }
  }
}

