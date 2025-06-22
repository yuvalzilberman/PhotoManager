using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoManager.Data.Models
{
    public class Photo
    {
        [Key] // Primary key
        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public DateTime Date { get; set; }

        public int Size { get; set; }

        public bool Private { get; set; }


    }
}
