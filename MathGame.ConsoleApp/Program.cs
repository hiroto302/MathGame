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
            // GameMaster_Lv2 master2 = new GameMaster_Lv2();
            string name = "player";
            // Player変数　playする人
            Player player = new Player(name);
            // 対戦相手　CPの変数
            CP cp = new CP();
            // CP_Lv2 cp2 = new CP_Lv2();
            // カードの生成
            master.MakeCard(10);
            // 引数のインスタンス変数に生成したカードを配布
            master.DistributeCard(player, cp);
            // ゲームの実行
            master.PlayGame(player, cp);
            // master2.PlayGame(player, cp2);
        }
    }
}
