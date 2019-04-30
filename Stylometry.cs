using SimpleNetNlp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stlometric_Analysis
{
    class Stylometry
    {
        

        public static string Stylofeatureprocess(string traindata)
        {
            var sentence = new Sentence(traindata);
            string s = Feature.data(traindata);
            var lemmas = sentence.Lemmas;
            var pos = sentence.PosTags;
            s = s+ counttags(pos);
            return s;
        }

        private static string counttags(IReadOnlyCollection<string> pos)
        {
            float div = (float)pos.Count/100.0f;
          
            POSTTAGS.CC = pos.Where(x => x.Equals("CC")).Count()/div;
            POSTTAGS.CD = pos.Where(x => x.Equals("CD")).Count()/div;
            POSTTAGS.DT = pos.Where(x => x.Equals("DT")).Count()/div;
            POSTTAGS.EX = pos.Where(x => x.Equals("EX")).Count()/div;
            POSTTAGS.FW = pos.Where(x => x.Equals("FW")).Count()/div;
            POSTTAGS.IN = pos.Where(x => x.Equals("IN")).Count()/div;
            POSTTAGS.JJ = pos.Where(x => x.Equals("JJ")).Count()/div;
            POSTTAGS.JJR = pos.Where(x => x.Equals("JJR")).Count()/div;
            POSTTAGS.JJS = pos.Where(x => x.Equals("JJS")).Count()/div;
            POSTTAGS.LS = pos.Where(x => x.Equals("LS")).Count()/div;
            POSTTAGS.MD= pos.Where(x => x.Equals("MD")).Count()/div;
            POSTTAGS.NN = pos.Where(x => x.Equals("NN")).Count()/div;
            POSTTAGS.NNS = pos.Where(x => x.Equals("NNS")).Count()/div;
            POSTTAGS.NNP = pos.Where(x => x.Equals("NNP")).Count()/div;
            POSTTAGS.NNPS = pos.Where(x => x.Equals("NNPS")).Count()/div;
            POSTTAGS.PDT = pos.Where(x => x.Equals("PDT")).Count()/div;
            POSTTAGS.POS = pos.Where(x => x.Equals("POS")).Count()/div;
            POSTTAGS.PRP = pos.Where(x => x.Equals("PRP")).Count()/div;
            POSTTAGS.PRPD = pos.Where(x => x.Equals("PRP$")).Count()/div;
            POSTTAGS.RB = pos.Where(x => x.Equals("RB")).Count()/div;
            POSTTAGS.RBR = pos.Where(x => x.Equals("RBR")).Count()/div;
            POSTTAGS.RBS = pos.Where(x => x.Equals("RBS")).Count()/div;
            POSTTAGS.RP = pos.Where(x => x.Equals("RP")).Count()/div;
            POSTTAGS.SYM = pos.Where(x => x.Equals("SYM")).Count()/div;
            POSTTAGS.TO = pos.Where(x => x.Equals("TO")).Count()/div;
            POSTTAGS.UH = pos.Where(x => x.Equals("UH")).Count()/div;
            POSTTAGS.VB = pos.Where(x => x.Equals("VB")).Count()/div;
            POSTTAGS.VBZ = pos.Where(x => x.Equals("VBZ")).Count()/div;
            POSTTAGS.VBP = pos.Where(x => x.Equals("VBP")).Count()/div;
            POSTTAGS.VBD = pos.Where(x => x.Equals("VBD")).Count()/div;
            POSTTAGS.VBN = pos.Where(x => x.Equals("VBN")).Count()/div;
            POSTTAGS.VBG = pos.Where(x => x.Equals("VBG")).Count()/div;
            POSTTAGS.WDT = pos.Where(x => x.Equals("WDT")).Count()/div;
            POSTTAGS.WP = pos.Where(x => x.Equals("WP")).Count()/div;
            POSTTAGS.WPD = pos.Where(x => x.Equals("WP$")).Count()/div;
            POSTTAGS.WRB = pos.Where(x => x.Equals("WRB")).Count()/div;

            return POSTTAGS.data();

        }
    }
}
