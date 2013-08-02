using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace length
{
    class Program
    {
        Dictionary<string, double> dic = new Dictionary<string, double>();
        StreamReader reader = new StreamReader("input.txt");
        StreamWriter writer = new StreamWriter("output.txt");
        void Processing()
        {
            try
            {
                writer.WriteLine("yishu890918@126.com");
                writer.WriteLine("");
                string line = "";
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line != "")
                    {
                        string[] s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string measure = s[1];
                        string value = s[3];
                        double metric = Convert.ToDouble(s[3]);
                        dic.Add(measure, metric);
                        if (measure.Equals("mile"))
                        {
                            dic.Add("miles", metric);
                        }
                        if (measure.Equals("yard"))
                        {
                            dic.Add("yards", metric);
                        }
                        if (measure.Equals("inch"))
                        {
                            dic.Add("inches", metric);
                        }
                        if (measure.Equals("foot"))
                        {
                            dic.Add("feet", metric);
                        }
                        if (measure.Equals("fath"))
                        {
                            dic.Add("faths", metric);
                        }
                        if (measure.Equals("furlong"))
                        {
                            dic.Add("furlongs", metric);
                        }
                    }
                    else{
                        break;
                    }
                }
                while (!reader.EndOfStream) 
                {
                    line = reader.ReadLine();
                    cal(0,line,0);
                }
             }   
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            finally
            {
                 reader.Close();
                 writer.Close();
            }
        }
        double trans(string measure, double metric) {
            double result = 0;
            if (metric != 0)
            {
                if (dic.ContainsKey(measure))
                {

                    result = dic[measure] * metric;
                }
            }
            else { result = 0; }
            return result;
        }

        void cal(double temp, string line, int protag) {

            int plus = line.IndexOf("+"); ;
            int minus = line.IndexOf("-");
            int pos = 0;
            int tag = 0;
            tag = protag;
            string str = "";

            if (plus == -1 && minus == -1)
            {
               
                string[] s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double metric = Convert.ToDouble(s[0]);
                string measure = s[1];
                double std;
                string result;
                std = trans(measure, metric);
                if (tag == 1)
                {
                    std = temp + std;
                }
                else if (tag == -1)
                {
                    std = temp - std;
                }
                result = std.ToString("F2");
                Console.WriteLine(result);
                writer.WriteLine(result + " " + "m");
                return;
            }
            else if ((plus > 0 && minus == -1)||(plus < minus && plus != -1)){
               protag=1;
               pos=plus; 
             }
            else if ((minus > 0 && plus == -1) || plus > minus && minus != -1)
            {
                 protag = -1;
                 pos = minus;
            }
            str = line.Substring(0, pos);
            string[] st = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double metrics = Convert.ToDouble(st[0]);
            string measures = st[1];
            double results = trans(measures, metrics);
            if (tag == 1) {
                 results = temp + results;
             }
             else if (tag == -1) {
                    results = temp - results;
             }
             cal(results, line.Substring(pos + 2), protag);
        }
        static void Main(string[] args)
        {
            Program app = new Program();
            app.Processing();
        }
    }
}