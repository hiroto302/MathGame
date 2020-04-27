using System;
using System.Collections.Generic;

namespace MathGame.ConsoleApp
{
  class Player_Lv2 : Player
  {
    public Player_Lv2(string name) : base(name)
    {
      // 親クラスの引数ありのコンストラクタの呼び出し
    }

    // スキップを行った場合、場にある札の枚数が失点となる
    public void Skip(int n)
    {
      point += n;
    }
  }
}