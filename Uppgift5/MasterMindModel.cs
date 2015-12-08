using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    class MasterMindModel
    {
        private string _secretKey;

        public MasterMindModel()
        {
            _secretKey = createSecretKey();
        }

        /* ANROP:    string secret = createSecretKey();
         * Uppgift:  Skapar en ny hemlig nyckel bestående av 4 siffror mellan
         *           1-7
         */
        private static string createSecretKey() {
            Random rnd = new Random();
            string symbols = "1234567";
            string secret = "";

            for (int i = 0; i < 4; i++) {
                char ch = symbols[rnd.Next(symbols.Length)];
                secret += ch;
            }

            return secret;
        }

        /* ANROP:   bool ok = SelfTest();
         * Uppgift: Används vid debugging. Metoden anropar alla andra metoder i 
         *          klassen och returnerar true om ingen bug hittades. 
         */
        public static bool SelfTest()
        {
            MasterMindModel model = new MasterMindModel();

            bool ok = IsValidKey("2361") && !IsValidKey("2368")
                       && !IsValidKey("ABCD") && !IsValidKey("+-*/")
                       && !IsValidKey("2301") && !IsValidKey("23611")
                       && !IsValidKey("231");

            for (int i = 0; i < 1000 && ok; i++)
                ok = IsValidKey(createSecretKey());

            System.Diagnostics.Debug.WriteLine("createSecretKey: " + ok);

            ok = IsValidKey(model._secretKey);
            System.Diagnostics.Debug.WriteLine("MasterMindModel konstruktor: " + ok);

            ok = ok && ( MatchKeys("1234", "1234") == new MatchResult(4,0) );
            ok = ok && (MatchKeys("1234", "4321") == new MatchResult(0, 4));
            ok = ok && (MatchKeys("1234", "1243") == new MatchResult(2, 2));
            ok = ok && (MatchKeys("1234", "1212") == new MatchResult(2, 0));
            ok = ok && (MatchKeys("1234", "5612") == new MatchResult(0, 2));
            ok = ok && (MatchKeys("1444", "1144") == new MatchResult(3, 0));
            ok = ok && (MatchKeys("5224", "4334") == new MatchResult(1, 0));
            ok = ok && (MatchKeys("1223", "2245") == new MatchResult(1, 1));
            ok = ok && (MatchKeys("1222", "3111") == new MatchResult(0,1));

            System.Diagnostics.Debug.WriteLine("MatchkKeys: " + ok);

            return ok;
        }

        /* ANROP: ok = IsValidKey(key);
         * Uppgift: Returnerar true omm key är en giltlig nyckel
         */
        public static bool IsValidKey(String key)
        {
            if (key.Length != 4)
                return false;

            foreach (char ch in key)
            {
                if (!Char.IsNumber(ch))
                    return false;

                int chInt = (int)Char.GetNumericValue(ch);
                if (!(chInt > 0 && chInt <= 7))
                    return false;
            }

            return true;
        }

        //Anrop: MatchResult mr = MatchKeys(secretKey, testKey);
        public static MatchResult MatchKeys(string secretKey, string testKey)
        {
            int countCorrect = 0;
            int countSemiCorrect = 0;
            char[] incorrect = new char[4];
            char[] newSecret = new char[4];

            for (int i = 0; i < testKey.Length; i++ )
            {
                if (testKey[i] == secretKey[i]) {
                    countCorrect++;
                }
                else
                {
                    incorrect[i] = testKey[i];
                    newSecret[i] = secretKey[i];
                }
            }

            if (countCorrect != 4) {
                for (int i = 0; i < 4; i++ )
                {
                    for (int j = 0; j < 4; j++ )
                    {
                        if (incorrect[i] == 0)
                            break;

                        if (i != j)
                        {
                            if (incorrect[i] == newSecret[j]) {
                                incorrect[i] = ' ';
                                newSecret[i] = ' ';
                                countSemiCorrect++;
                                break;
                            }
                        }
                    }
                }
            }

            return new MatchResult(countCorrect, countSemiCorrect);
        }

        public MatchResult TestIfMatch(string testKey)
        {
            return MatchKeys(_secretKey, testKey);
        }
    }
}
