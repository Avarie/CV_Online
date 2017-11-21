using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using LiteDB;
using WebGrease.Css.Extensions;

namespace cv.db
{
    public interface ILogRepository
    {
        void Add(LogItem item);
        List<LogItem> GetAll();
    }

    public class LogRepository : ILogRepository
    {
        private static LiteDatabase Db => new LiteDatabase(@ConfigurationManager.AppSettings["FileDbPath"]);
        public void Add(LogItem item)
        {
            using (Db)
            {
                Db.GetCollection<LogItem>("Logs").Insert(item);
            }
        }

        public List<LogItem> GetAll()
        {
            var all = new List<LogItem>();

            using (Db)
            {
                var items =  Db.GetCollection<LogItem>("Logs");
                items.FindAll().OrderByDescending(x =>x.Date).Take(200).ForEach(x => all.Add(x));
            }

            return all;
        }
    }

    public class LogItem
    {
        public LogItem()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }

        public LogItem(HttpRequestBase request, string action) : this()
        {
            Ip = request.UserHostAddress;
            Host = request.UserHostName;
            Target = action;
        }

        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Ip { get; set; }
        public string Host { get; set; }
        public string Target { get; set; }
    }
}