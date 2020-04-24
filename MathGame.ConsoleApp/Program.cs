using System;
using System.Collections.Generic;

// Play人数を2人(playerとcp)を想定し、数字を扱うゲーム作成
// 実行出来るゲームを複数用意する
// 静的メンバはインスタン間で共通なもののみに使用したり、使用用途に注意する
// 複数回、共通するような処理は積極的にメソッド化
namespace MathGame.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // ゲームの進行、管理を行う変数
            GameMaster master = new GameMaster();
            // Player変数　playする人とコンピューター
            Player player1 = new Player();
            CP cp = new CP();
            // カードの生成
            master.MakeCard(10);
            // 生成したカードの配布
            master.DistributeCard(player1, cp);
            // ゲームの実行
            master.PlayGame(player1, cp);
        }
    }
}
