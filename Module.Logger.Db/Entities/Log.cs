using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Logger.Db
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LogsId { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; }

        public string Messege { get; set; }
    }
}
