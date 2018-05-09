using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aheuIDA.Tests
{
    [TestClass]
    public class AheuiTest
    {
        [TestMethod, TestCategory("Standard")]
        public void TestBieups()
        {
            TestBieupChar();
            TestBieupSign();
            TestBieup();
        }

        public void TestBieupChar()
        {
            var code = @"밯맣밯맣밯맣밯맣밯맣밯맣희";
            var inputoutput = @"1+한글😃😄";
            AssertAheui(inputoutput, code, inputoutput);
        }

        public void TestBieupSign()
        {
            var code = @"방망방망방망희";
            int[] input = { 0, 42, -42 };
            var output = "042-42";
            AssertAheui(output, code, input);
        }

        public void TestBieup()
        {
            var code = @"박반받발밤밥밧밪밫밬밭붚
뭉멍멍멍멍멍멍멍멍멍멍멍
밖밗밙밚밝밞밟밠밡밢밣밦붔
뭉멍멍멍멍멍멍멍멍멍멍멍멍
밯망방망희";
            var input = @"밯3";
            var output = @"4434324453224689979975544481753";
            AssertAheui(output, code, input);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestBorder()
        {
            var code = @"볻         망볿
ㅇ         ㅇ희
멍         붒
ㅇ         ㅇ몽
";
            var output = "369";
            AssertAheui(output, code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestChieut()
        {
            var code = @"아ㅇㅇ부
희멍번처붇
ㅇㅇㅇ분멍
희멍번차붇
ㅇㅇㅇ희멍";
            AssertAheui("33", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestDefaultDirection()
        {
            var code = @"뵐망희
마본";
            AssertAheui("2", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestDefaultStorage()
        {
            var code = @"밞산바삳바사망희";
            AssertAheui("9", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestDigeut()
        {
            var code = @"반받다망희";
            AssertAheui("5", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestEmptySwap()
        {
            var code = @"뱐희파반망희";
            AssertAheui("", 2, code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestExhaustedStorage()
        {
            var code = @"아ㅇㅇ우
희멍벋망반망희";
            AssertAheui("3", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestExitCode()
        {
            var code = @"반월회";
            AssertAheui("", 2, code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestHieutPop()
        {
            var code = @"하멍번버";
            AssertAheui("", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestIeungHieut()
        {
            var code = @"아악안앋압알앗았앜앇헐";
            AssertAheui("", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestJieut()
        {
            var code = @"반반자망받반자망반받자망희";
            AssertAheui("110", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestLoop()
        {
            var code = @"밦밦따빠뚜
뿌뚜뻐뚜뻐
따ㅇㅇㅇ우
ㅇㅇ아ㅇ분
ㅇㅇ초뻐터
ㅇㅇ망희";
            AssertAheui("0", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestMieum()
        {
            var code = @"바반받밤발밦밠밣밞망만맘말망맋맠맟망희";
            AssertAheui("950", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestNieun()
        {
            var code = @"밟받나망희";
            AssertAheui("3", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestPieup()
        {
            var code = @"바밟밟땅밝밝땅팡망망우
숭ㅇㅇㅇㅇㅇㅇㅇㅇㅇ어
밟밟밝밝땅땅바팡망망희";
            AssertAheui("81494981", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestPrint()
        {
            var code = @"밞밞반다따반타뭉
ㅇㅇㅇㅇㅇㅇㅇ밞밞반다따반타맣희";
            AssertAheui("97a", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestQueue()
        {
            var code = @"상반받뱔우망이
뭉뻐벋번성
망망희";
            AssertAheui("235223", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestRieul()
        {
            var code = @"밟발라망희";
            AssertAheui("4", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestShebang()
        {
            var code = @"반망희";
            AssertAheui("2", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestSsangBieup()
        {
            var code = @"밟밟땅빵망망희";
            AssertAheui("8181", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestSsangDiegeut()
        {
            var code = @"발밞따망희";
            AssertAheui("45", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestSsangSiot()
        {
            var code = @"아바싹반싼받싿우
우멍석멍선멍섣어
아바쌀반쌈받쌉우
우멍설멍섬멍섭어
아바쌋반쌍받쌎우
우멍섯멍성멍섲어
아바쌏반쌐받쌑우
우멍섳멍섴멍섵어
아바쌒반싺받싻우
우멍섶멍섞멍섟어
아바싽반싾받쌁우
우멍섡멍섢멍섥어
아바쌂반쌃받쌄우
우멍섦멍섧멍섨어
아바쌅반쌆받쌇우
우멍섩멍섪멍섫어
아바쌊반쌌받싸우
희멍섮멍섰멍서어";
            AssertAheui("320320320320320320320320320\n", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestStorage()
        {
            var code = @"바반받뭉
우석멍멍
바반받뭉
우선멍멍
바반받뭉
우섣멍멍
바반받뭉
우설멍멍
바반받뭉
우섬멍멍
바반받뭉
우섭멍멍
바반받뭉
우섯멍멍
바반받뭉
우성멍멍
바반받뭉
우섲멍멍
바반받뭉
우섳멍멍
바반받뭉
우섴멍멍
바반받뭉
우섵멍멍
바반받뭉
우섶멍멍
바반받뭉
끝희멍멍";
            AssertAheui("320320320320320320320320023320320320320320", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestSyllable()
        {
            var code = @"ㅏ희ㅣ😄ㅓ
뱓ㅗㅈㅊ몽
ㅂ😃먕버헥";
            AssertAheui("3", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestTieut()
        {
            var code = @"받반타망희";
            AssertAheui("1", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestVowel2Step()
        {
            var code = @"뷷우희어밍우여
아아아아아아아반받망희
먕오뱞오뱗오뵬";
            AssertAheui("3596", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestVowelAdvanced()
        {
            var code = @"반븓븝불
우멍벎망이밟망희
빈
빋밟망희
붑으
발몽
ㅇ밞망분
ㅇ불법벋
의멍밞망희";
            AssertAheui("543295432954329", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestVowelBasic()
        {
            var code = @"붇희희멍
망밦망볿";
            AssertAheui("369", code);
        }

        [TestMethod, TestCategory("Standard")]
        public void TestVowelUseless()
        {
            var code = @"우아앙배벤뱯볩뷜뫙뫵뮝뭥뮁우
배맹희맹멩먱몡뮝봘봽붣붠붸어
벤멩
뱯먱
볩몡
뷜뮝
뫙봘
뫵봽
묑뵏
뭥붠
뮁붸
아오";
            AssertAheui("54320543205432054320", code);
        }

        private void AssertAheui(string expected, string code, params int[] args)
        {
            var exit = IntAheui.Execute(code, out var output, args);
            Assert.AreEqual(expected, output);
        }

        private void AssertAheui(string expected, int exitcode, string code, params int[] args)
        {
            var exit = IntAheui.Execute(code, out var output, args);
            Assert.AreEqual(expected, output);
            Assert.AreEqual(exit, exitcode);
        }

        private void AssertAheui(string expected, string code, string arg)
        {
            var intargs = arg.Cast<int>().ToArray();
            AssertAheui(expected, code, intargs);
        }

        private void AssertAheui(string expected, int exitcode, string code, string arg)
        {
            var intargs = arg.Cast<int>().ToArray();
            AssertAheui(expected, exitcode, code, intargs);
        }
    }
}
