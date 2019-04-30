using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stlometric_Analysis
{
    class Feature
    {
        
        public static double averageLength = 0;
        public static float ExclamationMarkRatio = 0.0f;
        public static float Apostiesratio = 0.0f;
        public static float Comaratio = 0.0f;
        public static float PeriodDartio = 0.0f;
        public static float QuestionMarks = 0.0f;
        public static float whitespaces = 0.0f;
        public static float coatation = 0.0f;
        public static float colon = 0.0f;
        public static float semcolon = 0.0f;
        public static string data(string text)
        {
            string[] words = text.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries);
            float count = (float)words.Count()/100.0f;
             averageLength = words.Average(w => w.Length);
            ExclamationMarkRatio=  text.Count(f => f == '!')/ count;
           // Comaratio = text.Count(f => f == ',') / count;
            PeriodDartio = text.Count(f => f == '.') / count;
            QuestionMarks = text.Count(f => f == '?') / count;
            whitespaces = text.Count(f => f == ' ') / count;
            Apostiesratio = text.Count(f => f =='\'') / count;
            coatation = text.Count(f => f == '"') / count;
            colon = text.Count(f => f == ';') / count;
            semcolon = text.Count(f => f == ':') / count;
            return "," + averageLength +
                "," + ExclamationMarkRatio +
                "," + PeriodDartio +
                "," + QuestionMarks +
                "," + Apostiesratio +
                "," + whitespaces +
                "," + coatation +
                  "," + colon +
                    "," + semcolon;
        }

    }
}
