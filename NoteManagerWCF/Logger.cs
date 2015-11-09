using System;
using System.IO;

namespace NoteManagerWCF
{
    public class Logger
    {
        private string filename;
        private StreamWriter sw;
        //private Logger instance;
        private string className;

        public Logger(Type classType)
        {
            className = classType.ToString();
            filename = AppDomain.CurrentDomain.BaseDirectory + "NoteManagerLog.txt";
        }

        public void Debug(string message)
        {
            string precision = " [DEBUG]\t" + className + "\t";
            Write(precision, message);
        }

        public void Error(string message)
        {
            string precision = " [ERROR]\t" + className + "\t";
            Write(precision, message);
        }

        public void Info(string message)
        {
            string precision = " [INFO]\t" + className + "\t";
            Write(precision, message);
        }

        public void Warn(string message)
        {
            string precision = " [WARN]\t" + className + "\t";
            Write(precision, message);
        }

        private void Write(string precision, string message)
        {
            using (sw = File.AppendText(filename))
            {
                sw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + precision + message);
            }
        }
    }
}