﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MasterProjekat1.Model
{
    public class Racun
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid ID
        {
            get;
            set;
        }

        public virtual int BrojRacuna
        {
            get;
            set;
        }

        public Guid Preduzece_ID
        {
            get;
            set;
        }


        [ForeignKey("Preduzece_ID")]
        public virtual Preduzece Preduzece
        {
            get;
            set;
        }

    }
}

