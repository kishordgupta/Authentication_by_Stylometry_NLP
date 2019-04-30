using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stlometric_Analysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
           string[] userids= File.ReadAllLines(@"C:\Users\kishor\Documents\styles\userids.csv");
            foreach (string u in userids)
                useridlist.Items.Add(u);

            LoadCSVOnDataGridView(@"C:\Users\kishor\Documents\styles\Data.csv",1);

        }
        /// <summary>
        /// for training data and test data dividing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\Data.csv");
            string trainingwrite = "";
            string testwrite = "";
            string tempu = "";
            foreach (var line in lines)
            {
                var data = line.Split(',');
                if (!data[0].Equals(tempu))
                {
                    testwrite = testwrite + "\n" + line;
                    tempu = data[0];
                    trainingwrite = trainingwrite + "\n" + tempu+",";
                }
                else {
                    trainingwrite = trainingwrite +" . "+ data[1];

                }


            }
            File.WriteAllText(@"C:\Users\kishor\Documents\styles\TrainData.csv", trainingwrite);
            File.WriteAllText(@"C:\Users\kishor\Documents\styles\TestData.csv", testwrite);
        }
        /// <summary>
        /// extract feature from traindata
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\TEST\TrainData.csv");
            int i = 0;
            string feature = "";
            foreach (string line in lines)
            {
                i++;
                string[] data = line.Split(',');
                string userid = data[0];
                System.Console.WriteLine(userid);
                string traindata = data[1].Replace(" . ", "");
               
                 feature = feature+ userid +Stylometry.Stylofeatureprocess(traindata)+"\n";

            }
            File.WriteAllText(@"C:\Users\kishor\Documents\styles\TEST\featursdata.csv", feature);
            LoadCSVOnDataGridView(@"C:\Users\kishor\Documents\styles\TEST\featursdata.csv", 2);
        }
        private double CalculateStdDev(IEnumerable<double> values)
        {
            double ret = 0;
            if (values.Count() > 0)
            {
                //Compute the Average      
                double avg = values.Average();
                //Perform the Sum of (value-avg)_2_2      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));
                //Put it all together      
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return ret;
        }
        private void rangecreate()
        {
            string[] compares = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\Test\featursdata.csv");
            Double[][] values = new double[compares.Length][];
            int k = 0;
            foreach (string s in compares)
            {
                string[] val = s.Split(',');
                values[k] = new double[val.Length-1];
                for (int i=0;i<val.Length-1;i++)
                {
                    string sa = val[i+1];
                    double va = double.Parse(sa);
                    values[k][i] = va;
                }
                k++;
            }

            Double[] avg = new double[values[0].Length];
            for (int i = 0; i < values[i].Length; i++)
            {
                double av = 0.0;
                for (int j= 0; j < values.Length; j++)
                {
                    av = av + values[j][i];
                }
                avg[i] = av/ values.Length;
            }

            Double[] stdev = new double[avg.Length];
            for (int i = 0; i < values[i].Length; i++)
            {
                double sdv = 0.0;
                for (int j = 0; j < values.Length; j++)
                {
                    sdv = sdv + Math.Pow(values[j][i] - avg[i], 2);
                }
                stdev[i] = Math.Sqrt((sdv / values.Length));
            }
            string a = "avg";
            string b = "stdev";
            string c = "UR";
            string d = "LR";
            for (int i = 0; i < avg.Length; i++)
            {
                a = a + "," + avg[i];
                b=b + "," + stdev[i] + "";
                c=c + "," + (avg[i] + (stdev[i] * 2)) + "";
               d = d + "," + (avg[i] -(stdev[i] * 2)) + "";
            }
            string t = a + "\n" + b + "\n" + c + "\n" + d;
            File.WriteAllText(@"C:\Users\kishor\Documents\styles\Test\range.csv", t);
        }

        private void LoadCSVOnDataGridView(string fileName,int i)
        {
            try
            {
                ReadCSV csv = new ReadCSV(fileName);

                try
                {  if(i==1)
                    dataGridView1.DataSource = csv.readCSV;
                    if (i == 2)
                        dataGridView2.DataSource = csv.readCSV;
                    if (i == 3)
                        dataGridView3.DataSource = csv.readCSV;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void textanalysis(string text)
        {
          
            string[] compares = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\Test\range.csv");
            string maxline = compares[2];
            string minLine = compares[3];
            string[] max = maxline.Split(',');
            string[] min = minLine.Split(',');
            string newfeaturedata = "";

            string line = useridlist.SelectedItem + Stylometry.Stylofeatureprocess(text);
            richTextBox1.Text = richTextBox1.Text + "\n" + line;
            string[] linedata = line.Split(',');
                newfeaturedata =  linedata[0] + ",";
                for (int i = 1; i < linedata.Length; i++)
                {
                    int decision = 0;
                    double up = double.Parse(max[i]);
                    double down = double.Parse(min[i]);
                    double fd = double.Parse(linedata[i]);
                    //  System.Console.WriteLine(up + "," + down + "," + fd);
                    if (fd <= up && fd >= down) decision = 0;
                    else if (fd >= up) decision = 1;
                    else decision = -1;

                    newfeaturedata = newfeaturedata + decision + ",";

                }
            Console.WriteLine(newfeaturedata);
            richTextBox1.Text = richTextBox1.Text + "\n" + newfeaturedata;
            matchresult(newfeaturedata);
        }
        private void Alltextanalysis()
        {

            string[] compares = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\dete.csv");
            string maxline = compares[2];
            string minLine = compares[3];
            string[] max = maxline.Split(',');
            string[] min = minLine.Split(',');
            string newfeaturedata = "";
            string[] alllines = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\TestData.csv");
            foreach (string linea in alllines)
            {
              
                string[] sp = linea.Split(',');
                string line = sp[0]+ Stylometry.Stylofeatureprocess(sp[1]);
                string[] linedata = line.Split(',');
                newfeaturedata = newfeaturedata+"\n" +sp[0] + ",";
                for (int i = 1; i < linedata.Length; i++)
                {
                    int decision = 0;
                    double up = double.Parse(max[i]);
                    double down = double.Parse(min[i]);
                    double fd = double.Parse(linedata[i]);
                    //  System.Console.WriteLine(up + "," + down + "," + fd);
                    if (fd <= up && fd >= down) decision = 0;
                    else if (fd >= up) decision = 1;
                    else decision = -1;

                    newfeaturedata = newfeaturedata + decision + ",";

                }
            }
            File.WriteAllText(@"C:\Users\kishor\Documents\styles\testresults.csv", newfeaturedata);

        }
        /// <summary>
        /// create profile values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            rangecreate();
            string[] lines = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\Test\featursdata.csv");
            string[] compares = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\Test\range.csv");
            string maxline = compares[2];
            string minLine = compares[3];
            string[] max = maxline.Split(',');
            string[] min = minLine.Split(',');
            string newfeaturedata = "";
            foreach (string line in lines)
            {
               string[] linedata = line.Split(',');
                newfeaturedata = newfeaturedata +linedata[0] + ",";
                for(int i=1;i<linedata.Length;i++)
                {
                    int decision = 0;
                  double up=  double.Parse(max[i]);
                  double down = double.Parse(min[i]);
                    double fd = double.Parse(linedata[i]);
                  //  System.Console.WriteLine(up + "," + down + "," + fd);
                    if(fd<=up && fd >=down) decision = 0;
                    else if (fd >= up) decision = 1;
                    else  decision = -1;

                    newfeaturedata = newfeaturedata + decision + ",";

                }
                newfeaturedata = newfeaturedata + "\n";
            }
            File.WriteAllText(@"C:\Users\kishor\Documents\styles\Test\results.csv", newfeaturedata);
            LoadCSVOnDataGridView(@"C:\Users\kishor\Documents\styles\TEST\results.csv", 3);
        }

        private void buttonlogin_Click(object sender, EventArgs e)
        {
            textanalysis(logintextsample.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Alltextanalysis();
        }

        private void matchresult(string t)
        {
            string[] train = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\Test\results.csv");
            string[] testdata = t.Split(',');
            string s = testdata[0] + ",";
            float matchrate = 0.0f;
            foreach (string traind in train)
            {
               
                string[] traindata = traind.Split(',');
                if (traindata[0].Equals(testdata[0]))
                {
                    int count = 0;
                    int bigdiff = 0;
                    int diff = 0;
                    for (int i = 1; i < traindata.Length - 1; i++)
                    {
                        if (traindata[i].Equals(testdata[i])) count = count + 1;
                        else
                        {
                            int a = int.Parse(traindata[i]);
                            int b = int.Parse(testdata[i]);

                            int r = Math.Abs(a) + Math.Abs(b);
                            if (r == 2)
                                bigdiff = bigdiff + 1;
                            else
                                diff = diff + 1;


                        }
                        //  if ((traindata[i]+""+testdata[i]).Equals("1-1")) count = count - 2;
                        // if ((traindata[i] + "" + testdata[i]).Equals("-11")) count = count - 2;
                        //  if ((traindata[i] + "" + testdata[i]).Equals("01")) count = count - 1;
                        //   if ((traindata[i] + "" + testdata[i]).Equals("10")) count = count - 1;
                        //  if ((traindata[i] + "" + testdata[i]).Equals("0-1")) count = count -1;
                        // if ((traindata[i] + "" + testdata[i]).Equals("-10")) count = count - 1;
                    }
                    s = s + " id " + traindata[0] + " - " + count + "-" + bigdiff + "-" + diff + ",";
                    matchrate = (count * 2 - bigdiff * 2 - diff) / 100.0f;

                    break;
                }
            }
            Console.WriteLine(s);
            richTextBox1.Text = richTextBox1.Text + "\n" + s;
            richTextBox1.Text = richTextBox1.Text + "\n" + "Success Rate "+ matchrate;
            label3.Text = "Match Rate " + matchrate;
            if (matchrate < 0.50) label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.Green;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            string[] test = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\testresults.csv");
            string[] train = File.ReadAllLines(@"C:\Users\kishor\Documents\styles\results.csv");

            foreach (string t in test)
            {
                string[] testdata = t.Split(',');
                string s = testdata[0]+",";
                foreach (string traind in train)
                {
                    string[] traindata = traind.Split(',');
                    int count = 0;
                    int bigdiff = 0;
                    int diff = 0;
                    for (int i = 1; i < traindata.Length-1; i++)
                    {
                        if (traindata[i].Equals(testdata[i])) count=count+1;
                        else { 
                        int a = int.Parse(traindata[i]);
                        int b = int.Parse(testdata[i]);

                            int r = Math.Abs(a) + Math.Abs(b);
                            if (r == 2)
                                bigdiff = bigdiff + 1;
                            else
                                diff = diff + 1;
                         

                        }
                        //  if ((traindata[i]+""+testdata[i]).Equals("1-1")) count = count - 2;
                        // if ((traindata[i] + "" + testdata[i]).Equals("-11")) count = count - 2;
                        //  if ((traindata[i] + "" + testdata[i]).Equals("01")) count = count - 1;
                        //   if ((traindata[i] + "" + testdata[i]).Equals("10")) count = count - 1;
                        //  if ((traindata[i] + "" + testdata[i]).Equals("0-1")) count = count -1;
                        // if ((traindata[i] + "" + testdata[i]).Equals("-10")) count = count - 1;
                    }
                    s = s + " id "+ traindata[0]+ " - "+ count +"-"+bigdiff+"-"+diff +"," ;
                }
                Console.WriteLine(s);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
