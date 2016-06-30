using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie
    {
        public Movie()
        {
        }

        public Movie(long id, string title,string releasedate,string genre,float price,string rate)
        {
            this.ID = id;
            this.dbTitle = title;
            this.ReleaseDate = DateTime.Parse(releasedate);
            this.dbGenre = genre;
            this.Price = price;
            this.dbRate = rate;
        }

        public long ID { get; set; }

        public string Title
        {
            get
            {
                return sTitle;
            }

            set
            {
                sTitle = value;
            }
        }

        private string sTitle = "";
        public string dbTitle
        {
            get
            {
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(sTitle));
            }

            set
            {
                sTitle = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
        }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Genre {
            get {
                    return sGenre;
                }
            set
                {
                    sGenre = value;
                }
        }
        private string sGenre = "";
        public string dbGenre
        {
            get
            {
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(sGenre));
            }

            set
            {
                sGenre = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
        }


        public float Price { get; set; }

        public string Rate {
            get
            {
                return sRate;
            }
            set
            {
                sRate = value;
            }
        }
        private string sRate = "";
        public string dbRate
        {
            get
            {
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(sRate));
            }

            set
            {
                sRate = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
        }
    }

}