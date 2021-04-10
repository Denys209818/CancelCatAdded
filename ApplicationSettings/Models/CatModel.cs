using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApplicationSettings.Models
{
    public class CatModel : INotifyPropertyChanged
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; 
                OnPropertyChanged("Name");
            }
        }

        private string _details;

        public string Details
        {
            get { return _details; }
            set { _details = value; 
                OnPropertyChanged("Details");
            }
        }

        private DateTime _birthday;

        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; 
                OnPropertyChanged("Birthday");
            }
        }

        private string _imgUrl;

        public string ImgUrl
        {
            get { return _imgUrl; }
            set { _imgUrl = value; 
                OnPropertyChanged("ImgUrl");
            }
        }

        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set { _price = value; 
                OnPropertyChanged("Price");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
