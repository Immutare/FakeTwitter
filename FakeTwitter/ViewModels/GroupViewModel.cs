using FakeTwitter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.ViewModels
{
    public class GroupViewModel
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //PROPERTIES
        //-------------------------------------------------------------------------------------------------------------
        public int? GroupId { get; set; }
        public string Name { get; set; }
        // public string Json { get; set; }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //AUX CLASS FOR SERIALIZING
        //-------------------------------------------------------------------------------------------------------------
        /*
        class GroupAuxClass
        {
            public int GroupId { get; set; }
            public int Name { get; set; }
        }
        */
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //CONSTRUCTORS
        //-------------------------------------------------------------------------------------------------------------
        public GroupViewModel()
        {

        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public GroupViewModel(string strJson_I)
        {
            if (string.IsNullOrWhiteSpace(strJson_I))
                return;

            GroupViewModel groupViewModel = JsonConvert.DeserializeObject<GroupViewModel>(strJson_I);

            this.GroupId = groupViewModel.GroupId;
            this.Name = groupViewModel.Name;
        }

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //METHODS
        //-------------------------------------------------------------------------------------------------------------
        public Group ToGroup()
        {
            return new Group
            {
                Id = (int)this.GroupId,
                Name = this.Name
            };
        }
        //-------------------------------------------------------------------------------------------------------------
    }
}