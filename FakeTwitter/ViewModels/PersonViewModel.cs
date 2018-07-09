using FakeTwitter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.ViewModels
{
    public class PersonViewModel
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //ATTRIBUTES
        //-------------------------------------------------------------------------------------------------------------
        public int Id { get; set; }
        public string Name { get; set; }
        public string At { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public int GroupId { get; set; }
        public string HashPassword { get; set; }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //CONSTRUCTORS
        //-------------------------------------------------------------------------------------------------------------
        public PersonViewModel()
        {

        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public PersonViewModel(string strJson_I)
        {
            if (string.IsNullOrWhiteSpace(strJson_I))
                return;

            PersonViewModel personViewModel = JsonConvert.DeserializeObject<PersonViewModel>(strJson_I); ;

            this.Id = personViewModel.Id;
            this.Name = personViewModel.Name;
            this.At = personViewModel.At;
            this.Photo = personViewModel.Photo;
            this.Email = personViewModel.Email;
            this.GroupId = personViewModel.GroupId;
            this.HashPassword = personViewModel.HashPassword;
        }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //AUXILIAR CLASS
        //-------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //METHODS
        //-------------------------------------------------------------------------------------------------------------
        public Person ToPerson()
        {
            return new Person
            {
                Id = this.Id,
                Name = this.Name,
                At = this.At,
                Photo = this.Photo,
                Email = this.Email,
                GroupId = this.GroupId
            };
        }
        //-------------------------------------------------------------------------------------------------------------
    }
}