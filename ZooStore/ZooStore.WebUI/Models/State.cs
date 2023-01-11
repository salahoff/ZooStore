using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class State
    {
        public string Key { get; set; }
        public string Name { get; set; }

        public static Collection<State> List()
        {
            Collection<State> list = new Collection<State>();
            //list.Add(new State() { Key = "AL", Name = "Россия" });
            //list.Add(new State() { Key = "AK", Name = "США" });
            //list.Add(new State() { Key = "AZ", Name = "Украина" });
            //list.Add(new State() { Key = "AR", Name = "Казахстан" });
            //list.Add(new State() { Key = "CA", Name = "Германия" });
            //list.Add(new State() { Key = "CO", Name = "Франция" });
            //list.Add(new State() { Key = "CT", Name = "Беларусь" });
            //list.Add(new State() { Key = "DE", Name = "Эстония" });

            list.Add(new State() { Key = "Курьеру при доставке", Name = "Курьеру при доставке" });
            list.Add(new State() { Key = "В магазине при самовывозе", Name = "В магазине при самовывозе" });


            return list;
        }
    }
}