﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using LRSkipAsync;
using StackAsync;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace MyWindows
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region 初期設定
        private CS_StackAsync stack;
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
            stack = new CS_StackAsync();

            textBox01.Text = "";
            textBox02.Text = "";

            // 初期表示をクリアする
            ClearResultTextBox();
        }

        #region ［Ｐｕｓｈ］ボタン押下
        private async void button01_Click(object sender, RoutedEventArgs e)
        {   // [Push]ボタン押下
            WriteLineResult(@"[Push]");

            try
            {   // 入力情報を数字に変換
                int num = int.Parse(textBox02.Text);

                await stack.PushAsync(num);           // 入力情報をスタックへＰｕｓｈ
            }
            catch (FormatException)
            {
                WriteLineResult("Format Exception:[{0}]", textBox02.Text);
            }
        }
        #endregion

        #region ［Ｐｏｐ］ボタン押下
        private async void button02_Click(object sender, RoutedEventArgs e)
        {   // [Pop]ボタン押下
            WriteLineResult(@"[Pop]");

            try
            {
                int i = await stack.CountAsync();           // スタックの登録件数を取り出す
                if (i > 0)
                {   // スタックの登録有り？
                    int num = await stack.PopAsync();       // スタックの情報を取り出す
                    WriteLineResult("buffer[0] = {1}", num);
                }
                else
                {
                    WriteLineResult(@"buffer[] = {Empty}");
                }
            }
            catch (ArgumentOutOfRangeException)
            {   // 空スタックからの情報取り出し
                WriteLineResult(@"ArgumentOutOfRangeException");
            }
            catch (FormatException)
            {
                int i = await stack.CountAsync();
                WriteLineResult("Format Exception : Count[{0}]", i);
            }
        }
        #endregion

        #region ［Ｑｕｅ］ボタン押下
        private async void button03_Click(object sender, RoutedEventArgs e)
        {   // [Que]ボタン押下
            WriteLineResult(@"[Que]");

            try
            {   // 入力情報を数字に変換
                int num = int.Parse(textBox02.Text);

                int i = await stack.QueAsync(num);       // スタックの情報をキュー設定する
                WriteLineResult("buffer[0] = {1}", i);
            }
            catch (FormatException)
            {
                WriteLineResult("Format Exception:[{0}]", textBox02.Text);
            }
            catch(ArgumentOutOfRangeException)
            {   // 空スタックからの情報取り出し
                WriteLineResult(@"ArgumentOutOfRangeException");
            }
        }
        #endregion

        #region ［Ｖｉｅｗ］ボタン押下
        private async void button04_Click(object sender, RoutedEventArgs e)
        {   // [View]ボタン押下
            WriteLineResult(@"[View]");

            int i = await stack.CountAsync();                   // スタックの登録件数を取り出す
            WriteLineResult("Count = {0}", i);
            int pos = int.Parse(textBox02.Text);
            if (pos < i)
            {   // スタックの登録有り？
                int num = await stack.ViewAsync(pos);           // スタックの指定位置の情報を取り出す
                WriteLineResult("buffer[{0}] = {1}", pos, num);
            }
        }
        #endregion

        #region ［Ｃｏｕｎｔ］ボタン押下
        private async void button05_Click(object sender, RoutedEventArgs e)
        {   // [Count]ボタン押下
            WriteLineResult(@"[Count]");

            int i = await stack.CountAsync();           // スタックの登録件数を取り出す
            WriteLineResult("Count = {0}", i);
        }
        #endregion

        #region ［Ｃｈｋｎｕｍ］ボタン押下
        private async void button06_Click(object sender, RoutedEventArgs e)
        {   // [Chknum]ボタン押下
            WriteLineResult(@"[Chknum]");

            int i = await stack.CountAsync();           // スタックの登録件数を取り出す
            WriteLineResult("Count = {0}", i);

            try
            {   // 入力情報を数字に変換
                int num = int.Parse(textBox02.Text);

                int pos = await stack.chknumAsync(num); // スタックの指定位置の情報を取り出す
                WriteLineResult("buffer[{0}] = {1}", pos, num);
            }
            catch (FormatException)
            {
                WriteLineResult("Format Exception:[{0}]", textBox02.Text);
            }
        }
        #endregion

        #region ［ＳＰｕｓｈ］ボタン押下
        private async void button07_Click(object sender, RoutedEventArgs e)
        {   // [SPush]ボタン押下
            WriteLineResult(@"[SPush]");

            if (textBox02.Text.Length != 0)
            {   // 入力情報有り？
                await stack.SPushAsync(textBox02.Text);
            }
            else
            {
                WriteLineResult(@"Null Pointer Exception");
            }
        }
        #endregion

        #region ［ＳＰｏｐ］ボタン押下
        private void button08_Click(object sender, RoutedEventArgs e)
        {   // [SPop]ボタン押下
            WriteLineResult(@"[SPop]");
        }
        #endregion

        #region ［ＳＱｕｅ］ボタン押下
        private void button09_Click(object sender, RoutedEventArgs e)
        {   // [SQue]ボタン押下
            WriteLineResult(@"[SQue]");
        }
        #endregion

        #region ［ＳＶｉｅｗ］ボタン押下
        private void button10_Click(object sender, RoutedEventArgs e)
        {   // [SView]ボタン押下
            WriteLineResult(@"[SView]");
        }
        #endregion

        #region ［ＳＣｏｕｎｔ］ボタン押下
        private async void button11_Click(object sender, RoutedEventArgs e)
        {   // [SCount]ボタン押下
            WriteLineResult(@"[SCount]");

            int i = await stack.SCountAsync();
            WriteLineResult("SCount = {0}", i);
        }
        #endregion

        #region ［Ｃｈｋｓｔｒ］ボタン押下
        private void button12_Click(object sender, RoutedEventArgs e)
        {   // [Chkstr]ボタン押下
            WriteLineResult(@"[Chkstr]");
        }
        #endregion

        #region ［Ｒｅｓｅｔ］ボタン押下
        private void button13_Click(object sender, RoutedEventArgs e)
        {   // [Reset]ボタン押下
            ClearResultTextBox();			// 初期表示をクリアする

            textBox01.Text = "";
            textBox02.Text = "";
        }
        #endregion
    }
}
