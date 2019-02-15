using System;

namespace UploadingToWebApi.Web.Models
{
    public class Person
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public int Age { get; set; }

        public GenderType Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}