using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyManagement.Model
{
    public class Address
    {
        public Address()
        {

        }
        public Address(int lat,int lng,string address1,string address2,string city
            ,string state,string country,string zip)
        {
            Lat = lat;
            Lng = lng;
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            Country = country;
            Zip = zip;
        }
        public int Id { get; set; }
        public int Lat { get; set; }
        public int Lng { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
    }

 

    



  




}
