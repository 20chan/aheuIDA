namespace aheuIDA
{
    [System.Diagnostics.DebuggerDisplay("{Letter}")]
    public sealed class Hangul
    {
        internal const string Choseongs = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        internal const string Jungseongs = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
        internal const string Jongseongs = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";

        public char Letter { get; }
        public char Choseong { get; }
        public char Jungseong { get; }
        public char Jongseong { get; }

        public bool IsInvalid { get; } = false;

        public Hangul(char letter)
        {
            Letter = letter;
            if (IsKorean(letter))
                (Choseong, Jungseong, Jongseong) = SeparateLetter(letter);
            else
                IsInvalid = true;
        }

        public Hangul(char choseong, char jungseong, char jongseong)
        {
            Letter = CombineLetter(choseong, jungseong, jongseong);
            (Choseong, Jungseong, Jongseong) = (choseong, jungseong, jongseong);
        }

        public static char CombineLetter(char cho, char jung, char jong)
        {
            int a, b, c;
            a = Choseongs.IndexOf(cho);
            b = Jungseongs.IndexOf(jung);
            c = Jongseongs.IndexOf(jong);

            return (char)('가' + (a * 21 + b) * 28 + c);
        }

        public static bool IsKorean(char letter)
            => '가' <= letter && letter <= '힇';

        public static (char cho, char jung, char jong) SeparateLetter(char letter)
        {
            int a, b, c;
            if (IsKorean(letter))
            {
                c = letter - '가';
                a = c / (21 * 28);
                c = c % (21 * 28);
                b = c / 28;
                c = c % 28;

                return (Choseongs[a], Jungseongs[b], Jongseongs[c]);
            }
            else throw new System.Exception();
        }

        public static int GetStrokeCount(char consonant)
        {
            switch (consonant)
            {
                case ' ':
                    return 0;
                case 'ㄱ':
                case 'ㄴ':
                case 'ㅅ':
                    return 2;
                case 'ㄷ':
                case 'ㅈ':
                case 'ㅋ':
                    return 3;
                case 'ㅁ':
                case 'ㅂ':
                case 'ㅊ':
                case 'ㅌ':
                case 'ㅍ':
                case 'ㄲ':
                case 'ㄳ':
                case 'ㅆ':
                    return 4;
                case 'ㄹ':
                case 'ㄵ':
                case 'ㄶ':
                    return 5;
                case 'ㅄ':
                    return 6;
                case 'ㄺ':
                case 'ㄽ':
                    return 7;
                case 'ㅀ':
                    return 8;
                case 'ㄻ':
                case 'ㄼ':
                case 'ㄾ':
                case 'ㄿ':
                    return 9;
            }
            return -1;
        }
    }
}
